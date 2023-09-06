using Microsoft.EntityFrameworkCore.Migrations;

namespace AdminBoard.Migrations.FuzzyGreenhouseDb
{
    public partial class Addedmanytomanyrelationshipbetweensetandsubsystem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SetSubsystem",
                columns: table => new
                {
                    SetsSetID = table.Column<int>(type: "int", nullable: false),
                    SubsystemsSubsystemID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SetSubsystem", x => new { x.SetsSetID, x.SubsystemsSubsystemID });
                    table.ForeignKey(
                        name: "FK_SetSubsystem_Set_SetsSetID",
                        column: x => x.SetsSetID,
                        principalTable: "Set",
                        principalColumn: "SetID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SetSubsystem_Subsystem_SubsystemsSubsystemID",
                        column: x => x.SubsystemsSubsystemID,
                        principalTable: "Subsystem",
                        principalColumn: "SubsystemID",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_SetSubsystem_SubsystemsSubsystemID",
                table: "SetSubsystem",
                column: "SubsystemsSubsystemID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SetSubsystem");
        }
    }
}
