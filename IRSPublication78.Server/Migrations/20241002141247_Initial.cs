using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace IRSPublication78.Server.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DeductibilityCodes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OrgType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DeductibilityLimitation = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeductibilityCodes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Organizations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EIN = table.Column<string>(type: "nvarchar(9)", maxLength: 9, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    State = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Organizations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DeductibilityCodeOrganization",
                columns: table => new
                {
                    DeductibilityCodesId = table.Column<int>(type: "int", nullable: false),
                    OrganizationsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeductibilityCodeOrganization", x => new { x.DeductibilityCodesId, x.OrganizationsId });
                    table.ForeignKey(
                        name: "FK_DeductibilityCodeOrganization_DeductibilityCodes_DeductibilityCodesId",
                        column: x => x.DeductibilityCodesId,
                        principalTable: "DeductibilityCodes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DeductibilityCodeOrganization_Organizations_OrganizationsId",
                        column: x => x.OrganizationsId,
                        principalTable: "Organizations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "DeductibilityCodes",
                columns: new[] { "Id", "Code", "DeductibilityLimitation", "OrgType" },
                values: new object[,]
                {
                    { 1, "PC", "50% (60% for cash contributions)", "A public charity." },
                    { 2, "POF", "50% (60% for cash contributions)", "A private operating foundation." },
                    { 3, "PF", "30% (generally)", "A private foundation." },
                    { 4, "GROUP", "Depends on various factors", "Generally, a central organization holding a group exemption letter, whose subordinate units covered by the group exemption are also eligible to receive tax-deductible contributions, even though they are not separately listed." },
                    { 5, "LODGE", "30%", "	A domestic fraternal society, operating under the lodge system, but only if the contribution is to be used exclusively for charitable purposes." },
                    { 6, "UNKWN", "Depends on various factors", "A charitable organization whose public charity status has not been determined." },
                    { 7, "EO", "Depends on various factors", "An organization described in section 170(c) of the Internal Revenue Code other than a public charity or private foundation." },
                    { 8, "FORGN", "Depends on various factors", "	A foreign-addressed organization. These are generally organizations formed in the United States that conduct activities in foreign countries. Certain foreign organizations that receive charitable contributions deductible pursuant to treaty are also included, as are organizations created in U.S. possessions." },
                    { 9, "SO", "50% (60% for cash contributions)", "A Type I, Type II, or functionally integrated Type III supporting organization." },
                    { 10, "SONFI", "50% (60% for cash contributions)", "A non-functionally integrated Type III supporting organization." },
                    { 11, "SOUNK", "	50% (60% for cash contributions)", "A supporting organization, unspecified type." }
                });

            migrationBuilder.InsertData(
                table: "Organizations",
                columns: new[] { "Id", "City", "Country", "EIN", "Name", "State" },
                values: new object[] { 1, "New York", "United States", "100000000", "Test Organization", "NY" });

            migrationBuilder.CreateIndex(
                name: "IX_DeductibilityCodeOrganization_OrganizationsId",
                table: "DeductibilityCodeOrganization",
                column: "OrganizationsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DeductibilityCodeOrganization");

            migrationBuilder.DropTable(
                name: "DeductibilityCodes");

            migrationBuilder.DropTable(
                name: "Organizations");
        }
    }
}
