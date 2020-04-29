using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AuthorsAPI.Migrations
{
    public partial class AuthorDOB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreditCard",
                table: "Authors");

            migrationBuilder.DropColumn(
                name: "Url",
                table: "Authors");

            migrationBuilder.AddColumn<DateTime>(
                name: "DOB",
                table: "Authors",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateTable(
                name: "HostedServiceLogs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Message = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HostedServiceLogs", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HostedServiceLogs");

            migrationBuilder.DropColumn(
                name: "DOB",
                table: "Authors");

            migrationBuilder.AddColumn<string>(
                name: "CreditCard",
                table: "Authors",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Url",
                table: "Authors",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
