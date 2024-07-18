using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TE_trsprt_remake.Migrations
{
    /// <inheritdoc />
    public partial class FKsConfiguration1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DepartementId",
                table: "Users",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_DepartementId",
                table: "Users",
                column: "DepartementId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Departements_DepartementId",
                table: "Users",
                column: "DepartementId",
                principalTable: "Departements",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Departements_DepartementId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_DepartementId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "DepartementId",
                table: "Users");
        }
    }
}
