using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace View.Migrations
{
    /// <inheritdoc />
    public partial class changedProperty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SpezificationCode",
                table: "AIRCRAFT_SPECIFICATIONS",
                newName: "SpecificationCode");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SpecificationCode",
                table: "AIRCRAFT_SPECIFICATIONS",
                newName: "SpezificationCode");
        }
    }
}
