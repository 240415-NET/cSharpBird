using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace cSharpBird.API.Migrations
{
    /// <inheritdoc />
    public partial class cSharpBirdWeb_v0 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Checklists",
                columns: table => new
                {
                    checklistID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    userId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    locationName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    checklistDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    distance = table.Column<float>(type: "real", nullable: false),
                    duration = table.Column<int>(type: "int", nullable: false),
                    stationary = table.Column<bool>(type: "bit", nullable: false),
                    cNotes = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Checklists", x => x.checklistID);
                });

            migrationBuilder.CreateTable(
                name: "Salts",
                columns: table => new
                {
                    userId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    salt = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Salts", x => x.userId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    userId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    userName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    displayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    hashedPW = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.userId);
                });

            migrationBuilder.CreateTable(
                name: "Birds",
                columns: table => new
                {
                    bandCode = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    speciesName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    numSeen = table.Column<int>(type: "int", nullable: false),
                    bbc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    bNotes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    checklistID = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Birds", x => x.bandCode);
                    table.ForeignKey(
                        name: "FK_Birds_Checklists_checklistID",
                        column: x => x.checklistID,
                        principalTable: "Checklists",
                        principalColumn: "checklistID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Birds_checklistID",
                table: "Birds",
                column: "checklistID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Birds");

            migrationBuilder.DropTable(
                name: "Salts");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Checklists");
        }
    }
}
