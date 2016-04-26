using System;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.Migrations;
using Arda.Kanban;

namespace Arda.Kanban.Migrations
{
    [DbContext(typeof(TaskDbContext))]
    [Migration("20160426170048_initial")]
    partial class initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0-rc1-16348")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Arda.Kanban.TaskItem", b =>
                {
                    b.Property<string>("Id");

                    b.Property<string>("Description");

                    b.Property<string>("Name");

                    b.Property<int>("State");

                    b.HasKey("Id");
                });
        }
    }
}
