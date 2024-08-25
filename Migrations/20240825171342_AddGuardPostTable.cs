using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TE_trsprt_remake.Migrations
{
    /// <inheritdoc />
    public partial class AddGuardPostTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GuardPosts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RequestId = table.Column<int>(type: "int", nullable: false),
                    PredKms = table.Column<float>(type: "real", nullable: false),
                    NbrPersons = table.Column<int>(type: "int", nullable: false),
                    FuelLevel = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RegCard = table.Column<bool>(type: "bit", nullable: false),
                    Insurance = table.Column<bool>(type: "bit", nullable: false),
                    CarHealthCert = table.Column<bool>(type: "bit", nullable: false),
                    Vignette = table.Column<bool>(type: "bit", nullable: false),
                    FuelCard = table.Column<bool>(type: "bit", nullable: false),
                    Accessories = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExCondition = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IntCondition = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MechCondition = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GuardPosts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GuardPosts_Requests_RequestId",
                        column: x => x.RequestId,
                        principalTable: "Requests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GuardPosts_RequestId",
                table: "GuardPosts",
                column: "RequestId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GuardPosts");
        }
    }
}
