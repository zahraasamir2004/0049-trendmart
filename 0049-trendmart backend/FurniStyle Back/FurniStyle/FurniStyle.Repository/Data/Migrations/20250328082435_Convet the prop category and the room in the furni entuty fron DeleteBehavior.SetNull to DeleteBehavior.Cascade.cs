using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FurniStyle.Repository.Data.Migrations
{
    /// <inheritdoc />
    public partial class ConvetthepropcategoryandtheroominthefurnientutyfronDeleteBehaviorSetNulltoDeleteBehaviorCascade : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Furnis_Categories_CategoryId",
                table: "Furnis");

            migrationBuilder.DropForeignKey(
                name: "FK_Furnis_Rooms_RoomId",
                table: "Furnis");

            migrationBuilder.AddForeignKey(
                name: "FK_Furnis_Categories_CategoryId",
                table: "Furnis",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Furnis_Rooms_RoomId",
                table: "Furnis",
                column: "RoomId",
                principalTable: "Rooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Furnis_Categories_CategoryId",
                table: "Furnis");

            migrationBuilder.DropForeignKey(
                name: "FK_Furnis_Rooms_RoomId",
                table: "Furnis");

            migrationBuilder.AddForeignKey(
                name: "FK_Furnis_Categories_CategoryId",
                table: "Furnis",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Furnis_Rooms_RoomId",
                table: "Furnis",
                column: "RoomId",
                principalTable: "Rooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
