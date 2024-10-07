using Azure.Core;
using IRSPublication78.Server.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using N.EntityFrameworkCore.Extensions;
using System.Diagnostics;
using System.IO.Compression;
using System.Linq;

namespace IRSPublication78.Server.Services
{
    public class OrganizationService
    {
        private PubContext pubContext;
        private IConfiguration Configuration;
        public OrganizationService(PubContext pubContext, IConfiguration configuration)
        {
            this.pubContext = pubContext;
            this.Configuration = configuration;
        }
        public async Task UpdateFromIRS(CancellationToken token)
        {
            List<Organization> list = new List<Organization>();
            List<DeductibilityCode> dCodes = await pubContext.DeductibilityCodes.ToListAsync();
            List<DeductibilityCodeOrganization> listDCO = new List<DeductibilityCodeOrganization>();
            using var client = new HttpClient();
            using var result = await client.GetAsync(Configuration["Original"], token);
            if (result.IsSuccessStatusCode)
            {
                var zipStream = await result.Content.ReadAsStreamAsync(token);
                using (var za = new ZipArchive(zipStream))
                {
                    if (za.Entries.FirstOrDefault(x => x.Name == "data-download-pub78.txt") != null)
                    {
                        using (var r = new StreamReader(za.GetEntry("data-download-pub78.txt").Open()))
                        {
                            string line;
                            int index = 1;
                            while ((line = r.ReadLine()) != null)
                            {
                                if (string.IsNullOrEmpty(line))
                                    continue;
                                string[] fields = line.Split('|');
                                list.Add(new Organization()
                                {
                                    EIN = fields[0],
                                    Name = fields[1],
                                    City = fields[2],
                                    State = fields[3],
                                    Country = fields[4]
                                });
                                
                                if (fields[5].Contains(','))
                                {
                                    foreach (var code in dCodes.Where(x => fields[5].Split(',').Contains(x.Code)).ToList())
                                    {
                                        listDCO.Add(new DeductibilityCodeOrganization() { DeductibilityCodesId = code.Id, OrganizationsId = index });
                                    }
                                }
                                else
                                {
                                    listDCO.Add(new DeductibilityCodeOrganization() { DeductibilityCodesId = dCodes.FirstOrDefault(x => x.Code == fields[5]).Id, OrganizationsId = index });
                                }
                                index++;
                            }
                        }
                    }
                }
                Debug.WriteLine(list.Count);
                await pubContext.BulkInsertAsync(list, token);
                await pubContext.BulkInsertAsync(listDCO, token);
                Debug.WriteLine("Bulk Insert complete");


            }
        }

        public async Task StartAsync(CancellationToken stoppingToken)
        {
            Debug.WriteLine("Starting task");
            await pubContext.Database.ExecuteSqlRawAsync("TRUNCATE TABLE [DeductibilityCodeOrganization];");
            await pubContext.Database.ExecuteSqlRawAsync("ALTER TABLE [dbo].[DeductibilityCodeOrganization] DROP CONSTRAINT [FK_DeductibilityCodeOrganization_Organizations_OrganizationsId]");
            await pubContext.Database.ExecuteSqlRawAsync("TRUNCATE TABLE [Organizations]");
            Debug.WriteLine("Database cleared");
            await UpdateFromIRS(stoppingToken);
            await pubContext.Database.ExecuteSqlRawAsync("ALTER TABLE [dbo].[DeductibilityCodeOrganization]  WITH CHECK ADD  CONSTRAINT [FK_DeductibilityCodeOrganization_Organizations_OrganizationsId] FOREIGN KEY([OrganizationsId]) REFERENCES [dbo].[Organizations] ([Id]) ON DELETE CASCADE");
            await pubContext.Database.ExecuteSqlRawAsync("ALTER TABLE [dbo].[DeductibilityCodeOrganization] CHECK CONSTRAINT [FK_DeductibilityCodeOrganization_Organizations_OrganizationsId]");

        }

        public async Task InsertAsync(List<DeductibilityCodeOrganization> items, CancellationToken ct = default(CancellationToken))
        {
            using (var connection = new SqlConnection())
            {
                connection.ConnectionString = Configuration.GetConnectionString("IrsPublication78");
                await connection.OpenAsync(ct);
                using (var bulk = new SqlBulkCopy(connection, SqlBulkCopyOptions.Default, null))
                {
                    using (var enumerator = items.GetEnumerator())
                    using (var customerReader = new ObjectDataReader<DeductibilityCodeOrganization>(enumerator))
                    {
                        bulk.DestinationTableName = "DeductibilityCodeOrganization";
                        bulk.ColumnMappings.Add(nameof(DeductibilityCodeOrganization.DeductibilityCodesId), "DeductibilityCodesId");
                        bulk.ColumnMappings.Add(nameof(DeductibilityCodeOrganization.OrganizationsId), "OrganizationsId");


                        bulk.EnableStreaming = true;
                        bulk.BatchSize = 5000;
                        //bulk.NotifyAfter = 5000;
                        //bulk.SqlRowsCopied += (sender, e) => Debug.WriteLine("RowsCopied: " + e.RowsCopied);
                        Stopwatch stopwatch = Stopwatch.StartNew();
                        await bulk.WriteToServerAsync(customerReader, ct);
                        stopwatch.Stop();
                        Debug.WriteLine(stopwatch.Elapsed);
                    }
                }
            }
        }

        public async Task<OrgSearchResult> SearchAsync(string searchText, int pageSize, int pageIndex)
        {
            var query = pubContext.Organizations.Include(x => x.DeductibilityCodes).Where(x => true);
            if (IsDigitsOnly(searchText))
            {
                query = query.Where(x => x.EIN.StartsWith(searchText));
            }
            else
            {
                query = query.Where(x => x.Name.StartsWith(searchText)).OrderBy(x => x.Name);
            }

            var totalRecords = await query.CountAsync();
            pageIndex = (pageIndex - 1) <= 0 ? 0 : pageIndex - 1;
            int totalPages = (int)Math.Ceiling((double)totalRecords / pageSize);
            if (totalPages <= pageIndex && totalPages > 0)
            {
                pageIndex = totalPages - 1;
            }

            var currentPage = await query.Skip(pageIndex * pageSize).Take(pageSize).ToListAsync();

            if (pageIndex == 0)
            {
                pubContext.SearchAudits.Add(new SearchAudit() { SearchText = searchText, TotalRecords = totalRecords });
                await pubContext.SaveChangesAsync();
            }

            return new OrgSearchResult() { Items = currentPage, TotalRecords = totalRecords };
        }
        static bool IsDigitsOnly(string str)
        {
            foreach (char c in str)
            {
                if (c < '0' || c > '9')
                    return false;
            }

            return true;
        }
    }

}
