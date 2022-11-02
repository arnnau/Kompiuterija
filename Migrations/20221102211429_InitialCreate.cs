using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Kompiuterija.Migrations
***REMOVED***
    public partial class InitialCreate : Migration
    ***REMOVED***
        protected override void Up(MigrationBuilder migrationBuilder)
        ***REMOVED***
            migrationBuilder.CreateTable(
                name: "computer",
                columns: table => new
                ***REMOVED***
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    user = table.Column<string>(maxLength: 255, nullable: false),
                    shop_id = table.Column<int>(nullable: false),
                    name = table.Column<string>(maxLength: 255, nullable: false),
                    registered = table.Column<DateTime>(type: "date", nullable: false)
        ***REMOVED***,
                constraints: table =>
                ***REMOVED***
                    table.PrimaryKey("PK_computer", x => x.id);
        ***REMOVED***);

            migrationBuilder.CreateTable(
                name: "part",
                columns: table => new
                ***REMOVED***
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    computer_id = table.Column<int>(nullable: false),
                    type = table.Column<string>(maxLength: 255, nullable: false),
                    name = table.Column<string>(maxLength: 255, nullable: false)
        ***REMOVED***,
                constraints: table =>
                ***REMOVED***
                    table.PrimaryKey("PK_part", x => x.id);
        ***REMOVED***);

            migrationBuilder.CreateTable(
                name: "shop",
                columns: table => new
                ***REMOVED***
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    address = table.Column<string>(maxLength: 255, nullable: false),
                    city = table.Column<string>(maxLength: 255, nullable: false)
        ***REMOVED***,
                constraints: table =>
                ***REMOVED***
                    table.PrimaryKey("PK_shop", x => x.id);
        ***REMOVED***);

            migrationBuilder.CreateTable(
                name: "user",
                columns: table => new
                ***REMOVED***
                    email = table.Column<string>(maxLength: 255, nullable: false),
                    password = table.Column<string>(maxLength: 255, nullable: false),
                    role = table.Column<string>(maxLength: 255, nullable: false)
        ***REMOVED***,
                constraints: table =>
                ***REMOVED***
                    table.PrimaryKey("PRIMARY", x => x.email);
        ***REMOVED***);
***REMOVED***

        protected override void Down(MigrationBuilder migrationBuilder)
        ***REMOVED***
            migrationBuilder.DropTable(
                name: "computer");

            migrationBuilder.DropTable(
                name: "part");

            migrationBuilder.DropTable(
                name: "shop");

            migrationBuilder.DropTable(
                name: "user");
***REMOVED***
***REMOVED***
***REMOVED***
