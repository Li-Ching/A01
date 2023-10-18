using System;
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
                    brand = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    phoneNumber = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    address = table.Column<string>(type: "varchar(max)", unicode: false, nullable: false),
                    logo = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    url = table.Column<string>(type: "varchar(max)", unicode: false, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Brand__06B772995AD10E28", x => x.brandId);
                });

            migrationBuilder.CreateTable(
                name: "Branch",
                columns: table => new
                {
                    branchId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    branchName = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    phoneNumber = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    address = table.Column<string>(type: "varchar(max)", unicode: false, nullable: false),
                    brandId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Branch__751EBD5F33FA43BF", x => x.branchId);
                    table.ForeignKey(
                        name: "FK__Branch__brandId__4E88ABD4",
                        column: x => x.brandId,
                        principalTable: "Brand",
                        principalColumn: "brandId");
                });

            migrationBuilder.CreateTable(
                name: "Furniture",
                columns: table => new
                {
                    furnitureId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    type = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    color = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    style = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    brandId = table.Column<int>(type: "int", nullable: false),
                    location = table.Column<string>(type: "varchar(max)", unicode: false, nullable: false),
                    picture = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Furnitur__BD41E4C50FA6B547", x => x.furnitureId);
                    table.ForeignKey(
                        name: "FK__Furniture__brand__571DF1D5",
                        column: x => x.brandId,
                        principalTable: "Brand",
                        principalColumn: "brandId");
                });

            migrationBuilder.CreateTable(
                name: "Favorite",
                columns: table => new
                {
                    favoriteId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    userId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    furnitureId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Favorite__876A64D58AA15765", x => x.favoriteId);
                    table.ForeignKey(
                        name: "FK__Favorite__furnit__59FA5E80",
                        column: x => x.furnitureId,
                        principalTable: "Furniture",
                        principalColumn: "furnitureId");
                });

            migrationBuilder.CreateTable(
                name: "Message",
                columns: table => new
                {
                    messageId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    userId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    furnitureId = table.Column<int>(type: "int", nullable: false),
                    message = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    isDelete = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Message__4808B9933BCF5750", x => x.messageId);
                    table.ForeignKey(
                        name: "FK__Message__furnitu__5CD6CB2B",
                        column: x => x.furnitureId,
                        principalTable: "Furniture",
                        principalColumn: "furnitureId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Branch_brandId",
                table: "Branch",
                column: "brandId");

            migrationBuilder.CreateIndex(
                name: "IX_Favorite_furnitureId",
                table: "Favorite",
                column: "furnitureId");

            migrationBuilder.CreateIndex(
                name: "IX_Furniture_brandId",
                table: "Furniture",
                column: "brandId");

            migrationBuilder.CreateIndex(
                name: "IX_Message_furnitureId",
                table: "Message",
                column: "furnitureId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Branch");

            migrationBuilder.DropTable(
                name: "Favorite");

            migrationBuilder.DropTable(
                name: "Message");

            migrationBuilder.DropTable(
                name: "Furniture");

            migrationBuilder.DropTable(
                name: "Brand");
        }
    }
}
