using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Arda.Kanban.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Activities",
                columns: table => new
                {
                    ActivityID = table.Column<Guid>(nullable: false),
                    ActivityName = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Activities", x => x.ActivityID);
                });

            migrationBuilder.CreateTable(
                name: "FiscalYears",
                columns: table => new
                {
                    FiscalYearID = table.Column<Guid>(nullable: false),
                    FullNumericFiscalYear = table.Column<int>(nullable: false),
                    TextualFiscalYear = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FiscalYears", x => x.FiscalYearID);
                });

            migrationBuilder.CreateTable(
                name: "Technologies",
                columns: table => new
                {
                    TechnologyID = table.Column<Guid>(nullable: false),
                    TechnologyName = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Technologies", x => x.TechnologyID);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UniqueName = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UniqueName);
                });

            migrationBuilder.CreateTable(
                name: "WorkloadBacklogs",
                columns: table => new
                {
                    WBID = table.Column<Guid>(nullable: false),
                    WBActivityActivityID = table.Column<Guid>(nullable: true),
                    WBComplexity = table.Column<int>(nullable: false),
                    WBCreatedBy = table.Column<string>(nullable: false),
                    WBCreatedDate = table.Column<DateTime>(nullable: false),
                    WBDescription = table.Column<string>(nullable: true),
                    WBEndDate = table.Column<DateTime>(nullable: false),
                    WBExpertise = table.Column<int>(nullable: false),
                    WBIsWorkload = table.Column<bool>(nullable: false),
                    WBStartDate = table.Column<DateTime>(nullable: false),
                    WBStatus = table.Column<int>(nullable: false),
                    WBTitle = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkloadBacklogs", x => x.WBID);
                    table.ForeignKey(
                        name: "FK_WorkloadBacklogs_Activities_WBActivityActivityID",
                        column: x => x.WBActivityActivityID,
                        principalTable: "Activities",
                        principalColumn: "ActivityID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Metrics",
                columns: table => new
                {
                    MetricID = table.Column<Guid>(nullable: false),
                    Description = table.Column<string>(nullable: false),
                    FiscalYearID = table.Column<Guid>(nullable: true),
                    MetricCategory = table.Column<string>(nullable: false),
                    MetricName = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Metrics", x => x.MetricID);
                    table.ForeignKey(
                        name: "FK_Metrics_FiscalYears_FiscalYearID",
                        column: x => x.FiscalYearID,
                        principalTable: "FiscalYears",
                        principalColumn: "FiscalYearID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Appointments",
                columns: table => new
                {
                    AppointmentID = table.Column<Guid>(nullable: false),
                    AppointmentComment = table.Column<string>(nullable: true),
                    AppointmentDate = table.Column<DateTime>(nullable: false),
                    AppointmentHoursDispensed = table.Column<int>(nullable: false),
                    AppointmentTE = table.Column<decimal>(nullable: false),
                    AppointmentUserUniqueName = table.Column<string>(nullable: false),
                    AppointmentWorkloadWBID = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Appointments", x => x.AppointmentID);
                    table.ForeignKey(
                        name: "FK_Appointments_Users_AppointmentUserUniqueName",
                        column: x => x.AppointmentUserUniqueName,
                        principalTable: "Users",
                        principalColumn: "UniqueName",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Appointments_WorkloadBacklogs_AppointmentWorkloadWBID",
                        column: x => x.AppointmentWorkloadWBID,
                        principalTable: "WorkloadBacklogs",
                        principalColumn: "WBID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Files",
                columns: table => new
                {
                    FileID = table.Column<Guid>(nullable: false),
                    FileDescription = table.Column<string>(nullable: true),
                    FileLink = table.Column<string>(nullable: false),
                    FileName = table.Column<string>(nullable: false),
                    WorkloadBacklogWBID = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Files", x => x.FileID);
                    table.ForeignKey(
                        name: "FK_Files_WorkloadBacklogs_WorkloadBacklogWBID",
                        column: x => x.WorkloadBacklogWBID,
                        principalTable: "WorkloadBacklogs",
                        principalColumn: "WBID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "WorkloadBacklogTechnologies",
                columns: table => new
                {
                    WBUTechnologyID = table.Column<Guid>(nullable: false),
                    TechnologyID = table.Column<Guid>(nullable: true),
                    WorkloadBacklogWBID = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkloadBacklogTechnologies", x => x.WBUTechnologyID);
                    table.ForeignKey(
                        name: "FK_WorkloadBacklogTechnologies_Technologies_TechnologyID",
                        column: x => x.TechnologyID,
                        principalTable: "Technologies",
                        principalColumn: "TechnologyID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WorkloadBacklogTechnologies_WorkloadBacklogs_WorkloadBacklogWBID",
                        column: x => x.WorkloadBacklogWBID,
                        principalTable: "WorkloadBacklogs",
                        principalColumn: "WBID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "WorkloadBacklogUsers",
                columns: table => new
                {
                    WBUserID = table.Column<Guid>(nullable: false),
                    UserUniqueName = table.Column<string>(nullable: true),
                    WorkloadBacklogWBID = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkloadBacklogUsers", x => x.WBUserID);
                    table.ForeignKey(
                        name: "FK_WorkloadBacklogUsers_Users_UserUniqueName",
                        column: x => x.UserUniqueName,
                        principalTable: "Users",
                        principalColumn: "UniqueName",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WorkloadBacklogUsers_WorkloadBacklogs_WorkloadBacklogWBID",
                        column: x => x.WorkloadBacklogWBID,
                        principalTable: "WorkloadBacklogs",
                        principalColumn: "WBID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "WorkloadBacklogMetrics",
                columns: table => new
                {
                    WBMetricID = table.Column<Guid>(nullable: false),
                    MetricID = table.Column<Guid>(nullable: true),
                    WorkloadBacklogWBID = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkloadBacklogMetrics", x => x.WBMetricID);
                    table.ForeignKey(
                        name: "FK_WorkloadBacklogMetrics_Metrics_MetricID",
                        column: x => x.MetricID,
                        principalTable: "Metrics",
                        principalColumn: "MetricID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WorkloadBacklogMetrics_WorkloadBacklogs_WorkloadBacklogWBID",
                        column: x => x.WorkloadBacklogWBID,
                        principalTable: "WorkloadBacklogs",
                        principalColumn: "WBID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_AppointmentUserUniqueName",
                table: "Appointments",
                column: "AppointmentUserUniqueName");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_AppointmentWorkloadWBID",
                table: "Appointments",
                column: "AppointmentWorkloadWBID");

            migrationBuilder.CreateIndex(
                name: "IX_Files_WorkloadBacklogWBID",
                table: "Files",
                column: "WorkloadBacklogWBID");

            migrationBuilder.CreateIndex(
                name: "IX_Metrics_FiscalYearID",
                table: "Metrics",
                column: "FiscalYearID");

            migrationBuilder.CreateIndex(
                name: "IX_WorkloadBacklogs_WBActivityActivityID",
                table: "WorkloadBacklogs",
                column: "WBActivityActivityID");

            migrationBuilder.CreateIndex(
                name: "IX_WorkloadBacklogMetrics_MetricID",
                table: "WorkloadBacklogMetrics",
                column: "MetricID");

            migrationBuilder.CreateIndex(
                name: "IX_WorkloadBacklogMetrics_WorkloadBacklogWBID",
                table: "WorkloadBacklogMetrics",
                column: "WorkloadBacklogWBID");

            migrationBuilder.CreateIndex(
                name: "IX_WorkloadBacklogTechnologies_TechnologyID",
                table: "WorkloadBacklogTechnologies",
                column: "TechnologyID");

            migrationBuilder.CreateIndex(
                name: "IX_WorkloadBacklogTechnologies_WorkloadBacklogWBID",
                table: "WorkloadBacklogTechnologies",
                column: "WorkloadBacklogWBID");

            migrationBuilder.CreateIndex(
                name: "IX_WorkloadBacklogUsers_UserUniqueName",
                table: "WorkloadBacklogUsers",
                column: "UserUniqueName");

            migrationBuilder.CreateIndex(
                name: "IX_WorkloadBacklogUsers_WorkloadBacklogWBID",
                table: "WorkloadBacklogUsers",
                column: "WorkloadBacklogWBID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Appointments");

            migrationBuilder.DropTable(
                name: "Files");

            migrationBuilder.DropTable(
                name: "WorkloadBacklogMetrics");

            migrationBuilder.DropTable(
                name: "WorkloadBacklogTechnologies");

            migrationBuilder.DropTable(
                name: "WorkloadBacklogUsers");

            migrationBuilder.DropTable(
                name: "Metrics");

            migrationBuilder.DropTable(
                name: "Technologies");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "WorkloadBacklogs");

            migrationBuilder.DropTable(
                name: "FiscalYears");

            migrationBuilder.DropTable(
                name: "Activities");
        }
    }
}
