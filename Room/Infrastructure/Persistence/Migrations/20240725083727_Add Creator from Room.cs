using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddCreatorfromRoom : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "creator_id",
                table: "rooms",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "ix_rooms_creator_id",
                table: "rooms",
                column: "creator_id");

            migrationBuilder.AddForeignKey(
                name: "fk_rooms_users_creator_id",
                table: "rooms",
                column: "creator_id",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_rooms_users_creator_id",
                table: "rooms");

            migrationBuilder.DropIndex(
                name: "ix_rooms_creator_id",
                table: "rooms");

            migrationBuilder.DropColumn(
                name: "creator_id",
                table: "rooms");
        }
    }
}
