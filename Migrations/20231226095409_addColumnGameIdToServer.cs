using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MultiApi.Migrations
{
    /// <inheritdoc />
    public partial class addColumnGameIdToServer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "GameId",
                schema: "public",
                table: "Servers",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GameId",
                schema: "public",
                table: "Servers");
        }
    }
}
