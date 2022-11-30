using Microsoft.EntityFrameworkCore.Migrations;

namespace CAFFEINE.Data.Migrations
{
    public partial class m1_v2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "text",
                table: "Captions",
                newName: "Text");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Text",
                table: "Captions",
                newName: "text");
        }
    }
}
