using Microsoft.EntityFrameworkCore.Migrations;

namespace AdminBoard.Migrations.FuzzyGreenhouseDb
{
    public partial class AddedRefToSubsystemInSet : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
          /*  migrationBuilder.AddColumn<int>(
                name: "SubsystemID",
                table: "Set",
                type: "int",
                nullable: false,
                defaultValue: 0);*/

         /*   migrationBuilder.CreateIndex(
                name: "IX_Set_SubsystemID",
                table: "Set",
                column: "SubsystemID");*/

            migrationBuilder.AddForeignKey(
                name: "FK_Set_Subsystem_SubsystemID",
                table: "Set",
                column: "SubsystemID",
                principalTable: "Subsystem",
                principalColumn: "SubsystemID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Set_Subsystem_SubsystemID",
                table: "Set");

            migrationBuilder.DropIndex(
                name: "IX_Set_SubsystemID",
                table: "Set");

            migrationBuilder.DropColumn(
                name: "SubsystemID",
                table: "Set");
        }
    }
}
