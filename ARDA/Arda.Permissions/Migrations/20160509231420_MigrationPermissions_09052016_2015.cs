using System;
using System.Collections.Generic;
using Microsoft.Data.Entity.Migrations;

namespace Arda.Permissions.Migrations
{
    public partial class MigrationPermissions_09052016_2015 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Token",
                table: "Permissions",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(name: "Token", table: "Permissions");
        }
    }
}
