using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TechAssess.SupplierService.Migrations
{
    public partial class CreateInitial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Countries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Suppliers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LegalName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TradeName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TaxIdentificationNumber = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Website = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhysicalAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CountryId = table.Column<int>(type: "int", nullable: false),
                    AnnualRevenueUSD = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    LastEdited = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Suppliers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Suppliers_Countries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Countries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Suppliers_CountryId",
                table: "Suppliers",
                column: "CountryId");

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "Name" },
                values: new object[,]
                {
                    { "Afghanistan" },{ "Albania" },{ "Algeria" },{ "Andorra" },{ "Angola" },{ "Antigua and Barbuda" },{ "Argentina" },{ "Armenia" },{ "Australia" },
                    { "Austria" },{ "Azerbaijan" },{ "Bahamas" },{ "Bahrain" },{ "Bangladesh" },{ "Barbados" },{ "Belarus" },{ "Belgium" },{ "Belize" },{ "Benin" },
                    { "Bhutan" },{ "Bolivia" },{ "Bosnia and Herzegovina" },{ "Botswana" },{ "Brazil" },{ "Brunei" },{ "Bulgaria" },{ "Burkina Faso" },{ "Burundi" },
                    { "Cabo Verde" },{ "Cambodia" },{ "Cameroon" },{ "Canada" },{ "Central African Republic" },{ "Chad" },{ "Chile" },{ "China" },{ "Colombia" },
                    { "Comoros" },{ "Congo" },{ "Costa Rica" },{ "Croatia" },{ "Cuba" },{ "Cyprus" },{ "Czech Republic" },{ "Denmark" },{ "Djibouti" },{ "Dominica" },
                    { "Dominican Republic" },{ "Ecuador" },{ "Egypt" },{ "El Salvador" },{ "Equatorial Guinea" },{ "Eritrea" },{ "Estonia" },{ "Eswatini" },{ "Ethiopia" },
                    { "Fiji" },{ "Finland" },{ "France" },{ "Gabon" },{ "Gambia" },{ "Georgia" },{ "Germany" },{ "Ghana" },{ "Greece" },{ "Grenada" },{ "Guatemala" },
                    { "Guinea" },{ "Guinea-Bissau" },{ "Guyana" },{ "Haiti" },{ "Honduras" },{ "Hungary" },{ "Iceland" },{ "India" },{ "Indonesia" },{ "Iran" },{ "Iraq" },
                    { "Ireland" },{ "Israel" },{ "Italy" },{ "Jamaica" },{ "Japan" },{ "Jordan" },{ "Kazakhstan" },{ "Kenya" },{ "Kiribati" },{ "Kosovo" },{ "Kuwait" },
                    { "Kyrgyzstan" },{ "Laos" },{ "Latvia" },{ "Lebanon" },{ "Lesotho" },{ "Liberia" },{ "Libya" },{ "Liechtenstein" },{ "Lithuania" },{ "Luxembourg" },
                    { "Madagascar" },{ "Malawi" },{ "Malaysia" },{ "Maldives" },{ "Mali" },{ "Malta" },{ "Marshall Islands" },{ "Mauritania" },{ "Mauritius" },{ "Mexico" },
                    { "Micronesia" },{ "Moldova" },{ "Monaco" },{ "Mongolia" },{ "Montenegro" },{ "Morocco" },{ "Mozambique" },{ "Myanmar" },{ "Namibia" },{ "Nauru" },
                    { "Nepal" },{ "Netherlands" },{ "New Zealand" },{ "Nicaragua" },{ "Niger" },{ "Nigeria" },{ "North Korea" },{ "North Macedonia" },{ "Norway" },{ "Oman" },
                    { "Pakistan" },{ "Palau" },{ "Palestine" },{ "Panama" },{ "Papua New Guinea" },{ "Paraguay" },{ "Peru" },{ "Philippines" },{ "Poland" },{ "Portugal" },
                    { "Qatar" },{ "Romania" },{ "Russia" },{ "Rwanda" },{ "Saint Kitts and Nevis" },{ "Saint Lucia" },{ "Saint Vincent and the Grenadines" },{ "Samoa" },
                    { "San Marino" },{ "Sao Tome and Principe" },{ "Saudi Arabia" },{ "Senegal" },{ "Serbia" },{ "Seychelles" },{ "Sierra Leone" },{ "Singapore" },{ "Slovakia" },
                    { "Slovenia" },{ "Solomon Islands" },{ "Somalia" },{ "South Africa" },{ "South Korea" },{ "South Sudan" },{ "Spain" },{ "Sri Lanka" },{ "Sudan" },
                    { "Suriname" },{ "Sweden" },{ "Switzerland" },{ "Syria" },{ "Taiwan" },{ "Tajikistan" },{ "Tanzania" },{ "Thailand" },{ "Timor-Leste" },{ "Togo" },
                    { "Tonga" },{ "Trinidad and Tobago" },{ "Tunisia" },{ "Turkey" },{ "Turkmenistan" },{ "Tuvalu" },{ "Uganda" },{ "Ukraine" },{ "United Arab Emirates" },
                    { "United Kingdom" },{ "United States" },{ "Uruguay" },{ "Uzbekistan" },{ "Vanuatu" },{ "Vatican City" },{ "Venezuela" },{ "Vietnam" },{ "Yemen" },
                    { "Zambia" },{ "Zimbabwe" }
                }
            );


            migrationBuilder.InsertData(
            table: "Suppliers",
            columns: new[]
            {
                "LegalName", "TradeName", "TaxIdentificationNumber", "PhoneNumber", "Email", "Website","PhysicalAddress", "CountryId", "AnnualRevenueUSD", "LastEdited"
            },
            values: new object[,]
            {
                { "Grupo Gloria", "Gloria", "12345678901", "(51) 1 555-1234", "info@grupogloria.com.pe", "http://www.grupogloria.com.pe", "Av. República de Panamá 2461, Lima, Peru", 136, 5000000.00m, DateTime.Now },
                { "Interbank", "Interbank", "23456789012", "(51) 1 555-2345", "contact@interbank.com.pe", "http://www.interbank.com.pe", "Av. Carlos Villarán 140, Lima, Peru", 136, 7000000.00m, DateTime.Now },
                { "Alicorp", "Alicorp", "34567890123", "(51) 1 555-3456", "support@alicorp.com.pe", "http://www.alicorp.com.pe", "Av. Argentina 4793, Lima, Peru", 136, 9000000.00m, DateTime.Now },
                { "Backus", "Backus", "45678901234", "(51) 1 555-4567", "info@backus.com.pe", "http://www.backus.com.pe", "Av. Nicolás Ayllón 3986, Lima, Peru", 136, 8000000.00m, DateTime.Now },
                { "Credicorp", "Credicorp", "56789012345", "(51) 1 555-5678", "contact@credicorp.com.pe", "http://www.credicorp.com.pe", "Av. El Derby 250, Lima, Peru", 136, 10000000.00m, DateTime.Now },
                { "Grupo Romero", "Romero", "67890123456", "(51) 1 555-6789", "info@gruporomero.com.pe", "http://www.gruporomero.com.pe", "Av. Pardo y Aliaga 640, Lima, Peru", 136, 11000000.00m, DateTime.Now },
                { "Southern Copper Corporation", "Southern Copper", "78901234567", "(51) 1 555-7890", "contact@southernperu.com.pe", "http://www.southernperu.com.pe", "Av. Caminos del Inca 171, Lima, Peru", 136, 13000000.00m, DateTime.Now },
                { "Banco de Crédito del Perú", "BCP", "89012345678", "(51) 1 555-8901", "info@bcp.com.pe", "http://www.bcp.com.pe", "Av. La Molina 1001, Lima, Peru", 136, 12000000.00m, DateTime.Now },
                { "Graña y Montero", "GyM", "90123456789", "(51) 1 555-9012", "contact@gym.com.pe", "http://www.gym.com.pe", "Av. Paseo de la República 4675, Lima, Peru", 136, 14000000.00m, DateTime.Now },
                { "Intercorp", "Intercorp", "01234567890", "(51) 1 555-0123", "info@intercorp.com.pe", "http://www.intercorp.com.pe", "Av. Paseo de la República 3074, Lima, Peru", 136, 15000000.00m, DateTime.Now }
            });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Suppliers");

            migrationBuilder.DropTable(
                name: "Countries");
        }
    }
}
