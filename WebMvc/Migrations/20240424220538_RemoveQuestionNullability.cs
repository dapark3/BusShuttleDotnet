using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebMvc.Migrations
{
    /// <inheritdoc />
    public partial class RemoveQuestionNullability : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Entries_Buses_BusId",
                table: "Entries");

            migrationBuilder.DropForeignKey(
                name: "FK_Entries_Drivers_DriverId",
                table: "Entries");

            migrationBuilder.DropForeignKey(
                name: "FK_Entries_Loops_LoopId",
                table: "Entries");

            migrationBuilder.DropForeignKey(
                name: "FK_Entries_Stops_StopId",
                table: "Entries");

            migrationBuilder.AlterColumn<int>(
                name: "StopId",
                table: "Entries",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "LoopId",
                table: "Entries",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "DriverId",
                table: "Entries",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "BusId",
                table: "Entries",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Entries_Buses_BusId",
                table: "Entries",
                column: "BusId",
                principalTable: "Buses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Entries_Drivers_DriverId",
                table: "Entries",
                column: "DriverId",
                principalTable: "Drivers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Entries_Loops_LoopId",
                table: "Entries",
                column: "LoopId",
                principalTable: "Loops",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Entries_Stops_StopId",
                table: "Entries",
                column: "StopId",
                principalTable: "Stops",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Entries_Buses_BusId",
                table: "Entries");

            migrationBuilder.DropForeignKey(
                name: "FK_Entries_Drivers_DriverId",
                table: "Entries");

            migrationBuilder.DropForeignKey(
                name: "FK_Entries_Loops_LoopId",
                table: "Entries");

            migrationBuilder.DropForeignKey(
                name: "FK_Entries_Stops_StopId",
                table: "Entries");

            migrationBuilder.AlterColumn<int>(
                name: "StopId",
                table: "Entries",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<int>(
                name: "LoopId",
                table: "Entries",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<int>(
                name: "DriverId",
                table: "Entries",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<int>(
                name: "BusId",
                table: "Entries",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddForeignKey(
                name: "FK_Entries_Buses_BusId",
                table: "Entries",
                column: "BusId",
                principalTable: "Buses",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Entries_Drivers_DriverId",
                table: "Entries",
                column: "DriverId",
                principalTable: "Drivers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Entries_Loops_LoopId",
                table: "Entries",
                column: "LoopId",
                principalTable: "Loops",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Entries_Stops_StopId",
                table: "Entries",
                column: "StopId",
                principalTable: "Stops",
                principalColumn: "Id");
        }
    }
}
