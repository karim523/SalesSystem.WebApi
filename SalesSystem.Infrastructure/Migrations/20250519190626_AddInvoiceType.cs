﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SalesSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddInvoiceType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "Invoices",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Type",
                table: "Invoices");
        }
    }
}
