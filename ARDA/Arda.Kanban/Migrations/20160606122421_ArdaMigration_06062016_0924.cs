using System;
using System.Collections.Generic;
using Microsoft.Data.Entity.Migrations;

namespace Arda.Kanban.Migrations
{
    public partial class ArdaMigration_06062016_0924 : Migration
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
                    table.PrimaryKey("PK_Activity", x => x.ActivityID);
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
                    table.PrimaryKey("PK_FiscalYear", x => x.FiscalYearID);
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
                    table.PrimaryKey("PK_Technology", x => x.TechnologyID);
                });
            migrationBuilder.CreateTable(
                name: "UsersKanban",
                columns: table => new
                {
                    UniqueName = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserKanbanViewModel", x => x.UniqueName);
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
                    WBStartDate = table.Column<DateTime>(nullable: false),
                    WBTitle = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkloadBacklog", x => x.WBID);
                    table.ForeignKey(
                        name: "FK_WorkloadBacklog_Activity_WBActivityActivityID",
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
                    FiscalYearFiscalYearID = table.Column<Guid>(nullable: true),
                    MetricCategory = table.Column<string>(nullable: false),
                    MetricName = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Metric", x => x.MetricID);
                    table.ForeignKey(
                        name: "FK_Metric_FiscalYear_FiscalYearFiscalYearID",
                        column: x => x.FiscalYearFiscalYearID,
                        principalTable: "FiscalYears",
                        principalColumn: "FiscalYearID",
                        onDelete: ReferentialAction.Restrict);
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
                    table.PrimaryKey("PK_File", x => x.FileID);
                    table.ForeignKey(
                        name: "FK_File_WorkloadBacklog_WorkloadBacklogWBID",
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
                    TechnologyTechnologyID = table.Column<Guid>(nullable: true),
                    WorkloadBacklogWBID = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkloadBacklogTechnology", x => x.WBUTechnologyID);
                    table.ForeignKey(
                        name: "FK_WorkloadBacklogTechnology_Technology_TechnologyTechnologyID",
                        column: x => x.TechnologyTechnologyID,
                        principalTable: "Technologies",
                        principalColumn: "TechnologyID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WorkloadBacklogTechnology_WorkloadBacklog_WorkloadBacklogWBID",
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
                    KanbanUserUniqueName = table.Column<string>(nullable: true),
                    WorkloadBacklogWBID = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkloadBacklogUser", x => x.WBUserID);
                    table.ForeignKey(
                        name: "FK_WorkloadBacklogUser_UserKanbanViewModel_KanbanUserUniqueName",
                        column: x => x.KanbanUserUniqueName,
                        principalTable: "UsersKanban",
                        principalColumn: "UniqueName",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WorkloadBacklogUser_WorkloadBacklog_WorkloadBacklogWBID",
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
                    MetricMetricID = table.Column<Guid>(nullable: true),
                    WorkloadBacklogWBID = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkloadBacklogMetric", x => x.WBMetricID);
                    table.ForeignKey(
                        name: "FK_WorkloadBacklogMetric_Metric_MetricMetricID",
                        column: x => x.MetricMetricID,
                        principalTable: "Metrics",
                        principalColumn: "MetricID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WorkloadBacklogMetric_WorkloadBacklog_WorkloadBacklogWBID",
                        column: x => x.WorkloadBacklogWBID,
                        principalTable: "WorkloadBacklogs",
                        principalColumn: "WBID",
                        onDelete: ReferentialAction.Restrict);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable("Files");
            migrationBuilder.DropTable("WorkloadBacklogMetrics");
            migrationBuilder.DropTable("WorkloadBacklogTechnologies");
            migrationBuilder.DropTable("WorkloadBacklogUsers");
            migrationBuilder.DropTable("Metrics");
            migrationBuilder.DropTable("Technologies");
            migrationBuilder.DropTable("UsersKanban");
            migrationBuilder.DropTable("WorkloadBacklogs");
            migrationBuilder.DropTable("FiscalYears");
            migrationBuilder.DropTable("Activities");
        }
    }
}
