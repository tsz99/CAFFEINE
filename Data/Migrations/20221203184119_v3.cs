using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CAFFEINE.Data.Migrations
{
    public partial class v3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "gifContent",
                table: "Caffs",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "gifContent",
                table: "Caffs");
        }
    }
}
