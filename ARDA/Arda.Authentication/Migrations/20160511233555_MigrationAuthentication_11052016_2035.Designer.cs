using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Arda.Authentication.Models;

namespace Arda.Authentication.Migrations
{
    [DbContext(typeof(AuthenticationContext))]
    [Migration("20160511233555_MigrationAuthentication_11052016_2035")]
    partial class MigrationAuthentication_11052016_2035
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0-rc1-16348")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Arda.Authentication.Models.User", b =>
                {
                    b.Property<Guid>("UserID");

                    b.Property<string>("Avatar");

                    b.Property<string>("Email")
                        .IsRequired();

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 200);

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 200);

                    b.Property<byte>("Status");

                    b.Property<string>("Token")
                        .IsRequired();

                    b.HasKey("UserID");

                    b.HasAnnotation("Relational:TableName", "User");
                });
        }
    }
}
