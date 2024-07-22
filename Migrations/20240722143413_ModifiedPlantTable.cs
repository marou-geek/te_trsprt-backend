using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TE_trsprt_remake.Migrations
{
    /// <inheritdoc />
    public partial class ModifiedPlantTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Plants_Buildings_BuildingId",
                table: "Plants");

            migrationBuilder.DropTable(
                name: "Buildings");

            migrationBuilder.DropIndex(
                name: "IX_Plants_BuildingId",
                table: "Plants");

            migrationBuilder.AlterColumn<string>(
                name: "BuildingId",
                table: "Plants",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "BuildingId",
                table: "Plants",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateTable(
                name: "Buildings",
                columns: table => new
                {
                    BuildingId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BuildingName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Buildings", x => x.BuildingId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Plants_BuildingId",
                table: "Plants",
                column: "BuildingId");

            migrationBuilder.AddForeignKey(
                name: "FK_Plants_Buildings_BuildingId",
                table: "Plants",
                column: "BuildingId",
                principalTable: "Buildings",
                principalColumn: "BuildingId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
