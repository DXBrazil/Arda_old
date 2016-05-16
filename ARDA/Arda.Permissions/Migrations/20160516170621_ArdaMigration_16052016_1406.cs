using System;
using System.Collections.Generic;
using Microsoft.Data.Entity.Migrations;

namespace Arda.Permissions.Migrations
{
    public partial class ArdaMigration_16052016_1406 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UsersProperties",
                columns: table => new
                {
                    UniqueName = table.Column<string>(nullable: false),
                    AuthCode = table.Column<string>(nullable: true),
                    UserPropertiesUniqueName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserProperties", x => x.UniqueName);
                    table.ForeignKey(
                        name: "FK_UserProperties_UserProperties_UserPropertiesUniqueName",
                        column: x => x.UserPropertiesUniqueName,
                        principalTable: "UsersProperties",
                        principalColumn: "UniqueName",
                        onDelete: ReferentialAction.Restrict);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable("UsersProperties");
        }
    }
}
