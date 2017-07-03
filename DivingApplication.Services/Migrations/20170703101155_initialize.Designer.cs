using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using DivingApplication.Services.DatabaseContext;
using DivingApplication.Entities.Entity;

namespace DivingApplication.Services.Migrations
{
    [DbContext(typeof(DivingApplicationDbContext))]
    [Migration("20170703101155_initialize")]
    partial class initialize
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
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
        }
    }
}
