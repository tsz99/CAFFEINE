using Microsoft.EntityFrameworkCore.Migrations;

namespace CAFFEINE.Data.Migrations
{
    public partial class m1_v3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Captions");

            migrationBuilder.AddColumn<string>(
                name: "Caption",
                table: "Ciffs",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Caption",
                table: "Ciffs");

            migrationBuilder.CreateTable(
                name: "Captions",
                columns: table => new
                {
                    DB_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CiffDB_ID = table.Column<int>(type: "int", nullable: true),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Captions", x => x.DB_ID);
                    table.ForeignKey(
                        name: "FK_Captions_Ciffs_CiffDB_ID",
                        column: x => x.CiffDB_ID,
                        principalTable: "Ciffs",
                        principalColumn: "DB_ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Captions_CiffDB_ID",
                table: "Captions",
                column: "CiffDB_ID");
        }
    }
}
