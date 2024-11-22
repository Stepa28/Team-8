using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Addmap : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_battles_tiles_dto_map_tiles_type",
                table: "battles");

            migrationBuilder.DropTable(
                name: "tiles_dto");

            migrationBuilder.DropIndex(
                name: "ix_battles_map_tiles_type",
                table: "battles");

            migrationBuilder.DropColumn(
                name: "map_tiles_type",
                table: "battles");

            migrationBuilder.AddColumn<int>(
                name: "map_id",
                table: "battles",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "maps",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    tiles_type = table.Column<int[]>(type: "integer[]", nullable: false),
                    count_row = table.Column<int>(type: "integer", nullable: false),
                    count_column = table.Column<int>(type: "integer", nullable: false),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_maps", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "ix_battles_map_id",
                table: "battles",
                column: "map_id");

            migrationBuilder.AddForeignKey(
                name: "fk_battles_maps_map_id",
                table: "battles",
                column: "map_id",
                principalTable: "maps",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_battles_maps_map_id",
                table: "battles");

            migrationBuilder.DropTable(
                name: "maps");

            migrationBuilder.DropIndex(
                name: "ix_battles_map_id",
                table: "battles");

            migrationBuilder.DropColumn(
                name: "map_id",
                table: "battles");

            migrationBuilder.AddColumn<int[]>(
                name: "map_tiles_type",
                table: "battles",
                type: "integer[]",
                nullable: false,
                defaultValue: new int[0]);

            migrationBuilder.CreateTable(
                name: "tiles_dto",
                columns: table => new
                {
                    tiles_type = table.Column<int[]>(type: "integer[]", nullable: false),
                    count_column = table.Column<int>(type: "integer", nullable: false),
                    count_row = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_tiles_dto", x => x.tiles_type);
                });

            migrationBuilder.CreateIndex(
                name: "ix_battles_map_tiles_type",
                table: "battles",
                column: "map_tiles_type");

            migrationBuilder.AddForeignKey(
                name: "fk_battles_tiles_dto_map_tiles_type",
                table: "battles",
                column: "map_tiles_type",
                principalTable: "tiles_dto",
                principalColumn: "tiles_type",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
