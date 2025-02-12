using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace View.Migrations
{
    /// <inheritdoc />
    public partial class addedInheritance : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ENERGY_SYSTEMS");

            migrationBuilder.DropTable(
                name: "ENVIROMENTAL_SYSTEMS");

            migrationBuilder.DropTable(
                name: "WEAPONS");

            migrationBuilder.AddColumn<string>(
                name: "MachineryType",
                table: "MACHINERIES",
                type: "TEXT",
                maxLength: 21,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MachineryType",
                table: "MACHINERIES");

            migrationBuilder.CreateTable(
                name: "ENERGY_SYSTEMS",
                columns: table => new
                {
                    MachineryId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ENERGY_SYSTEMS", x => x.MachineryId);
                    table.ForeignKey(
                        name: "FK_ENERGY_SYSTEMS_MACHINERIES_MachineryId",
                        column: x => x.MachineryId,
                        principalTable: "MACHINERIES",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ENVIROMENTAL_SYSTEMS",
                columns: table => new
                {
                    MachineryId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ENVIROMENTAL_SYSTEMS", x => x.MachineryId);
                    table.ForeignKey(
                        name: "FK_ENVIROMENTAL_SYSTEMS_MACHINERIES_MachineryId",
                        column: x => x.MachineryId,
                        principalTable: "MACHINERIES",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WEAPONS",
                columns: table => new
                {
                    MachineryId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WEAPONS", x => x.MachineryId);
                    table.ForeignKey(
                        name: "FK_WEAPONS_MACHINERIES_MachineryId",
                        column: x => x.MachineryId,
                        principalTable: "MACHINERIES",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }
    }
}
