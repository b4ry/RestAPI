using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace PortfolioApplication.Migrations.Migrations
{
    public partial class DeleteCascadeOnProjectsExperience : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Projects_Experiences_ExperienceId",
                table: "Projects");

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_Experiences_ExperienceId",
                table: "Projects",
                column: "ExperienceId",
                principalTable: "Experiences",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Projects_Experiences_ExperienceId",
                table: "Projects");

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_Experiences_ExperienceId",
                table: "Projects",
                column: "ExperienceId",
                principalTable: "Experiences",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
