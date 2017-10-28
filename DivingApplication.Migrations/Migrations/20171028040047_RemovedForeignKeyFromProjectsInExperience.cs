using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DivingApplication.Migrations.Migrations
{
    public partial class RemovedForeignKeyFromProjectsInExperience : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Projects_Experiences_ProjectId",
                table: "Projects");

            migrationBuilder.RenameColumn(
                name: "ProjectId",
                table: "Projects",
                newName: "ExperienceId");

            migrationBuilder.RenameIndex(
                name: "IX_Projects_ProjectId",
                table: "Projects",
                newName: "IX_Projects_ExperienceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_Experiences_ExperienceId",
                table: "Projects",
                column: "ExperienceId",
                principalTable: "Experiences",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Projects_Experiences_ExperienceId",
                table: "Projects");

            migrationBuilder.RenameColumn(
                name: "ExperienceId",
                table: "Projects",
                newName: "ProjectId");

            migrationBuilder.RenameIndex(
                name: "IX_Projects_ExperienceId",
                table: "Projects",
                newName: "IX_Projects_ProjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_Experiences_ProjectId",
                table: "Projects",
                column: "ProjectId",
                principalTable: "Experiences",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
