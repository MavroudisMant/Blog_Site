using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Blog_Site.Migrations
{
    public partial class Test : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<byte[]>(
                name: "PasswordSalt",
                table: "Users",
                type: "varbinary(128)",
                maxLength: 128,
                nullable: true,
                oldClrType: typeof(byte[]),
                oldType: "varbinary(128)",
                oldMaxLength: 128);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<byte[]>(
                name: "PasswordSalt",
                table: "Users",
                type: "varbinary(128)",
                maxLength: 128,
                nullable: false,
                defaultValue: new byte[0],
                oldClrType: typeof(byte[]),
                oldType: "varbinary(128)",
                oldMaxLength: 128,
                oldNullable: true);
        }
    }
}
