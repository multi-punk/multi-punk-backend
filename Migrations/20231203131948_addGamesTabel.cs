using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MultiApi.Migrations
{
    /// <inheritdoc />
    public partial class addGamesTabel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Games",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    MinPlayersCount = table.Column<int>(type: "integer", nullable: false),
                    MaxPlayersCount = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Games", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Games",
                schema: "public");
        }
    }
}
