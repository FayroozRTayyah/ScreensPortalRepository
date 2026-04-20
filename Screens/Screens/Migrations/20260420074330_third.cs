using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Screens.Migrations
{
    /// <inheritdoc />
    public partial class third : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "screens",
                columns: table => new
                {
                    screenId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    screenName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    screenDescription = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    screenStatus = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_screens", x => x.screenId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "screens");
        }
    }
}
