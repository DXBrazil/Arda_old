using System;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.Migrations;
using Arda.Kanban.Models;

namespace Arda.Kanban.Migrations
{
    [DbContext(typeof(KanbanContext))]
    [Migration("20160605175845_ArdaMigration_05052016_1458")]
    partial class ArdaMigration_05052016_1458
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0-rc1-16386")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

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

            modelBuilder.Entity("Arda.Kanban.Models.MetricsByWorkload", b =>
                {
                    b.Property<Guid>("MetricID");

                    b.Property<Guid>("WorkloadID");

                    b.HasKey("MetricID", "WorkloadID");

                    b.HasAnnotation("Relational:TableName", "MetricsByWorkloads");
                });

            modelBuilder.Entity("Arda.Kanban.Models.TechnologiesByWorkload", b =>
                {
                    b.Property<Guid>("TechnologyID");

                    b.Property<Guid>("WorkloadID");

                    b.HasKey("TechnologyID", "WorkloadID");

                    b.HasAnnotation("Relational:TableName", "TechnologiesByWorkloads");
                });

            modelBuilder.Entity("Arda.Kanban.Models.Technology", b =>
                {
                    b.Property<Guid>("TechnologyID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("TechnologyDescription");

                    b.Property<string>("TechnologyShortText")
                        .IsRequired();

                    b.HasKey("TechnologyID");

                    b.HasAnnotation("Relational:TableName", "Technologies");
                });

            modelBuilder.Entity("Arda.Kanban.Models.Workload", b =>
                {
                    b.Property<Guid>("WorkloadID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Activity")
                        .IsRequired();

                    b.Property<string>("Complexity")
                        .IsRequired();

                    b.Property<string>("Description")
                        .IsRequired();

                    b.Property<DateTime>("EstimatedEndDate");

                    b.Property<string>("Expertise")
                        .IsRequired();

                    b.Property<string>("FileOrVirtualDirectoryLink");

                    b.Property<DateTime>("StartDate");

                    b.Property<string>("Users")
                        .IsRequired();

                    b.HasKey("WorkloadID");

                    b.HasAnnotation("Relational:TableName", "Workloads");
                });

            modelBuilder.Entity("Arda.Kanban.Models.Metric", b =>
                {
                    b.HasOne("Arda.Kanban.Models.FiscalYear")
                        .WithMany()
                        .HasForeignKey("FiscalYearFiscalYearID");
                });

            modelBuilder.Entity("Arda.Kanban.Models.MetricsByWorkload", b =>
                {
                    b.HasOne("Arda.Kanban.Models.Metric")
                        .WithMany()
                        .HasForeignKey("MetricID");

                    b.HasOne("Arda.Kanban.Models.Workload")
                        .WithMany()
                        .HasForeignKey("WorkloadID");
                });

            modelBuilder.Entity("Arda.Kanban.Models.TechnologiesByWorkload", b =>
                {
                    b.HasOne("Arda.Kanban.Models.Technology")
                        .WithMany()
                        .HasForeignKey("TechnologyID");

                    b.HasOne("Arda.Kanban.Models.Workload")
                        .WithMany()
                        .HasForeignKey("WorkloadID");
                });
        }
    }
}
