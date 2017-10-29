using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using PortfolioApplication.Services.DatabaseContext;
using PortfolioApplication.Entities.Enums;

namespace PortfolioApplication.Migrations.Migrations
{
    [DbContext(typeof(PortfolioApplicationDbContext))]
    partial class DivingApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.2")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("PortfolioApplication.Entities.Entities.DivingGear", b =>
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

            modelBuilder.Entity("PortfolioApplication.Entities.Entities.DivingGearType", b =>
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

            modelBuilder.Entity("PortfolioApplication.Entities.Entities.ExperienceEntity", b =>
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

                    b.ToTable("Experiences");
                });

            modelBuilder.Entity("PortfolioApplication.Entities.Entities.JunctionEntities.ProjectTechnology", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("ProjectId");

                    b.Property<int>("TechnologyId");

                    b.HasKey("Id");

                    b.HasIndex("ProjectId");

                    b.HasIndex("TechnologyId");

                    b.ToTable("ProjectsTechnologies");
                });

            modelBuilder.Entity("PortfolioApplication.Entities.Entities.ProjectEntity", b =>
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

                    b.Property<int>("ProjectTypeId");

                    b.Property<DateTime>("StartTime");

                    b.HasKey("Id");

                    b.HasIndex("ExperienceId");

                    b.HasIndex("ProjectTypeId");

                    b.ToTable("Projects");
                });

            modelBuilder.Entity("PortfolioApplication.Entities.Entities.ProjectTypeEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<int>("ProjectTypeEnum");

                    b.HasKey("Id");

                    b.ToTable("ProjectTypes");
                });

            modelBuilder.Entity("PortfolioApplication.Entities.Entities.TechnologyEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<int>("TechnologyTypeId");

                    b.HasKey("Id");

                    b.HasIndex("TechnologyTypeId");

                    b.ToTable("Technologies");
                });

            modelBuilder.Entity("PortfolioApplication.Entities.Entities.TechnologyTypeEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<int>("TechnologyTypeEnum");

                    b.HasKey("Id");

                    b.ToTable("TechnologyTypes");
                });

            modelBuilder.Entity("PortfolioApplication.Entities.Entities.DivingGear", b =>
                {
                    b.HasOne("PortfolioApplication.Entities.Entities.DivingGearType", "DivingGearType")
                        .WithMany()
                        .HasForeignKey("DivingGearTypeId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("PortfolioApplication.Entities.Entities.JunctionEntities.ProjectTechnology", b =>
                {
                    b.HasOne("PortfolioApplication.Entities.Entities.ProjectEntity")
                        .WithMany("ProjectsTechnologies")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("PortfolioApplication.Entities.Entities.TechnologyEntity")
                        .WithMany("ProjectsTechnologies")
                        .HasForeignKey("TechnologyId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("PortfolioApplication.Entities.Entities.ProjectEntity", b =>
                {
                    b.HasOne("PortfolioApplication.Entities.Entities.ExperienceEntity")
                        .WithMany("Projects")
                        .HasForeignKey("ExperienceId");

                    b.HasOne("PortfolioApplication.Entities.Entities.ProjectTypeEntity", "ProjectType")
                        .WithMany()
                        .HasForeignKey("ProjectTypeId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("PortfolioApplication.Entities.Entities.TechnologyEntity", b =>
                {
                    b.HasOne("PortfolioApplication.Entities.Entities.TechnologyTypeEntity", "TechnologyType")
                        .WithMany()
                        .HasForeignKey("TechnologyTypeId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
