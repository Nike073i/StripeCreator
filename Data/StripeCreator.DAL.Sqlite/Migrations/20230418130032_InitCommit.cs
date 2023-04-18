using Microsoft.EntityFrameworkCore.Migrations;

namespace StripeCreator.DAL.Sqlite.Migrations
{
    public partial class InitCommit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DbClient",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    FirstName = table.Column<string>(type: "TEXT", nullable: false),
                    SecondName = table.Column<string>(type: "TEXT", nullable: false),
                    ContactNumber = table.Column<string>(type: "TEXT", nullable: false),
                    Email = table.Column<string>(type: "TEXT", nullable: false),
                    Other = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DbClient", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DbCloth",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Count = table.Column<int>(type: "INTEGER", nullable: false),
                    Type = table.Column<int>(type: "INTEGER", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Price = table.Column<decimal>(type: "TEXT", nullable: false),
                    Manufacturer = table.Column<string>(type: "TEXT", nullable: false),
                    ColorHex = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DbCloth", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DbProduct",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Price = table.Column<decimal>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DbProduct", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DbThread",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Weight = table.Column<int>(type: "INTEGER", nullable: false),
                    Type = table.Column<int>(type: "INTEGER", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Price = table.Column<decimal>(type: "TEXT", nullable: false),
                    Manufacturer = table.Column<string>(type: "TEXT", nullable: false),
                    ColorHex = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DbThread", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DbOrder",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    ClientId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Price = table.Column<decimal>(type: "TEXT", nullable: false),
                    ContactNumber = table.Column<string>(type: "TEXT", nullable: false),
                    Email = table.Column<string>(type: "TEXT", nullable: false),
                    Other = table.Column<string>(type: "TEXT", nullable: true),
                    Status = table.Column<int>(type: "INTEGER", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DbOrder", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DbOrder_DbClient_ClientId",
                        column: x => x.ClientId,
                        principalTable: "DbClient",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DbOrderProduct",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    ProductId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Quantity = table.Column<int>(type: "INTEGER", nullable: false),
                    OrderId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DbOrderProduct", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DbOrderProduct_DbOrder_OrderId",
                        column: x => x.OrderId,
                        principalTable: "DbOrder",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DbOrderProduct_DbProduct_ProductId",
                        column: x => x.ProductId,
                        principalTable: "DbProduct",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DbOrder_ClientId",
                table: "DbOrder",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_DbOrderProduct_OrderId",
                table: "DbOrderProduct",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_DbOrderProduct_ProductId",
                table: "DbOrderProduct",
                column: "ProductId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DbCloth");

            migrationBuilder.DropTable(
                name: "DbOrderProduct");

            migrationBuilder.DropTable(
                name: "DbThread");

            migrationBuilder.DropTable(
                name: "DbOrder");

            migrationBuilder.DropTable(
                name: "DbProduct");

            migrationBuilder.DropTable(
                name: "DbClient");
        }
    }
}
