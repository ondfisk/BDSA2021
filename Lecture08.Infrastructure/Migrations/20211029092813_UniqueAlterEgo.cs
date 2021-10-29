using Microsoft.EntityFrameworkCore.Migrations;

namespace Lecture08.Infrastructure.Migrations
{
    public partial class UniqueAlterEgo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Characters_AlterEgo",
                table: "Characters",
                column: "AlterEgo",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Characters_AlterEgo",
                table: "Characters");
        }
    }
}
