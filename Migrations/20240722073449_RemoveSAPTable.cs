using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TE_trsprt_remake.Migrations
{
    /// <inheritdoc />
    public partial class RemoveSAPTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Plants_SAPs_SAPId1",
                table: "Plants");

            migrationBuilder.DropTable(
                name: "SAPs");

            migrationBuilder.DropIndex(
                name: "IX_Plants_SAPId1",
                table: "Plants");

            migrationBuilder.DropColumn(
                name: "SAPId1",
                table: "Plants");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SAPId1",
                table: "Plants",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "SAPs",
                columns: table => new
                {
                    SAPId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SAPName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SAPs", x => x.SAPId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Plants_SAPId1",
                table: "Plants",
                column: "SAPId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Plants_SAPs_SAPId1",
                table: "Plants",
                column: "SAPId1",
                principalTable: "SAPs",
                principalColumn: "SAPId");
        }
    }
}
