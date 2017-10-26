using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using DivingApplication.Services.DatabaseContext;
using DivingApplication.Entities.Enums;

namespace DivingApplication.Migrations.Migrations
{
    [DbContext(typeof(DivingApplicationDbContext))]
    [Migration("20171026093326_CoreEntitiesAdded")]
    partial class CoreEntitiesAdded
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.2")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("DivingApplication.Entities.Entities.DivingGear", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Brand")
                        .HasMaxLength(10);

                    b.Property<int>("DivingGearTypeId");

                    b.Property<string>("Model")
                        .HasMaxLength(10);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(10);

                    b.HasKey("Id");

                    b.HasIndex("DivingGearTypeId");

                    b.ToTable("DivingGears");
                });

            modelBuilder.Entity("DivingApplication.Entities.Entities.DivingGearType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("DivingGearTypeEnum");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.ToTable("DivingGearTypes");
                });

            modelBuilder.Entity("DivingApplication.Entities.Entities.Experience", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CompanyName")
                        .IsRequired()
                        .HasMaxLength(10);

                    b.Property<string>("Position")
                        .IsRequired()
                        .HasMaxLength(10);

                    b.HasKey("Id");

                    b.ToTable("Experience");
                });

            modelBuilder.Entity("DivingApplication.Entities.Entities.Project", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description")
                        .IsRequired();

                    b.Property<DateTime>("EndTime");

                    b.Property<int?>("ExperienceId");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.Property<DateTime>("StartTime");

                    b.HasKey("Id");

                    b.HasIndex("ExperienceId");

                    b.ToTable("Projects");
                });

            modelBuilder.Entity("DivingApplication.Entities.Entities.Technology", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.Property<int?>("ProjectId");

                    b.HasKey("Id");

                    b.HasIndex("ProjectId");

                    b.ToTable("Technologies");
                });

            modelBuilder.Entity("DivingApplication.Entities.Entities.DivingGear", b =>
                {
                    b.HasOne("DivingApplication.Entities.Entities.DivingGearType", "DivingGearType")
                        .WithMany()
                        .HasForeignKey("DivingGearTypeId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DivingApplication.Entities.Entities.Project", b =>
                {
                    b.HasOne("DivingApplication.Entities.Entities.Experience")
                        .WithMany("Projects")
                        .HasForeignKey("ExperienceId");
                });

            modelBuilder.Entity("DivingApplication.Entities.Entities.Technology", b =>
                {
                    b.HasOne("DivingApplication.Entities.Entities.Project")
                        .WithMany("Technologies")
                        .HasForeignKey("ProjectId");
                });
        }
    }
}
