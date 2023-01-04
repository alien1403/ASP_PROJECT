using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Taskmanager.Data.Migrations
{
    public partial class Ye : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_Task_Members_Task_MemberIdMember_Task_MemberIdTask",
                table: "Tasks");

            migrationBuilder.DropIndex(
                name: "IX_Tasks_Task_MemberIdMember_Task_MemberIdTask",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "Task_MemberIdMember",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "Task_MemberIdTask",
                table: "Tasks");

            migrationBuilder.CreateTable(
                name: "TaskTask_Member",
                columns: table => new
                {
                    TasksId = table.Column<int>(type: "int", nullable: false),
                    TaskMembersIdMember = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TaskMembersIdTask = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskTask_Member", x => new { x.TasksId, x.TaskMembersIdMember, x.TaskMembersIdTask });
                    table.ForeignKey(
                        name: "FK_TaskTask_Member_Task_Members_TaskMembersIdMember_TaskMembersIdTask",
                        columns: x => new { x.TaskMembersIdMember, x.TaskMembersIdTask },
                        principalTable: "Task_Members",
                        principalColumns: new[] { "IdMember", "IdTask" },
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TaskTask_Member_Tasks_TasksId",
                        column: x => x.TasksId,
                        principalTable: "Tasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TaskTask_Member_TaskMembersIdMember_TaskMembersIdTask",
                table: "TaskTask_Member",
                columns: new[] { "TaskMembersIdMember", "TaskMembersIdTask" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TaskTask_Member");

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

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_Task_MemberIdMember_Task_MemberIdTask",
                table: "Tasks",
                columns: new[] { "Task_MemberIdMember", "Task_MemberIdTask" });

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_Task_Members_Task_MemberIdMember_Task_MemberIdTask",
                table: "Tasks",
                columns: new[] { "Task_MemberIdMember", "Task_MemberIdTask" },
                principalTable: "Task_Members",
                principalColumns: new[] { "IdMember", "IdTask" });
        }
    }
}
