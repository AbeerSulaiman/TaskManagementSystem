using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace TaskManagementSystem.Migrations
{
    /// <inheritdoc />
    public partial class updatemodels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Members_TodoTasks_TodoTasksId",
                table: "Members");

            migrationBuilder.DropForeignKey(
                name: "FK_TaskLists_Members_MemberId",
                table: "TaskLists");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Members",
                table: "Members");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "TaskLists");

            migrationBuilder.DropColumn(
                name: "MemberId",
                table: "Members");

            migrationBuilder.DropColumn(
                name: "MemberName",
                table: "Members");

            migrationBuilder.RenameColumn(
                name: "TodoTasksId",
                table: "Members",
                newName: "TeamId");

            migrationBuilder.RenameColumn(
                name: "TaskId",
                table: "Members",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "MemberRole",
                table: "Members",
                newName: "Name");

            migrationBuilder.RenameIndex(
                name: "IX_Members_TodoTasksId",
                table: "Members",
                newName: "IX_Members_TeamId");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Members",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Members",
                table: "Members",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Teams",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teams", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Members_Teams_TeamId",
                table: "Members",
                column: "TeamId",
                principalTable: "Teams",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TaskLists_Members_MemberId",
                table: "TaskLists",
                column: "MemberId",
                principalTable: "Members",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Members_Teams_TeamId",
                table: "Members");

            migrationBuilder.DropForeignKey(
                name: "FK_TaskLists_Members_MemberId",
                table: "TaskLists");

            migrationBuilder.DropTable(
                name: "Teams");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Members",
                table: "Members");

            migrationBuilder.RenameColumn(
                name: "TeamId",
                table: "Members",
                newName: "TodoTasksId");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Members",
                newName: "MemberRole");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Members",
                newName: "TaskId");

            migrationBuilder.RenameIndex(
                name: "IX_Members_TeamId",
                table: "Members",
                newName: "IX_Members_TodoTasksId");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "TaskLists",
                type: "text",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "TaskId",
                table: "Members",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddColumn<int>(
                name: "MemberId",
                table: "Members",
                type: "integer",
                nullable: false,
                defaultValue: 0)
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddColumn<string>(
                name: "MemberName",
                table: "Members",
                type: "text",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Members",
                table: "Members",
                column: "MemberId");

            migrationBuilder.AddForeignKey(
                name: "FK_Members_TodoTasks_TodoTasksId",
                table: "Members",
                column: "TodoTasksId",
                principalTable: "TodoTasks",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TaskLists_Members_MemberId",
                table: "TaskLists",
                column: "MemberId",
                principalTable: "Members",
                principalColumn: "MemberId");
        }
    }
}
