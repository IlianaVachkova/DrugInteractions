using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace DrugInteractions.Data.Migrations
{
    public partial class RemoveColumnPharmaceuticalForm : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PharmaceuticalForm",
                table: "Drugs");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AspNetUsers",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PharmaceuticalForm",
                table: "Drugs",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AspNetUsers",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 100);
        }
    }
}
