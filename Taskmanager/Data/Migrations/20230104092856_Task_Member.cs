using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Taskmanager.Data.Migrations
{
    public partial class Task_Member : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Task_MemberIdMember",
                table: "Tasks",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Task_MemberIdTask",
                table: "Tasks",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Task_MemberIdMember",
                table: "Members",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Task_MemberIdTask",
                table: "Members",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Task_Member",
                columns: table => new
                {
                    IdMember = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IdTask = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Task_Member", x => new { x.IdMember, x.IdTask });
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_Task_MemberIdMember_Task_MemberIdTask",
                table: "Tasks",
                columns: new[] { "Task_MemberIdMember", "Task_MemberIdTask" });

            migrationBuilder.CreateIndex(
                name: "IX_Members_Task_MemberIdMember_Task_MemberIdTask",
                table: "Members",
                columns: new[] { "Task_MemberIdMember", "Task_MemberIdTask" });

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Members_Task_Member_Task_MemberIdMember_Task_MemberIdTask",
                table: "Members");

            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_Task_Member_Task_MemberIdMember_Task_MemberIdTask",
                table: "Tasks");

            migrationBuilder.DropTable(
                name: "Task_Member");

            migrationBuilder.DropIndex(
                name: "IX_Tasks_Task_MemberIdMember_Task_MemberIdTask",
                table: "Tasks");

            migrationBuilder.DropIndex(
                name: "IX_Members_Task_MemberIdMember_Task_MemberIdTask",
                table: "Members");

            migrationBuilder.DropColumn(
                name: "Task_MemberIdMember",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "Task_MemberIdTask",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "Task_MemberIdMember",
                table: "Members");

            migrationBuilder.DropColumn(
                name: "Task_MemberIdTask",
                table: "Members");
        }
    }
}
