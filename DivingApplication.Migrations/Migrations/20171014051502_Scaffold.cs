using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DivingApplication.Migrations.Migrations
{
    public partial class Scaffold : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DivingGearTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    DivingGearTypeEnum = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DivingGearTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DivingGears",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Brand = table.Column<string>(maxLength: 10, nullable: true),
                    DivingGearTypeId = table.Column<Guid>(nullable: false),
                    Model = table.Column<string>(maxLength: 10, nullable: true),
                    Name = table.Column<string>(maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DivingGears", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DivingGears_DivingGearTypes_DivingGearTypeId",
                        column: x => x.DivingGearTypeId,
                        principalTable: "DivingGearTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DivingGears_DivingGearTypeId",
                table: "DivingGears",
                column: "DivingGearTypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DivingGears");

            migrationBuilder.DropTable(
                name: "DivingGearTypes");
        }
    }
}
