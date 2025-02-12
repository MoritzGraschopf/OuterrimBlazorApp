using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace View.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AIRCRAFT_SPECIFICATIONS",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Structure = table.Column<int>(type: "INTEGER", nullable: false),
                    FuelTankCapacity = table.Column<int>(type: "INTEGER", nullable: false),
                    MinSpeed = table.Column<int>(type: "INTEGER", nullable: false),
                    MaxSpeed = table.Column<int>(type: "INTEGER", nullable: false),
                    MaxAltitude = table.Column<int>(type: "INTEGER", nullable: false),
                    SpezificationCode = table.Column<string>(type: "TEXT", maxLength: 45, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AIRCRAFT_SPECIFICATIONS", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CRIME_SYNDICATES",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 120, nullable: false),
                    Location = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CRIME_SYNDICATES", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MERCENARIES",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FirstName = table.Column<string>(type: "TEXT", nullable: false),
                    LastName = table.Column<string>(type: "TEXT", nullable: false),
                    BodySkills = table.Column<int>(type: "INTEGER", nullable: false),
                    CombatSkills = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MERCENARIES", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AIRCRAFTS",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Fuel = table.Column<int>(type: "INTEGER", nullable: false),
                    Speed = table.Column<int>(type: "INTEGER", nullable: false),
                    Altitude = table.Column<int>(type: "INTEGER", nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    SpecificationId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AIRCRAFTS", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AIRCRAFTS_AIRCRAFT_SPECIFICATIONS_SpecificationId",
                        column: x => x.SpecificationId,
                        principalTable: "AIRCRAFT_SPECIFICATIONS",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MERCENARY_REPUTATIONS",
                columns: table => new
                {
                    SyndicateId = table.Column<int>(type: "INTEGER", nullable: false),
                    MercenaryId = table.Column<int>(type: "INTEGER", nullable: false),
                    ReputationChange = table.Column<string>(type: "TEXT", maxLength: 45, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MERCENARY_REPUTATIONS", x => new { x.SyndicateId, x.MercenaryId });
                    table.ForeignKey(
                        name: "FK_MERCENARY_REPUTATIONS_CRIME_SYNDICATES_SyndicateId",
                        column: x => x.SyndicateId,
                        principalTable: "CRIME_SYNDICATES",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MERCENARY_REPUTATIONS_MERCENARIES_MercenaryId",
                        column: x => x.MercenaryId,
                        principalTable: "MERCENARIES",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AIRCRAFT_CREW_JT",
                columns: table => new
                {
                    AircraftId = table.Column<int>(type: "INTEGER", nullable: false),
                    MercenaryId = table.Column<int>(type: "INTEGER", nullable: false),
                    JoinedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AIRCRAFT_CREW_JT", x => new { x.AircraftId, x.MercenaryId });
                    table.ForeignKey(
                        name: "FK_AIRCRAFT_CREW_JT_AIRCRAFTS_AircraftId",
                        column: x => x.AircraftId,
                        principalTable: "AIRCRAFTS",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AIRCRAFT_CREW_JT_MERCENARIES_MercenaryId",
                        column: x => x.MercenaryId,
                        principalTable: "MERCENARIES",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "COMPARTMENTS",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    AircraftId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_COMPARTMENTS", x => x.Id);
                    table.ForeignKey(
                        name: "FK_COMPARTMENTS_AIRCRAFTS_AircraftId",
                        column: x => x.AircraftId,
                        principalTable: "AIRCRAFTS",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MACHINERIES",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Label = table.Column<string>(type: "TEXT", maxLength: 45, nullable: false),
                    Function = table.Column<string>(type: "TEXT", nullable: false),
                    CompartmentId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MACHINERIES", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MACHINERIES_COMPARTMENTS_CompartmentId",
                        column: x => x.CompartmentId,
                        principalTable: "COMPARTMENTS",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_AIRCRAFT_CREW_JT_MercenaryId",
                table: "AIRCRAFT_CREW_JT",
                column: "MercenaryId");

            migrationBuilder.CreateIndex(
                name: "IX_AIRCRAFTS_SpecificationId",
                table: "AIRCRAFTS",
                column: "SpecificationId");

            migrationBuilder.CreateIndex(
                name: "IX_COMPARTMENTS_AircraftId",
                table: "COMPARTMENTS",
                column: "AircraftId");

            migrationBuilder.CreateIndex(
                name: "IX_MACHINERIES_CompartmentId",
                table: "MACHINERIES",
                column: "CompartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_MERCENARY_REPUTATIONS_MercenaryId",
                table: "MERCENARY_REPUTATIONS",
                column: "MercenaryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AIRCRAFT_CREW_JT");

            migrationBuilder.DropTable(
                name: "ENERGY_SYSTEMS");

            migrationBuilder.DropTable(
                name: "ENVIROMENTAL_SYSTEMS");

            migrationBuilder.DropTable(
                name: "MERCENARY_REPUTATIONS");

            migrationBuilder.DropTable(
                name: "WEAPONS");

            migrationBuilder.DropTable(
                name: "CRIME_SYNDICATES");

            migrationBuilder.DropTable(
                name: "MERCENARIES");

            migrationBuilder.DropTable(
                name: "MACHINERIES");

            migrationBuilder.DropTable(
                name: "COMPARTMENTS");

            migrationBuilder.DropTable(
                name: "AIRCRAFTS");

            migrationBuilder.DropTable(
                name: "AIRCRAFT_SPECIFICATIONS");
        }
    }
}
