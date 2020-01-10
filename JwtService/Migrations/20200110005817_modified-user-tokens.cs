using Microsoft.EntityFrameworkCore.Migrations;

namespace JwtService.Migrations
{
    public partial class modifiedusertokens : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Tokens",
                table: "Tokens");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Tokens");

            migrationBuilder.AddColumn<long>(
                name: "ID",
                table: "Tokens",
                nullable: false,
                defaultValue: 0L)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<long>(
                name: "UserID",
                table: "Tokens",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tokens",
                table: "Tokens",
                column: "ID");

            migrationBuilder.CreateIndex(
                name: "IX_Tokens_Token",
                table: "Tokens",
                column: "Token",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tokens_UserID",
                table: "Tokens",
                column: "UserID");

            migrationBuilder.AddForeignKey(
                name: "FK_Tokens_Users_UserID",
                table: "Tokens",
                column: "UserID",
                principalTable: "Users",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tokens_Users_UserID",
                table: "Tokens");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Tokens",
                table: "Tokens");

            migrationBuilder.DropIndex(
                name: "IX_Tokens_Token",
                table: "Tokens");

            migrationBuilder.DropIndex(
                name: "IX_Tokens_UserID",
                table: "Tokens");

            migrationBuilder.DropColumn(
                name: "ID",
                table: "Tokens");

            migrationBuilder.DropColumn(
                name: "UserID",
                table: "Tokens");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Tokens",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tokens",
                table: "Tokens",
                column: "Token");
        }
    }
}
