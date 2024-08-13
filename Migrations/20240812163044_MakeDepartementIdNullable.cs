﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TE_trsprt_remake.Migrations
{
    /// <inheritdoc />
    public partial class MakeDepartementIdNullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Departements_DepartementId",
                table: "Users");

            migrationBuilder.AlterColumn<int>(
                name: "DepartementId",
                table: "Users",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

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

            migrationBuilder.AlterColumn<int>(
                name: "DepartementId",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Departements_DepartementId",
                table: "Users",
                column: "DepartementId",
                principalTable: "Departements",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
