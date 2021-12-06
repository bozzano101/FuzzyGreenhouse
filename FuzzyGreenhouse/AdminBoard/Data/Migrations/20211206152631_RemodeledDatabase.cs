using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AdminBoard.Data.Migrations
{
    public partial class RemodeledDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rule_Sensors_SensorID",
                table: "Rule");

            migrationBuilder.DropForeignKey(
                name: "FK_Set_Sensors_SensorID",
                table: "Set");

            migrationBuilder.DropTable(
                name: "Sensors");

            migrationBuilder.DropIndex(
                name: "IX_Set_SensorID",
                table: "Set");

            migrationBuilder.DropIndex(
                name: "IX_Rule_SensorID",
                table: "Rule");

            migrationBuilder.DropColumn(
                name: "SensorID",
                table: "Set");

            migrationBuilder.DropColumn(
                name: "SensorID",
                table: "Rule");

            migrationBuilder.RenameColumn(
                name: "ValueName",
                table: "Value",
                newName: "Name");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Value",
                newName: "ValueName");

            migrationBuilder.AddColumn<int>(
                name: "SensorID",
                table: "Set",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SensorID",
                table: "Rule",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Sensors",
                columns: table => new
                {
                    SensorID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sensors", x => x.SensorID);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Set_SensorID",
                table: "Set",
                column: "SensorID");

            migrationBuilder.CreateIndex(
                name: "IX_Rule_SensorID",
                table: "Rule",
                column: "SensorID");

            migrationBuilder.AddForeignKey(
                name: "FK_Rule_Sensors_SensorID",
                table: "Rule",
                column: "SensorID",
                principalTable: "Sensors",
                principalColumn: "SensorID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Set_Sensors_SensorID",
                table: "Set",
                column: "SensorID",
                principalTable: "Sensors",
                principalColumn: "SensorID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
