﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DataBase.Data.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AgreementStatus",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "char(13)", maxLength: 13, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AgreementStatus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AgreementType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "char(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AgreementType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Directions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "char(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Directions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Faculties",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "char(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Faculties", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "InteractionTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "char(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InteractionTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PartnerTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "char(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PartnerTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Agreements",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AgreementNumber = table.Column<string>(type: "char(15)", maxLength: 15, nullable: false),
                    StarDateTime = table.Column<DateTime>(type: "date", nullable: false),
                    EndDateTime = table.Column<DateTime>(type: "date", nullable: false),
                    AgreementTypeId = table.Column<int>(type: "integer", nullable: false),
                    AgreementStatusId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Agreements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Agreements_AgreementStatus_AgreementStatusId",
                        column: x => x.AgreementStatusId,
                        principalTable: "AgreementStatus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Agreements_AgreementType_AgreementTypeId",
                        column: x => x.AgreementTypeId,
                        principalTable: "AgreementType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Divisions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FullName = table.Column<string>(type: "char(200)", maxLength: 200, nullable: false),
                    ShortName = table.Column<string>(type: "char(50)", maxLength: 50, nullable: false),
                    FacultyId = table.Column<int>(type: "integer", nullable: false),
                    Contacts = table.Column<string>(type: "char(500)", maxLength: 500, nullable: true),
                    Site = table.Column<string>(type: "char(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Divisions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Divisions_Faculties_FacultyId",
                        column: x => x.FacultyId,
                        principalTable: "Faculties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Partners",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FullName = table.Column<string>(type: "char(200)", maxLength: 200, nullable: false),
                    ShortName = table.Column<string>(type: "char(50)", maxLength: 50, nullable: false),
                    Address = table.Column<string>(type: "char(100)", maxLength: 100, nullable: true),
                    Site = table.Column<string>(type: "char(100)", maxLength: 100, nullable: true),
                    ContactData = table.Column<string>(type: "char(500)", maxLength: 500, nullable: true),
                    City = table.Column<string>(type: "char(100)", maxLength: 100, nullable: true),
                    PartnerTypeId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Partners", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Partners_PartnerTypes_PartnerTypeId",
                        column: x => x.PartnerTypeId,
                        principalTable: "PartnerTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DirectionDivision",
                columns: table => new
                {
                    DirectionsId = table.Column<int>(type: "integer", nullable: false),
                    DivisionsId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DirectionDivision", x => new { x.DirectionsId, x.DivisionsId });
                    table.ForeignKey(
                        name: "FK_DirectionDivision_Directions_DirectionsId",
                        column: x => x.DirectionsId,
                        principalTable: "Directions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DirectionDivision_Divisions_DivisionsId",
                        column: x => x.DivisionsId,
                        principalTable: "Divisions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DivisionsInAgreement",
                columns: table => new
                {
                    DivisionInAgreementId = table.Column<int>(type: "integer", nullable: false),
                    AgreementId = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "char(100)", maxLength: 100, nullable: false),
                    DivisionId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DivisionsInAgreement", x => new { x.DivisionInAgreementId, x.AgreementId });
                    table.ForeignKey(
                        name: "FK_DivisionsInAgreement_Agreements_AgreementId",
                        column: x => x.AgreementId,
                        principalTable: "Agreements",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DivisionsInAgreement_Divisions_DivisionId",
                        column: x => x.DivisionId,
                        principalTable: "Divisions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DirectionPartner",
                columns: table => new
                {
                    DirectionsId = table.Column<int>(type: "integer", nullable: false),
                    PartnersId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DirectionPartner", x => new { x.DirectionsId, x.PartnersId });
                    table.ForeignKey(
                        name: "FK_DirectionPartner_Directions_DirectionsId",
                        column: x => x.DirectionsId,
                        principalTable: "Directions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DirectionPartner_Partners_PartnersId",
                        column: x => x.PartnersId,
                        principalTable: "Partners",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Interactions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PartnerId = table.Column<int>(type: "integer", nullable: false),
                    DivisionId = table.Column<int>(type: "integer", nullable: false),
                    InteractionTypeId = table.Column<int>(type: "integer", nullable: false),
                    Theme = table.Column<string>(type: "char(500)", maxLength: 500, nullable: false),
                    ContactCode = table.Column<string>(type: "char(9)", maxLength: 9, nullable: false),
                    SigningDateTime = table.Column<DateTime>(type: "date", nullable: false),
                    BeginigDateTime = table.Column<DateTime>(type: "date", nullable: false),
                    EndingDateTime = table.Column<DateTime>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Interactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Interactions_Divisions_DivisionId",
                        column: x => x.DivisionId,
                        principalTable: "Divisions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Interactions_InteractionTypes_InteractionTypeId",
                        column: x => x.InteractionTypeId,
                        principalTable: "InteractionTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Interactions_Partners_PartnerId",
                        column: x => x.PartnerId,
                        principalTable: "Partners",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PartnersInAgreement",
                columns: table => new
                {
                    AgreementId = table.Column<int>(type: "integer", nullable: false),
                    PartnerInAgreementId = table.Column<int>(type: "integer", nullable: false),
                    ContactPersons = table.Column<string>(type: "char(500)", maxLength: 500, nullable: false),
                    PartnerId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PartnersInAgreement", x => new { x.AgreementId, x.PartnerInAgreementId });
                    table.ForeignKey(
                        name: "FK_PartnersInAgreement_Agreements_AgreementId",
                        column: x => x.AgreementId,
                        principalTable: "Agreements",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PartnersInAgreement_Partners_PartnerId",
                        column: x => x.PartnerId,
                        principalTable: "Partners",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DirectionInteraction",
                columns: table => new
                {
                    DirectionsId = table.Column<int>(type: "integer", nullable: false),
                    InteractionsId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DirectionInteraction", x => new { x.DirectionsId, x.InteractionsId });
                    table.ForeignKey(
                        name: "FK_DirectionInteraction_Directions_DirectionsId",
                        column: x => x.DirectionsId,
                        principalTable: "Directions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DirectionInteraction_Interactions_InteractionsId",
                        column: x => x.InteractionsId,
                        principalTable: "Interactions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Agreements_AgreementNumber",
                table: "Agreements",
                column: "AgreementNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Agreements_AgreementStatusId",
                table: "Agreements",
                column: "AgreementStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Agreements_AgreementTypeId",
                table: "Agreements",
                column: "AgreementTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_AgreementStatus_Name",
                table: "AgreementStatus",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AgreementType_Name",
                table: "AgreementType",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DirectionDivision_DivisionsId",
                table: "DirectionDivision",
                column: "DivisionsId");

            migrationBuilder.CreateIndex(
                name: "IX_DirectionInteraction_InteractionsId",
                table: "DirectionInteraction",
                column: "InteractionsId");

            migrationBuilder.CreateIndex(
                name: "IX_DirectionPartner_PartnersId",
                table: "DirectionPartner",
                column: "PartnersId");

            migrationBuilder.CreateIndex(
                name: "IX_Divisions_FacultyId",
                table: "Divisions",
                column: "FacultyId");

            migrationBuilder.CreateIndex(
                name: "IX_Divisions_ShortName",
                table: "Divisions",
                column: "ShortName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DivisionsInAgreement_AgreementId",
                table: "DivisionsInAgreement",
                column: "AgreementId");

            migrationBuilder.CreateIndex(
                name: "IX_DivisionsInAgreement_DivisionId",
                table: "DivisionsInAgreement",
                column: "DivisionId");

            migrationBuilder.CreateIndex(
                name: "IX_Interactions_DivisionId",
                table: "Interactions",
                column: "DivisionId");

            migrationBuilder.CreateIndex(
                name: "IX_Interactions_InteractionTypeId",
                table: "Interactions",
                column: "InteractionTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Interactions_PartnerId",
                table: "Interactions",
                column: "PartnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Partners_PartnerTypeId",
                table: "Partners",
                column: "PartnerTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Partners_ShortName",
                table: "Partners",
                column: "ShortName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PartnersInAgreement_ContactPersons",
                table: "PartnersInAgreement",
                column: "ContactPersons",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PartnersInAgreement_PartnerId",
                table: "PartnersInAgreement",
                column: "PartnerId");

            migrationBuilder.CreateIndex(
                name: "IX_PartnerTypes_Name",
                table: "PartnerTypes",
                column: "Name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DirectionDivision");

            migrationBuilder.DropTable(
                name: "DirectionInteraction");

            migrationBuilder.DropTable(
                name: "DirectionPartner");

            migrationBuilder.DropTable(
                name: "DivisionsInAgreement");

            migrationBuilder.DropTable(
                name: "PartnersInAgreement");

            migrationBuilder.DropTable(
                name: "Interactions");

            migrationBuilder.DropTable(
                name: "Directions");

            migrationBuilder.DropTable(
                name: "Agreements");

            migrationBuilder.DropTable(
                name: "Divisions");

            migrationBuilder.DropTable(
                name: "InteractionTypes");

            migrationBuilder.DropTable(
                name: "Partners");

            migrationBuilder.DropTable(
                name: "AgreementStatus");

            migrationBuilder.DropTable(
                name: "AgreementType");

            migrationBuilder.DropTable(
                name: "Faculties");

            migrationBuilder.DropTable(
                name: "PartnerTypes");
        }
    }
}
