#nullable disable

using Microsoft.EntityFrameworkCore.Migrations;

namespace Todo.Infrastructure.Migrations.Users;

/// <inheritdoc />
public partial class Init_Users : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.EnsureSchema(
            "users");

        migrationBuilder.CreateTable(
            "Users",
            schema: "users",
            columns: table => new
            {
                Id = table.Column<Guid>("uuid", nullable: false),
                FirstName = table.Column<string>("text", nullable: false),
                LastName = table.Column<string>("text", nullable: true),
                CreatedAtUtc = table.Column<DateTime>("timestamp with time zone", nullable: false)
            },
            constraints: table => { table.PrimaryKey("PK_Users", x => x.Id); });
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            "Users",
            "users");
    }
}