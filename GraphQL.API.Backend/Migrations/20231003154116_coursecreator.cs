﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GraphQL.API.Backend.Migrations
{
    /// <inheritdoc />
    public partial class coursecreator : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreatorId",
                table: "Courses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatorId",
                table: "Courses");
        }
    }
}
