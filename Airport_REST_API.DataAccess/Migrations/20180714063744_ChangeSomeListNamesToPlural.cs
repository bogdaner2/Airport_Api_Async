using Microsoft.EntityFrameworkCore.Migrations;

namespace Airport_REST_API.DataAccess.Migrations
{
    public partial class ChangeSomeListNamesToPlural : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Aircraft_AircraftType_TypeId",
                table: "Aircraft");

            migrationBuilder.DropForeignKey(
                name: "FK_Departures_Aircraft_AircraftId",
                table: "Departures");

            migrationBuilder.DropForeignKey(
                name: "FK_Stewardess_Crews_CrewId",
                table: "Stewardess");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Stewardess",
                table: "Stewardess");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AircraftType",
                table: "AircraftType");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Aircraft",
                table: "Aircraft");

            migrationBuilder.RenameTable(
                name: "Stewardess",
                newName: "Stewardesses");

            migrationBuilder.RenameTable(
                name: "AircraftType",
                newName: "AircraftTypes");

            migrationBuilder.RenameTable(
                name: "Aircraft",
                newName: "Aircrafts");

            migrationBuilder.RenameIndex(
                name: "IX_Stewardess_CrewId",
                table: "Stewardesses",
                newName: "IX_Stewardesses_CrewId");

            migrationBuilder.RenameIndex(
                name: "IX_Aircraft_TypeId",
                table: "Aircrafts",
                newName: "IX_Aircrafts_TypeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Stewardesses",
                table: "Stewardesses",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AircraftTypes",
                table: "AircraftTypes",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Aircrafts",
                table: "Aircrafts",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Aircrafts_AircraftTypes_TypeId",
                table: "Aircrafts",
                column: "TypeId",
                principalTable: "AircraftTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Departures_Aircrafts_AircraftId",
                table: "Departures",
                column: "AircraftId",
                principalTable: "Aircrafts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Stewardesses_Crews_CrewId",
                table: "Stewardesses",
                column: "CrewId",
                principalTable: "Crews",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Aircrafts_AircraftTypes_TypeId",
                table: "Aircrafts");

            migrationBuilder.DropForeignKey(
                name: "FK_Departures_Aircrafts_AircraftId",
                table: "Departures");

            migrationBuilder.DropForeignKey(
                name: "FK_Stewardesses_Crews_CrewId",
                table: "Stewardesses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Stewardesses",
                table: "Stewardesses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AircraftTypes",
                table: "AircraftTypes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Aircrafts",
                table: "Aircrafts");

            migrationBuilder.RenameTable(
                name: "Stewardesses",
                newName: "Stewardess");

            migrationBuilder.RenameTable(
                name: "AircraftTypes",
                newName: "AircraftType");

            migrationBuilder.RenameTable(
                name: "Aircrafts",
                newName: "Aircraft");

            migrationBuilder.RenameIndex(
                name: "IX_Stewardesses_CrewId",
                table: "Stewardess",
                newName: "IX_Stewardess_CrewId");

            migrationBuilder.RenameIndex(
                name: "IX_Aircrafts_TypeId",
                table: "Aircraft",
                newName: "IX_Aircraft_TypeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Stewardess",
                table: "Stewardess",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AircraftType",
                table: "AircraftType",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Aircraft",
                table: "Aircraft",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Aircraft_AircraftType_TypeId",
                table: "Aircraft",
                column: "TypeId",
                principalTable: "AircraftType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Departures_Aircraft_AircraftId",
                table: "Departures",
                column: "AircraftId",
                principalTable: "Aircraft",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Stewardess_Crews_CrewId",
                table: "Stewardess",
                column: "CrewId",
                principalTable: "Crews",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
