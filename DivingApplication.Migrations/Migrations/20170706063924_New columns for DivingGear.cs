using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DivingApplication.Migrations.Migrations
{
    public partial class NewcolumnsforDivingGear : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "DivingGears",
                maxLength: 10,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 50);

            migrationBuilder.AddColumn<string>(
                name: "Brand",
                table: "DivingGears",
                maxLength: 10,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Model",
                table: "DivingGears",
                maxLength: 10,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Brand",
                table: "DivingGears");

            migrationBuilder.DropColumn(
                name: "Model",
                table: "DivingGears");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "DivingGears",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 10);

            migrationBuilder.CreateTable(
                name: "AccountSummaries",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AccountNumber = table.Column<string>(nullable: false),
                    Balance = table.Column<double>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Type = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountSummaries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AccountDetails",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AccountSummaryId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AccountDetails_AccountSummaries_AccountSummaryId",
                        column: x => x.AccountSummaryId,
                        principalTable: "AccountSummaries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AccountTransactions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AccountDetailId = table.Column<int>(nullable: true),
                    Amount = table.Column<double>(nullable: false),
                    Description = table.Column<string>(nullable: false),
                    TransactionDate = table.Column<DateTimeOffset>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountTransactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AccountTransactions_AccountDetails_AccountDetailId",
                        column: x => x.AccountDetailId,
                        principalTable: "AccountDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AccountDetails_AccountSummaryId",
                table: "AccountDetails",
                column: "AccountSummaryId");

            migrationBuilder.CreateIndex(
                name: "IX_AccountTransactions_AccountDetailId",
                table: "AccountTransactions",
                column: "AccountDetailId");
        }
    }
}
