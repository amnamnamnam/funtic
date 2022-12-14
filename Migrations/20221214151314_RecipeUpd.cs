using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FUNTIK.Migrations
{
    /// <inheritdoc />
    public partial class RecipeUpd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CacaoPercent",
                table: "Recipes",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "FatPercent",
                table: "Recipes",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CacaoPercent",
                table: "Recipes");

            migrationBuilder.DropColumn(
                name: "FatPercent",
                table: "Recipes");
        }
    }
}
