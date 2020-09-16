using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Homework_Adform.DAL.Migrations
{
    public partial class Homework_AdformDALDBContextsHomeworkDBContextSeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "FirstName", "LastName", "Password", "Username" },
                values: new object[] { 1L, "Vinay", "Tripathi", "123", "vinay" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "FirstName", "LastName", "Password", "Username" },
                values: new object[] { 2L, "Ashish", "Tripathi", "123", "ashish" });

            migrationBuilder.InsertData(
                table: "Labels",
                columns: new[] { "Id", "CreatedBy", "CreatedDate", "Description", "UpdatedDate" },
                values: new object[] { 1L, 1L, new DateTime(2020, 9, 13, 19, 24, 22, 501, DateTimeKind.Utc).AddTicks(2574), "Label 1", null });

            migrationBuilder.InsertData(
                table: "TodoLists",
                columns: new[] { "Id", "CreatedBy", "CreatedDate", "Description", "LabelId", "UpdatedDate" },
                values: new object[] { 2L, 1L, new DateTime(2020, 9, 13, 19, 24, 22, 501, DateTimeKind.Utc).AddTicks(4996), "List 2", null, null });

            migrationBuilder.InsertData(
                table: "TodoLists",
                columns: new[] { "Id", "CreatedBy", "CreatedDate", "Description", "LabelId", "UpdatedDate" },
                values: new object[] { 3L, 2L, new DateTime(2020, 9, 13, 19, 24, 22, 501, DateTimeKind.Utc).AddTicks(5035), "List 3", null, null });

            migrationBuilder.InsertData(
                table: "TodoLists",
                columns: new[] { "Id", "CreatedBy", "CreatedDate", "Description", "LabelId", "UpdatedDate" },
                values: new object[] { 1L, 1L, new DateTime(2020, 9, 13, 19, 24, 22, 501, DateTimeKind.Utc).AddTicks(3932), "List 1", 1L, null });

            migrationBuilder.InsertData(
                table: "TodoItems",
                columns: new[] { "Id", "CreatedBy", "CreatedDate", "LabelId", "ListId", "Notes", "UpdatedDate" },
                values: new object[] { 1L, 1L, new DateTime(2020, 9, 14, 0, 54, 22, 501, DateTimeKind.Local).AddTicks(6928), 1L, 1L, "Item 1", null });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "TodoItems",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "TodoLists",
                keyColumn: "Id",
                keyValue: 2L);

            migrationBuilder.DeleteData(
                table: "TodoLists",
                keyColumn: "Id",
                keyValue: 3L);

            migrationBuilder.DeleteData(
                table: "TodoLists",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2L);

            migrationBuilder.DeleteData(
                table: "Labels",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1L);
        }
    }
}
