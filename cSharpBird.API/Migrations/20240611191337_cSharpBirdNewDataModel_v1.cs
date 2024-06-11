using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace cSharpBird.API.Migrations
{
    /// <inheritdoc />
    public partial class cSharpBirdNewDataModel_v1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Birds_Checklists_checklistID",
                table: "Birds");

            migrationBuilder.DropColumn(
                name: "cNotes",
                table: "Checklists");

            migrationBuilder.DropColumn(
                name: "distance",
                table: "Checklists");

            migrationBuilder.DropColumn(
                name: "duration",
                table: "Checklists");

            migrationBuilder.DropColumn(
                name: "stationary",
                table: "Checklists");

            migrationBuilder.DropColumn(
                name: "bNotes",
                table: "Birds");

            migrationBuilder.DropColumn(
                name: "bandCode",
                table: "Birds");

            migrationBuilder.DropColumn(
                name: "bbc",
                table: "Birds");

            migrationBuilder.RenameColumn(
                name: "checklistID",
                table: "Birds",
                newName: "checklistId");

            migrationBuilder.RenameColumn(
                name: "randomBird",
                table: "Birds",
                newName: "randomBirdId");

            migrationBuilder.RenameIndex(
                name: "IX_Birds_checklistID",
                table: "Birds",
                newName: "IX_Birds_checklistId");

            migrationBuilder.AlterColumn<Guid>(
                name: "checklistId",
                table: "Birds",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Birds_Checklists_checklistId",
                table: "Birds",
                column: "checklistId",
                principalTable: "Checklists",
                principalColumn: "checklistID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Birds_Checklists_checklistId",
                table: "Birds");

            migrationBuilder.RenameColumn(
                name: "checklistId",
                table: "Birds",
                newName: "checklistID");

            migrationBuilder.RenameColumn(
                name: "randomBirdId",
                table: "Birds",
                newName: "randomBird");

            migrationBuilder.RenameIndex(
                name: "IX_Birds_checklistId",
                table: "Birds",
                newName: "IX_Birds_checklistID");

            migrationBuilder.AddColumn<string>(
                name: "cNotes",
                table: "Checklists",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<float>(
                name: "distance",
                table: "Checklists",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<int>(
                name: "duration",
                table: "Checklists",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "stationary",
                table: "Checklists",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<Guid>(
                name: "checklistID",
                table: "Birds",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<string>(
                name: "bNotes",
                table: "Birds",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "bandCode",
                table: "Birds",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "bbc",
                table: "Birds",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Birds_Checklists_checklistID",
                table: "Birds",
                column: "checklistID",
                principalTable: "Checklists",
                principalColumn: "checklistID");
        }
    }
}
