using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Arda.Kanban.Models;

namespace Arda.Kanban.Migrations
{
    [DbContext(typeof(KanbanContext))]
    [Migration("20160825200358_InitialMigration")]
    partial class InitialMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.0-rtm-21431")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Arda.Common.Models.Kanban.Activity", b =>
                {
                    b.Property<Guid>("ActivityID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ActivityName")
                        .IsRequired();

                    b.HasKey("ActivityID");

                    b.ToTable("Activities");
                });

            modelBuilder.Entity("Arda.Common.Models.Kanban.Appointment", b =>
                {
                    b.Property<Guid>("AppointmentID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AppointmentComment");

                    b.Property<DateTime>("AppointmentDate");

                    b.Property<int>("AppointmentHoursDispensed");

                    b.Property<decimal>("AppointmentTE");

                    b.Property<string>("AppointmentUserUniqueName")
                        .IsRequired();

                    b.Property<Guid?>("AppointmentWorkloadWBID")
                        .IsRequired();

                    b.HasKey("AppointmentID");

                    b.HasIndex("AppointmentUserUniqueName");

                    b.HasIndex("AppointmentWorkloadWBID");

                    b.ToTable("Appointments");
                });

            modelBuilder.Entity("Arda.Common.Models.Kanban.File", b =>
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

                    b.HasIndex("WorkloadBacklogWBID");

                    b.ToTable("Files");
                });

            modelBuilder.Entity("Arda.Common.Models.Kanban.FiscalYear", b =>
                {
                    b.Property<Guid>("FiscalYearID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("FullNumericFiscalYear");

                    b.Property<string>("TextualFiscalYear")
                        .IsRequired();

                    b.HasKey("FiscalYearID");

                    b.ToTable("FiscalYears");
                });

            modelBuilder.Entity("Arda.Common.Models.Kanban.Metric", b =>
                {
                    b.Property<Guid>("MetricID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description")
                        .IsRequired();

                    b.Property<Guid?>("FiscalYearID");

                    b.Property<string>("MetricCategory")
                        .IsRequired();

                    b.Property<string>("MetricName")
                        .IsRequired();

                    b.HasKey("MetricID");

                    b.HasIndex("FiscalYearID");

                    b.ToTable("Metrics");
                });

            modelBuilder.Entity("Arda.Common.Models.Kanban.Technology", b =>
                {
                    b.Property<Guid>("TechnologyID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("TechnologyName")
                        .IsRequired();

                    b.HasKey("TechnologyID");

                    b.ToTable("Technologies");
                });

            modelBuilder.Entity("Arda.Common.Models.Kanban.User", b =>
                {
                    b.Property<string>("UniqueName");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("UniqueName");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Arda.Common.Models.Kanban.WorkloadBacklog", b =>
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

                    b.Property<bool>("WBIsWorkload");

                    b.Property<DateTime>("WBStartDate");

                    b.Property<int>("WBStatus");

                    b.Property<string>("WBTitle")
                        .IsRequired();

                    b.HasKey("WBID");

                    b.HasIndex("WBActivityActivityID");

                    b.ToTable("WorkloadBacklogs");
                });

            modelBuilder.Entity("Arda.Common.Models.Kanban.WorkloadBacklogMetric", b =>
                {
                    b.Property<Guid>("WBMetricID")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid?>("MetricID");

                    b.Property<Guid?>("WorkloadBacklogWBID");

                    b.HasKey("WBMetricID");

                    b.HasIndex("MetricID");

                    b.HasIndex("WorkloadBacklogWBID");

                    b.ToTable("WorkloadBacklogMetrics");
                });

            modelBuilder.Entity("Arda.Common.Models.Kanban.WorkloadBacklogTechnology", b =>
                {
                    b.Property<Guid>("WBUTechnologyID")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid?>("TechnologyID");

                    b.Property<Guid?>("WorkloadBacklogWBID");

                    b.HasKey("WBUTechnologyID");

                    b.HasIndex("TechnologyID");

                    b.HasIndex("WorkloadBacklogWBID");

                    b.ToTable("WorkloadBacklogTechnologies");
                });

            modelBuilder.Entity("Arda.Common.Models.Kanban.WorkloadBacklogUser", b =>
                {
                    b.Property<Guid>("WBUserID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("UserUniqueName");

                    b.Property<Guid?>("WorkloadBacklogWBID");

                    b.HasKey("WBUserID");

                    b.HasIndex("UserUniqueName");

                    b.HasIndex("WorkloadBacklogWBID");

                    b.ToTable("WorkloadBacklogUsers");
                });

            modelBuilder.Entity("Arda.Common.Models.Kanban.Appointment", b =>
                {
                    b.HasOne("Arda.Common.Models.Kanban.User", "AppointmentUser")
                        .WithMany("AppointmentUsers")
                        .HasForeignKey("AppointmentUserUniqueName")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Arda.Common.Models.Kanban.WorkloadBacklog", "AppointmentWorkload")
                        .WithMany("WBAppointments")
                        .HasForeignKey("AppointmentWorkloadWBID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Arda.Common.Models.Kanban.File", b =>
                {
                    b.HasOne("Arda.Common.Models.Kanban.WorkloadBacklog", "WorkloadBacklog")
                        .WithMany("WBFiles")
                        .HasForeignKey("WorkloadBacklogWBID");
                });

            modelBuilder.Entity("Arda.Common.Models.Kanban.Metric", b =>
                {
                    b.HasOne("Arda.Common.Models.Kanban.FiscalYear", "FiscalYear")
                        .WithMany("Metrics")
                        .HasForeignKey("FiscalYearID");
                });

            modelBuilder.Entity("Arda.Common.Models.Kanban.WorkloadBacklog", b =>
                {
                    b.HasOne("Arda.Common.Models.Kanban.Activity", "WBActivity")
                        .WithMany("WBs")
                        .HasForeignKey("WBActivityActivityID");
                });

            modelBuilder.Entity("Arda.Common.Models.Kanban.WorkloadBacklogMetric", b =>
                {
                    b.HasOne("Arda.Common.Models.Kanban.Metric", "Metric")
                        .WithMany("WBMetrics")
                        .HasForeignKey("MetricID");

                    b.HasOne("Arda.Common.Models.Kanban.WorkloadBacklog", "WorkloadBacklog")
                        .WithMany("WBMetrics")
                        .HasForeignKey("WorkloadBacklogWBID");
                });

            modelBuilder.Entity("Arda.Common.Models.Kanban.WorkloadBacklogTechnology", b =>
                {
                    b.HasOne("Arda.Common.Models.Kanban.Technology", "Technology")
                        .WithMany("WBTechnologies")
                        .HasForeignKey("TechnologyID");

                    b.HasOne("Arda.Common.Models.Kanban.WorkloadBacklog", "WorkloadBacklog")
                        .WithMany("WBTechnologies")
                        .HasForeignKey("WorkloadBacklogWBID");
                });

            modelBuilder.Entity("Arda.Common.Models.Kanban.WorkloadBacklogUser", b =>
                {
                    b.HasOne("Arda.Common.Models.Kanban.User", "User")
                        .WithMany("WBUsers")
                        .HasForeignKey("UserUniqueName");

                    b.HasOne("Arda.Common.Models.Kanban.WorkloadBacklog", "WorkloadBacklog")
                        .WithMany("WBUsers")
                        .HasForeignKey("WorkloadBacklogWBID");
                });
        }
    }
}
