using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AccessControlReader.Migrations
{
    /// <inheritdoc />
    public partial class ChangeTypes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ReadersItems",
                columns: table => new
                {
                    Reader_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MACaddr = table.Column<string>(type: "nvarchar(12)", maxLength: 12, nullable: true),
                    Tier = table.Column<short>(type: "smallint", nullable: false),
                    ToShow = table.Column<string>(type: "nvarchar(34)", maxLength: 34, nullable: true),
                    Comment = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    ErrorNo = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReadersItems", x => x.Reader_ID);
                });

            migrationBuilder.CreateTable(
                name: "UsersItems",
                columns: table => new
                {
                    User_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsersItems", x => x.User_ID);
                });

            migrationBuilder.CreateTable(
                name: "CardItems",
                columns: table => new
                {
                    Card_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Card_ID_number = table.Column<long>(type: "bigint", nullable: false),
                    User_ID = table.Column<int>(type: "int", nullable: false),
                    User_ID1 = table.Column<int>(type: "int", nullable: true),
                    Card_UID = table.Column<string>(type: "varchar(25)", nullable: true),
                    Tier = table.Column<short>(type: "smallint", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CardItems", x => x.Card_ID);
                    table.ForeignKey(
                        name: "FK_CardItems_UsersItems_User_ID1",
                        column: x => x.User_ID1,
                        principalTable: "UsersItems",
                        principalColumn: "User_ID");
                });

            migrationBuilder.CreateTable(
                name: "ReadingsItems",
                columns: table => new
                {
                    Reading_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Reader_ID = table.Column<int>(type: "int", nullable: false),
                    Reader_ID1 = table.Column<int>(type: "int", nullable: true),
                    User_ID = table.Column<int>(type: "int", nullable: true),
                    User_ID1 = table.Column<int>(type: "int", nullable: true),
                    Access = table.Column<bool>(type: "bit", nullable: false),
                    Data = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Card_ID_number = table.Column<decimal>(type: "decimal(20,0)", maxLength: 12, nullable: true),
                    Card_ID = table.Column<int>(type: "int", nullable: true),
                    Time = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReadingsItems", x => x.Reading_ID);
                    table.ForeignKey(
                        name: "FK_ReadingsItems_CardItems_Card_ID",
                        column: x => x.Card_ID,
                        principalTable: "CardItems",
                        principalColumn: "Card_ID");
                    table.ForeignKey(
                        name: "FK_ReadingsItems_ReadersItems_Reader_ID1",
                        column: x => x.Reader_ID1,
                        principalTable: "ReadersItems",
                        principalColumn: "Reader_ID");
                    table.ForeignKey(
                        name: "FK_ReadingsItems_UsersItems_User_ID1",
                        column: x => x.User_ID1,
                        principalTable: "UsersItems",
                        principalColumn: "User_ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_CardItems_User_ID1",
                table: "CardItems",
                column: "User_ID1");

            migrationBuilder.CreateIndex(
                name: "IX_ReadingsItems_Card_ID",
                table: "ReadingsItems",
                column: "Card_ID");

            migrationBuilder.CreateIndex(
                name: "IX_ReadingsItems_Reader_ID1",
                table: "ReadingsItems",
                column: "Reader_ID1");

            migrationBuilder.CreateIndex(
                name: "IX_ReadingsItems_User_ID1",
                table: "ReadingsItems",
                column: "User_ID1");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ReadingsItems");

            migrationBuilder.DropTable(
                name: "CardItems");

            migrationBuilder.DropTable(
                name: "ReadersItems");

            migrationBuilder.DropTable(
                name: "UsersItems");
        }
    }
}
