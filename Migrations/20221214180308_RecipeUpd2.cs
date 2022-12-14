using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FUNTIK.Migrations
{
    /// <inheritdoc />
    public partial class RecipeUpd2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Mass",
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Mass",
                table: "Recipes");

            migrationBuilder.DropColumn(
                name: "MilkPersent",
                table: "Recipes");

            migrationBuilder.DropColumn(
                name: "SugarPercent",
                table: "Recipes");
        }
    }
}
