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
    partial class DivingApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.2")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("DivingApplication.Entities.Entity.AccountDetail", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccountSummaryId");

                    b.HasKey("Id");

                    b.HasIndex("AccountSummaryId");

                    b.ToTable("AccountDetails");
                });

            modelBuilder.Entity("DivingApplication.Entities.Entity.AccountSummary", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AccountNumber")
                        .IsRequired();

                    b.Property<double>("Balance");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<int>("Type");

                    b.HasKey("Id");

                    b.ToTable("AccountSummaries");
                });

            modelBuilder.Entity("DivingApplication.Entities.Entity.AccountTransaction", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("AccountDetailId");

                    b.Property<double>("Amount");

                    b.Property<string>("Description")
                        .IsRequired();

                    b.Property<DateTimeOffset>("TransactionDate");

                    b.HasKey("Id");

                    b.HasIndex("AccountDetailId");

                    b.ToTable("AccountTransactions");
                });

            modelBuilder.Entity("DivingApplication.Entities.Entity.DivingGear", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("DivingGearTypeId");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50);

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

            modelBuilder.Entity("DivingApplication.Entities.Entity.AccountDetail", b =>
                {
                    b.HasOne("DivingApplication.Entities.Entity.AccountSummary", "AccountSummary")
                        .WithMany()
                        .HasForeignKey("AccountSummaryId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DivingApplication.Entities.Entity.AccountTransaction", b =>
                {
                    b.HasOne("DivingApplication.Entities.Entity.AccountDetail")
                        .WithMany("AccountTransactions")
                        .HasForeignKey("AccountDetailId");
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
