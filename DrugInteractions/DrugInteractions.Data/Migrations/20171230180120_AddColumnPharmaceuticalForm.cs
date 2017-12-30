using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace DrugInteractions.Data.Migrations
{
    public partial class AddColumnPharmaceuticalForm : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PharmaceuticalForm",
                table: "Drugs",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PharmaceuticalForm",
                table: "Drugs");
        }
    }
}
