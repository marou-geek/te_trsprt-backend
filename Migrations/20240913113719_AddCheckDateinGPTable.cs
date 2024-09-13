using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TE_trsprt_remake.Migrations
{
    /// <inheritdoc />
    public partial class AddCheckDateinGPTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CheckDate",
                table: "GuardPosts",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CheckDate",
                table: "GuardPosts");
        }
    }
}
