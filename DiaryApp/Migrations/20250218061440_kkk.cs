using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DiaryApp.Migrations
{
    /// <inheritdoc />
    public partial class kkk : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "DiaryEntries",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_DiaryEntries_UserId",
                table: "DiaryEntries",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_DiaryEntries_UserAccounts_UserId",
                table: "DiaryEntries",
                column: "UserId",
                principalTable: "UserAccounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DiaryEntries_UserAccounts_UserId",
                table: "DiaryEntries");

            migrationBuilder.DropIndex(
                name: "IX_DiaryEntries_UserId",
                table: "DiaryEntries");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "DiaryEntries");
        }
    }
}
