using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using CrankBankAPI.DatabaseContext;
using CrankBankAPI.Entity;

namespace CrankBankAPI.Migrations
{
    [DbContext(typeof(CrankBankDbContext))]
    [Migration("20170503122438_init")]
    partial class init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.1")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("CrankBank.Entity.AccountDetail", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccountSummaryId");

                    b.HasKey("Id");

                    b.HasIndex("AccountSummaryId");

                    b.ToTable("AccountDetails");
                });

            modelBuilder.Entity("CrankBank.Entity.AccountSummary", b =>
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

            modelBuilder.Entity("CrankBank.Entity.AccountTransaction", b =>
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

            modelBuilder.Entity("CrankBank.Entity.AccountDetail", b =>
                {
                    b.HasOne("CrankBank.Entity.AccountSummary", "AccountSummary")
                        .WithMany()
                        .HasForeignKey("AccountSummaryId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("CrankBank.Entity.AccountTransaction", b =>
                {
                    b.HasOne("CrankBank.Entity.AccountDetail")
                        .WithMany("AccountTransactions")
                        .HasForeignKey("AccountDetailId");
                });
        }
    }
}
