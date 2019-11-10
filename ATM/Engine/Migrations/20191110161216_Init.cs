using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Engine.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AccountStatuses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 100, nullable: false),
                    Description = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountStatuses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AccountTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 100, nullable: false),
                    Description = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TransactionTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 100, nullable: false),
                    Description = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransactionTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Passport = table.Column<string>(maxLength: 8, nullable: false),
                    LastName = table.Column<string>(nullable: true),
                    FirstName = table.Column<string>(nullable: true),
                    Patronymic = table.Column<string>(nullable: true),
                    DateBirth = table.Column<DateTime>(nullable: false),
                    TaxNumber = table.Column<string>(nullable: true),
                    Telephone1 = table.Column<string>(nullable: true),
                    Telephone2 = table.Column<string>(nullable: true),
                    Telephone3 = table.Column<string>(nullable: true),
                    City = table.Column<string>(nullable: true),
                    Street = table.Column<string>(nullable: true),
                    HouseNum = table.Column<string>(nullable: true),
                    ApartmentNum = table.Column<string>(nullable: true),
                    Notes = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Passport);
                });

            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TypeId = table.Column<int>(nullable: true),
                    AmountMoney = table.Column<decimal>(nullable: false),
                    Creation = table.Column<DateTime>(nullable: false),
                    StatusId = table.Column<int>(nullable: true),
                    Notes = table.Column<string>(nullable: true),
                    OwnerPassport = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Accounts_Users_OwnerPassport",
                        column: x => x.OwnerPassport,
                        principalTable: "Users",
                        principalColumn: "Passport",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Accounts_AccountStatuses_StatusId",
                        column: x => x.StatusId,
                        principalTable: "AccountStatuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Accounts_AccountTypes_TypeId",
                        column: x => x.TypeId,
                        principalTable: "AccountTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Cards",
                columns: table => new
                {
                    Number = table.Column<string>(maxLength: 16, nullable: false),
                    ExpireDate = table.Column<DateTime>(nullable: false),
                    Cvv2 = table.Column<string>(nullable: true),
                    PinHash = table.Column<string>(nullable: true),
                    Notes = table.Column<string>(nullable: true),
                    AccountId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cards", x => x.Number);
                    table.ForeignKey(
                        name: "FK_Cards_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Transactions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DateTime = table.Column<DateTime>(nullable: false),
                    TypeId = table.Column<int>(nullable: true),
                    AmountMoney = table.Column<decimal>(nullable: false),
                    Notes = table.Column<string>(nullable: true),
                    AccountFromId = table.Column<int>(nullable: false),
                    AccountToId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transactions_Accounts_AccountFromId",
                        column: x => x.AccountFromId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Transactions_Accounts_AccountToId",
                        column: x => x.AccountToId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Transactions_TransactionTypes_TypeId",
                        column: x => x.TypeId,
                        principalTable: "TransactionTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "AccountStatuses",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 1, "Active account", "Active" });

            migrationBuilder.InsertData(
                table: "AccountStatuses",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 0, "Closed account", "Closed" });

            migrationBuilder.InsertData(
                table: "AccountStatuses",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 2, "Frozen account", "Frozen" });

            migrationBuilder.InsertData(
                table: "AccountTypes",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 1, "Credit account", "Credit" });

            migrationBuilder.InsertData(
                table: "AccountTypes",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 0, "Debit account", "Debit" });

            migrationBuilder.InsertData(
                table: "TransactionTypes",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 0, "Withdraw cash from ATM", "Withdraw" });

            migrationBuilder.InsertData(
                table: "TransactionTypes",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 1, "Send money to another person", "ToUser" });

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_OwnerPassport",
                table: "Accounts",
                column: "OwnerPassport");

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_StatusId",
                table: "Accounts",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_TypeId",
                table: "Accounts",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Cards_AccountId",
                table: "Cards",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_AccountFromId",
                table: "Transactions",
                column: "AccountFromId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_AccountToId",
                table: "Transactions",
                column: "AccountToId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_TypeId",
                table: "Transactions",
                column: "TypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cards");

            migrationBuilder.DropTable(
                name: "Transactions");

            migrationBuilder.DropTable(
                name: "Accounts");

            migrationBuilder.DropTable(
                name: "TransactionTypes");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "AccountStatuses");

            migrationBuilder.DropTable(
                name: "AccountTypes");
        }
    }
}
