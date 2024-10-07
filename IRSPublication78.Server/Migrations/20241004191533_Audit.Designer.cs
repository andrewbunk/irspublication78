﻿// <auto-generated />
using IRSPublication78.Server;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace IRSPublication78.Server.Migrations
{
    [DbContext(typeof(PubContext))]
    [Migration("20241004191533_Audit")]
    partial class Audit
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("IRSPublication78.Server.Models.DeductibilityCode", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DeductibilityLimitation")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OrgType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("DeductibilityCodes");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Code = "PC",
                            DeductibilityLimitation = "50% (60% for cash contributions)",
                            OrgType = "A public charity."
                        },
                        new
                        {
                            Id = 2,
                            Code = "POF",
                            DeductibilityLimitation = "50% (60% for cash contributions)",
                            OrgType = "A private operating foundation."
                        },
                        new
                        {
                            Id = 3,
                            Code = "PF",
                            DeductibilityLimitation = "30% (generally)",
                            OrgType = "A private foundation."
                        },
                        new
                        {
                            Id = 4,
                            Code = "GROUP",
                            DeductibilityLimitation = "Depends on various factors",
                            OrgType = "Generally, a central organization holding a group exemption letter, whose subordinate units covered by the group exemption are also eligible to receive tax-deductible contributions, even though they are not separately listed."
                        },
                        new
                        {
                            Id = 5,
                            Code = "LODGE",
                            DeductibilityLimitation = "30%",
                            OrgType = "	A domestic fraternal society, operating under the lodge system, but only if the contribution is to be used exclusively for charitable purposes."
                        },
                        new
                        {
                            Id = 6,
                            Code = "UNKWN",
                            DeductibilityLimitation = "Depends on various factors",
                            OrgType = "A charitable organization whose public charity status has not been determined."
                        },
                        new
                        {
                            Id = 7,
                            Code = "EO",
                            DeductibilityLimitation = "Depends on various factors",
                            OrgType = "An organization described in section 170(c) of the Internal Revenue Code other than a public charity or private foundation."
                        },
                        new
                        {
                            Id = 8,
                            Code = "FORGN",
                            DeductibilityLimitation = "Depends on various factors",
                            OrgType = "	A foreign-addressed organization. These are generally organizations formed in the United States that conduct activities in foreign countries. Certain foreign organizations that receive charitable contributions deductible pursuant to treaty are also included, as are organizations created in U.S. possessions."
                        },
                        new
                        {
                            Id = 9,
                            Code = "SO",
                            DeductibilityLimitation = "50% (60% for cash contributions)",
                            OrgType = "A Type I, Type II, or functionally integrated Type III supporting organization."
                        },
                        new
                        {
                            Id = 10,
                            Code = "SONFI",
                            DeductibilityLimitation = "50% (60% for cash contributions)",
                            OrgType = "A non-functionally integrated Type III supporting organization."
                        },
                        new
                        {
                            Id = 11,
                            Code = "SOUNK",
                            DeductibilityLimitation = "	50% (60% for cash contributions)",
                            OrgType = "A supporting organization, unspecified type."
                        });
                });

            modelBuilder.Entity("IRSPublication78.Server.Models.DeductibilityCodeOrganization", b =>
                {
                    b.Property<int>("DeductibilityCodesId")
                        .HasColumnType("int");

                    b.Property<int>("OrganizationsId")
                        .HasColumnType("int");

                    b.HasKey("DeductibilityCodesId", "OrganizationsId");

                    b.HasIndex("OrganizationsId");

                    b.ToTable("DeductibilityCodeOrganization");
                });

            modelBuilder.Entity("IRSPublication78.Server.Models.Organization", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("City")
                        .IsRequired()
                        .HasMaxLength(23)
                        .HasColumnType("nvarchar(23)");

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasMaxLength(22)
                        .HasColumnType("nvarchar(22)");

                    b.Property<string>("EIN")
                        .IsRequired()
                        .HasMaxLength(9)
                        .HasColumnType("nvarchar(9)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(72)
                        .HasColumnType("nvarchar(72)");

                    b.Property<string>("State")
                        .IsRequired()
                        .HasMaxLength(2)
                        .HasColumnType("nvarchar(2)");

                    b.HasKey("Id");

                    b.ToTable("Organizations");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            City = "New York",
                            Country = "United States",
                            EIN = "100000000",
                            Name = "Test Organization",
                            State = "NY"
                        });
                });

            modelBuilder.Entity("IRSPublication78.Server.Models.SearchAudit", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("SearchText")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TotalRecords")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("SearchAudits");
                });

            modelBuilder.Entity("IRSPublication78.Server.Models.DeductibilityCodeOrganization", b =>
                {
                    b.HasOne("IRSPublication78.Server.Models.DeductibilityCode", null)
                        .WithMany()
                        .HasForeignKey("DeductibilityCodesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("IRSPublication78.Server.Models.Organization", null)
                        .WithMany()
                        .HasForeignKey("OrganizationsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
