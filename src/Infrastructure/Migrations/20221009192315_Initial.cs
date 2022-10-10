using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyClothesCA.Infrastructure.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    CategoryId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.CategoryId);
                });

            migrationBuilder.CreateTable(
                name: "Colours",
                columns: table => new
                {
                    ColourId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Colours", x => x.ColourId);
                });

            migrationBuilder.CreateTable(
                name: "Seasons",
                columns: table => new
                {
                    SeasonId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Seasons", x => x.SeasonId);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Images",
                columns: table => new
                {
                    ImageId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Extension = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RemoteImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AddedByApplicationUserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Images", x => x.ImageId);
                    table.ForeignKey(
                        name: "FK_Images_AspNetUsers_AddedByApplicationUserId",
                        column: x => x.AddedByApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "UsersCategories",
                columns: table => new
                {
                    ApplicationUserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CategoryId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsersCategories", x => new { x.ApplicationUserId, x.CategoryId });
                    table.ForeignKey(
                        name: "FK_UsersCategories_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UsersCategories_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Clothes",
                columns: table => new
                {
                    GarmentId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ApplicationUserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CategoryId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    SeasonId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ColourId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ImageId = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clothes", x => x.GarmentId);
                    table.ForeignKey(
                        name: "FK_Clothes_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Clothes_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "CategoryId");
                    table.ForeignKey(
                        name: "FK_Clothes_Colours_ColourId",
                        column: x => x.ColourId,
                        principalTable: "Colours",
                        principalColumn: "ColourId");
                    table.ForeignKey(
                        name: "FK_Clothes_Seasons_SeasonId",
                        column: x => x.SeasonId,
                        principalTable: "Seasons",
                        principalColumn: "SeasonId");
                });

            migrationBuilder.CreateTable(
                name: "UserClothes",
                columns: table => new
                {
                    ApplicationUserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    GarmentId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ApplicationUserId1 = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserClothes", x => new { x.ApplicationUserId, x.GarmentId });
                    table.ForeignKey(
                        name: "FK_UserClothes_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserClothes_AspNetUsers_ApplicationUserId1",
                        column: x => x.ApplicationUserId1,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UserClothes_Clothes_GarmentId",
                        column: x => x.GarmentId,
                        principalTable: "Clothes",
                        principalColumn: "GarmentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "CategoryId", "ImageUrl", "Name" },
                values: new object[,]
                {
                    { "1", "https://images.unsplash.com/photo-1620799140408-edc6dcb6d633?ixlib=rb-1.2.1&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=772&q=80", "Tops" },
                    { "2", "https://image.made-in-china.com/202f0j00KstVjilPEIov/Spring-Autumn-Solid-Children-Sweatpants-Overalls-Casual-Kids-Clothes-Trousers-Pants-for-Toddler-Baby-Boys.jpg", "Bottoms" },
                    { "3", "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRasPMutrSVbqShT-0oGXzDQzvW02zn1qxHZw&usqp=CAU", "Shoes" },
                    { "4", "https://media.istockphoto.com/photos/fashion-in-new-york-city-picture-id186543936?k=20&m=186543936&s=612x612&w=0&h=gHHBtMHDGD7EnVRZawGxKpSI_LEhYEvS49DGo3UYu4I=", "Dresses" },
                    { "5", "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcSDCbH7ePNeJPR_vH1LejSPlGtItvFEM4dc4g&usqp=CAU", "Bags" },
                    { "6", "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcR852-Y1089K1O-4Z3A5kcuYqEE2Yg9T_95Dg&usqp=CAU", "Accessories" },
                    { "7", "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTK7lBmtYRlf3xGbkpz12obB6wk61zW4-LTX7I5XwdN2jBvSslJlCS9O1r_TUR77Fts4zQ&usqp=CAU", "Outerwear" }
                });

            migrationBuilder.InsertData(
                table: "Colours",
                columns: new[] { "ColourId", "Name" },
                values: new object[,]
                {
                    { "1", "White" },
                    { "10", "Turquoise" },
                    { "11", "Mint" },
                    { "12", "Ruby" },
                    { "2", "Black" },
                    { "3", "Red" },
                    { "4", "Blue" },
                    { "5", "Orange" },
                    { "6", "Brown" },
                    { "7", "Beige" },
                    { "8", "Green" },
                    { "9", "Yellow" }
                });

            migrationBuilder.InsertData(
                table: "Seasons",
                columns: new[] { "SeasonId", "Name" },
                values: new object[,]
                {
                    { "133edefa-efd8-4e09-a978-f84d348a937a", "Spring" },
                    { "4fe18530-af92-4a1a-9e97-9df990b3c4b1", "Autumn " },
                    { "ae959e74-8642-469e-ad27-832c6c7cef85", "Summer" },
                    { "e1950f0d-ad0e-4558-8773-a2bf16a4cf25", "Winter" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Clothes_ApplicationUserId",
                table: "Clothes",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Clothes_CategoryId",
                table: "Clothes",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Clothes_ColourId",
                table: "Clothes",
                column: "ColourId");

            migrationBuilder.CreateIndex(
                name: "IX_Clothes_SeasonId",
                table: "Clothes",
                column: "SeasonId");

            migrationBuilder.CreateIndex(
                name: "IX_Images_AddedByApplicationUserId",
                table: "Images",
                column: "AddedByApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserClothes_ApplicationUserId1",
                table: "UserClothes",
                column: "ApplicationUserId1");

            migrationBuilder.CreateIndex(
                name: "IX_UserClothes_GarmentId",
                table: "UserClothes",
                column: "GarmentId");

            migrationBuilder.CreateIndex(
                name: "IX_UsersCategories_CategoryId",
                table: "UsersCategories",
                column: "CategoryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Images");

            migrationBuilder.DropTable(
                name: "UserClothes");

            migrationBuilder.DropTable(
                name: "UsersCategories");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Clothes");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Colours");

            migrationBuilder.DropTable(
                name: "Seasons");
        }
    }
}
