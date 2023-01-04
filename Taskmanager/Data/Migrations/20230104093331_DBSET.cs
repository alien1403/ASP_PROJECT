using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Taskmanager.Data.Migrations
{
    public partial class DBSET : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Members_Task_Member_Task_MemberIdMember_Task_MemberIdTask",
                table: "Members");

            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_Task_Member_Task_MemberIdMember_Task_MemberIdTask",
                table: "Tasks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Task_Member",
                table: "Task_Member");

            migrationBuilder.RenameTable(
                name: "Task_Member",
                newName: "Task_Members");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Task_Members",
                table: "Task_Members",
                columns: new[] { "IdMember", "IdTask" });

            migrationBuilder.AddForeignKey(
                name: "FK_Members_Task_Members_Task_MemberIdMember_Task_MemberIdTask",
                table: "Members",
                columns: new[] { "Task_MemberIdMember", "Task_MemberIdTask" },
                principalTable: "Task_Members",
                principalColumns: new[] { "IdMember", "IdTask" });

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_Task_Members_Task_MemberIdMember_Task_MemberIdTask",
                table: "Tasks",
                columns: new[] { "Task_MemberIdMember", "Task_MemberIdTask" },
                principalTable: "Task_Members",
                principalColumns: new[] { "IdMember", "IdTask" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Members_Task_Members_Task_MemberIdMember_Task_MemberIdTask",
                table: "Members");

            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_Task_Members_Task_MemberIdMember_Task_MemberIdTask",
                table: "Tasks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Task_Members",
                table: "Task_Members");

            migrationBuilder.RenameTable(
                name: "Task_Members",
                newName: "Task_Member");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Task_Member",
                table: "Task_Member",
                columns: new[] { "IdMember", "IdTask" });

            migrationBuilder.AddForeignKey(
                name: "FK_Members_Task_Member_Task_MemberIdMember_Task_MemberIdTask",
                table: "Members",
                columns: new[] { "Task_MemberIdMember", "Task_MemberIdTask" },
                principalTable: "Task_Member",
                principalColumns: new[] { "IdMember", "IdTask" });

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_Task_Member_Task_MemberIdMember_Task_MemberIdTask",
                table: "Tasks",
                columns: new[] { "Task_MemberIdMember", "Task_MemberIdTask" },
                principalTable: "Task_Member",
                principalColumns: new[] { "IdMember", "IdTask" });
        }
    }
}
