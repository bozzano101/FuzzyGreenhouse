using Microsoft.EntityFrameworkCore.Migrations;

namespace AdminBoard.Migrations.FuzzyGreenhouseDb
{
    public partial class AddedSetIDFieldToValueModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Value_Set_SetID",
                table: "Value");

            migrationBuilder.AlterColumn<int>(
                name: "SetID",
                table: "Value",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Value_Set_SetID",
                table: "Value",
                column: "SetID",
                principalTable: "Set",
                principalColumn: "SetID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Value_Set_SetID",
                table: "Value");

            migrationBuilder.AlterColumn<int>(
                name: "SetID",
                table: "Value",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Value_Set_SetID",
                table: "Value",
                column: "SetID",
                principalTable: "Set",
                principalColumn: "SetID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
