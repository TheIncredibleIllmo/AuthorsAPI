using Microsoft.EntityFrameworkCore.Migrations;

namespace AuthorsAPI.Migrations
{
    public partial class AuthorCreditCard : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Age",
                table: "Authors",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "CreditCard",
                table: "Authors",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Url",
                table: "Authors",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Age",
                table: "Authors");

            migrationBuilder.DropColumn(
                name: "CreditCard",
                table: "Authors");

            migrationBuilder.DropColumn(
                name: "Url",
                table: "Authors");
        }
    }
}
