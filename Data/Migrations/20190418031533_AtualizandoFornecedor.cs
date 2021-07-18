using Microsoft.EntityFrameworkCore.Migrations;

namespace sonmarket.Migrations
{
    public partial class AtualizandoFornecedor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Status",
                table: "Fornecedores",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Fornecedores");
        }
    }
}
