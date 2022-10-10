using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyClothesCA.Infrastructure.Migrations
{
    public partial class Second : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Seasons",
                keyColumn: "SeasonId",
                keyValue: "133edefa-efd8-4e09-a978-f84d348a937a");

            migrationBuilder.DeleteData(
                table: "Seasons",
                keyColumn: "SeasonId",
                keyValue: "4fe18530-af92-4a1a-9e97-9df990b3c4b1");

            migrationBuilder.DeleteData(
                table: "Seasons",
                keyColumn: "SeasonId",
                keyValue: "ae959e74-8642-469e-ad27-832c6c7cef85");

            migrationBuilder.DeleteData(
                table: "Seasons",
                keyColumn: "SeasonId",
                keyValue: "e1950f0d-ad0e-4558-8773-a2bf16a4cf25");

            migrationBuilder.InsertData(
                table: "Seasons",
                columns: new[] { "SeasonId", "Name" },
                values: new object[,]
                {
                    { "0c84af58-67c4-41a4-81ef-fbd864f3f758", "Spring" },
                    { "41e43896-df44-443a-ad87-74781a3b52ed", "Summer" },
                    { "742f395f-1d93-45f0-af9c-92cb10082956", "Winter" },
                    { "f96622db-2708-43cc-99e3-3e8e7d80ae77", "Autumn " }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Seasons",
                keyColumn: "SeasonId",
                keyValue: "0c84af58-67c4-41a4-81ef-fbd864f3f758");

            migrationBuilder.DeleteData(
                table: "Seasons",
                keyColumn: "SeasonId",
                keyValue: "41e43896-df44-443a-ad87-74781a3b52ed");

            migrationBuilder.DeleteData(
                table: "Seasons",
                keyColumn: "SeasonId",
                keyValue: "742f395f-1d93-45f0-af9c-92cb10082956");

            migrationBuilder.DeleteData(
                table: "Seasons",
                keyColumn: "SeasonId",
                keyValue: "f96622db-2708-43cc-99e3-3e8e7d80ae77");

            migrationBuilder.InsertData(
                table: "Seasons",
                columns: new[] { "SeasonId", "Name" },
                values: new object[,]
                {
                    { "133edefa-efd8-4e09-a978-f84d348a937a", "Spring" },
                    { "4fe18530-af92-4a1a-9e97-9df990b3c4b1", "Autumn " },
                    { "ae959e74-8642-469e-ad27-832c6c7cef85", "Summer" },
                    { "e1950f0d-ad0e-4558-8773-a2bf16a4cf25", "Winter" }
                });
        }
    }
}
