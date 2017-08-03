using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TheNewsWebsite.Migrations
{
    public partial class TheNewsWebsite : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NumPost",
                table: "Authors");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "NumPost",
                table: "Authors",
                nullable: false,
                defaultValue: 0);
        }
    }
}
