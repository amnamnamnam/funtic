using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FUNTIK.Migrations
{
    /// <inheritdoc />
    public partial class RecipeUpdate62 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FatPercent",
                table: "Recipes");

            migrationBuilder.DropColumn(
                name: "MilkPersent",
                table: "Recipes");

            migrationBuilder.DropColumn(
                name: "SugarPercent",
                table: "Recipes");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FatPercent",
                table: "Recipes",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MilkPersent",
                table: "Recipes",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SugarPercent",
                table: "Recipes",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
