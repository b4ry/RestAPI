using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace PortfolioApplication.Migrations.Migrations
{
    public partial class RenamedIconUrlColumnToIconClassInTechnologyEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IconUrl",
                table: "Technologies",
                newName: "IconClass");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IconClass",
                table: "Technologies",
                newName: "IconUrl");
        }
    }
}
