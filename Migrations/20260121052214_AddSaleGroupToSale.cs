using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ForajidoManagenment.Migrations
{
    /// <inheritdoc />
    public partial class AddSaleGroupToSale : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SaleGroup",
                table: "Sales",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SaleGroup",
                table: "Sales");
        }
    }
}
