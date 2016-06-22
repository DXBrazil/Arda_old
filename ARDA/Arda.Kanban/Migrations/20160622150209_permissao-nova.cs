using System;
using System.Collections.Generic;
using Microsoft.Data.Entity.Migrations;

namespace Arda.Kanban.Migrations
{
    public partial class permissaonova : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(name: "FK_Appointment_User_AppointmentUserUniqueName", table: "Appointments");
            migrationBuilder.DropForeignKey(name: "FK_Appointment_WorkloadBacklog_AppointmentWorkloadWBID", table: "Appointments");
            migrationBuilder.AddForeignKey(
                name: "FK_Appointment_User_AppointmentUserUniqueName",
                table: "Appointments",
                column: "AppointmentUserUniqueName",
                principalTable: "Users",
                principalColumn: "UniqueName",
                onDelete: ReferentialAction.Cascade);
            migrationBuilder.AddForeignKey(
                name: "FK_Appointment_WorkloadBacklog_AppointmentWorkloadWBID",
                table: "Appointments",
                column: "AppointmentWorkloadWBID",
                principalTable: "WorkloadBacklogs",
                principalColumn: "WBID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(name: "FK_Appointment_User_AppointmentUserUniqueName", table: "Appointments");
            migrationBuilder.DropForeignKey(name: "FK_Appointment_WorkloadBacklog_AppointmentWorkloadWBID", table: "Appointments");
            migrationBuilder.AddForeignKey(
                name: "FK_Appointment_User_AppointmentUserUniqueName",
                table: "Appointments",
                column: "AppointmentUserUniqueName",
                principalTable: "Users",
                principalColumn: "UniqueName",
                onDelete: ReferentialAction.Restrict);
            migrationBuilder.AddForeignKey(
                name: "FK_Appointment_WorkloadBacklog_AppointmentWorkloadWBID",
                table: "Appointments",
                column: "AppointmentWorkloadWBID",
                principalTable: "WorkloadBacklogs",
                principalColumn: "WBID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
