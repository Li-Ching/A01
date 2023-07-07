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
            migrationBuilder.AddColumn<string>(
                name: "picture",
                table: "Furniture",
                type: "varchar(50)",
                unicode: false,
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "FavoriteId",
                table: "Favorite",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<string>(
                name: "phoneNumber",
                table: "Brand",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddPrimaryKey(
                name: "PK__Favorite",
                table: "Favorite",
                column: "FavoriteId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK__Favorite",
                table: "Favorite");

            migrationBuilder.DropColumn(
                name: "picture",
                table: "Furniture");

            migrationBuilder.DropColumn(
                name: "FavoriteId",
                table: "Favorite");

            migrationBuilder.AlterColumn<int>(
                name: "phoneNumber",
                table: "Brand",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }
    }
}
