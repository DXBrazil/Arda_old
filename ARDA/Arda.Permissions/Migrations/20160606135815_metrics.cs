using System;
using System.Collections.Generic;
using Microsoft.Data.Entity.Migrations;

namespace Arda.Permissions.Migrations
{
    public partial class metrics : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(name: "FK_Resource_Module_ModuleID", table: "Resources");
            migrationBuilder.DropForeignKey(name: "FK_UsersPermission_Resource_ResourceID", table: "UsersPermissions");
            migrationBuilder.AddForeignKey(
                name: "FK_Resource_Module_ModuleID",
                table: "Resources",
                column: "ModuleID",
                principalTable: "Modules",
                principalColumn: "ModuleID",
                onDelete: ReferentialAction.Cascade);
            migrationBuilder.AddForeignKey(
                name: "FK_UsersPermission_Resource_ResourceID",
                table: "UsersPermissions",
                column: "ResourceID",
                principalTable: "Resources",
                principalColumn: "ResourceID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(name: "FK_Resource_Module_ModuleID", table: "Resources");
            migrationBuilder.DropForeignKey(name: "FK_UsersPermission_Resource_ResourceID", table: "UsersPermissions");
            migrationBuilder.AddForeignKey(
                name: "FK_Resource_Module_ModuleID",
                table: "Resources",
                column: "ModuleID",
                principalTable: "Modules",
                principalColumn: "ModuleID",
                onDelete: ReferentialAction.Restrict);
            migrationBuilder.AddForeignKey(
                name: "FK_UsersPermission_Resource_ResourceID",
                table: "UsersPermissions",
                column: "ResourceID",
                principalTable: "Resources",
                principalColumn: "ResourceID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
