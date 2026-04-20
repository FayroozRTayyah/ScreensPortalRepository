using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Screens.Migrations
{
    /// <inheritdoc />
    public partial class fourth : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_images_imageScreenId",
                table: "images",
                column: "imageScreenId");

            migrationBuilder.AddForeignKey(
                name: "FK_images_screens_imageScreenId",
                table: "images",
                column: "imageScreenId",
                principalTable: "screens",
                principalColumn: "screenId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_images_screens_imageScreenId",
                table: "images");

            migrationBuilder.DropIndex(
                name: "IX_images_imageScreenId",
                table: "images");
        }
    }
}
