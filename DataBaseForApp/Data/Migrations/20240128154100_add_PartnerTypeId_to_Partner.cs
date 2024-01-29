using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataBase.Data.Migrations
{
    /// <inheritdoc />
    public partial class add_PartnerTypeId_to_Partner : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "PartnerTypes",
                type: "varchar",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "char(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "ContactPersons",
                table: "PartnersInAgreement",
                type: "varchar",
                maxLength: 500,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "char(500)",
                oldMaxLength: 500);

            migrationBuilder.AlterColumn<string>(
                name: "Site",
                table: "Partners",
                type: "varchar",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "char(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ShortName",
                table: "Partners",
                type: "varchar",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "char(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "FullName",
                table: "Partners",
                type: "varchar",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "char(200)",
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<string>(
                name: "ContactData",
                table: "Partners",
                type: "varchar",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "char(500)",
                oldMaxLength: 500,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "City",
                table: "Partners",
                type: "varchar",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "char(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Address",
                table: "Partners",
                type: "varchar",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "char(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "InteractionTypes",
                type: "varchar",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "char(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Theme",
                table: "Interactions",
                type: "varchar",
                maxLength: 500,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "char(500)",
                oldMaxLength: 500);

            migrationBuilder.AlterColumn<string>(
                name: "ContactCode",
                table: "Interactions",
                type: "varchar",
                maxLength: 9,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "char(9)",
                oldMaxLength: 9);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Faculties",
                type: "varchar",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "char(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "DivisionsInAgreement",
                type: "varchar",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "char(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Site",
                table: "Divisions",
                type: "varchar",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "char(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ShortName",
                table: "Divisions",
                type: "varchar",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "char(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "FullName",
                table: "Divisions",
                type: "varchar",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "char(200)",
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<string>(
                name: "Contacts",
                table: "Divisions",
                type: "varchar",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "char(500)",
                oldMaxLength: 500,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Directions",
                type: "varchar",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "char(200)",
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AgreementType",
                type: "varchar",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "char(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AgreementStatus",
                type: "varchar",
                maxLength: 13,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "char(13)",
                oldMaxLength: 13);

            migrationBuilder.AlterColumn<string>(
                name: "AgreementNumber",
                table: "Agreements",
                type: "varchar",
                maxLength: 15,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "char(15)",
                oldMaxLength: 15);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "PartnerTypes",
                type: "char(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "ContactPersons",
                table: "PartnersInAgreement",
                type: "char(500)",
                maxLength: 500,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar",
                oldMaxLength: 500);

            migrationBuilder.AlterColumn<string>(
                name: "Site",
                table: "Partners",
                type: "char(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ShortName",
                table: "Partners",
                type: "char(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "FullName",
                table: "Partners",
                type: "char(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar",
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<string>(
                name: "ContactData",
                table: "Partners",
                type: "char(500)",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar",
                oldMaxLength: 500,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "City",
                table: "Partners",
                type: "char(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Address",
                table: "Partners",
                type: "char(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "InteractionTypes",
                type: "char(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Theme",
                table: "Interactions",
                type: "char(500)",
                maxLength: 500,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar",
                oldMaxLength: 500);

            migrationBuilder.AlterColumn<string>(
                name: "ContactCode",
                table: "Interactions",
                type: "char(9)",
                maxLength: 9,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar",
                oldMaxLength: 9);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Faculties",
                type: "char(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "DivisionsInAgreement",
                type: "char(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Site",
                table: "Divisions",
                type: "char(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ShortName",
                table: "Divisions",
                type: "char(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "FullName",
                table: "Divisions",
                type: "char(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar",
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<string>(
                name: "Contacts",
                table: "Divisions",
                type: "char(500)",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar",
                oldMaxLength: 500,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Directions",
                type: "char(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar",
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AgreementType",
                type: "char(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AgreementStatus",
                type: "char(13)",
                maxLength: 13,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar",
                oldMaxLength: 13);

            migrationBuilder.AlterColumn<string>(
                name: "AgreementNumber",
                table: "Agreements",
                type: "char(15)",
                maxLength: 15,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar",
                oldMaxLength: 15);
        }
    }
}
