using Microsoft.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Arda.Kanban.Models
{
    public class KanbanContext : DbContext
    {
        public DbSet<FiscalYear> FiscalYears { get; set; }

        public DbSet<Metric> Metrics { get; set; }

        //public DbSet<Technology> Technologies { get; set; }

        //public DbSet<Workload> Workloads { get; set; }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    // Metrics
        //    modelBuilder.Entity<MetricsByWorkload>()
        //        .HasKey(t => new { t.MetricID, t.WorkloadID });

        //    modelBuilder.Entity<MetricsByWorkload>()
        //        .HasOne(pt => pt.Metric)
        //        .WithMany(p => p.MetricsByWorkloads)
        //        .HasForeignKey(pt => pt.MetricID);

        //    modelBuilder.Entity<MetricsByWorkload>()
        //        .HasOne(pt => pt.Workload)
        //        .WithMany(t => t.MetricsByWorkloads)
        //        .HasForeignKey(pt => pt.WorkloadID);

        //    // Technologies
        //    modelBuilder.Entity<TechnologiesByWorkload>()
        //        .HasKey(t => new { t.TechnologyID, t.WorkloadID });

        //    modelBuilder.Entity<TechnologiesByWorkload>()
        //        .HasOne(pt => pt.Technology)
        //        .WithMany(p => p.TechnologiesByWorkloads)
        //        .HasForeignKey(pt => pt.TechnologyID);

        //    modelBuilder.Entity<TechnologiesByWorkload>()
        //        .HasOne(pt => pt.Workload)
        //        .WithMany(t => t.TechnologiesByWorkloads)
        //        .HasForeignKey(pt => pt.WorkloadID);
        //}
    }
}
