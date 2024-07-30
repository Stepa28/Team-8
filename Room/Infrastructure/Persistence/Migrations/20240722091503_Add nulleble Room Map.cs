using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddnullebleRoomMap : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_rooms_maps_current_map_id",
                table: "rooms");

            migrationBuilder.AlterColumn<int>(
                name: "current_map_id",
                table: "rooms",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "fk_rooms_maps_current_map_id",
                table: "rooms",
                column: "current_map_id",
                principalTable: "maps",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_rooms_maps_current_map_id",
                table: "rooms");

            migrationBuilder.AlterColumn<int>(
                name: "current_map_id",
                table: "rooms",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "fk_rooms_maps_current_map_id",
                table: "rooms",
                column: "current_map_id",
                principalTable: "maps",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
