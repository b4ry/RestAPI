using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using DivingApplicationAPI.DatabaseContext;
using DivingApplicationAPI.Entity;

namespace DivingApplicationAPI.Migrations
{
    [DbContext(typeof(DivingApplicationDbContext))]
    [Migration("20170503122438_init")]
    partial class init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.1")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("DivingApplication.Entity.AccountDetail", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccountSummaryId");

                    b.HasKey("Id");

                    b.HasIndex("AccountSummaryId");

                    b.ToTable("AccountDetails");
                });

            modelBuilder.Entity("DivingApplication.Entity.AccountSummary", b =>
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

            modelBuilder.Entity("DivingApplication.Entity.AccountTransaction", b =>
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

            modelBuilder.Entity("DivingApplication.Entity.AccountDetail", b =>
                {
                    b.HasOne("DivingApplication.Entity.AccountSummary", "AccountSummary")
                        .WithMany()
                        .HasForeignKey("AccountSummaryId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DivingApplication.Entity.AccountTransaction", b =>
                {
                    b.HasOne("DivingApplication.Entity.AccountDetail")
                        .WithMany("AccountTransactions")
                        .HasForeignKey("AccountDetailId");
                });
        }
    }
}
