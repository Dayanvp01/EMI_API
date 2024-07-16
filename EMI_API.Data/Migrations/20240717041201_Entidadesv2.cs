using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EMI_API.Data.Migrations
{
    /// <inheritdoc />
    public partial class Entidadesv2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PositionHistorys_Employees_EmployeeId",
                table: "PositionHistorys");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PositionHistorys",
                table: "PositionHistorys");

            migrationBuilder.RenameTable(
                name: "PositionHistorys",
                newName: "PositionsHistories");

            migrationBuilder.RenameIndex(
                name: "IX_PositionHistorys_EmployeeId",
                table: "PositionsHistories",
                newName: "IX_PositionsHistories_EmployeeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PositionsHistories",
                table: "PositionsHistories",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PositionsHistories_Employees_EmployeeId",
                table: "PositionsHistories",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PositionsHistories_Employees_EmployeeId",
                table: "PositionsHistories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PositionsHistories",
                table: "PositionsHistories");

            migrationBuilder.RenameTable(
                name: "PositionsHistories",
                newName: "PositionHistorys");

            migrationBuilder.RenameIndex(
                name: "IX_PositionsHistories_EmployeeId",
                table: "PositionHistorys",
                newName: "IX_PositionHistorys_EmployeeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PositionHistorys",
                table: "PositionHistorys",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PositionHistorys_Employees_EmployeeId",
                table: "PositionHistorys",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
