using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace cSharpBird.API.Migrations
{
    /// <inheritdoc />
    public partial class cSharpBird_V2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Birds",
                table: "Birds");

            migrationBuilder.AlterColumn<string>(
                name: "bandCode",
                table: "Birds",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<Guid>(
                name: "randomBird",
                table: "Birds",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_Birds",
                table: "Birds",
                column: "randomBird");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Birds",
                table: "Birds");

            migrationBuilder.DropColumn(
                name: "randomBird",
                table: "Birds");

            migrationBuilder.AlterColumn<string>(
                name: "bandCode",
                table: "Birds",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Birds",
                table: "Birds",
                column: "bandCode");
        }
    }
}
