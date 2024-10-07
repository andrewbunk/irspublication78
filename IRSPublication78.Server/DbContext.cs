using IRSPublication78.Server.Models;
using Microsoft.EntityFrameworkCore;

namespace IRSPublication78.Server
{
    public class PubContext : DbContext
    {
        public DbSet<Organization> Organizations { get; set; }
        public DbSet<DeductibilityCode> DeductibilityCodes { get; set; }
        public DbSet<SearchAudit> SearchAudits { get; set; }

        public PubContext(DbContextOptions<PubContext> options)
            : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Organization>()
                .HasMany(e => e.DeductibilityCodes)
                .WithMany(e => e.Organizations)
                .UsingEntity<DeductibilityCodeOrganization>(
                //"DeductibilityCodeOrganization",
                //l => l.HasOne(typeof(DeductibilityCode)).WithMany().HasForeignKey("DeductibilityCodesId").HasPrincipalKey(nameof(DeductibilityCode.Id)),
                //r => r.HasOne(typeof(Organization)).WithMany().HasForeignKey("OrganizationsId").HasPrincipalKey(nameof(Organization.Id)),
                //j => j.HasKey("DeductibilityCodesId", "OrganizationsId")
                );


            modelBuilder.Entity<Organization>()
                .HasData(new { Id = 1, EIN = "100000000", Name = "Test Organization", City = "New York", State = "NY", Country = "United States" });

            modelBuilder.Entity<DeductibilityCode>()
                .HasData(new { Id = 1, Code = "PC", OrgType = "A public charity.", DeductibilityLimitation = "50% (60% for cash contributions)" },
                new { Id = 2, Code = "POF", OrgType = "A private operating foundation.", DeductibilityLimitation = "50% (60% for cash contributions)" },
                new { Id = 3, Code = "PF", OrgType = "A private foundation.", DeductibilityLimitation = "30% (generally)" },
                new { Id = 4, Code = "GROUP", OrgType = "Generally, a central organization holding a group exemption letter, whose subordinate units covered by the group exemption are also eligible to receive tax-deductible contributions, even though they are not separately listed.", DeductibilityLimitation = "Depends on various factors" },
                new { Id = 5, Code = "LODGE", OrgType = "\tA domestic fraternal society, operating under the lodge system, but only if the contribution is to be used exclusively for charitable purposes.", DeductibilityLimitation = "30%" },
                new { Id = 6, Code = "UNKWN", OrgType = "A charitable organization whose public charity status has not been determined.", DeductibilityLimitation = "Depends on various factors" },
                new { Id = 7, Code = "EO", OrgType = "An organization described in section 170(c) of the Internal Revenue Code other than a public charity or private foundation.", DeductibilityLimitation = "Depends on various factors" },
                new { Id = 8, Code = "FORGN", OrgType = "\tA foreign-addressed organization. These are generally organizations formed in the United States that conduct activities in foreign countries. Certain foreign organizations that receive charitable contributions deductible pursuant to treaty are also included, as are organizations created in U.S. possessions.", DeductibilityLimitation = "Depends on various factors" },
                new { Id = 9, Code = "SO", OrgType = "A Type I, Type II, or functionally integrated Type III supporting organization.", DeductibilityLimitation = "50% (60% for cash contributions)" },
                new { Id = 10, Code = "SONFI", OrgType = "A non-functionally integrated Type III supporting organization.", DeductibilityLimitation = "50% (60% for cash contributions)" },
                new { Id = 11, Code = "SOUNK", OrgType = "A supporting organization, unspecified type.", DeductibilityLimitation = "\t50% (60% for cash contributions)" }
                );

            //modelBuilder.Entity<DeductibilityCodeOrganization>()
            //    .Has;
        }
    }
}
