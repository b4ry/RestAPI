using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using DivingApplication.Services.DatabaseContext;
using DivingApplication.Entities.Enum;

namespace DivingApplication.Migrations.Migrations
{
    [DbContext(typeof(DivingApplicationDbContext))]
    [Migration("20171014051502_Scaffold")]
    partial class Scaffold
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.2")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("DivingApplication.Entities.Entity.DivingGear", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Brand")
                        .HasMaxLength(10);

                    b.Property<Guid>("DivingGearTypeId");

                    b.Property<string>("Model")
                        .HasMaxLength(10);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(10);

                    b.HasKey("Id");

                    b.HasIndex("DivingGearTypeId");

                    b.ToTable("DivingGears");
                });

            modelBuilder.Entity("DivingApplication.Entities.Entity.DivingGearType", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("DivingGearTypeEnum");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.ToTable("DivingGearTypes");
                });

            modelBuilder.Entity("DivingApplication.Entities.Entity.DivingGear", b =>
                {
                    b.HasOne("DivingApplication.Entities.Entity.DivingGearType", "DivingGearType")
                        .WithMany()
                        .HasForeignKey("DivingGearTypeId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
