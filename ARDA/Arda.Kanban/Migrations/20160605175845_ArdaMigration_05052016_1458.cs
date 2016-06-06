using System;
using System.Collections.Generic;
using Microsoft.Data.Entity.Migrations;

namespace Arda.Kanban.Migrations
{
    public partial class ArdaMigration_05052016_1458 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                    TechnologyDescription = table.Column<string>(nullable: true),
                    TechnologyShortText = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Technology", x => x.TechnologyID);
                });
            migrationBuilder.CreateTable(
                name: "Workloads",
                columns: table => new
                {
                    WorkloadID = table.Column<Guid>(nullable: false),
                    Activity = table.Column<string>(nullable: false),
                    Complexity = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: false),
                    EstimatedEndDate = table.Column<DateTime>(nullable: false),
                    Expertise = table.Column<string>(nullable: false),
                    FileOrVirtualDirectoryLink = table.Column<string>(nullable: true),
                    StartDate = table.Column<DateTime>(nullable: false),
                    Users = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Workload", x => x.WorkloadID);
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
                name: "TechnologiesByWorkloads",
                columns: table => new
                {
                    TechnologyID = table.Column<Guid>(nullable: false),
                    WorkloadID = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TechnologiesByWorkload", x => new { x.TechnologyID, x.WorkloadID });
                    table.ForeignKey(
                        name: "FK_TechnologiesByWorkload_Technology_TechnologyID",
                        column: x => x.TechnologyID,
                        principalTable: "Technologies",
                        principalColumn: "TechnologyID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TechnologiesByWorkload_Workload_WorkloadID",
                        column: x => x.WorkloadID,
                        principalTable: "Workloads",
                        principalColumn: "WorkloadID",
                        onDelete: ReferentialAction.Cascade);
                });
            migrationBuilder.CreateTable(
                name: "MetricsByWorkloads",
                columns: table => new
                {
                    MetricID = table.Column<Guid>(nullable: false),
                    WorkloadID = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MetricsByWorkload", x => new { x.MetricID, x.WorkloadID });
                    table.ForeignKey(
                        name: "FK_MetricsByWorkload_Metric_MetricID",
                        column: x => x.MetricID,
                        principalTable: "Metrics",
                        principalColumn: "MetricID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MetricsByWorkload_Workload_WorkloadID",
                        column: x => x.WorkloadID,
                        principalTable: "Workloads",
                        principalColumn: "WorkloadID",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable("MetricsByWorkloads");
            migrationBuilder.DropTable("TechnologiesByWorkloads");
            migrationBuilder.DropTable("Metrics");
            migrationBuilder.DropTable("Technologies");
            migrationBuilder.DropTable("Workloads");
            migrationBuilder.DropTable("FiscalYears");
        }
    }
}
