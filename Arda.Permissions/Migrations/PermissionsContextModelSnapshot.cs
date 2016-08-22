using System;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.Migrations;
using Arda.Kanban.Models;

namespace Arda.Permissions.Migrations
{
    [DbContext(typeof(PermissionsContext))]
    partial class PermissionsContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0-rc1-16348")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Arda.Common.Models.Permissions.Module", b =>
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

            modelBuilder.Entity("Arda.Common.Models.Permissions.Resource", b =>
                {
                    b.Property<int>("ResourceID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Category")
                        .IsRequired();

                    b.Property<int>("CategorySequence");

                    b.Property<string>("DisplayName")
                        .IsRequired();

                    b.Property<int>("ModuleID");

                    b.Property<string>("ResourceName")
                        .IsRequired();

                    b.Property<int>("ResourceSequence");

                    b.HasKey("ResourceID");

                    b.HasAnnotation("Relational:TableName", "Resources");
                });

            modelBuilder.Entity("Arda.Common.Models.Permissions.User", b =>
                {
                    b.Property<string>("UniqueName");

                    b.Property<string>("GivenName");

                    b.Property<string>("JobTitle");

                    b.Property<string>("ManagerUniqueName");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<string>("PhotoBase64");

                    b.Property<int>("Status");

                    b.Property<string>("Surname");

                    b.HasKey("UniqueName");

                    b.HasAnnotation("Relational:TableName", "Users");
                });

            modelBuilder.Entity("Arda.Common.Models.Permissions.UsersPermission", b =>
                {
                    b.Property<int>("PermissionID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("ResourceID");

                    b.Property<string>("UniqueName");

                    b.HasKey("PermissionID");

                    b.HasAnnotation("Relational:TableName", "UsersPermissions");
                });

            modelBuilder.Entity("Arda.Common.Models.Permissions.Resource", b =>
                {
                    b.HasOne("Arda.Common.Models.Permissions.Module")
                        .WithMany()
                        .HasForeignKey("ModuleID");
                });

            modelBuilder.Entity("Arda.Common.Models.Permissions.UsersPermission", b =>
                {
                    b.HasOne("Arda.Common.Models.Permissions.Resource")
                        .WithMany()
                        .HasForeignKey("ResourceID");

                    b.HasOne("Arda.Common.Models.Permissions.User")
                        .WithMany()
                        .HasForeignKey("UniqueName");
                });
        }
    }
}
