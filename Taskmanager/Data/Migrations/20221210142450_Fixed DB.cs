using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Taskmanager.Data.Migrations
{
    public partial class FixedDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Projects_ProjectId",
                table: "Comments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Members",
                table: "Members");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Members");

            migrationBuilder.RenameColumn(
                name: "ProjectId",
                table: "Comments",
                newName: "TaskId");

            migrationBuilder.RenameIndex(
                name: "IX_Comments_ProjectId",
                table: "Comments",
                newName: "IX_Comments_TaskId");

            migrationBuilder.AlterColumn<string>(
                name: "IdAdmin",
                table: "Teams",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "IdAdmin",
                table: "Projects",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "IdTeam",
                table: "Members",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "IdMember",
                table: "Members",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Members",
                table: "Members",
                columns: new[] { "IdMember", "IdTeam" });

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Tasks_TaskId",
                table: "Comments",
                column: "TaskId",
                principalTable: "Tasks",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Tasks_TaskId",
                table: "Comments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Members",
                table: "Members");

            migrationBuilder.RenameColumn(
                name: "TaskId",
                table: "Comments",
                newName: "ProjectId");

            migrationBuilder.RenameIndex(
                name: "IX_Comments_TaskId",
                table: "Comments",
                newName: "IX_Comments_ProjectId");

            migrationBuilder.AlterColumn<int>(
                name: "IdAdmin",
                table: "Teams",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "IdAdmin",
                table: "Projects",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "IdTeam",
                table: "Members",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "IdMember",
                table: "Members",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Members",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Members",
                table: "Members",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Projects_ProjectId",
                table: "Comments",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id");
        }
    }
}
