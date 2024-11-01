using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectA.Data.Migrations
{
    /// <inheritdoc />
    public partial class suaTheLoai : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            

            // Cập nhật độ dài trường Name trong bảng TheLoai từ nvarchar(10) thành nvarchar(50)
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "TheLoai",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(10)",
                oldMaxLength: 10);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Khôi phục lại độ dài trường Name trong bảng TheLoai về nvarchar(10)
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "TheLoai",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            // Tạo lại bảng TacGia với cấu trúc ban đầu
            migrationBuilder.CreateTable(
                name: "TacGia",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Biography = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                });
        }
    }
}
