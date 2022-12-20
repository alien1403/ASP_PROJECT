using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Taskmanager.Data.Migrations
{
    public partial class TaskFK : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_Projects_ProjectId",
                table: "Tasks");

            migrationBuilder.RenameColumn(
                name: "ProjectId",
                table: "Tasks",
                newName: "IdProject");

            migrationBuilder.RenameIndex(
                name: "IX_Tasks_ProjectId",
                table: "Tasks",
                newName: "IX_Tasks_IdProject");

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_Projects_IdProject",
                table: "Tasks",
                column: "IdProject",
                principalTable: "Projects",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_Projects_IdProject",
                table: "Tasks");

            migrationBuilder.RenameColumn(
                name: "IdProject",
                table: "Tasks",
                newName: "ProjectId");

            migrationBuilder.RenameIndex(
                name: "IX_Tasks_IdProject",
                table: "Tasks",
                newName: "IX_Tasks_ProjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_Projects_ProjectId",
                table: "Tasks",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id");
        }
    }
}
