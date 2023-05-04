using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StripeCreator.DAL.Sqlite.Migrations
{
    /// <inheritdoc />
    public partial class RenameTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DbOrder_DbClient_ClientId",
                table: "DbOrder");

            migrationBuilder.DropForeignKey(
                name: "FK_DbOrderProduct_DbOrder_OrderId",
                table: "DbOrderProduct");

            migrationBuilder.DropForeignKey(
                name: "FK_DbOrderProduct_DbProduct_ProductId",
                table: "DbOrderProduct");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DbThread",
                table: "DbThread");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DbProduct",
                table: "DbProduct");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DbOrder",
                table: "DbOrder");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DbCloth",
                table: "DbCloth");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DbClient",
                table: "DbClient");

            migrationBuilder.RenameTable(
                name: "DbThread",
                newName: "Threads");

            migrationBuilder.RenameTable(
                name: "DbProduct",
                newName: "Products");

            migrationBuilder.RenameTable(
                name: "DbOrder",
                newName: "Orders");

            migrationBuilder.RenameTable(
                name: "DbCloth",
                newName: "Cloths");

            migrationBuilder.RenameTable(
                name: "DbClient",
                newName: "Clients");

            migrationBuilder.RenameIndex(
                name: "IX_DbOrder_ClientId",
                table: "Orders",
                newName: "IX_Orders_ClientId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Threads",
                table: "Threads",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Products",
                table: "Products",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Orders",
                table: "Orders",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Cloths",
                table: "Cloths",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Clients",
                table: "Clients",
                column: "Id");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Clients_ClientId",
                table: "Orders",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DbOrderProduct_Orders_OrderId",
                table: "DbOrderProduct");

            migrationBuilder.DropForeignKey(
                name: "FK_DbOrderProduct_Products_ProductId",
                table: "DbOrderProduct");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Clients_ClientId",
                table: "Orders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Threads",
                table: "Threads");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Products",
                table: "Products");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Orders",
                table: "Orders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Cloths",
                table: "Cloths");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Clients",
                table: "Clients");

            migrationBuilder.RenameTable(
                name: "Threads",
                newName: "DbThread");

            migrationBuilder.RenameTable(
                name: "Products",
                newName: "DbProduct");

            migrationBuilder.RenameTable(
                name: "Orders",
                newName: "DbOrder");

            migrationBuilder.RenameTable(
                name: "Cloths",
                newName: "DbCloth");

            migrationBuilder.RenameTable(
                name: "Clients",
                newName: "DbClient");

            migrationBuilder.RenameIndex(
                name: "IX_Orders_ClientId",
                table: "DbOrder",
                newName: "IX_DbOrder_ClientId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DbThread",
                table: "DbThread",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DbProduct",
                table: "DbProduct",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DbOrder",
                table: "DbOrder",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DbCloth",
                table: "DbCloth",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DbClient",
                table: "DbClient",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DbOrder_DbClient_ClientId",
                table: "DbOrder",
                column: "ClientId",
                principalTable: "DbClient",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DbOrderProduct_DbOrder_OrderId",
                table: "DbOrderProduct",
                column: "OrderId",
                principalTable: "DbOrder",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DbOrderProduct_DbProduct_ProductId",
                table: "DbOrderProduct",
                column: "ProductId",
                principalTable: "DbProduct",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
