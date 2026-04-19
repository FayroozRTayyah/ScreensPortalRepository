using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Screens.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "images",
                columns: table => new
                {
                    imageID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    imageTitle = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    imageDescription = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    imageBath = table.Column<string>(type: "nvarchar(20)", maxLength: 50, nullable: false),
                    imagefromDate = table.Column<DateOnly>(type: "date", nullable: false),
                    imagetoDate = table.Column<DateOnly>(type: "date", nullable: false),
                    image_status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_images", x => x.imageID);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "images");
        }
    }
}
