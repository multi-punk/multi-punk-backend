using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace MultiApi.Migrations
{
    /// <inheritdoc />
    public partial class smallChangesAndAddServersTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PartyId",
                schema: "public",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "Game",
                schema: "public",
                table: "Statistic",
                newName: "GameId");

            migrationBuilder.RenameColumn(
                name: "type",
                schema: "public",
                table: "ApiKeys",
                newName: "Type");

            migrationBuilder.RenameColumn(
                name: "ownerId",
                schema: "public",
                table: "ApiKeys",
                newName: "OwnerId");

            migrationBuilder.CreateTable(
                name: "Servers",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    URL = table.Column<string>(type: "text", nullable: false),
                    Port = table.Column<int>(type: "integer", nullable: false),
                    IsInUse = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Servers", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Servers",
                schema: "public");

            migrationBuilder.RenameColumn(
                name: "GameId",
                schema: "public",
                table: "Statistic",
                newName: "Game");

            migrationBuilder.RenameColumn(
                name: "Type",
                schema: "public",
                table: "ApiKeys",
                newName: "type");

            migrationBuilder.RenameColumn(
                name: "OwnerId",
                schema: "public",
                table: "ApiKeys",
                newName: "ownerId");

            migrationBuilder.AddColumn<string>(
                name: "PartyId",
                schema: "public",
                table: "Users",
                type: "text",
                nullable: true);
        }
    }
}
