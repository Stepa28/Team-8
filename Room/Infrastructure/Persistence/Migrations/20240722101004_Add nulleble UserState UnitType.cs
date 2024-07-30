using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddnullebleUserStateUnitType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_user_states_unit_types_unit_type_id",
                table: "user_states");

            migrationBuilder.AlterColumn<int>(
                name: "unit_type_id",
                table: "user_states",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "fk_user_states_unit_types_unit_type_id",
                table: "user_states",
                column: "unit_type_id",
                principalTable: "unit_types",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_user_states_unit_types_unit_type_id",
                table: "user_states");

            migrationBuilder.AlterColumn<int>(
                name: "unit_type_id",
                table: "user_states",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "fk_user_states_unit_types_unit_type_id",
                table: "user_states",
                column: "unit_type_id",
                principalTable: "unit_types",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
