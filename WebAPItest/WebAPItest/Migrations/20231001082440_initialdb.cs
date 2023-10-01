using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebAPItest.Migrations
{
    /// <inheritdoc />
    public partial class initialdb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Brand",
                columns: table => new
                {
                    brandId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    brand = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    phoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    address = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    logo = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Brand__06B772993166DED0", x => x.brandId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    userId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    username = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    email = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    password = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Users__CB9A1CFF73659BC8", x => x.userId);
                });

            migrationBuilder.CreateTable(
                name: "Furniture",
                columns: table => new
                {
                    furnitureId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    type = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    color = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    style = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    brandId = table.Column<int>(type: "int", nullable: false),
                    location = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    picture = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    BrandId1 = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Furnitur__BD41E4C59D892524", x => x.furnitureId);
                    table.ForeignKey(
                        name: "FK_Furniture_Brand_BrandId1",
                        column: x => x.BrandId1,
                        principalTable: "Brand",
                        principalColumn: "brandId");
                    table.ForeignKey(
                        name: "FK__Furniture__brand__286302EC",
                        column: x => x.brandId,
                        principalTable: "Brand",
                        principalColumn: "brandId");
                });

            migrationBuilder.CreateTable(
                name: "Favorite",
                columns: table => new
                {
                    FavoriteId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    userId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    furnitureId = table.Column<int>(type: "int", nullable: false),
                    UserId1 = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Favorite", x => x.FavoriteId);
                    table.ForeignKey(
                        name: "FK_Favorite_Users_UserId1",
                        column: x => x.UserId1,
                        principalTable: "Users",
                        principalColumn: "userId");
                    table.ForeignKey(
                        name: "FK__Favorite__furnit__2B3F6F97",
                        column: x => x.furnitureId,
                        principalTable: "Furniture",
                        principalColumn: "furnitureId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Favorite_furnitureId",
                table: "Favorite",
                column: "furnitureId");

            migrationBuilder.CreateIndex(
                name: "IX_Favorite_UserId1",
                table: "Favorite",
                column: "UserId1");

            migrationBuilder.CreateIndex(
                name: "IX_Furniture_brandId",
                table: "Furniture",
                column: "brandId");

            migrationBuilder.CreateIndex(
                name: "IX_Furniture_BrandId1",
                table: "Furniture",
                column: "BrandId1");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Favorite");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Furniture");

            migrationBuilder.DropTable(
                name: "Brand");
        }
    }
}
