using Microsoft.EntityFrameworkCore.Migrations;

namespace AdminBoard.Migrations.FuzzyGreenhouseDb
{
    public partial class AddedForeignKeyIdsInRuleModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rule_Value_InputValue1ValueID",
                table: "Rule");

            migrationBuilder.DropForeignKey(
                name: "FK_Rule_Value_InputValue2ValueID",
                table: "Rule");

            migrationBuilder.DropForeignKey(
                name: "FK_Rule_Value_OutputValueValueID",
                table: "Rule");

            migrationBuilder.DropIndex(
                name: "IX_Rule_InputValue1ValueID",
                table: "Rule");

            migrationBuilder.DropIndex(
                name: "IX_Rule_InputValue2ValueID",
                table: "Rule");

            migrationBuilder.DropIndex(
                name: "IX_Rule_OutputValueValueID",
                table: "Rule");

            migrationBuilder.DropColumn(
                name: "InputValue1ValueID",
                table: "Rule");

            migrationBuilder.DropColumn(
                name: "InputValue2ValueID",
                table: "Rule");

            migrationBuilder.DropColumn(
                name: "OutputValueValueID",
                table: "Rule");

            migrationBuilder.AddColumn<int>(
                name: "InputValue1ID",
                table: "Rule",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "InputValue2ID",
                table: "Rule",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "OutputValueID",
                table: "Rule",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Rule_InputValue1ID",
                table: "Rule",
                column: "InputValue1ID");

            migrationBuilder.CreateIndex(
                name: "IX_Rule_InputValue2ID",
                table: "Rule",
                column: "InputValue2ID");

            migrationBuilder.CreateIndex(
                name: "IX_Rule_OutputValueID",
                table: "Rule",
                column: "OutputValueID");

            migrationBuilder.AddForeignKey(
                name: "FK_Rule_Value_InputValue1ID",
                table: "Rule",
                column: "InputValue1ID",
                principalTable: "Value",
                principalColumn: "ValueID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Rule_Value_InputValue2ID",
                table: "Rule",
                column: "InputValue2ID",
                principalTable: "Value",
                principalColumn: "ValueID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Rule_Value_OutputValueID",
                table: "Rule",
                column: "OutputValueID",
                principalTable: "Value",
                principalColumn: "ValueID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rule_Value_InputValue1ID",
                table: "Rule");

            migrationBuilder.DropForeignKey(
                name: "FK_Rule_Value_InputValue2ID",
                table: "Rule");

            migrationBuilder.DropForeignKey(
                name: "FK_Rule_Value_OutputValueID",
                table: "Rule");

            migrationBuilder.DropIndex(
                name: "IX_Rule_InputValue1ID",
                table: "Rule");

            migrationBuilder.DropIndex(
                name: "IX_Rule_InputValue2ID",
                table: "Rule");

            migrationBuilder.DropIndex(
                name: "IX_Rule_OutputValueID",
                table: "Rule");

            migrationBuilder.DropColumn(
                name: "InputValue1ID",
                table: "Rule");

            migrationBuilder.DropColumn(
                name: "InputValue2ID",
                table: "Rule");

            migrationBuilder.DropColumn(
                name: "OutputValueID",
                table: "Rule");

            migrationBuilder.AddColumn<int>(
                name: "InputValue1ValueID",
                table: "Rule",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "InputValue2ValueID",
                table: "Rule",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OutputValueValueID",
                table: "Rule",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Rule_InputValue1ValueID",
                table: "Rule",
                column: "InputValue1ValueID");

            migrationBuilder.CreateIndex(
                name: "IX_Rule_InputValue2ValueID",
                table: "Rule",
                column: "InputValue2ValueID");

            migrationBuilder.CreateIndex(
                name: "IX_Rule_OutputValueValueID",
                table: "Rule",
                column: "OutputValueValueID");

            migrationBuilder.AddForeignKey(
                name: "FK_Rule_Value_InputValue1ValueID",
                table: "Rule",
                column: "InputValue1ValueID",
                principalTable: "Value",
                principalColumn: "ValueID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Rule_Value_InputValue2ValueID",
                table: "Rule",
                column: "InputValue2ValueID",
                principalTable: "Value",
                principalColumn: "ValueID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Rule_Value_OutputValueValueID",
                table: "Rule",
                column: "OutputValueValueID",
                principalTable: "Value",
                principalColumn: "ValueID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
