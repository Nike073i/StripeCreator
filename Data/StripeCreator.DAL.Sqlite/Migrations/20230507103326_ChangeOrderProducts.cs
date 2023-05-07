using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StripeCreator.DAL.Sqlite.Migrations
{
    /// <inheritdoc />
    public partial class ChangeOrderProducts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DbOrderProduct_Orders_OrderId",
                table: "DbOrderProduct");

            migrationBuilder.DropForeignKey(
                name: "FK_DbOrderProduct_Products_ProductId",
                table: "DbOrderProduct");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DbOrderProduct",
                table: "DbOrderProduct");

            migrationBuilder.DropIndex(
                name: "IX_DbOrderProduct_OrderId",
                table: "DbOrderProduct");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "DbOrderProduct");

            migrationBuilder.RenameTable(
                name: "DbOrderProduct",
                newName: "OrderProducts");

            migrationBuilder.RenameIndex(
                name: "IX_DbOrderProduct_ProductId",
                table: "OrderProducts",
                newName: "IX_OrderProducts_ProductId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderProducts",
                table: "OrderProducts",
                columns: new[] { "OrderId", "ProductId" });

            migrationBuilder.AddForeignKey(
                name: "FK_OrderProducts_Orders_OrderId",
                table: "OrderProducts",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderProducts_Products_ProductId",
                table: "OrderProducts",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderProducts_Orders_OrderId",
                table: "OrderProducts");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderProducts_Products_ProductId",
                table: "OrderProducts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderProducts",
                table: "OrderProducts");

            migrationBuilder.RenameTable(
                name: "OrderProducts",
                newName: "DbOrderProduct");

            migrationBuilder.RenameIndex(
                name: "IX_OrderProducts_ProductId",
                table: "DbOrderProduct",
                newName: "IX_DbOrderProduct_ProductId");

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "DbOrderProduct",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_DbOrderProduct",
                table: "DbOrderProduct",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_DbOrderProduct_OrderId",
                table: "DbOrderProduct",
                column: "OrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_DbOrderProduct_Orders_OrderId",
                table: "DbOrderProduct",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DbOrderProduct_Products_ProductId",
                table: "DbOrderProduct",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
