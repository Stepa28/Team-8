using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tiles_dto",
                columns: table => new
                {
                    tiles_type = table.Column<int[]>(type: "integer[]", nullable: false),
                    count_row = table.Column<int>(type: "integer", nullable: false),
                    count_column = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_tiles_dto", x => x.tiles_type);
                });

            migrationBuilder.CreateTable(
                name: "battles",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    map_tiles_type = table.Column<int[]>(type: "integer[]", nullable: false),
                    users = table.Column<List<Guid>>(type: "uuid[]", nullable: false),
                    walking_player = table.Column<Guid>(type: "uuid", nullable: false),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_battles", x => x.id);
                    table.ForeignKey(
                        name: "fk_battles_tiles_dto_map_tiles_type",
                        column: x => x.map_tiles_type,
                        principalTable: "tiles_dto",
                        principalColumn: "tiles_type",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "current_unit_states",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    ultimate = table.Column<int>(type: "integer", nullable: false),
                    health = table.Column<byte>(type: "smallint", nullable: false),
                    attack_power = table.Column<int>(type: "integer", nullable: false),
                    defense_power = table.Column<int>(type: "integer", nullable: false),
                    speed = table.Column<int>(type: "integer", nullable: false),
                    unit_type_id = table.Column<int>(type: "integer", nullable: false),
                    x_cord = table.Column<int>(type: "integer", nullable: false),
                    y_cord = table.Column<int>(type: "integer", nullable: false),
                    battle_id = table.Column<int>(type: "integer", nullable: false),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_current_unit_states", x => x.id);
                    table.ForeignKey(
                        name: "fk_current_unit_states_battles_battle_id",
                        column: x => x.battle_id,
                        principalTable: "battles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_battles_map_tiles_type",
                table: "battles",
                column: "map_tiles_type");

            migrationBuilder.CreateIndex(
                name: "ix_current_unit_states_battle_id",
                table: "current_unit_states",
                column: "battle_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "current_unit_states");

            migrationBuilder.DropTable(
                name: "battles");

            migrationBuilder.DropTable(
                name: "tiles_dto");
        }
    }
}
