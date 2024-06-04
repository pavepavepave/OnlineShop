using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OnlineShop.Db.Migrations
{
    public partial class EditDefaultGoods : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("0122481d-10e3-4b58-89ee-c72269d5c95e"),
                columns: new[] { "Cost", "Description", "Name" },
                values: new object[] { 10000m, "х/м 25х30 2022г.", "В минуты отдыха" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("0bc0cbd7-de40-46bd-8133-59c8a35b2761"),
                columns: new[] { "Cost", "Description", "Name" },
                values: new object[] { 17500m, "Холст. Масло. 30х40 2023 г.", "Взломщик" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("21b68580-8f57-45a4-a415-1ccfa97337c3"),
                columns: new[] { "Cost", "Description", "Name" },
                values: new object[] { 15000m, "Холст. Масло. 30х40 2021г.", "Голландский натюрморт" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("37acd775-ee3a-4e6d-83c2-3458adcbf87f"),
                columns: new[] { "Cost", "Description", "Name" },
                values: new object[] { 15000m, "Холст. Масло. 30х40.", "Ландыши" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("ac80a1ac-4c7b-4662-8aac-f14a5a90b957"),
                columns: new[] { "Cost", "Description", "Name" },
                values: new object[] { 15000m, "Холст. Масло. 30х40 см.", "Закат" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("c60de567-a236-4f59-bc76-4aa1109545d6"),
                columns: new[] { "Cost", "Description", "Name" },
                values: new object[] { 25000m, "Копия. Х/м 30х40 2021г.", "Олива в Гефсиманском саду" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("0122481d-10e3-4b58-89ee-c72269d5c95e"),
                columns: new[] { "Cost", "Description", "Name" },
                values: new object[] { 810m, "Краски акварельные для рисования 24 цвета.", "Краска акварель" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("0bc0cbd7-de40-46bd-8133-59c8a35b2761"),
                columns: new[] { "Cost", "Description", "Name" },
                values: new object[] { 910m, "Набор меловой пастели 48 цв.", "Набор меловой пастели" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("21b68580-8f57-45a4-a415-1ccfa97337c3"),
                columns: new[] { "Cost", "Description", "Name" },
                values: new object[] { 750m, "Набор пастели 24 цвета.", "Набор сухой пастели, 24 цвета" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("37acd775-ee3a-4e6d-83c2-3458adcbf87f"),
                columns: new[] { "Cost", "Description", "Name" },
                values: new object[] { 1950m, "Краски масляные художественные для рисования, набор из 10 цветов.", "Набор масляных красок, 10 цветов" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("ac80a1ac-4c7b-4662-8aac-f14a5a90b957"),
                columns: new[] { "Cost", "Description", "Name" },
                values: new object[] { 400m, "Набор гуаши 5 цветов, художественная 40 мл.", "Гуашь художественная, набор из 5 цветов" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("c60de567-a236-4f59-bc76-4aa1109545d6"),
                columns: new[] { "Cost", "Description", "Name" },
                values: new object[] { 2000m, "5 цветов, 50 мл.", "Краски акриловые, 5х50 мл" });
        }
    }
}
