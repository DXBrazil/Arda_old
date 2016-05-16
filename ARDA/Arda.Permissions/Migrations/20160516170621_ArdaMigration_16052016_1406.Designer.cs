using System;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.Migrations;
using Arda.Permissions.Models;

namespace Arda.Permissions.Migrations
{
    [DbContext(typeof(PermissionsContext))]
    [Migration("20160516170621_ArdaMigration_16052016_1406")]
    partial class ArdaMigration_16052016_1406
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0-rc1-16386")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Arda.Permissions.Models.UserProperties", b =>
                {
                    b.Property<string>("UniqueName");

                    b.Property<string>("AuthCode");

                    b.Property<string>("UserPropertiesUniqueName");

                    b.HasKey("UniqueName");

                    b.HasAnnotation("Relational:TableName", "UsersProperties");
                });

            modelBuilder.Entity("Arda.Permissions.Models.UserProperties", b =>
                {
                    b.HasOne("Arda.Permissions.Models.UserProperties")
                        .WithMany()
                        .HasForeignKey("UserPropertiesUniqueName");
                });
        }
    }
}
