using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using NetTopologySuite.Geometries;

namespace DiasDataAccessLayer.Migrations.DiasFacilityManagementTemplate.SqlServer.Development
{
    public partial class CombineDiasFacilityManagement : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "test",
                schema: "idn",
                table: "RoleClaim");

            migrationBuilder.EnsureSchema(
                name: "adm");

            migrationBuilder.EnsureSchema(
                name: "usr");

            migrationBuilder.EnsureSchema(
                name: "lst");

            migrationBuilder.AlterDatabase(
                collation: "Turkish_CI_AS");

            migrationBuilder.AlterColumn<string>(
                name: "UserName",
                schema: "idn",
                table: "User",
                type: "varchar(50)",
                unicode: false,
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PasswordHash",
                schema: "idn",
                table: "User",
                type: "varchar(1000)",
                unicode: false,
                maxLength: 1000,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                schema: "idn",
                table: "User",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsActive",
                schema: "idn",
                table: "User",
                type: "bit",
                nullable: true,
                defaultValueSql: "((1))",
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                schema: "idn",
                table: "User",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "AddedTime",
                schema: "idn",
                table: "User",
                type: "datetime",
                nullable: false,
                defaultValueSql: "(getdate())",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<int>(
                name: "AddedByUserId",
                schema: "idn",
                table: "User",
                type: "int",
                nullable: false,
                defaultValueSql: "((1))",
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<DateTime>(
                name: "AccountLockTime",
                schema: "idn",
                table: "User",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<byte>(
                name: "AccountLockout",
                schema: "idn",
                table: "User",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.AddColumn<string>(
                name: "MobilePhoneNumber",
                schema: "idn",
                table: "User",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "WorkShiftId",
                schema: "idn",
                table: "User",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ParentRoleId",
                schema: "idn",
                table: "RoleClaim",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "BasicTicketState",
                schema: "lst",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BasicStateDescription = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: false),
                    AddedByUserId = table.Column<int>(type: "int", nullable: false, defaultValueSql: "((1))"),
                    AddedTime = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    LastModifiedByUserId = table.Column<int>(type: "int", nullable: true),
                    LastModifiedTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true, defaultValueSql: "((1))"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BasicTicketState", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BasicTicketState_User_AddedByUserId",
                        column: x => x.AddedByUserId,
                        principalSchema: "idn",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BasicTicketState_User_LastModifiedByUserId",
                        column: x => x.LastModifiedByUserId,
                        principalSchema: "idn",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Location",
                schema: "lst",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LocationName = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    LocationNumber = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    LocationDescription = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: false),
                    LatitudeLongitude = table.Column<Geometry>(type: "geography", nullable: true),
                    LocationHierarchy = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: false),
                    HierarchicalParentId = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: true),
                    AddedByUserId = table.Column<int>(type: "int", nullable: false, defaultValueSql: "((1))"),
                    AddedTime = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    LastModifiedByUserId = table.Column<int>(type: "int", nullable: true),
                    LastModifiedTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true, defaultValueSql: "((1))"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Location", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Location_AddedByUserId_User_Id",
                        column: x => x.AddedByUserId,
                        principalSchema: "idn",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Location_LastModifiedByUserId_User_Id",
                        column: x => x.LastModifiedByUserId,
                        principalSchema: "idn",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LocationV2",
                schema: "lst",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HierarchyId = table.Column<HierarchyId>(type: "hierarchyid", nullable: false),
                    OldHierarchyId = table.Column<HierarchyId>(type: "hierarchyid", nullable: true),
                    LocationName = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    LocationNumber = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    LocationDescription = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: false),
                    LatitudeLongitude = table.Column<Geometry>(type: "geography", nullable: true),
                    AddedByUserId = table.Column<int>(type: "int", nullable: false, defaultValueSql: "((1))"),
                    AddedTime = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    LastModifiedByUserId = table.Column<int>(type: "int", nullable: true),
                    LastModifiedTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true, defaultValueSql: "((1))"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LocationV2", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LocationV2_AddedByUserId_User_Id",
                        column: x => x.AddedByUserId,
                        principalSchema: "idn",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LocationV2_LastModifiedByUserId_User_Id",
                        column: x => x.LastModifiedByUserId,
                        principalSchema: "idn",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MenuPage",
                schema: "lst",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    HierarchicalOrder = table.Column<int>(type: "int", nullable: false),
                    HierarchicalLevel = table.Column<int>(type: "int", nullable: false),
                    ParentId = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    MenuText = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    UrlPath = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    MenuIcon = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    ExpandOnStart = table.Column<bool>(type: "bit", nullable: false),
                    MenuImagePath = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    AddedByUserId = table.Column<int>(type: "int", nullable: false, defaultValueSql: "((1))"),
                    AddedTime = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    LastModifiedByUserId = table.Column<int>(type: "int", nullable: true),
                    LastModifiedTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true, defaultValueSql: "((1))"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MenuPage", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MenuPage_AddedByUserId_User_Id",
                        column: x => x.AddedByUserId,
                        principalSchema: "idn",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MenuPage_LastModifiedByUserId_User_Id",
                        column: x => x.LastModifiedByUserId,
                        principalSchema: "idn",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MenuPageV2",
                schema: "lst",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HierarchyId = table.Column<HierarchyId>(type: "hierarchyid", nullable: false),
                    OldHierarchyId = table.Column<HierarchyId>(type: "hierarchyid", nullable: true),
                    MenuText = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    UrlPath = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    MenuIcon = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    ExpandOnStart = table.Column<bool>(type: "bit", nullable: false),
                    MenuImagePath = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    AddedByUserId = table.Column<int>(type: "int", nullable: false, defaultValueSql: "((1))"),
                    AddedTime = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    LastModifiedByUserId = table.Column<int>(type: "int", nullable: true),
                    LastModifiedTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true, defaultValueSql: "((1))"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MenuPageV2", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MenuPageV2_AddedByUserId_User_Id",
                        column: x => x.AddedByUserId,
                        principalSchema: "idn",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MenuPageV2_LastModifiedByUserId_User_Id",
                        column: x => x.LastModifiedByUserId,
                        principalSchema: "idn",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ResolutionFormChoiceOption",
                schema: "adm",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ChoiceOptionText = table.Column<string>(type: "varchar(1000)", unicode: false, maxLength: 1000, nullable: false),
                    AddedByUserId = table.Column<int>(type: "int", nullable: false, defaultValueSql: "((1))"),
                    AddedTime = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    LastModifiedByUserId = table.Column<int>(type: "int", nullable: true),
                    LastModifiedTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true, defaultValueSql: "((1))"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResolutionFormChoiceOption", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ResolutionFormChoiceOption_AddedByUserId_User_Id",
                        column: x => x.AddedByUserId,
                        principalSchema: "idn",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ResolutionFormChoiceOption_LastModifiedByUserId_User_Id",
                        column: x => x.LastModifiedByUserId,
                        principalSchema: "idn",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ResolutionFormQuestionType",
                schema: "lst",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QuestionType = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    AddedByUserId = table.Column<int>(type: "int", nullable: false, defaultValueSql: "((1))"),
                    AddedTime = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    LastModifiedByUserId = table.Column<int>(type: "int", nullable: true),
                    LastModifiedTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true, defaultValueSql: "((1))"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResolutionFormQuestionType", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ResolutionFormQuestionType_AddedByUserId_User_Id",
                        column: x => x.AddedByUserId,
                        principalSchema: "idn",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ResolutionFormQuestionType_LastModifiedByUserId_User_Id",
                        column: x => x.LastModifiedByUserId,
                        principalSchema: "idn",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ResolutionFormSingleQuestion",
                schema: "adm",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TicketFormId = table.Column<int>(type: "int", nullable: false),
                    QuestionText = table.Column<string>(type: "varchar(2000)", unicode: false, maxLength: 2000, nullable: false),
                    IsMandatory = table.Column<bool>(type: "bit", nullable: true),
                    AddedByUserId = table.Column<int>(type: "int", nullable: false, defaultValueSql: "((1))"),
                    AddedTime = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    LastModifiedByUserId = table.Column<int>(type: "int", nullable: true),
                    LastModifiedTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true, defaultValueSql: "((1))"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResolutionFormSingleQuestion", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ResolutionFormSingleQuestion_AddedByUserId_User_Id",
                        column: x => x.AddedByUserId,
                        principalSchema: "idn",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ResolutionFormSingleQuestion_LastModifiedByUserId_User_Id",
                        column: x => x.LastModifiedByUserId,
                        principalSchema: "idn",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TicketPriority",
                schema: "lst",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", unicode: false, maxLength: 200, nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: true),
                    AddedByUserId = table.Column<int>(type: "int", nullable: false, defaultValueSql: "((1))"),
                    AddedTime = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    LastModifiedByUserId = table.Column<int>(type: "int", nullable: true),
                    LastModifiedTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true, defaultValueSql: "((1))"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TicketPriority", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TicketReasonCategory",
                schema: "adm",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryName = table.Column<string>(type: "varchar(250)", unicode: false, maxLength: 250, nullable: false),
                    CategoryDescription = table.Column<string>(type: "varchar(250)", unicode: false, maxLength: 250, nullable: false),
                    CategoryHierarchy = table.Column<string>(type: "varchar(250)", unicode: false, maxLength: 250, nullable: false),
                    HierarchicalParentId = table.Column<string>(type: "varchar(250)", unicode: false, maxLength: 250, nullable: true),
                    AddedByUserId = table.Column<int>(type: "int", nullable: false, defaultValueSql: "((1))"),
                    AddedTime = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    LastModifiedByUserId = table.Column<int>(type: "int", nullable: true),
                    LastModifiedTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true, defaultValueSql: "((1))"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TicketReasonCategory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TicketReasonCategory_AddedByUserId_User_Id",
                        column: x => x.AddedByUserId,
                        principalSchema: "idn",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TicketReasonCategory_LastModifiedByUserId_User_Id",
                        column: x => x.LastModifiedByUserId,
                        principalSchema: "idn",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TicketReasonCategoryV2",
                schema: "adm",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HierarchyId = table.Column<HierarchyId>(type: "hierarchyid", nullable: false),
                    OldHierarchyId = table.Column<HierarchyId>(type: "hierarchyid", nullable: true),
                    CategoryName = table.Column<string>(type: "varchar(250)", unicode: false, maxLength: 250, nullable: false),
                    CategoryDescription = table.Column<string>(type: "varchar(250)", unicode: false, maxLength: 250, nullable: false),
                    AddedByUserId = table.Column<int>(type: "int", nullable: false, defaultValueSql: "((1))"),
                    AddedTime = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    LastModifiedByUserId = table.Column<int>(type: "int", nullable: true),
                    LastModifiedTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true, defaultValueSql: "((1))"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TicketReasonCategoryV2", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TicketReasonCategoryV2_AddedByUserId_User_Id",
                        column: x => x.AddedByUserId,
                        principalSchema: "idn",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TicketReasonCategoryV2_LastModifiedByUserId_User_Id",
                        column: x => x.LastModifiedByUserId,
                        principalSchema: "idn",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TicketState",
                schema: "lst",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StateDescription = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: false),
                    AddedByUserId = table.Column<int>(type: "int", nullable: false, defaultValueSql: "((1))"),
                    AddedTime = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    LastModifiedByUserId = table.Column<int>(type: "int", nullable: true),
                    LastModifiedTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true, defaultValueSql: "((1))"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TicketState", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TicketState_AddedByUserId_User_Id",
                        column: x => x.AddedByUserId,
                        principalSchema: "idn",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TicketState_LastModifiedByUserId_User_Id",
                        column: x => x.LastModifiedByUserId,
                        principalSchema: "idn",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserMenuPage",
                schema: "adm",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApplicationPageId = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    ApplicationUserId = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    AddedByUserId = table.Column<int>(type: "int", nullable: false, defaultValueSql: "((1))"),
                    AddedTime = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    LastModifiedByUserId = table.Column<int>(type: "int", nullable: true),
                    LastModifiedTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true, defaultValueSql: "((1))"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserMenuPage", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserMenuPage_AddedByUserId_User_Id",
                        column: x => x.AddedByUserId,
                        principalSchema: "idn",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserMenuPage_LastModifiedByUserId_User_Id",
                        column: x => x.LastModifiedByUserId,
                        principalSchema: "idn",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "WorkShift",
                schema: "lst",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ShiftName = table.Column<string>(type: "varchar(250)", unicode: false, maxLength: 250, nullable: false),
                    ShiftStartTime = table.Column<DateTime>(type: "datetime", nullable: false),
                    ShiftEndTime = table.Column<DateTime>(type: "datetime", nullable: false),
                    AddedByUserId = table.Column<int>(type: "int", nullable: false, defaultValueSql: "((1))"),
                    AddedTime = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    LastModifiedByUserId = table.Column<int>(type: "int", nullable: true),
                    LastModifiedTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true, defaultValueSql: "((1))"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkShift", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkShift_AddedByUserId_User_Id",
                        column: x => x.AddedByUserId,
                        principalSchema: "idn",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WorkShift_LastModifiedByUserId_User_Id",
                        column: x => x.LastModifiedByUserId,
                        principalSchema: "idn",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BasicTicket",
                schema: "usr",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TicketDescription = table.Column<string>(type: "varchar(2000)", unicode: false, maxLength: 2000, nullable: false),
                    MobilePhoneNumber = table.Column<int>(type: "int", nullable: true),
                    StateId = table.Column<int>(type: "int", nullable: false, defaultValueSql: "((9))"),
                    AddedByUserId = table.Column<int>(type: "int", nullable: false, defaultValueSql: "((1))"),
                    AddedTime = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    LastModifiedByUserId = table.Column<int>(type: "int", nullable: true),
                    LastModifiedTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true, defaultValueSql: "((1))"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BasicTicket", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BasicTicket_AddedByUserId_User_Id",
                        column: x => x.AddedByUserId,
                        principalSchema: "idn",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BasicTicket_BasicTicketState_StateId_Id",
                        column: x => x.StateId,
                        principalSchema: "lst",
                        principalTable: "BasicTicketState",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BasicTicket_LastModifiedByUserId_User_Id",
                        column: x => x.LastModifiedByUserId,
                        principalSchema: "idn",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ResolutionFormQuestion",
                schema: "adm",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QuestionText = table.Column<string>(type: "varchar(1000)", unicode: false, maxLength: 1000, nullable: false),
                    ResolutionFormQuestionTypeId = table.Column<int>(type: "int", nullable: false),
                    AddedByUserId = table.Column<int>(type: "int", nullable: false, defaultValueSql: "((1))"),
                    AddedTime = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    LastModifiedByUserId = table.Column<int>(type: "int", nullable: true),
                    LastModifiedTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true, defaultValueSql: "((1))"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResolutionFormQuestion", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ResolutionFormQuestion_AddedByUserId_User_Id",
                        column: x => x.AddedByUserId,
                        principalSchema: "idn",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ResolutionFormQuestion_LastModifiedByUserId_User_Id",
                        column: x => x.LastModifiedByUserId,
                        principalSchema: "idn",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ResolutionFormQuestion_ResolutionFormQuestionTypeId_ResolutionFormQuestionType_Id",
                        column: x => x.ResolutionFormQuestionTypeId,
                        principalSchema: "lst",
                        principalTable: "ResolutionFormQuestionType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TicketReason",
                schema: "adm",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReasonName = table.Column<string>(type: "varchar(250)", unicode: false, maxLength: 250, nullable: false),
                    ReasonDescription = table.Column<string>(type: "varchar(250)", unicode: false, maxLength: 250, nullable: false),
                    ResponseTime = table.Column<int>(type: "int", nullable: false),
                    ResolutionTime = table.Column<int>(type: "int", nullable: false),
                    TicketReasonCategoryId = table.Column<int>(type: "int", nullable: false),
                    Code = table.Column<int>(type: "int", nullable: false),
                    AddedByUserId = table.Column<int>(type: "int", nullable: false, defaultValueSql: "((1))"),
                    AddedTime = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    LastModifiedByUserId = table.Column<int>(type: "int", nullable: true),
                    LastModifiedTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true, defaultValueSql: "((1))"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TicketReason", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TicketReason_AddedByUserId_User_Id",
                        column: x => x.AddedByUserId,
                        principalSchema: "idn",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TicketReason_LastModifiedByUserId_User_Id",
                        column: x => x.LastModifiedByUserId,
                        principalSchema: "idn",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TicketReason_TicketReasonCategoryId_TicketReasonCategoryV2_Id",
                        column: x => x.TicketReasonCategoryId,
                        principalSchema: "adm",
                        principalTable: "TicketReasonCategoryV2",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TicketStateTransition",
                schema: "lst",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SourceTicketStateId = table.Column<int>(type: "int", nullable: false),
                    DestinationTicketStateId = table.Column<int>(type: "int", nullable: false),
                    AddedByUserId = table.Column<int>(type: "int", nullable: false, defaultValueSql: "((1))"),
                    AddedTime = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    LastModifiedByUserId = table.Column<int>(type: "int", nullable: true),
                    LastModifiedTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true, defaultValueSql: "((1))"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TicketStateTransition", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ticketstatelevel_ticketstate",
                        column: x => x.DestinationTicketStateId,
                        principalSchema: "lst",
                        principalTable: "TicketState",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ticketstatelevel_ticketstate1",
                        column: x => x.SourceTicketStateId,
                        principalSchema: "lst",
                        principalTable: "TicketState",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TicketStateTransition_AddedByUserId_User_Id",
                        column: x => x.AddedByUserId,
                        principalSchema: "idn",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TicketStateTransition_LastModifiedByUserId_User_Id",
                        column: x => x.LastModifiedByUserId,
                        principalSchema: "idn",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AssignmentGroup",
                schema: "adm",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "Atama Grubu tanım tablosunun primary keyi")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GroupName = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false, comment: "Atama grubu adını tutar"),
                    GroupManagerUserId = table.Column<int>(type: "int", nullable: false, comment: "Atama grubunun sorumlusu HR tablosunun ID si ile tutulur FK"),
                    TicketReasonId = table.Column<int>(type: "int", nullable: false, comment: "arama nedeni tablosunun ID si"),
                    AddedByUserId = table.Column<int>(type: "int", nullable: false, defaultValueSql: "((1))"),
                    AddedTime = table.Column<DateTime>(type: "datetime", nullable: false),
                    LastModifiedByUserId = table.Column<int>(type: "int", nullable: true),
                    LastModifiedTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true, defaultValueSql: "((1))"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssignmentGroup", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AssignmentGroup_AddedByUserId_User_Id",
                        column: x => x.AddedByUserId,
                        principalSchema: "idn",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_assignmentgroup_hr",
                        column: x => x.GroupManagerUserId,
                        principalSchema: "idn",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AssignmentGroup_LastModifiedByUserId_User_Id",
                        column: x => x.LastModifiedByUserId,
                        principalSchema: "idn",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_assignmentgroup_reason",
                        column: x => x.TicketReasonId,
                        principalSchema: "adm",
                        principalTable: "TicketReason",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ResolutionForm",
                schema: "adm",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TicketReasonId = table.Column<int>(type: "int", nullable: true),
                    TicketReasonCategoryId = table.Column<int>(type: "int", nullable: true),
                    FormDescription = table.Column<string>(type: "varchar(2000)", unicode: false, maxLength: 2000, nullable: false),
                    TicketStateId = table.Column<int>(type: "int", nullable: false),
                    IsMandatory = table.Column<bool>(type: "bit", nullable: true),
                    AddedByUserId = table.Column<int>(type: "int", nullable: false, defaultValueSql: "((1))"),
                    AddedTime = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    LastModifiedByUserId = table.Column<int>(type: "int", nullable: true),
                    LastModifiedTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true, defaultValueSql: "((1))"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResolutionForm", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ResolutionForm_AddedByUserId_User_Id",
                        column: x => x.AddedByUserId,
                        principalSchema: "idn",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ResolutionForm_LastModifiedByUserId_User_Id",
                        column: x => x.LastModifiedByUserId,
                        principalSchema: "idn",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ResolutionForm_TicketReasonCategoryId_TicketReasonCategoryV2_Id",
                        column: x => x.TicketReasonCategoryId,
                        principalSchema: "adm",
                        principalTable: "TicketReasonCategoryV2",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ticketforms_reason",
                        column: x => x.TicketReasonId,
                        principalSchema: "adm",
                        principalTable: "TicketReason",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ticketforms_ticketstate",
                        column: x => x.TicketStateId,
                        principalSchema: "lst",
                        principalTable: "TicketState",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ResolutionFormV2",
                schema: "adm",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TicketReasonId = table.Column<int>(type: "int", nullable: true),
                    TicketReasonCategoryId = table.Column<int>(type: "int", nullable: true),
                    TicketStateId = table.Column<int>(type: "int", nullable: false),
                    FormXml = table.Column<string>(type: "varchar(5000)", unicode: false, maxLength: 5000, nullable: false),
                    IsMandatory = table.Column<bool>(type: "bit", nullable: true),
                    AddedByUserId = table.Column<int>(type: "int", nullable: false, defaultValueSql: "((1))"),
                    AddedTime = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    LastModifiedByUserId = table.Column<int>(type: "int", nullable: true),
                    LastModifiedTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true, defaultValueSql: "((1))"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResolutionFormV2", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ResolutionFormV2_AddedByUserId_User_Id",
                        column: x => x.AddedByUserId,
                        principalSchema: "idn",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ResolutionFormV2_LastModifiedByUserId_User_Id",
                        column: x => x.LastModifiedByUserId,
                        principalSchema: "idn",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ResolutionFormV2_TicketReasonCategoryId_TicketReasonCategoryV2_Id",
                        column: x => x.TicketReasonCategoryId,
                        principalSchema: "adm",
                        principalTable: "TicketReasonCategoryV2",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ResolutionFormV2_TicketReasonId_TicketReason_Id",
                        column: x => x.TicketReasonId,
                        principalSchema: "adm",
                        principalTable: "TicketReason",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ResolutionFormV2_TicketStateId_TicketState_Id",
                        column: x => x.TicketStateId,
                        principalSchema: "lst",
                        principalTable: "TicketState",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AssignmentGroupAuthorizedLocation",
                schema: "adm",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AssignmentGroupId = table.Column<int>(type: "int", nullable: false),
                    LocationId = table.Column<int>(type: "int", nullable: false),
                    AddedByUserId = table.Column<int>(type: "int", nullable: false, defaultValueSql: "((1))"),
                    AddedTime = table.Column<DateTime>(type: "datetime", nullable: false),
                    LastModifiedByUserId = table.Column<int>(type: "int", nullable: true),
                    LastModifiedTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true, defaultValueSql: "((1))"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssignmentGroupAuthorizedLocation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AssignmentGroupAuthorizedLocation_AddedByUserId_User_Id",
                        column: x => x.AddedByUserId,
                        principalSchema: "idn",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AssignmentGroupAuthorizedLocation_LastModifiedByUserId_User_Id",
                        column: x => x.LastModifiedByUserId,
                        principalSchema: "idn",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AssignmentGroupAuthorizedLocation_LocationId_LocationV2_Id",
                        column: x => x.LocationId,
                        principalSchema: "lst",
                        principalTable: "LocationV2",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_assignmentgroupauthorizedplaces_assignmentgroup",
                        column: x => x.AssignmentGroupId,
                        principalSchema: "adm",
                        principalTable: "AssignmentGroup",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AssignmentGroupEmployee",
                schema: "adm",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AssignmentGroupId = table.Column<int>(type: "int", nullable: false),
                    EmployeeUserId = table.Column<int>(type: "int", nullable: false),
                    AddedByUserId = table.Column<int>(type: "int", nullable: false, defaultValueSql: "((1))"),
                    AddedTime = table.Column<DateTime>(type: "datetime", nullable: false),
                    LastModifiedByUserId = table.Column<int>(type: "int", nullable: true),
                    LastModifiedTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true, defaultValueSql: "((1))"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssignmentGroupEmployee", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AssignmentGroupEmployee_AddedByUserId_User_Id",
                        column: x => x.AddedByUserId,
                        principalSchema: "idn",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_assignmentgroupemployee_assignmentgroup",
                        column: x => x.AssignmentGroupId,
                        principalSchema: "adm",
                        principalTable: "AssignmentGroup",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_assignmentgroupemployee_hr",
                        column: x => x.EmployeeUserId,
                        principalSchema: "idn",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AssignmentGroupEmployee_LastModifiedByUserId_User_Id",
                        column: x => x.LastModifiedByUserId,
                        principalSchema: "idn",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Ticket",
                schema: "usr",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TicketReportedUserId = table.Column<int>(type: "int", nullable: false),
                    TicketOwnerUserId = table.Column<string>(type: "varchar(250)", unicode: false, maxLength: 250, nullable: false),
                    TicketDescription = table.Column<string>(type: "varchar(2000)", unicode: false, maxLength: 2000, nullable: true),
                    TicketOpenedTime = table.Column<DateTime>(type: "datetime", nullable: false),
                    TicketStatusId = table.Column<int>(type: "int", nullable: false),
                    PeriodicTicketId = table.Column<int>(type: "int", nullable: true),
                    TicketAssignedUserId = table.Column<int>(type: "int", nullable: true),
                    TickedAssignedAssignmentGroupId = table.Column<int>(type: "int", nullable: true),
                    TicketReasonId = table.Column<int>(type: "int", nullable: false),
                    TicketPriority = table.Column<int>(type: "int", nullable: false),
                    BasicTicketId = table.Column<int>(type: "int", nullable: true),
                    AddedByUserId = table.Column<int>(type: "int", nullable: false, defaultValueSql: "((1))"),
                    AddedTime = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    LastModifiedByUserId = table.Column<int>(type: "int", nullable: true),
                    LastModifiedTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true, defaultValueSql: "((1))"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ticket", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ticket_AddedByUserId_User_Id",
                        column: x => x.AddedByUserId,
                        principalSchema: "idn",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ticket_assignmentgroup",
                        column: x => x.TickedAssignedAssignmentGroupId,
                        principalSchema: "adm",
                        principalTable: "AssignmentGroup",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Ticket_Basic_Tickets",
                        column: x => x.BasicTicketId,
                        principalSchema: "usr",
                        principalTable: "BasicTicket",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ticket_hr",
                        column: x => x.TicketAssignedUserId,
                        principalSchema: "idn",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Ticket_LastModifiedByUserId_User_Id",
                        column: x => x.LastModifiedByUserId,
                        principalSchema: "idn",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ticket_reason",
                        column: x => x.TicketReasonId,
                        principalSchema: "adm",
                        principalTable: "TicketReason",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ticket_ticketstate",
                        column: x => x.TicketStatusId,
                        principalSchema: "lst",
                        principalTable: "TicketState",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ResolutionFormMultipleChoice",
                schema: "adm",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QuestionText = table.Column<string>(type: "varchar(2000)", unicode: false, maxLength: 2000, nullable: false),
                    TicketFormId = table.Column<int>(type: "int", nullable: false),
                    IsMandatory = table.Column<bool>(type: "bit", nullable: false),
                    Option1Text = table.Column<string>(type: "varchar(2000)", unicode: false, maxLength: 2000, nullable: true),
                    Option2Text = table.Column<string>(type: "varchar(2000)", unicode: false, maxLength: 2000, nullable: true),
                    Option3Text = table.Column<string>(type: "varchar(2000)", unicode: false, maxLength: 2000, nullable: true),
                    Option4Text = table.Column<string>(type: "varchar(2000)", unicode: false, maxLength: 2000, nullable: true),
                    Option5Text = table.Column<string>(type: "varchar(2000)", unicode: false, maxLength: 2000, nullable: true),
                    AddedByUserId = table.Column<int>(type: "int", nullable: false, defaultValueSql: "((1))"),
                    AddedTime = table.Column<DateTime>(type: "datetime", nullable: false),
                    LastModifiedByUserId = table.Column<int>(type: "int", nullable: true),
                    LastModifiedTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true, defaultValueSql: "((1))"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResolutionFormMultipleChoice", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ResolutionFormMultipleChoice_AddedByUserId_User_Id",
                        column: x => x.AddedByUserId,
                        principalSchema: "idn",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ResolutionFormMultipleChoice_LastModifiedByUserId_User_Id",
                        column: x => x.LastModifiedByUserId,
                        principalSchema: "idn",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ticketformQwithMultiple_ticketformQwithMultiple",
                        column: x => x.TicketFormId,
                        principalSchema: "adm",
                        principalTable: "ResolutionForm",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ResolutionFormYesNo",
                schema: "adm",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TicketFormId = table.Column<int>(type: "int", nullable: false),
                    QuestionText = table.Column<string>(type: "varchar(2000)", unicode: false, maxLength: 2000, nullable: false),
                    IsMandatory = table.Column<bool>(type: "bit", nullable: false),
                    AddedByUserId = table.Column<int>(type: "int", nullable: false, defaultValueSql: "((1))"),
                    AddedTime = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    LastModifiedByUserId = table.Column<int>(type: "int", nullable: true),
                    LastModifiedTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true, defaultValueSql: "((1))"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResolutionFormYesNo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ResolutionFormYesNo_AddedByUserId_User_Id",
                        column: x => x.AddedByUserId,
                        principalSchema: "idn",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ResolutionFormYesNo_LastModifiedByUserId_User_Id",
                        column: x => x.LastModifiedByUserId,
                        principalSchema: "idn",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ticketformQwithYesNo_ticketforms",
                        column: x => x.TicketFormId,
                        principalSchema: "adm",
                        principalTable: "ResolutionForm",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ResolutionFormAnswer",
                schema: "adm",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Answer = table.Column<string>(type: "varchar(2000)", unicode: false, maxLength: 2000, nullable: true, comment: "If yes or no question it is null"),
                    YesOrNo = table.Column<bool>(type: "bit", nullable: true, comment: "If not yes or no question it is null"),
                    ResolutionFormQuestionId = table.Column<int>(type: "int", nullable: false),
                    ResolutionFormId = table.Column<int>(type: "int", nullable: false),
                    AddedByUserId = table.Column<int>(type: "int", nullable: false, defaultValueSql: "((1))"),
                    AddedTime = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    LastModifiedByUserId = table.Column<int>(type: "int", nullable: true),
                    LastModifiedTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true, defaultValueSql: "((1))"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResolutionFormAnswer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ResolutionFormAnswer_AddedByUserId_User_Id",
                        column: x => x.AddedByUserId,
                        principalSchema: "idn",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ResolutionFormAnswer_LastModifiedByUserId_User_Id",
                        column: x => x.LastModifiedByUserId,
                        principalSchema: "idn",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ResolutionFormAnswer_ResolutionFormId_ResolutionForm_Id",
                        column: x => x.ResolutionFormId,
                        principalSchema: "adm",
                        principalTable: "ResolutionFormV2",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ResolutionFormAnswer_ResolutionFormQuestionId_ResolutionFormQuestion_Id",
                        column: x => x.ResolutionFormQuestionId,
                        principalSchema: "adm",
                        principalTable: "ResolutionFormQuestion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ResolutionFormQuestionAnswer",
                schema: "adm",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ResolutionFormId = table.Column<int>(type: "int", nullable: false),
                    TicketId = table.Column<int>(type: "int", nullable: false),
                    QuestionText = table.Column<string>(type: "varchar(2000)", unicode: false, maxLength: 2000, nullable: false),
                    AnswerText = table.Column<string>(type: "varchar(2000)", unicode: false, maxLength: 2000, nullable: false),
                    AddedByUserId = table.Column<int>(type: "int", nullable: false, defaultValueSql: "((1))"),
                    AddedTime = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    LastModifiedByUserId = table.Column<int>(type: "int", nullable: true),
                    LastModifiedTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true, defaultValueSql: "((1))"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResolutionFormQuestionAnswer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ResolutionFormQuestionAnswer_AddedByUserId_User_Id",
                        column: x => x.AddedByUserId,
                        principalSchema: "idn",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ResolutionFormQuestionAnswer_LastModifiedByUserId_User_Id",
                        column: x => x.LastModifiedByUserId,
                        principalSchema: "idn",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ticketformQAnswers_ticket",
                        column: x => x.TicketId,
                        principalSchema: "usr",
                        principalTable: "Ticket",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ticketformQAnswers_ticketforms",
                        column: x => x.ResolutionFormId,
                        principalSchema: "adm",
                        principalTable: "ResolutionForm",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TicketAuditHistory",
                schema: "usr",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TicketId = table.Column<int>(type: "int", nullable: false),
                    HistoryType = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    HistoryAddTime = table.Column<DateTime>(type: "datetime", nullable: false),
                    PreviousTicketStateId = table.Column<int>(type: "int", nullable: true),
                    NextTicketStateId = table.Column<int>(type: "int", nullable: true),
                    ActivityStartTime = table.Column<DateTime>(type: "datetime", nullable: true),
                    ActivityEndTime = table.Column<DateTime>(type: "datetime", nullable: true),
                    LocationId = table.Column<int>(type: "int", nullable: true),
                    PreviousTicketAssignedUserId = table.Column<int>(type: "int", nullable: true),
                    NextTicketAssignedUserId = table.Column<int>(type: "int", nullable: true),
                    PreviousAssignedAssignmentGroupId = table.Column<int>(type: "int", nullable: true),
                    NextAssignedAssignmentGroupId = table.Column<int>(type: "int", nullable: true),
                    AddedByUserId = table.Column<int>(type: "int", nullable: false),
                    AddedTime = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    LastModifiedByUserId = table.Column<int>(type: "int", nullable: true),
                    LastModifiedTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true, defaultValueSql: "((1))"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TicketAuditHistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TicketAuditHistory_AddedByUserId_User_Id",
                        column: x => x.AddedByUserId,
                        principalSchema: "idn",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TicketAuditHistory_LastModifiedByUserId_User_Id",
                        column: x => x.LastModifiedByUserId,
                        principalSchema: "idn",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TicketAuditHistory_LocationId_LocationV2_Id",
                        column: x => x.LocationId,
                        principalSchema: "lst",
                        principalTable: "LocationV2",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ticketHistory_assignmentgroup",
                        column: x => x.PreviousAssignedAssignmentGroupId,
                        principalSchema: "adm",
                        principalTable: "AssignmentGroup",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ticketHistory_assignmentgroup1",
                        column: x => x.NextAssignedAssignmentGroupId,
                        principalSchema: "adm",
                        principalTable: "AssignmentGroup",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ticketHistory_hr1",
                        column: x => x.PreviousTicketAssignedUserId,
                        principalSchema: "idn",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ticketHistory_hr2",
                        column: x => x.NextTicketAssignedUserId,
                        principalSchema: "idn",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ticketHistory_ticket",
                        column: x => x.TicketId,
                        principalSchema: "usr",
                        principalTable: "Ticket",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ticketHistory_ticketstate",
                        column: x => x.PreviousTicketStateId,
                        principalSchema: "lst",
                        principalTable: "TicketState",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ticketHistory_ticketstate1",
                        column: x => x.NextTicketStateId,
                        principalSchema: "lst",
                        principalTable: "TicketState",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TicketNote",
                schema: "usr",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NoteText = table.Column<string>(type: "varchar(2000)", unicode: false, maxLength: 2000, nullable: false),
                    TicketId = table.Column<int>(type: "int", nullable: false),
                    AddedByUserId = table.Column<int>(type: "int", nullable: false, defaultValueSql: "((1))"),
                    AddedTime = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    LastModifiedByUserId = table.Column<int>(type: "int", nullable: true),
                    LastModifiedTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true, defaultValueSql: "((1))"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TicketNote", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ticket_Notes_Ticket",
                        column: x => x.TicketId,
                        principalSchema: "usr",
                        principalTable: "Ticket",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TicketNote_AddedByUserId_User_Id",
                        column: x => x.AddedByUserId,
                        principalSchema: "idn",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TicketNote_LastModifiedByUserId_User_Id",
                        column: x => x.LastModifiedByUserId,
                        principalSchema: "idn",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TicketRelatedLocation",
                schema: "usr",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TicketId = table.Column<int>(type: "int", nullable: false),
                    TicketLocationId = table.Column<int>(type: "int", nullable: false),
                    AddedByUserId = table.Column<int>(type: "int", nullable: false, defaultValueSql: "((1))"),
                    AddedTime = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    LastModifiedByUserId = table.Column<int>(type: "int", nullable: true),
                    LastModifiedTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true, defaultValueSql: "((1))"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TicketRelatedLocation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TicketRelatedLocation_AddedByUserId_User_Id",
                        column: x => x.AddedByUserId,
                        principalSchema: "idn",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TicketRelatedLocation_LastModifiedByUserId_User_Id",
                        column: x => x.LastModifiedByUserId,
                        principalSchema: "idn",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TicketRelatedLocation_TicketLocationId_LocationV2_Id",
                        column: x => x.TicketLocationId,
                        principalSchema: "lst",
                        principalTable: "LocationV2",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ticketRelations_ticket",
                        column: x => x.TicketId,
                        principalSchema: "usr",
                        principalTable: "Ticket",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Attachment",
                schema: "usr",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TicketId = table.Column<int>(type: "int", nullable: true),
                    AttachmentDescription = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: true),
                    FolderName = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: false),
                    TicketNoteId = table.Column<int>(type: "int", nullable: true),
                    BasicTicketId = table.Column<int>(type: "int", nullable: true),
                    FileType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FileData = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    AddedByUserId = table.Column<int>(type: "int", nullable: false, defaultValueSql: "((1))"),
                    AddedTime = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    LastModifiedByUserId = table.Column<int>(type: "int", nullable: true),
                    LastModifiedTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true, defaultValueSql: "((1))"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attachment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Attachment_AddedByUserId_User_Id",
                        column: x => x.AddedByUserId,
                        principalSchema: "idn",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Attachment_LastModifiedByUserId_User_Id",
                        column: x => x.LastModifiedByUserId,
                        principalSchema: "idn",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Attachments_Basic_Ticket",
                        column: x => x.BasicTicketId,
                        principalSchema: "usr",
                        principalTable: "BasicTicket",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Attachments_Ticket_Notes",
                        column: x => x.TicketNoteId,
                        principalSchema: "usr",
                        principalTable: "TicketNote",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ticketattachments_ticketattachments",
                        column: x => x.TicketId,
                        principalSchema: "usr",
                        principalTable: "Ticket",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PeriodicTicket",
                schema: "usr",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PeriodicName = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    StartDateTime = table.Column<DateTime>(type: "datetime", nullable: false),
                    EndDateTime = table.Column<DateTime>(type: "datetime", nullable: false),
                    TicketPriority = table.Column<int>(type: "int", nullable: false),
                    LocationId = table.Column<int>(type: "int", nullable: true),
                    PeriodFrequency = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    TicketReasonId = table.Column<int>(type: "int", nullable: false),
                    AddedByUserId = table.Column<int>(type: "int", nullable: false, defaultValueSql: "((1))"),
                    AddedTime = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    LastModifiedByUserId = table.Column<int>(type: "int", nullable: true),
                    LastModifiedTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true, defaultValueSql: "((1))"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PeriodicTicket", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PeriodicTicket_AddedByUserId_User_Id",
                        column: x => x.AddedByUserId,
                        principalSchema: "idn",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PeriodicTicket_LastModifiedByUserId_User_Id",
                        column: x => x.LastModifiedByUserId,
                        principalSchema: "idn",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_periodicticketdefinitions_place",
                        column: x => x.LocationId,
                        principalSchema: "usr",
                        principalTable: "TicketRelatedLocation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_periodicticketdefinitions_reason",
                        column: x => x.TicketReasonId,
                        principalSchema: "adm",
                        principalTable: "TicketReason",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_User_AddedByUserId",
                schema: "idn",
                table: "User",
                column: "AddedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_User_LastModifiedByUserId",
                schema: "idn",
                table: "User",
                column: "LastModifiedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_User_WorkShiftId",
                schema: "idn",
                table: "User",
                column: "WorkShiftId");

            migrationBuilder.CreateIndex(
                name: "IX_AssignmentGroup_AddedByUserId",
                schema: "adm",
                table: "AssignmentGroup",
                column: "AddedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_AssignmentGroup_GroupManagerUserId",
                schema: "adm",
                table: "AssignmentGroup",
                column: "GroupManagerUserId");

            migrationBuilder.CreateIndex(
                name: "IX_AssignmentGroup_LastModifiedByUserId",
                schema: "adm",
                table: "AssignmentGroup",
                column: "LastModifiedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_AssignmentGroup_TicketReasonId",
                schema: "adm",
                table: "AssignmentGroup",
                column: "TicketReasonId");

            migrationBuilder.CreateIndex(
                name: "IX_AssignmentGroupAuthorizedLocation_AddedByUserId",
                schema: "adm",
                table: "AssignmentGroupAuthorizedLocation",
                column: "AddedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_AssignmentGroupAuthorizedLocation_AssignmentGroupId",
                schema: "adm",
                table: "AssignmentGroupAuthorizedLocation",
                column: "AssignmentGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_AssignmentGroupAuthorizedLocation_LastModifiedByUserId",
                schema: "adm",
                table: "AssignmentGroupAuthorizedLocation",
                column: "LastModifiedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_AssignmentGroupAuthorizedLocation_LocationId",
                schema: "adm",
                table: "AssignmentGroupAuthorizedLocation",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_AssignmentGroupEmployee_AddedByUserId",
                schema: "adm",
                table: "AssignmentGroupEmployee",
                column: "AddedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_AssignmentGroupEmployee_AssignmentGroupId",
                schema: "adm",
                table: "AssignmentGroupEmployee",
                column: "AssignmentGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_AssignmentGroupEmployee_EmployeeUserId",
                schema: "adm",
                table: "AssignmentGroupEmployee",
                column: "EmployeeUserId");

            migrationBuilder.CreateIndex(
                name: "IX_AssignmentGroupEmployee_LastModifiedByUserId",
                schema: "adm",
                table: "AssignmentGroupEmployee",
                column: "LastModifiedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Attachment_AddedByUserId",
                schema: "usr",
                table: "Attachment",
                column: "AddedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Attachment_BasicTicketId",
                schema: "usr",
                table: "Attachment",
                column: "BasicTicketId");

            migrationBuilder.CreateIndex(
                name: "IX_Attachment_LastModifiedByUserId",
                schema: "usr",
                table: "Attachment",
                column: "LastModifiedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Attachment_TicketId",
                schema: "usr",
                table: "Attachment",
                column: "TicketId");

            migrationBuilder.CreateIndex(
                name: "IX_Attachment_TicketNoteId",
                schema: "usr",
                table: "Attachment",
                column: "TicketNoteId");

            migrationBuilder.CreateIndex(
                name: "IX_BasicTicket_AddedByUserId",
                schema: "usr",
                table: "BasicTicket",
                column: "AddedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_BasicTicket_LastModifiedByUserId",
                schema: "usr",
                table: "BasicTicket",
                column: "LastModifiedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_BasicTicket_StateId",
                schema: "usr",
                table: "BasicTicket",
                column: "StateId");

            migrationBuilder.CreateIndex(
                name: "IX_BasicTicketState_AddedByUserId",
                schema: "lst",
                table: "BasicTicketState",
                column: "AddedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_BasicTicketState_LastModifiedByUserId",
                schema: "lst",
                table: "BasicTicketState",
                column: "LastModifiedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Location_AddedByUserId",
                schema: "lst",
                table: "Location",
                column: "AddedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Location_LastModifiedByUserId",
                schema: "lst",
                table: "Location",
                column: "LastModifiedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_LocationV2_AddedByUserId",
                schema: "lst",
                table: "LocationV2",
                column: "AddedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_LocationV2_LastModifiedByUserId",
                schema: "lst",
                table: "LocationV2",
                column: "LastModifiedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_MenuPage_AddedByUserId",
                schema: "lst",
                table: "MenuPage",
                column: "AddedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_MenuPage_LastModifiedByUserId",
                schema: "lst",
                table: "MenuPage",
                column: "LastModifiedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_MenuPageV2_AddedByUserId",
                schema: "lst",
                table: "MenuPageV2",
                column: "AddedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_MenuPageV2_LastModifiedByUserId",
                schema: "lst",
                table: "MenuPageV2",
                column: "LastModifiedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_PeriodicTicket_AddedByUserId",
                schema: "usr",
                table: "PeriodicTicket",
                column: "AddedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_PeriodicTicket_LastModifiedByUserId",
                schema: "usr",
                table: "PeriodicTicket",
                column: "LastModifiedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_PeriodicTicket_LocationId",
                schema: "usr",
                table: "PeriodicTicket",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_PeriodicTicket_TicketReasonId",
                schema: "usr",
                table: "PeriodicTicket",
                column: "TicketReasonId");

            migrationBuilder.CreateIndex(
                name: "IX_ResolutionForm_AddedByUserId",
                schema: "adm",
                table: "ResolutionForm",
                column: "AddedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ResolutionForm_LastModifiedByUserId",
                schema: "adm",
                table: "ResolutionForm",
                column: "LastModifiedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ResolutionForm_TicketReasonCategoryId",
                schema: "adm",
                table: "ResolutionForm",
                column: "TicketReasonCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ResolutionForm_TicketReasonId",
                schema: "adm",
                table: "ResolutionForm",
                column: "TicketReasonId");

            migrationBuilder.CreateIndex(
                name: "IX_ResolutionForm_TicketStateId",
                schema: "adm",
                table: "ResolutionForm",
                column: "TicketStateId");

            migrationBuilder.CreateIndex(
                name: "IX_ResolutionFormAnswer_AddedByUserId",
                schema: "adm",
                table: "ResolutionFormAnswer",
                column: "AddedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ResolutionFormAnswer_LastModifiedByUserId",
                schema: "adm",
                table: "ResolutionFormAnswer",
                column: "LastModifiedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ResolutionFormAnswer_ResolutionFormId",
                schema: "adm",
                table: "ResolutionFormAnswer",
                column: "ResolutionFormId");

            migrationBuilder.CreateIndex(
                name: "IX_ResolutionFormAnswer_ResolutionFormQuestionId",
                schema: "adm",
                table: "ResolutionFormAnswer",
                column: "ResolutionFormQuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_ResolutionFormChoiceOption_AddedByUserId",
                schema: "adm",
                table: "ResolutionFormChoiceOption",
                column: "AddedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ResolutionFormChoiceOption_LastModifiedByUserId",
                schema: "adm",
                table: "ResolutionFormChoiceOption",
                column: "LastModifiedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ResolutionFormMultipleChoice_AddedByUserId",
                schema: "adm",
                table: "ResolutionFormMultipleChoice",
                column: "AddedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ResolutionFormMultipleChoice_LastModifiedByUserId",
                schema: "adm",
                table: "ResolutionFormMultipleChoice",
                column: "LastModifiedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ResolutionFormMultipleChoice_TicketFormId",
                schema: "adm",
                table: "ResolutionFormMultipleChoice",
                column: "TicketFormId");

            migrationBuilder.CreateIndex(
                name: "IX_ResolutionFormQuestion_AddedByUserId",
                schema: "adm",
                table: "ResolutionFormQuestion",
                column: "AddedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ResolutionFormQuestion_LastModifiedByUserId",
                schema: "adm",
                table: "ResolutionFormQuestion",
                column: "LastModifiedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ResolutionFormQuestion_ResolutionFormQuestionTypeId",
                schema: "adm",
                table: "ResolutionFormQuestion",
                column: "ResolutionFormQuestionTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ResolutionFormQuestionAnswer_AddedByUserId",
                schema: "adm",
                table: "ResolutionFormQuestionAnswer",
                column: "AddedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ResolutionFormQuestionAnswer_LastModifiedByUserId",
                schema: "adm",
                table: "ResolutionFormQuestionAnswer",
                column: "LastModifiedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ResolutionFormQuestionAnswer_ResolutionFormId",
                schema: "adm",
                table: "ResolutionFormQuestionAnswer",
                column: "ResolutionFormId");

            migrationBuilder.CreateIndex(
                name: "IX_ResolutionFormQuestionAnswer_TicketId",
                schema: "adm",
                table: "ResolutionFormQuestionAnswer",
                column: "TicketId");

            migrationBuilder.CreateIndex(
                name: "IX_ResolutionFormQuestionType_AddedByUserId",
                schema: "lst",
                table: "ResolutionFormQuestionType",
                column: "AddedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ResolutionFormQuestionType_LastModifiedByUserId",
                schema: "lst",
                table: "ResolutionFormQuestionType",
                column: "LastModifiedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ResolutionFormSingleQuestion_AddedByUserId",
                schema: "adm",
                table: "ResolutionFormSingleQuestion",
                column: "AddedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ResolutionFormSingleQuestion_LastModifiedByUserId",
                schema: "adm",
                table: "ResolutionFormSingleQuestion",
                column: "LastModifiedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ResolutionFormV2_AddedByUserId",
                schema: "adm",
                table: "ResolutionFormV2",
                column: "AddedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ResolutionFormV2_LastModifiedByUserId",
                schema: "adm",
                table: "ResolutionFormV2",
                column: "LastModifiedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ResolutionFormV2_TicketReasonCategoryId",
                schema: "adm",
                table: "ResolutionFormV2",
                column: "TicketReasonCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ResolutionFormV2_TicketReasonId",
                schema: "adm",
                table: "ResolutionFormV2",
                column: "TicketReasonId");

            migrationBuilder.CreateIndex(
                name: "IX_ResolutionFormV2_TicketStateId",
                schema: "adm",
                table: "ResolutionFormV2",
                column: "TicketStateId");

            migrationBuilder.CreateIndex(
                name: "IX_ResolutionFormYesNo_AddedByUserId",
                schema: "adm",
                table: "ResolutionFormYesNo",
                column: "AddedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ResolutionFormYesNo_LastModifiedByUserId",
                schema: "adm",
                table: "ResolutionFormYesNo",
                column: "LastModifiedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ResolutionFormYesNo_TicketFormId",
                schema: "adm",
                table: "ResolutionFormYesNo",
                column: "TicketFormId");

            migrationBuilder.CreateIndex(
                name: "IX_Ticket_AddedByUserId",
                schema: "usr",
                table: "Ticket",
                column: "AddedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Ticket_BasicTicketId",
                schema: "usr",
                table: "Ticket",
                column: "BasicTicketId");

            migrationBuilder.CreateIndex(
                name: "IX_Ticket_LastModifiedByUserId",
                schema: "usr",
                table: "Ticket",
                column: "LastModifiedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Ticket_TickedAssignedAssignmentGroupId",
                schema: "usr",
                table: "Ticket",
                column: "TickedAssignedAssignmentGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Ticket_TicketAssignedUserId",
                schema: "usr",
                table: "Ticket",
                column: "TicketAssignedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Ticket_TicketReasonId",
                schema: "usr",
                table: "Ticket",
                column: "TicketReasonId");

            migrationBuilder.CreateIndex(
                name: "IX_Ticket_TicketStatusId",
                schema: "usr",
                table: "Ticket",
                column: "TicketStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_TicketAuditHistory_AddedByUserId",
                schema: "usr",
                table: "TicketAuditHistory",
                column: "AddedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_TicketAuditHistory_LastModifiedByUserId",
                schema: "usr",
                table: "TicketAuditHistory",
                column: "LastModifiedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_TicketAuditHistory_LocationId",
                schema: "usr",
                table: "TicketAuditHistory",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_TicketAuditHistory_NextAssignedAssignmentGroupId",
                schema: "usr",
                table: "TicketAuditHistory",
                column: "NextAssignedAssignmentGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_TicketAuditHistory_NextTicketAssignedUserId",
                schema: "usr",
                table: "TicketAuditHistory",
                column: "NextTicketAssignedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_TicketAuditHistory_NextTicketStateId",
                schema: "usr",
                table: "TicketAuditHistory",
                column: "NextTicketStateId");

            migrationBuilder.CreateIndex(
                name: "IX_TicketAuditHistory_PreviousAssignedAssignmentGroupId",
                schema: "usr",
                table: "TicketAuditHistory",
                column: "PreviousAssignedAssignmentGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_TicketAuditHistory_PreviousTicketAssignedUserId",
                schema: "usr",
                table: "TicketAuditHistory",
                column: "PreviousTicketAssignedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_TicketAuditHistory_PreviousTicketStateId",
                schema: "usr",
                table: "TicketAuditHistory",
                column: "PreviousTicketStateId");

            migrationBuilder.CreateIndex(
                name: "IX_TicketAuditHistory_TicketId",
                schema: "usr",
                table: "TicketAuditHistory",
                column: "TicketId");

            migrationBuilder.CreateIndex(
                name: "IX_TicketNote_AddedByUserId",
                schema: "usr",
                table: "TicketNote",
                column: "AddedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_TicketNote_LastModifiedByUserId",
                schema: "usr",
                table: "TicketNote",
                column: "LastModifiedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_TicketNote_TicketId",
                schema: "usr",
                table: "TicketNote",
                column: "TicketId");

            migrationBuilder.CreateIndex(
                name: "IX_TicketReason_AddedByUserId",
                schema: "adm",
                table: "TicketReason",
                column: "AddedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_TicketReason_LastModifiedByUserId",
                schema: "adm",
                table: "TicketReason",
                column: "LastModifiedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_TicketReason_TicketReasonCategoryId",
                schema: "adm",
                table: "TicketReason",
                column: "TicketReasonCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_TicketReasonCategory_AddedByUserId",
                schema: "adm",
                table: "TicketReasonCategory",
                column: "AddedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_TicketReasonCategory_LastModifiedByUserId",
                schema: "adm",
                table: "TicketReasonCategory",
                column: "LastModifiedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_TicketReasonCategoryV2_AddedByUserId",
                schema: "adm",
                table: "TicketReasonCategoryV2",
                column: "AddedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_TicketReasonCategoryV2_LastModifiedByUserId",
                schema: "adm",
                table: "TicketReasonCategoryV2",
                column: "LastModifiedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_TicketRelatedLocation_AddedByUserId",
                schema: "usr",
                table: "TicketRelatedLocation",
                column: "AddedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_TicketRelatedLocation_LastModifiedByUserId",
                schema: "usr",
                table: "TicketRelatedLocation",
                column: "LastModifiedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_TicketRelatedLocation_TicketId",
                schema: "usr",
                table: "TicketRelatedLocation",
                column: "TicketId");

            migrationBuilder.CreateIndex(
                name: "IX_TicketRelatedLocation_TicketLocationId",
                schema: "usr",
                table: "TicketRelatedLocation",
                column: "TicketLocationId");

            migrationBuilder.CreateIndex(
                name: "IX_TicketState_AddedByUserId",
                schema: "lst",
                table: "TicketState",
                column: "AddedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_TicketState_LastModifiedByUserId",
                schema: "lst",
                table: "TicketState",
                column: "LastModifiedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_TicketStateTransition_AddedByUserId",
                schema: "lst",
                table: "TicketStateTransition",
                column: "AddedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_TicketStateTransition_DestinationTicketStateId",
                schema: "lst",
                table: "TicketStateTransition",
                column: "DestinationTicketStateId");

            migrationBuilder.CreateIndex(
                name: "IX_TicketStateTransition_LastModifiedByUserId",
                schema: "lst",
                table: "TicketStateTransition",
                column: "LastModifiedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_TicketStateTransition_SourceTicketStateId",
                schema: "lst",
                table: "TicketStateTransition",
                column: "SourceTicketStateId");

            migrationBuilder.CreateIndex(
                name: "IX_UserMenuPage_AddedByUserId",
                schema: "adm",
                table: "UserMenuPage",
                column: "AddedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserMenuPage_LastModifiedByUserId",
                schema: "adm",
                table: "UserMenuPage",
                column: "LastModifiedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkShift_AddedByUserId",
                schema: "lst",
                table: "WorkShift",
                column: "AddedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkShift_LastModifiedByUserId",
                schema: "lst",
                table: "WorkShift",
                column: "LastModifiedByUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_hr_shift",
                schema: "idn",
                table: "User",
                column: "WorkShiftId",
                principalSchema: "lst",
                principalTable: "WorkShift",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_User_AddedByUserId_User_Id",
                schema: "idn",
                table: "User",
                column: "AddedByUserId",
                principalSchema: "idn",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_User_LastModifiedByUserId_User_Id",
                schema: "idn",
                table: "User",
                column: "LastModifiedByUserId",
                principalSchema: "idn",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_hr_shift",
                schema: "idn",
                table: "User");

            migrationBuilder.DropForeignKey(
                name: "FK_User_AddedByUserId_User_Id",
                schema: "idn",
                table: "User");

            migrationBuilder.DropForeignKey(
                name: "FK_User_LastModifiedByUserId_User_Id",
                schema: "idn",
                table: "User");

            migrationBuilder.DropTable(
                name: "AssignmentGroupAuthorizedLocation",
                schema: "adm");

            migrationBuilder.DropTable(
                name: "AssignmentGroupEmployee",
                schema: "adm");

            migrationBuilder.DropTable(
                name: "Attachment",
                schema: "usr");

            migrationBuilder.DropTable(
                name: "Location",
                schema: "lst");

            migrationBuilder.DropTable(
                name: "MenuPage",
                schema: "lst");

            migrationBuilder.DropTable(
                name: "MenuPageV2",
                schema: "lst");

            migrationBuilder.DropTable(
                name: "PeriodicTicket",
                schema: "usr");

            migrationBuilder.DropTable(
                name: "ResolutionFormAnswer",
                schema: "adm");

            migrationBuilder.DropTable(
                name: "ResolutionFormChoiceOption",
                schema: "adm");

            migrationBuilder.DropTable(
                name: "ResolutionFormMultipleChoice",
                schema: "adm");

            migrationBuilder.DropTable(
                name: "ResolutionFormQuestionAnswer",
                schema: "adm");

            migrationBuilder.DropTable(
                name: "ResolutionFormSingleQuestion",
                schema: "adm");

            migrationBuilder.DropTable(
                name: "ResolutionFormYesNo",
                schema: "adm");

            migrationBuilder.DropTable(
                name: "TicketAuditHistory",
                schema: "usr");

            migrationBuilder.DropTable(
                name: "TicketPriority",
                schema: "lst");

            migrationBuilder.DropTable(
                name: "TicketReasonCategory",
                schema: "adm");

            migrationBuilder.DropTable(
                name: "TicketStateTransition",
                schema: "lst");

            migrationBuilder.DropTable(
                name: "UserMenuPage",
                schema: "adm");

            migrationBuilder.DropTable(
                name: "WorkShift",
                schema: "lst");

            migrationBuilder.DropTable(
                name: "TicketNote",
                schema: "usr");

            migrationBuilder.DropTable(
                name: "TicketRelatedLocation",
                schema: "usr");

            migrationBuilder.DropTable(
                name: "ResolutionFormV2",
                schema: "adm");

            migrationBuilder.DropTable(
                name: "ResolutionFormQuestion",
                schema: "adm");

            migrationBuilder.DropTable(
                name: "ResolutionForm",
                schema: "adm");

            migrationBuilder.DropTable(
                name: "LocationV2",
                schema: "lst");

            migrationBuilder.DropTable(
                name: "Ticket",
                schema: "usr");

            migrationBuilder.DropTable(
                name: "ResolutionFormQuestionType",
                schema: "lst");

            migrationBuilder.DropTable(
                name: "AssignmentGroup",
                schema: "adm");

            migrationBuilder.DropTable(
                name: "BasicTicket",
                schema: "usr");

            migrationBuilder.DropTable(
                name: "TicketState",
                schema: "lst");

            migrationBuilder.DropTable(
                name: "TicketReason",
                schema: "adm");

            migrationBuilder.DropTable(
                name: "BasicTicketState",
                schema: "lst");

            migrationBuilder.DropTable(
                name: "TicketReasonCategoryV2",
                schema: "adm");

            migrationBuilder.DropIndex(
                name: "IX_User_AddedByUserId",
                schema: "idn",
                table: "User");

            migrationBuilder.DropIndex(
                name: "IX_User_LastModifiedByUserId",
                schema: "idn",
                table: "User");

            migrationBuilder.DropIndex(
                name: "IX_User_WorkShiftId",
                schema: "idn",
                table: "User");

            migrationBuilder.DropColumn(
                name: "AccountLockTime",
                schema: "idn",
                table: "User");

            migrationBuilder.DropColumn(
                name: "AccountLockout",
                schema: "idn",
                table: "User");

            migrationBuilder.DropColumn(
                name: "MobilePhoneNumber",
                schema: "idn",
                table: "User");

            migrationBuilder.DropColumn(
                name: "WorkShiftId",
                schema: "idn",
                table: "User");

            migrationBuilder.DropColumn(
                name: "ParentRoleId",
                schema: "idn",
                table: "RoleClaim");

            migrationBuilder.AlterDatabase(
                oldCollation: "Turkish_CI_AS");

            migrationBuilder.AlterColumn<string>(
                name: "UserName",
                schema: "idn",
                table: "User",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(50)",
                oldUnicode: false,
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "PasswordHash",
                schema: "idn",
                table: "User",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(1000)",
                oldUnicode: false,
                oldMaxLength: 1000);

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                schema: "idn",
                table: "User",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<bool>(
                name: "IsActive",
                schema: "idn",
                table: "User",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true,
                oldDefaultValueSql: "((1))");

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                schema: "idn",
                table: "User",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<DateTime>(
                name: "AddedTime",
                schema: "idn",
                table: "User",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldDefaultValueSql: "(getdate())");

            migrationBuilder.AlterColumn<int>(
                name: "AddedByUserId",
                schema: "idn",
                table: "User",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValueSql: "((1))");

            migrationBuilder.AddColumn<string>(
                name: "test",
                schema: "idn",
                table: "RoleClaim",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
