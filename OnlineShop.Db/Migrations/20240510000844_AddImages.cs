using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OnlineShop.Db.Migrations
{
    public partial class AddImages : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "Products");

            migrationBuilder.CreateTable(
                name: "Images",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Images", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Images_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Images",
                columns: new[] { "Id", "ProductId", "Url" },
                values: new object[,]
                {
                    { new Guid("56509768-29a5-4bdf-ada5-66e40a06b2b7"), new Guid("ac80a1ac-4c7b-4662-8aac-f14a5a90b957"), "/images/goods/image1.jpg" },
                    { new Guid("d096d5aa-159a-442c-8507-e4449cb78376"), new Guid("0122481d-10e3-4b58-89ee-c72269d5c95e"), "/images/goods/image2.jpg" },
                    { new Guid("d4571918-a7ec-4361-ba2b-8b04ec88ad5b"), new Guid("c60de567-a236-4f59-bc76-4aa1109545d6"), "/images/goods/image3.jpg" },
                    { new Guid("fd4ad9ce-2b57-4abb-9330-6e7ca265b688"), new Guid("37acd775-ee3a-4e6d-83c2-3458adcbf87f"), "/images/goods/image4.jpg" },
                    { new Guid("d720a522-4957-4a50-af7a-8edee651f53c"), new Guid("21b68580-8f57-45a4-a415-1ccfa97337c3"), "/images/goods/image5.jpg" },
                    { new Guid("99a13c69-060e-4663-a6a4-8490630bd027"), new Guid("0bc0cbd7-de40-46bd-8133-59c8a35b2761"), "/images/goods/image6.jpg" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Images_ProductId",
                table: "Images",
                column: "ProductId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Images");

            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("0122481d-10e3-4b58-89ee-c72269d5c95e"),
                column: "Image",
                value: "/images/paints/images.jpg");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("0bc0cbd7-de40-46bd-8133-59c8a35b2761"),
                column: "Image",
                value: "/images/paints/images.jpg");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("21b68580-8f57-45a4-a415-1ccfa97337c3"),
                column: "Image",
                value: "/images/paints/images.jpg");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("37acd775-ee3a-4e6d-83c2-3458adcbf87f"),
                column: "Image",
                value: "/images/paints/images.jpg");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("ac80a1ac-4c7b-4662-8aac-f14a5a90b957"),
                column: "Image",
                value: "/images/paints/images.jpg");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("c60de567-a236-4f59-bc76-4aa1109545d6"),
                column: "Image",
                value: "/images/paints/images.jpg");
        }
    }
}
