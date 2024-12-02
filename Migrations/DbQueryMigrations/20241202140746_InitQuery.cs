using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Teledock.Migrations.DbQueryMigrations
{
    /// <inheritdoc />
    public partial class InitQuery : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "клиенты",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Инн = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    имя = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    тип = table.Column<int>(type: "int", nullable: false),
                    датадобавления = table.Column<DateTime>(name: "дата добавления", type: "datetime(6)", nullable: false),
                    датаобновления = table.Column<DateTime>(name: "дата обновления", type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_клиенты", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "учредители",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Инн = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    фио = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    датадобавления = table.Column<DateTime>(name: "дата добавления", type: "datetime(6)", nullable: false),
                    датаобновления = table.Column<DateTime>(name: "дата обновления", type: "datetime(6)", nullable: true),
                    ClientId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_учредители", x => x.Id);
                    table.ForeignKey(
                        name: "FK_учредители_клиенты_ClientId",
                        column: x => x.ClientId,
                        principalTable: "клиенты",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_клиенты_Инн",
                table: "клиенты",
                column: "Инн",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_учредители_Инн",
                table: "учредители",
                column: "Инн",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_учредители_ClientId",
                table: "учредители",
                column: "ClientId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "учредители");

            migrationBuilder.DropTable(
                name: "клиенты");
        }
    }
}
