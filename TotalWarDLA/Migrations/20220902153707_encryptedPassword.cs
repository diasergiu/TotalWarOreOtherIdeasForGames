using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TotalWarDLA.Migrations
{
    public partial class encryptedPassword : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name:"Password__",
                table: "Users",
                type: "varbinary(max)",
                nullable: true
                );
            migrationBuilder.DropColumn(
                name: "Password",
                table: "Users");
            migrationBuilder.RenameColumn(
                name: "Password__",
                table: "Users",
                newName: "Password");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Password__",
                table: "Users",
                type: "varbinary(max)",
                nullable: true);
            migrationBuilder.DropColumn(
                name: "Password",
                table: "Users");
            migrationBuilder.RenameColumn(
                name: "Password__",
                table: "Users",
                newName: "Password");
        }
    }
}
