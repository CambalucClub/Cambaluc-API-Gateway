using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReverseProxy.Store.EFCore.Migrations
{
    public partial class AddDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Clusters",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    LoadBalancingPolicy = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clusters", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "SessionAffinityOptionSettings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Key = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Value = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SessionAffinityOptionSettings", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Destinations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Address = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Health = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ClusterId = table.Column<string>(type: "varchar(255)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Destinations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Destinations_Clusters_ClusterId",
                        column: x => x.ClusterId,
                        principalTable: "Clusters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "HealthCheckOptions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    AvailableDestinationsPolicy = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ClusterId = table.Column<string>(type: "varchar(255)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HealthCheckOptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HealthCheckOptions_Clusters_ClusterId",
                        column: x => x.ClusterId,
                        principalTable: "Clusters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ProxyHttpClientOptions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    SslProtocols = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DangerousAcceptAnyServerCertificate = table.Column<bool>(type: "tinyint(1)", nullable: true),
                    MaxConnectionsPerServer = table.Column<int>(type: "int", nullable: true),
                    EnableMultipleHttp2Connections = table.Column<bool>(type: "tinyint(1)", nullable: true),
                    RequestHeaderEncoding = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ClusterId = table.Column<string>(type: "varchar(255)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProxyHttpClientOptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProxyHttpClientOptions_Clusters_ClusterId",
                        column: x => x.ClusterId,
                        principalTable: "Clusters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ProxyRoutes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    RouteId = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Order = table.Column<int>(type: "int", nullable: true),
                    ClusterId = table.Column<string>(type: "varchar(255)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AuthorizationPolicy = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CorsPolicy = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProxyRoutes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProxyRoutes_Clusters_ClusterId",
                        column: x => x.ClusterId,
                        principalTable: "Clusters",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "RequestProxyOptions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ActivityTimeout = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Version = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    VersionPolicy = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AllowResponseBuffering = table.Column<bool>(type: "tinyint(1)", nullable: true),
                    ClusterId = table.Column<string>(type: "varchar(255)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequestProxyOptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RequestProxyOptions_Clusters_ClusterId",
                        column: x => x.ClusterId,
                        principalTable: "Clusters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "SessionAffinityOptions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Enabled = table.Column<bool>(type: "tinyint(1)", nullable: true),
                    Policy = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    FailurePolicy = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AffinityKeyName = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ClusterId = table.Column<string>(type: "varchar(255)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SessionAffinityOptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SessionAffinityOptions_Clusters_ClusterId",
                        column: x => x.ClusterId,
                        principalTable: "Clusters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ActiveHealthCheckOptions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Enabled = table.Column<bool>(type: "tinyint(1)", nullable: true),
                    Interval = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Timeout = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Policy = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Path = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    HealthCheckOptionsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActiveHealthCheckOptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ActiveHealthCheckOptions_HealthCheckOptions_HealthCheckOptio~",
                        column: x => x.HealthCheckOptionsId,
                        principalTable: "HealthCheckOptions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "PassiveHealthCheckOptions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Enabled = table.Column<bool>(type: "tinyint(1)", nullable: true),
                    Policy = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ReactivationPeriod = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    HealthCheckOptionsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PassiveHealthCheckOptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PassiveHealthCheckOptions_HealthCheckOptions_HealthCheckOpti~",
                        column: x => x.HealthCheckOptionsId,
                        principalTable: "HealthCheckOptions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "WebProxyConfig",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Address = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    BypassOnLocal = table.Column<bool>(type: "tinyint(1)", nullable: true),
                    UseDefaultCredentials = table.Column<bool>(type: "tinyint(1)", nullable: true),
                    HttpClientConfigId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WebProxyConfig", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WebProxyConfig_ProxyHttpClientOptions_HttpClientConfigId",
                        column: x => x.HttpClientConfigId,
                        principalTable: "ProxyHttpClientOptions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Metadatas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ClusterId = table.Column<string>(type: "varchar(255)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DestinationId = table.Column<int>(type: "int", nullable: true),
                    ProxyRouteId = table.Column<int>(type: "int", nullable: true),
                    Key = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Value = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Metadatas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Metadatas_Clusters_ClusterId",
                        column: x => x.ClusterId,
                        principalTable: "Clusters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Metadatas_Destinations_DestinationId",
                        column: x => x.DestinationId,
                        principalTable: "Destinations",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Metadatas_ProxyRoutes_ProxyRouteId",
                        column: x => x.ProxyRouteId,
                        principalTable: "ProxyRoutes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ProxyMatches",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Methods = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Hosts = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Path = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ProxyRouteId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProxyMatches", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProxyMatches_ProxyRoutes_ProxyRouteId",
                        column: x => x.ProxyRouteId,
                        principalTable: "ProxyRoutes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Transforms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Type = table.Column<int>(type: "int", nullable: false),
                    ProxyRouteId = table.Column<int>(type: "int", nullable: true),
                    Key = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Value = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transforms", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transforms_ProxyRoutes_ProxyRouteId",
                        column: x => x.ProxyRouteId,
                        principalTable: "ProxyRoutes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "SessionAffinityCookie",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Path = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Domain = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    HttpOnly = table.Column<bool>(type: "tinyint(1)", nullable: true),
                    SecurePolicy = table.Column<int>(type: "int", nullable: true),
                    SameSite = table.Column<int>(type: "int", nullable: true),
                    Expiration = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    MaxAge = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsEssential = table.Column<bool>(type: "tinyint(1)", nullable: true),
                    SessionAffinityConfigId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SessionAffinityCookie", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SessionAffinityCookie_SessionAffinityOptions_SessionAffinity~",
                        column: x => x.SessionAffinityConfigId,
                        principalTable: "SessionAffinityOptions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "RouteHeaders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Values = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Mode = table.Column<int>(type: "int", nullable: false),
                    IsCaseSensitive = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    ProxyMatchId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RouteHeaders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RouteHeaders_ProxyMatches_ProxyMatchId",
                        column: x => x.ProxyMatchId,
                        principalTable: "ProxyMatches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "RouteQueryParameter",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Values = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Mode = table.Column<int>(type: "int", nullable: false),
                    IsCaseSensitive = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    ProxyMatchId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RouteQueryParameter", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RouteQueryParameter_ProxyMatches_ProxyMatchId",
                        column: x => x.ProxyMatchId,
                        principalTable: "ProxyMatches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_ActiveHealthCheckOptions_HealthCheckOptionsId",
                table: "ActiveHealthCheckOptions",
                column: "HealthCheckOptionsId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Destinations_ClusterId",
                table: "Destinations",
                column: "ClusterId");

            migrationBuilder.CreateIndex(
                name: "IX_HealthCheckOptions_ClusterId",
                table: "HealthCheckOptions",
                column: "ClusterId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Metadatas_ClusterId",
                table: "Metadatas",
                column: "ClusterId");

            migrationBuilder.CreateIndex(
                name: "IX_Metadatas_DestinationId",
                table: "Metadatas",
                column: "DestinationId");

            migrationBuilder.CreateIndex(
                name: "IX_Metadatas_ProxyRouteId",
                table: "Metadatas",
                column: "ProxyRouteId");

            migrationBuilder.CreateIndex(
                name: "IX_PassiveHealthCheckOptions_HealthCheckOptionsId",
                table: "PassiveHealthCheckOptions",
                column: "HealthCheckOptionsId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProxyHttpClientOptions_ClusterId",
                table: "ProxyHttpClientOptions",
                column: "ClusterId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProxyMatches_ProxyRouteId",
                table: "ProxyMatches",
                column: "ProxyRouteId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProxyRoutes_ClusterId",
                table: "ProxyRoutes",
                column: "ClusterId");

            migrationBuilder.CreateIndex(
                name: "IX_RequestProxyOptions_ClusterId",
                table: "RequestProxyOptions",
                column: "ClusterId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RouteHeaders_ProxyMatchId",
                table: "RouteHeaders",
                column: "ProxyMatchId");

            migrationBuilder.CreateIndex(
                name: "IX_RouteQueryParameter_ProxyMatchId",
                table: "RouteQueryParameter",
                column: "ProxyMatchId");

            migrationBuilder.CreateIndex(
                name: "IX_SessionAffinityCookie_SessionAffinityConfigId",
                table: "SessionAffinityCookie",
                column: "SessionAffinityConfigId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SessionAffinityOptions_ClusterId",
                table: "SessionAffinityOptions",
                column: "ClusterId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Transforms_ProxyRouteId",
                table: "Transforms",
                column: "ProxyRouteId");

            migrationBuilder.CreateIndex(
                name: "IX_WebProxyConfig_HttpClientConfigId",
                table: "WebProxyConfig",
                column: "HttpClientConfigId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ActiveHealthCheckOptions");

            migrationBuilder.DropTable(
                name: "Metadatas");

            migrationBuilder.DropTable(
                name: "PassiveHealthCheckOptions");

            migrationBuilder.DropTable(
                name: "RequestProxyOptions");

            migrationBuilder.DropTable(
                name: "RouteHeaders");

            migrationBuilder.DropTable(
                name: "RouteQueryParameter");

            migrationBuilder.DropTable(
                name: "SessionAffinityCookie");

            migrationBuilder.DropTable(
                name: "SessionAffinityOptionSettings");

            migrationBuilder.DropTable(
                name: "Transforms");

            migrationBuilder.DropTable(
                name: "WebProxyConfig");

            migrationBuilder.DropTable(
                name: "Destinations");

            migrationBuilder.DropTable(
                name: "HealthCheckOptions");

            migrationBuilder.DropTable(
                name: "ProxyMatches");

            migrationBuilder.DropTable(
                name: "SessionAffinityOptions");

            migrationBuilder.DropTable(
                name: "ProxyHttpClientOptions");

            migrationBuilder.DropTable(
                name: "ProxyRoutes");

            migrationBuilder.DropTable(
                name: "Clusters");
        }
    }
}
