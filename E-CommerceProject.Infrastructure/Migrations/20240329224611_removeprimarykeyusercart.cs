using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_CommerceProject.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class removeprimarykeyusercart : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_UserCarts",
                table: "UserCarts");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "UserCarts",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserCarts",
                table: "UserCarts",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_UserCarts_UserId",
                table: "UserCarts",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_UserCarts",
                table: "UserCarts");

            migrationBuilder.DropIndex(
                name: "IX_UserCarts_UserId",
                table: "UserCarts");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "UserCarts");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserCarts",
                table: "UserCarts",
                columns: new[] { "UserId", "ProductId" });
        }
    }
}
