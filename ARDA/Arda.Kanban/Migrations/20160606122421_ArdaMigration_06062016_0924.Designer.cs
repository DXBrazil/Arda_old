using System;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.Migrations;
using Arda.Common.Models.Kanban;

namespace Arda.Kanban.Migrations
{
    [DbContext(typeof(KanbanContext))]
    [Migration("20160606122421_ArdaMigration_06062016_0924")]
    partial class ArdaMigration_06062016_0924
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0-rc1-16386")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Arda.Kanban.Models.Activity", b =>
                {
                    b.Property<Guid>("ActivityID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ActivityName")
                        .IsRequired();

                    b.HasKey("ActivityID");

                    b.HasAnnotation("Relational:TableName", "Activities");
                });

            modelBuilder.Entity("Arda.Kanban.Models.File", b =>
                {
                    b.Property<Guid>("FileID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("FileDescription");

                    b.Property<string>("FileLink")
                        .IsRequired();

                    b.Property<string>("FileName")
                        .IsRequired();

                    b.Property<Guid?>("WorkloadBacklogWBID");

                    b.HasKey("FileID");

                    b.HasAnnotation("Relational:TableName", "Files");
                });

            modelBuilder.Entity("Arda.Kanban.Models.FiscalYear", b =>
                {
                    b.Property<Guid>("FiscalYearID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("FullNumericFiscalYear");

                    b.Property<string>("TextualFiscalYear")
                        .IsRequired();

                    b.HasKey("FiscalYearID");

                    b.HasAnnotation("Relational:TableName", "FiscalYears");
                });

            modelBuilder.Entity("Arda.Kanban.Models.Metric", b =>
                {
                    b.Property<Guid>("MetricID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description")
                        .IsRequired();

                    b.Property<Guid?>("FiscalYearFiscalYearID");

                    b.Property<string>("MetricCategory")
                        .IsRequired();

                    b.Property<string>("MetricName")
                        .IsRequired();

                    b.HasKey("MetricID");

                    b.HasAnnotation("Relational:TableName", "Metrics");
                });

            modelBuilder.Entity("Arda.Kanban.Models.Technology", b =>
                {
                    b.Property<Guid>("TechnologyID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("TechnologyName")
                        .IsRequired();

                    b.HasKey("TechnologyID");

                    b.HasAnnotation("Relational:TableName", "Technologies");
                });

            modelBuilder.Entity("Arda.Kanban.Models.WorkloadBacklog", b =>
                {
                    b.Property<Guid>("WBID")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid?>("WBActivityActivityID");

                    b.Property<int>("WBComplexity");

                    b.Property<string>("WBCreatedBy")
                        .IsRequired();

                    b.Property<DateTime>("WBCreatedDate");

                    b.Property<string>("WBDescription");

                    b.Property<DateTime>("WBEndDate");

                    b.Property<int>("WBExpertise");

                    b.Property<DateTime>("WBStartDate");

                    b.Property<string>("WBTitle")
                        .IsRequired();

                    b.HasKey("WBID");

                    b.HasAnnotation("Relational:TableName", "WorkloadBacklogs");
                });

            modelBuilder.Entity("Arda.Kanban.Models.WorkloadBacklogMetric", b =>
                {
                    b.Property<Guid>("WBMetricID")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid?>("MetricMetricID");

                    b.Property<Guid?>("WorkloadBacklogWBID");

                    b.HasKey("WBMetricID");

                    b.HasAnnotation("Relational:TableName", "WorkloadBacklogMetrics");
                });

            modelBuilder.Entity("Arda.Kanban.Models.WorkloadBacklogTechnology", b =>
                {
                    b.Property<Guid>("WBUTechnologyID")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid?>("TechnologyTechnologyID");

                    b.Property<Guid?>("WorkloadBacklogWBID");

                    b.HasKey("WBUTechnologyID");

                    b.HasAnnotation("Relational:TableName", "WorkloadBacklogTechnologies");
                });

            modelBuilder.Entity("Arda.Kanban.Models.WorkloadBacklogUser", b =>
                {
                    b.Property<Guid>("WBUserID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("KanbanUserUniqueName");

                    b.Property<Guid?>("WorkloadBacklogWBID");

                    b.HasKey("WBUserID");

                    b.HasAnnotation("Relational:TableName", "WorkloadBacklogUsers");
                });

            modelBuilder.Entity("Arda.Kanban.ViewModels.UserKanbanViewModel", b =>
                {
                    b.Property<string>("UniqueName");

                    b.HasKey("UniqueName");

                    b.HasAnnotation("Relational:TableName", "UsersKanban");
                });

            modelBuilder.Entity("Arda.Kanban.Models.File", b =>
                {
                    b.HasOne("Arda.Kanban.Models.WorkloadBacklog")
                        .WithMany()
                        .HasForeignKey("WorkloadBacklogWBID");
                });

            modelBuilder.Entity("Arda.Kanban.Models.Metric", b =>
                {
                    b.HasOne("Arda.Kanban.Models.FiscalYear")
                        .WithMany()
                        .HasForeignKey("FiscalYearFiscalYearID");
                });

            modelBuilder.Entity("Arda.Kanban.Models.WorkloadBacklog", b =>
                {
                    b.HasOne("Arda.Kanban.Models.Activity")
                        .WithMany()
                        .HasForeignKey("WBActivityActivityID");
                });

            modelBuilder.Entity("Arda.Kanban.Models.WorkloadBacklogMetric", b =>
                {
                    b.HasOne("Arda.Kanban.Models.Metric")
                        .WithMany()
                        .HasForeignKey("MetricMetricID");

                    b.HasOne("Arda.Kanban.Models.WorkloadBacklog")
                        .WithMany()
                        .HasForeignKey("WorkloadBacklogWBID");
                });

            modelBuilder.Entity("Arda.Kanban.Models.WorkloadBacklogTechnology", b =>
                {
                    b.HasOne("Arda.Kanban.Models.Technology")
                        .WithMany()
                        .HasForeignKey("TechnologyTechnologyID");

                    b.HasOne("Arda.Kanban.Models.WorkloadBacklog")
                        .WithMany()
                        .HasForeignKey("WorkloadBacklogWBID");
                });

            modelBuilder.Entity("Arda.Kanban.Models.WorkloadBacklogUser", b =>
                {
                    b.HasOne("Arda.Kanban.ViewModels.UserKanbanViewModel")
                        .WithMany()
                        .HasForeignKey("KanbanUserUniqueName");

                    b.HasOne("Arda.Kanban.Models.WorkloadBacklog")
                        .WithMany()
                        .HasForeignKey("WorkloadBacklogWBID");
                });
        }
    }
}
