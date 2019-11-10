using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Engine.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                "AccountStatuses",
                table => new
                {
                    Id = table.Column<int>(),
                    Name = table.Column<string>(maxLength: 100),
                    Description = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table => { table.PrimaryKey("PK_AccountStatuses", x => x.Id); });

            migrationBuilder.CreateTable(
                "AccountTypes",
                table => new
                {
                    Id = table.Column<int>(),
                    Name = table.Column<string>(maxLength: 100),
                    Description = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table => { table.PrimaryKey("PK_AccountTypes", x => x.Id); });

            migrationBuilder.CreateTable(
                "TransactionTypes",
                table => new
                {
                    Id = table.Column<int>(),
                    Name = table.Column<string>(maxLength: 100),
                    Description = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table => { table.PrimaryKey("PK_TransactionTypes", x => x.Id); });

            migrationBuilder.CreateTable(
                "Users",
                table => new
                {
                    Passport = table.Column<string>(maxLength: 8),
                    LastName = table.Column<string>(nullable: true),
                    FirstName = table.Column<string>(nullable: true),
                    Patronymic = table.Column<string>(nullable: true),
                    DateBirth = table.Column<DateTime>(),
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
                constraints: table => { table.PrimaryKey("PK_Users", x => x.Passport); });

            migrationBuilder.CreateTable(
                "Accounts",
                table => new
                {
                    Id = table.Column<int>()
                        .Annotation("Sqlite:Autoincrement", true),
                    TypeId = table.Column<int>(nullable: true),
                    AmountMoney = table.Column<decimal>(),
                    Creation = table.Column<DateTime>(),
                    StatusId = table.Column<int>(nullable: true),
                    Notes = table.Column<string>(nullable: true),
                    OwnerPassport = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.Id);
                    table.ForeignKey(
                        "FK_Accounts_Users_OwnerPassport",
                        x => x.OwnerPassport,
                        "Users",
                        "Passport",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        "FK_Accounts_AccountStatuses_StatusId",
                        x => x.StatusId,
                        "AccountStatuses",
                        "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        "FK_Accounts_AccountTypes_TypeId",
                        x => x.TypeId,
                        "AccountTypes",
                        "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                "Cards",
                table => new
                {
                    Number = table.Column<string>(maxLength: 16),
                    ExpireDate = table.Column<DateTime>(),
                    Cvv2 = table.Column<string>(nullable: true),
                    PinHash = table.Column<string>(nullable: true),
                    Notes = table.Column<string>(nullable: true),
                    AccountId = table.Column<int>()
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cards", x => x.Number);
                    table.ForeignKey(
                        "FK_Cards_Accounts_AccountId",
                        x => x.AccountId,
                        "Accounts",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                "Transactions",
                table => new
                {
                    Id = table.Column<int>()
                        .Annotation("Sqlite:Autoincrement", true),
                    DateTime = table.Column<DateTime>(),
                    TypeId = table.Column<int>(nullable: true),
                    AmountMoney = table.Column<decimal>(),
                    Notes = table.Column<string>(nullable: true),
                    AccountFromId = table.Column<int>(),
                    AccountToId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.Id);
                    table.ForeignKey(
                        "FK_Transactions_Accounts_AccountFromId",
                        x => x.AccountFromId,
                        "Accounts",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        "FK_Transactions_Accounts_AccountToId",
                        x => x.AccountToId,
                        "Accounts",
                        "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        "FK_Transactions_TransactionTypes_TypeId",
                        x => x.TypeId,
                        "TransactionTypes",
                        "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                "AccountStatuses",
                new[] {"Id", "Description", "Name"},
                new object[] {1, "Active account", "Active"});

            migrationBuilder.InsertData(
                "AccountStatuses",
                new[] {"Id", "Description", "Name"},
                new object[] {0, "Closed account", "Closed"});

            migrationBuilder.InsertData(
                "AccountStatuses",
                new[] {"Id", "Description", "Name"},
                new object[] {2, "Frozen account", "Frozen"});

            migrationBuilder.InsertData(
                "AccountTypes",
                new[] {"Id", "Description", "Name"},
                new object[] {1, "Credit account", "Credit"});

            migrationBuilder.InsertData(
                "AccountTypes",
                new[] {"Id", "Description", "Name"},
                new object[] {0, "Debit account", "Debit"});

            migrationBuilder.InsertData(
                "TransactionTypes",
                new[] {"Id", "Description", "Name"},
                new object[] {0, "Withdraw cash from ATM", "Withdraw"});

            migrationBuilder.InsertData(
                "TransactionTypes",
                new[] {"Id", "Description", "Name"},
                new object[] {1, "Send money to another person", "ToUser"});

            migrationBuilder.CreateIndex(
                "IX_Accounts_OwnerPassport",
                "Accounts",
                "OwnerPassport");

            migrationBuilder.CreateIndex(
                "IX_Accounts_StatusId",
                "Accounts",
                "StatusId");

            migrationBuilder.CreateIndex(
                "IX_Accounts_TypeId",
                "Accounts",
                "TypeId");

            migrationBuilder.CreateIndex(
                "IX_Cards_AccountId",
                "Cards",
                "AccountId");

            migrationBuilder.CreateIndex(
                "IX_Transactions_AccountFromId",
                "Transactions",
                "AccountFromId");

            migrationBuilder.CreateIndex(
                "IX_Transactions_AccountToId",
                "Transactions",
                "AccountToId");

            migrationBuilder.CreateIndex(
                "IX_Transactions_TypeId",
                "Transactions",
                "TypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                "Cards");

            migrationBuilder.DropTable(
                "Transactions");

            migrationBuilder.DropTable(
                "Accounts");

            migrationBuilder.DropTable(
                "TransactionTypes");

            migrationBuilder.DropTable(
                "Users");

            migrationBuilder.DropTable(
                "AccountStatuses");

            migrationBuilder.DropTable(
                "AccountTypes");
        }
    }
}