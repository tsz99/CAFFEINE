using Microsoft.EntityFrameworkCore.Migrations;

namespace CAFFEINE.Data.Migrations
{
    public partial class m1_v4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Caffs_CaffDB_ID",
                table: "Comments");

            migrationBuilder.AlterColumn<int>(
                name: "CaffDB_ID",
                table: "Comments",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Caffs_CaffDB_ID",
                table: "Comments",
                column: "CaffDB_ID",
                principalTable: "Caffs",
                principalColumn: "DB_ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Caffs_CaffDB_ID",
                table: "Comments");

            migrationBuilder.AlterColumn<int>(
                name: "CaffDB_ID",
                table: "Comments",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Caffs_CaffDB_ID",
                table: "Comments",
                column: "CaffDB_ID",
                principalTable: "Caffs",
                principalColumn: "DB_ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
