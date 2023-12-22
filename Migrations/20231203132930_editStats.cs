using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace MultiApi.Migrations
{
    /// <inheritdoc />
    public partial class editStats : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Statistic",
                schema: "public",
                table: "Statistic");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                schema: "public",
                table: "Statistic",
                type: "integer",
                nullable: false,
                defaultValue: 0)
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Statistic",
                schema: "public",
                table: "Statistic",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Statistic",
                schema: "public",
                table: "Statistic");

            migrationBuilder.DropColumn(
                name: "Id",
                schema: "public",
                table: "Statistic");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Statistic",
                schema: "public",
                table: "Statistic",
                column: "UserId");
        }
    }
}
