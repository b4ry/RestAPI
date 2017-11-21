using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace PortfolioApplication.Migrations.Migrations
{
    public partial class AddedIndexOnExperienceEntityColumnsCompanyNameAndPosition : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Experiences_CompanyName_Position",
                table: "Experiences",
                columns: new[] { "CompanyName", "Position" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Experiences_CompanyName_Position",
                table: "Experiences");
        }
    }
}
