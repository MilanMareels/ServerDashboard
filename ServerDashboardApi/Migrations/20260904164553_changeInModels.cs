using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ServerDashboardApi.Migrations
{
    /// <inheritdoc />
    public partial class changeInModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TopAndBottomFans",
                table: "Tempertures",
                newName: "TopFans");

            migrationBuilder.RenameColumn(
                name: "BackFans",
                table: "Tempertures",
                newName: "BottomFans");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TopFans",
                table: "Tempertures",
                newName: "TopAndBottomFans");

            migrationBuilder.RenameColumn(
                name: "BottomFans",
                table: "Tempertures",
                newName: "BackFans");
        }
    }
}
