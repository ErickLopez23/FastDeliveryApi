using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FastDeliveryApi.Migrations
{
    /// <inheritdoc />
    public partial class MyFirstMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", maxLength: 100, nullable: false),
                    PhoneNumberCustomer = table.Column<string>(type: "text", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "text", maxLength: 120, nullable: false),
                    Address = table.Column<string>(type: "text", maxLength: 120, nullable: false),
                    Status = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "Id", "Address", "Email", "Name", "PhoneNumberCustomer", "Status" },
                values: new object[,]
                {
                    { 1, "San Miguel", "suleyma@univo.edu.sv", "Suleyma Lopez", "0000-0000", true },
                    { 2, "San Miguel", "kvasquez@univo.edu.sv", "Kevin Vasquez", "2200-5500", true }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Customers");
        }
    }
}
