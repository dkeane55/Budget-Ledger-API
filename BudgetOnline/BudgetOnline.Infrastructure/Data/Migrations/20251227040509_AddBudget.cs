using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BudgetOnline.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddBudget : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("3493639e-8408-4530-b35b-6ff783ca7fc6"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("9fcc1059-3d1d-4e48-a40b-9ebc5a6517b1"));

            migrationBuilder.AddColumn<decimal>(
                name: "Budget",
                table: "Categories",
                type: "TEXT",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Budget", "Name" },
                values: new object[,]
                {
                    { new Guid("5a37242f-ac76-4a55-b997-42470f0479fb"), 100m, "Food" },
                    { new Guid("80ae648f-add0-4127-8d8b-ddaa1a459a4a"), 500m, "Rent" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("5a37242f-ac76-4a55-b997-42470f0479fb"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("80ae648f-add0-4127-8d8b-ddaa1a459a4a"));

            migrationBuilder.DropColumn(
                name: "Budget",
                table: "Categories");

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("3493639e-8408-4530-b35b-6ff783ca7fc6"), "Food" },
                    { new Guid("9fcc1059-3d1d-4e48-a40b-9ebc5a6517b1"), "Rent" }
                });
        }
    }
}
