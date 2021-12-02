using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AdminBoard.Data.Migrations
{
    public partial class Added_Value_Set_Rule_Sensor_Entities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.CreateTable(
                name: "Set",
                columns: table => new
                {
                    SetID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Type = table.Column<int>(type: "int", nullable: false),
                    SensorID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Set", x => x.SetID);
                    table.ForeignKey(
                        name: "FK_Set_Sensors_SensorID",
                        column: x => x.SensorID,
                        principalTable: "Sensors",
                        principalColumn: "SensorID",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Value",
                columns: table => new
                {
                    ValueID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ValueName = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    XCoords = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    YCoords = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    SetID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Value", x => x.ValueID);
                    table.ForeignKey(
                        name: "FK_Value_Set_SetID",
                        column: x => x.SetID,
                        principalTable: "Set",
                        principalColumn: "SetID",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Rule",
                columns: table => new
                {
                    RuleID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    InputValue1ValueID = table.Column<int>(type: "int", nullable: true),
                    InputValue2ValueID = table.Column<int>(type: "int", nullable: true),
                    OutputValueValueID = table.Column<int>(type: "int", nullable: true),
                    Operator = table.Column<int>(type: "int", nullable: false),
                    SensorID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rule", x => x.RuleID);
                    table.ForeignKey(
                        name: "FK_Rule_Sensors_SensorID",
                        column: x => x.SensorID,
                        principalTable: "Sensors",
                        principalColumn: "SensorID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Rule_Value_InputValue1ValueID",
                        column: x => x.InputValue1ValueID,
                        principalTable: "Value",
                        principalColumn: "ValueID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Rule_Value_InputValue2ValueID",
                        column: x => x.InputValue2ValueID,
                        principalTable: "Value",
                        principalColumn: "ValueID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Rule_Value_OutputValueValueID",
                        column: x => x.OutputValueValueID,
                        principalTable: "Value",
                        principalColumn: "ValueID",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

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

            migrationBuilder.CreateIndex(
                name: "IX_Rule_SensorID",
                table: "Rule",
                column: "SensorID");

            migrationBuilder.CreateIndex(
                name: "IX_Set_SensorID",
                table: "Set",
                column: "SensorID");

            migrationBuilder.CreateIndex(
                name: "IX_Value_SetID",
                table: "Value",
                column: "SetID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Rule");

            migrationBuilder.DropTable(
                name: "Value");

            migrationBuilder.DropTable(
                name: "Set");

            migrationBuilder.DropTable(
                name: "Sensors");
        }
    }
}
