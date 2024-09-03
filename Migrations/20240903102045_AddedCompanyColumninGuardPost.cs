using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TE_trsprt_remake.Migrations
{
    /// <inheritdoc />
    public partial class AddedCompanyColumninGuardPost : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Company",
                table: "GuardPosts",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Company",
                table: "GuardPosts");
        }
    }
}
