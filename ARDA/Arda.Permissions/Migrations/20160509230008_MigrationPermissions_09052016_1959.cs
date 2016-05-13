using System;
using System.Collections.Generic;
using Microsoft.Data.Entity.Migrations;

namespace Arda.Permissions.Migrations
{
    public partial class MigrationPermissions_09052016_1959 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Permissions",
                columns: table => new
                {
                    PermissionID = table.Column<Guid>(nullable: false),
                    PermissionsByUser = table.Column<string>(nullable: true),
                    UserID = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permission", x => x.PermissionID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable("Permissions");
        }
    }
}
