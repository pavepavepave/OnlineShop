using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OnlineShop.Db.Migrations
{
    public partial class AddImagesPainting : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Compares");

            migrationBuilder.DropTable(
                name: "Favorites");

            migrationBuilder.CreateTable(
                name: "Paintings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Paintings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ImagesPaintings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PaintingId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImagesPaintings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ImagesPaintings_Paintings_PaintingId",
                        column: x => x.PaintingId,
                        principalTable: "Paintings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Paintings",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { new Guid("01e5de59-7e9d-4985-9fb9-84e67fbbbdfe"), "Холст. Масло. 20х25 см. 2022 г.", "Пряный аромат" },
                    { new Guid("dc00811d-5c83-4bff-ab04-e10f56dc4faf"), "Холст. Масло. 40х60 см. 2021 г.", "Северное сияние" },
                    { new Guid("2bee2bc7-3a2b-49ed-a608-c24d2d3eecec"), "Холст. Масло. 20х25 см. 2023 г.", "Городская эстетика" },
                    { new Guid("158cbcb3-cd2b-4a27-bafd-e1c026e97dc0"), "Холст. Масло. 20х25 см. 2023 г.", "Балерина" },
                    { new Guid("fb352635-60db-40cf-8f87-6a94a1355e49"), "Холст. Масло. 50х60. 2021 г.", "Важный матч" },
                    { new Guid("2d6334d7-8965-450e-8c37-741031080e31"), "Холст. Масло. 40х60 см. 2022 г.", "Зимний вечер" }
                });

            migrationBuilder.InsertData(
                table: "ImagesPaintings",
                columns: new[] { "Id", "PaintingId", "Url" },
                values: new object[,]
                {
                    { new Guid("26a8e059-69bc-47ac-93fd-7f75715d8f82"), new Guid("01e5de59-7e9d-4985-9fb9-84e67fbbbdfe"), "/images/paintings/image1.jpg" },
                    { new Guid("8e913665-4107-40c2-bc31-be6bc537d14d"), new Guid("dc00811d-5c83-4bff-ab04-e10f56dc4faf"), "/images/paintings/image2.jpg" },
                    { new Guid("2fcbeb71-e0bd-49dd-9643-5b7c6b07b993"), new Guid("2bee2bc7-3a2b-49ed-a608-c24d2d3eecec"), "/images/paintings/image3.jpg" },
                    { new Guid("bb9e2489-d497-4113-8fd3-1d9398524d04"), new Guid("158cbcb3-cd2b-4a27-bafd-e1c026e97dc0"), "/images/paintings/image4.jpg" },
                    { new Guid("89152783-b7eb-4ee1-be08-d2b1e44ae58d"), new Guid("fb352635-60db-40cf-8f87-6a94a1355e49"), "/images/paintings/image5.jpg" },
                    { new Guid("86ace72f-e08c-42ab-8fb6-13abb11ea178"), new Guid("2d6334d7-8965-450e-8c37-741031080e31"), "/images/paintings/image6.jpg" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ImagesPaintings_PaintingId",
                table: "ImagesPaintings",
                column: "PaintingId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ImagesPaintings");

            migrationBuilder.DropTable(
                name: "Paintings");

            migrationBuilder.CreateTable(
                name: "Compares",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Compares", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Compares_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Favorites",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Favorites", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Favorites_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Compares_ProductId",
                table: "Compares",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Favorites_ProductId",
                table: "Favorites",
                column: "ProductId");
        }
    }
}
