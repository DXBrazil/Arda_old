using System;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.Migrations;
using Arda.Permissions.Models;

namespace Arda.Permissions.Migrations
{
    [DbContext(typeof(PermissionsContext))]
    [Migration("20160524185922_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0-rc1-16386")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Arda.Permissions.Models.Module", b =>
                {
                    b.Property<int>("ModuleID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Endpoint")
                        .IsRequired();

                    b.Property<string>("ModuleName")
                        .IsRequired();

                    b.HasKey("ModuleID");

                    b.HasAnnotation("Relational:TableName", "Modules");
                });

            modelBuilder.Entity("Arda.Permissions.Models.Resource", b =>
                {
                    b.Property<int>("ResourceID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("ModuleID");

                    b.Property<string>("ResourceName")
                        .IsRequired();

                    b.HasKey("ResourceID");

                    b.HasAnnotation("Relational:TableName", "Resources");
                });

            modelBuilder.Entity("Arda.Permissions.Models.User", b =>
                {
                    b.Property<string>("UniqueName");

                    b.Property<int>("Status");

                    b.HasKey("UniqueName");

                    b.HasAnnotation("Relational:TableName", "Users");
                });

            modelBuilder.Entity("Arda.Permissions.Models.UsersPermission", b =>
                {
                    b.Property<int>("PermissionID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("ResourceID");

                    b.Property<string>("UniqueName");

                    b.HasKey("PermissionID");

                    b.HasAnnotation("Relational:TableName", "UsersPermissions");
                });

            modelBuilder.Entity("Arda.Permissions.Models.Resource", b =>
                {
                    b.HasOne("Arda.Permissions.Models.Module")
                        .WithMany()
                        .HasForeignKey("ModuleID");
                });

            modelBuilder.Entity("Arda.Permissions.Models.UsersPermission", b =>
                {
                    b.HasOne("Arda.Permissions.Models.Resource")
                        .WithMany()
                        .HasForeignKey("ResourceID");

                    b.HasOne("Arda.Permissions.Models.User")
                        .WithMany()
                        .HasForeignKey("UniqueName");
                });
        }
    }
}
