using System;
using System.Collections.Generic;
using Microsoft.Data.Entity.Migrations;

namespace Arda.Authentication.Migrations
{
    public partial class MigrationAuthentication_05052016_1617 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte>(
                name: "Status",
                table: "User",
                nullable: false,
                defaultValue: (byte)0);
            migrationBuilder.AddColumn<string>(
                name: "Token",
                table: "User",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(name: "Status", table: "User");
            migrationBuilder.DropColumn(name: "Token", table: "User");
        }
    }
}
