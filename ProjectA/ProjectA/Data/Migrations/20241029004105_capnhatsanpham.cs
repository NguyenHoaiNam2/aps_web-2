using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectA.Data.Migrations
{
    /// <inheritdoc />
    public partial class capnhatsanpham : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "NhaCungCapId",
                table: "SanPham",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_SanPham_NhaCungCapId",
                table: "SanPham",
                column: "NhaCungCapId");

            migrationBuilder.AddForeignKey(
                name: "FK_SanPham_NhaCungCap_NhaCungCapId",
                table: "SanPham",
                column: "NhaCungCapId",
                principalTable: "NhaCungCap",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SanPham_NhaCungCap_NhaCungCapId",
                table: "SanPham");

            migrationBuilder.DropIndex(
                name: "IX_SanPham_NhaCungCapId",
                table: "SanPham");

            migrationBuilder.DropColumn(
                name: "NhaCungCapId",
                table: "SanPham");
        }
    }
}
