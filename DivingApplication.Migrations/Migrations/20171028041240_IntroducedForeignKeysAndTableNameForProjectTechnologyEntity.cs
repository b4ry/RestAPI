using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DivingApplication.Migrations.Migrations
{
    public partial class IntroducedForeignKeysAndTableNameForProjectTechnologyEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectTechnology_Projects_ProjectId",
                table: "ProjectTechnology");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectTechnology_Technologies_TechnologyId",
                table: "ProjectTechnology");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProjectTechnology",
                table: "ProjectTechnology");

            migrationBuilder.RenameTable(
                name: "ProjectTechnology",
                newName: "ProjectsTechnologies");

            migrationBuilder.RenameIndex(
                name: "IX_ProjectTechnology_TechnologyId",
                table: "ProjectsTechnologies",
                newName: "IX_ProjectsTechnologies_TechnologyId");

            migrationBuilder.RenameIndex(
                name: "IX_ProjectTechnology_ProjectId",
                table: "ProjectsTechnologies",
                newName: "IX_ProjectsTechnologies_ProjectId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProjectsTechnologies",
                table: "ProjectsTechnologies",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectsTechnologies_Projects_ProjectId",
                table: "ProjectsTechnologies",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectsTechnologies_Technologies_TechnologyId",
                table: "ProjectsTechnologies",
                column: "TechnologyId",
                principalTable: "Technologies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectsTechnologies_Projects_ProjectId",
                table: "ProjectsTechnologies");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectsTechnologies_Technologies_TechnologyId",
                table: "ProjectsTechnologies");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProjectsTechnologies",
                table: "ProjectsTechnologies");

            migrationBuilder.RenameTable(
                name: "ProjectsTechnologies",
                newName: "ProjectTechnology");

            migrationBuilder.RenameIndex(
                name: "IX_ProjectsTechnologies_TechnologyId",
                table: "ProjectTechnology",
                newName: "IX_ProjectTechnology_TechnologyId");

            migrationBuilder.RenameIndex(
                name: "IX_ProjectsTechnologies_ProjectId",
                table: "ProjectTechnology",
                newName: "IX_ProjectTechnology_ProjectId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProjectTechnology",
                table: "ProjectTechnology",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectTechnology_Projects_ProjectId",
                table: "ProjectTechnology",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectTechnology_Technologies_TechnologyId",
                table: "ProjectTechnology",
                column: "TechnologyId",
                principalTable: "Technologies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
