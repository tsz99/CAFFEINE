using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CAFFEINE.Data.Migrations
{
    public partial class m1_v5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.AddColumn<DateTime>(
                name: "DT_Created",
                table: "Comments",
                nullable: false,
                defaultValue: DateTime.Now);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Created",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "DT_Created",
                table: "Comments");
        }
    }
}
