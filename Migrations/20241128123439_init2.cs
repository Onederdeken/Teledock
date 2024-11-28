using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Teledock.Migrations
{
    /// <inheritdoc />
    public partial class init2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_учредители_киенты_ClientId",
                table: "учредители");

            migrationBuilder.DropPrimaryKey(
                name: "PK_киенты",
                table: "киенты");

            migrationBuilder.RenameTable(
                name: "киенты",
                newName: "клиенты");

            migrationBuilder.AddPrimaryKey(
                name: "PK_клиенты",
                table: "клиенты",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_учредители_клиенты_ClientId",
                table: "учредители",
                column: "ClientId",
                principalTable: "клиенты",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_учредители_клиенты_ClientId",
                table: "учредители");

            migrationBuilder.DropPrimaryKey(
                name: "PK_клиенты",
                table: "клиенты");

            migrationBuilder.RenameTable(
                name: "клиенты",
                newName: "киенты");

            migrationBuilder.AddPrimaryKey(
                name: "PK_киенты",
                table: "киенты",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_учредители_киенты_ClientId",
                table: "учредители",
                column: "ClientId",
                principalTable: "киенты",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
