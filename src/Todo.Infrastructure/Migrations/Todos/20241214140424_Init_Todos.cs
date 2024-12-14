#nullable disable

using Microsoft.EntityFrameworkCore.Migrations;

namespace Todo.Infrastructure.Migrations.Todos;

/// <inheritdoc />
public partial class Init_Todos : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.EnsureSchema(
            "todos");

        migrationBuilder.CreateTable(
            "TodoGroups",
            schema: "todos",
            columns: table => new
            {
                Id = table.Column<Guid>("uuid", nullable: false),
                GroupTitle = table.Column<string>("text", nullable: false),
                CreateAtUtc = table.Column<DateTime>("timestamp with time zone", nullable: false)
            },
            constraints: table => { table.PrimaryKey("PK_TodoGroups", x => x.Id); });

        migrationBuilder.CreateTable(
            "Todos",
            schema: "todos",
            columns: table => new
            {
                Id = table.Column<Guid>("uuid", nullable: false),
                Title = table.Column<string>("text", nullable: false),
                Description = table.Column<string>("text", nullable: true),
                IsCompleted = table.Column<bool>("boolean", nullable: false),
                UserId = table.Column<Guid>("uuid", nullable: false),
                CreatedAtUtc = table.Column<DateTime>("timestamp with time zone", nullable: false),
                TodoGroupId = table.Column<Guid>("uuid", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Todos", x => x.Id);
                table.ForeignKey(
                    "FK_Todos_TodoGroups_TodoGroupId",
                    x => x.TodoGroupId,
                    principalSchema: "todos",
                    principalTable: "TodoGroups",
                    principalColumn: "Id");
            });

        migrationBuilder.CreateIndex(
            "IX_Todos_TodoGroupId",
            schema: "todos",
            table: "Todos",
            column: "TodoGroupId");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            "Todos",
            "todos");

        migrationBuilder.DropTable(
            "TodoGroups",
            "todos");
    }
}