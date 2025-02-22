﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DiaryApp.Migrations
{
    /// <inheritdoc />
    public partial class seeddata : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "DiaryEntries",
                columns: new[] { "Id", "Content", "Created", "Title" },
                values: new object[,]
                {
                    { 1, "Went Hiking with Joe", new DateTime(2025, 2, 14, 13, 35, 27, 793, DateTimeKind.Local).AddTicks(8705), "Went Hiking" },
                    { 2, "Went shopping with Joe", new DateTime(2025, 2, 14, 13, 35, 27, 793, DateTimeKind.Local).AddTicks(9015), "Wwent Shopping" },
                    { 3, "Went diving with Joe", new DateTime(2025, 2, 14, 13, 35, 27, 793, DateTimeKind.Local).AddTicks(9017), "Went Diving" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "DiaryEntries",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "DiaryEntries",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "DiaryEntries",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
