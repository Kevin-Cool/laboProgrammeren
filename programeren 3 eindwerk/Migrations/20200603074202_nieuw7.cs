using Microsoft.EntityFrameworkCore.Migrations;

namespace programeren_3_eindwerk.Migrations
{
    public partial class nieuw7 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Grafen",
                columns: table => new
                {
                    GraafID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Grafen", x => x.GraafID);
                });

            migrationBuilder.CreateTable(
                name: "Provincies",
                columns: table => new
                {
                    ProvincieID = table.Column<int>(nullable: false),
                    ProvincieNaam = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Provincies", x => x.ProvincieID);
                });

            migrationBuilder.CreateTable(
                name: "Gemeenten",
                columns: table => new
                {
                    GemeenteID = table.Column<int>(nullable: false),
                    GemeenteNaam = table.Column<string>(nullable: true),
                    ProvincieID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gemeenten", x => x.GemeenteID);
                    table.ForeignKey(
                        name: "FK_Gemeenten_Provincies_ProvincieID",
                        column: x => x.ProvincieID,
                        principalTable: "Provincies",
                        principalColumn: "ProvincieID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Straten",
                columns: table => new
                {
                    StraatID = table.Column<int>(nullable: false),
                    GraafID = table.Column<int>(nullable: true),
                    Straatnaam = table.Column<string>(nullable: true),
                    GemeenteID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Straten", x => x.StraatID);
                    table.ForeignKey(
                        name: "FK_Straten_Gemeenten_GemeenteID",
                        column: x => x.GemeenteID,
                        principalTable: "Gemeenten",
                        principalColumn: "GemeenteID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Straten_Grafen_GraafID",
                        column: x => x.GraafID,
                        principalTable: "Grafen",
                        principalColumn: "GraafID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Segmenten",
                columns: table => new
                {
                    SegmentID = table.Column<int>(nullable: false),
                    BeginknoopKnoopID = table.Column<int>(nullable: true),
                    EindknoopKnoopID = table.Column<int>(nullable: true),
                    GraafID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Segmenten", x => x.SegmentID);
                    table.ForeignKey(
                        name: "FK_Segmenten_Grafen_GraafID",
                        column: x => x.GraafID,
                        principalTable: "Grafen",
                        principalColumn: "GraafID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Puntes",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    X = table.Column<double>(nullable: false),
                    Y = table.Column<double>(nullable: false),
                    SegmentID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Puntes", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Puntes_Segmenten_SegmentID",
                        column: x => x.SegmentID,
                        principalTable: "Segmenten",
                        principalColumn: "SegmentID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Knopen",
                columns: table => new
                {
                    KnoopID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PuntID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Knopen", x => x.KnoopID);
                    table.ForeignKey(
                        name: "FK_Knopen_Puntes_PuntID",
                        column: x => x.PuntID,
                        principalTable: "Puntes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Gemeenten_ProvincieID",
                table: "Gemeenten",
                column: "ProvincieID");

            migrationBuilder.CreateIndex(
                name: "IX_Knopen_PuntID",
                table: "Knopen",
                column: "PuntID");

            migrationBuilder.CreateIndex(
                name: "IX_Puntes_SegmentID",
                table: "Puntes",
                column: "SegmentID");

            migrationBuilder.CreateIndex(
                name: "IX_Segmenten_BeginknoopKnoopID",
                table: "Segmenten",
                column: "BeginknoopKnoopID");

            migrationBuilder.CreateIndex(
                name: "IX_Segmenten_EindknoopKnoopID",
                table: "Segmenten",
                column: "EindknoopKnoopID");

            migrationBuilder.CreateIndex(
                name: "IX_Segmenten_GraafID",
                table: "Segmenten",
                column: "GraafID");

            migrationBuilder.CreateIndex(
                name: "IX_Straten_GemeenteID",
                table: "Straten",
                column: "GemeenteID");

            migrationBuilder.CreateIndex(
                name: "IX_Straten_GraafID",
                table: "Straten",
                column: "GraafID");

            migrationBuilder.AddForeignKey(
                name: "FK_Segmenten_Knopen_BeginknoopKnoopID",
                table: "Segmenten",
                column: "BeginknoopKnoopID",
                principalTable: "Knopen",
                principalColumn: "KnoopID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Segmenten_Knopen_EindknoopKnoopID",
                table: "Segmenten",
                column: "EindknoopKnoopID",
                principalTable: "Knopen",
                principalColumn: "KnoopID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Knopen_Puntes_PuntID",
                table: "Knopen");

            migrationBuilder.DropTable(
                name: "Straten");

            migrationBuilder.DropTable(
                name: "Gemeenten");

            migrationBuilder.DropTable(
                name: "Provincies");

            migrationBuilder.DropTable(
                name: "Puntes");

            migrationBuilder.DropTable(
                name: "Segmenten");

            migrationBuilder.DropTable(
                name: "Knopen");

            migrationBuilder.DropTable(
                name: "Grafen");
        }
    }
}
