using System;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.Migrations;
using Arda.Kanban.Models;

namespace Arda.Kanban.Migrations
{
    [DbContext(typeof(KanbanContext))]
    [Migration("20160524012712_ArdaMigration_23052016_2224")]
    partial class ArdaMigration_23052016_2224
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
        }
    }
}
