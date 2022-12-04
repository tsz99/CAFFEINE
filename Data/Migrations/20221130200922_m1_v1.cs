using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CAFFEINE.Data.Migrations
{
    public partial class m1_v1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Caffs",
                columns: table => new
                {
                    DB_ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Creator = table.Column<string>(nullable: true),
                    Year = table.Column<int>(nullable: false),
                    Month = table.Column<int>(nullable: false),
                    Day = table.Column<int>(nullable: false),
                    Hour = table.Column<int>(nullable: false),
                    Minute = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Caffs", x => x.DB_ID);
                });

            migrationBuilder.CreateTable(
                name: "Ciffs",
                columns: table => new
                {
                    DB_ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Pixels = table.Column<byte[]>(nullable: true),
                    Width = table.Column<int>(nullable: false),
                    Height = table.Column<int>(nullable: false),
                    Duration = table.Column<int>(nullable: false),
                    CaffDB_ID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ciffs", x => x.DB_ID);
                    table.ForeignKey(
                        name: "FK_Ciffs_Caffs_CaffDB_ID",
                        column: x => x.CaffDB_ID,
                        principalTable: "Caffs",
                        principalColumn: "DB_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    DB_ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Creator = table.Column<string>(nullable: true),
                    Text = table.Column<string>(nullable: true),
                    CaffDB_ID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.DB_ID);
                    table.ForeignKey(
                        name: "FK_Comments_Caffs_CaffDB_ID",
                        column: x => x.CaffDB_ID,
                        principalTable: "Caffs",
                        principalColumn: "DB_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Captions",
                columns: table => new
                {
                    DB_ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    text = table.Column<string>(nullable: true),
                    CiffDB_ID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Captions", x => x.DB_ID);
                    table.ForeignKey(
                        name: "FK_Captions_Ciffs_CiffDB_ID",
                        column: x => x.CiffDB_ID,
                        principalTable: "Ciffs",
                        principalColumn: "DB_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tags",
                columns: table => new
                {
                    DB_ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Text = table.Column<string>(nullable: true),
                    CiffDB_ID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tags", x => x.DB_ID);
                    table.ForeignKey(
                        name: "FK_Tags_Ciffs_CiffDB_ID",
                        column: x => x.CiffDB_ID,
                        principalTable: "Ciffs",
                        principalColumn: "DB_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Captions_CiffDB_ID",
                table: "Captions",
                column: "CiffDB_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Ciffs_CaffDB_ID",
                table: "Ciffs",
                column: "CaffDB_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_CaffDB_ID",
                table: "Comments",
                column: "CaffDB_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Tags_CiffDB_ID",
                table: "Tags",
                column: "CiffDB_ID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Captions");

            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "Tags");

            migrationBuilder.DropTable(
                name: "Ciffs");

            migrationBuilder.DropTable(
                name: "Caffs");
        }
    }
}
