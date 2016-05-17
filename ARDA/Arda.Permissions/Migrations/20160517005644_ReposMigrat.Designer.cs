using System;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.Migrations;
using Arda.Permissions.Models;

namespace Arda.Permissions.Migrations
{
    [DbContext(typeof(UserPermissionsContext))]
    [Migration("20160517005644_ReposMigrat")]
    partial class ReposMigrat
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0-rc1-16386")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Arda.Permissions.Models.UsersPermissions", b =>
                {
                    b.Property<string>("UniqueName");

                    b.Property<string>("PermissionsSerialized")
                        .IsRequired();

                    b.HasKey("UniqueName");

                    b.HasAnnotation("Relational:TableName", "UsersPermissions");
                });
        }
    }
}
