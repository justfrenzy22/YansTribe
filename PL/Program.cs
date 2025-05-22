using System.Text;
using bll.interfaces;
using bll.services;
using bll.views;
using dal.interfaces.db;
using dal.interfaces.repo;
using dal.mapper;
using dal.queries;
using dal.repo;
using pl.middleware;

using DotNetEnv;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.IdentityModel.Tokens;
using pl.views;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);

// Load environment variables
Env.Load();

// Connection string
string connString = "Server=localhost;database=yanstribe;Integrated Security=True;TrustServerCertificate=True";

// Register application services
builder.Services.AddControllersWithViews();

// Register views
builder.Services.AddTransient<UserView>();
builder.Services.AddTransient<PostView>();
builder.Services.AddTransient<FriendView>();

// Register queries
builder.Services.AddTransient<AdminQuery>();
builder.Services.AddTransient<UserQuery>();
builder.Services.AddTransient<PostQuery>();
builder.Services.AddTransient<FriendQuery>();

// Mapper
builder.Services.AddTransient<UserMapper>();

// Register repositories
builder.Services.AddTransient<IDBRepo>(prov => new DBRepo(connString));
builder.Services.AddTransient<IAdminRepo>(provider =>
{
    var dbRepo = provider.GetRequiredService<IDBRepo>();
    var adminQuery = provider.GetRequiredService<AdminQuery>();
    var userQuery = provider.GetRequiredService<UserQuery>();
    var mapper = provider.GetRequiredService<UserMapper>();

    return new AdminRepo(dbRepo, adminQuery, userQuery, mapper);
});
builder.Services.AddTransient<IUserRepo>(provider =>
{
    var dbRepo = provider.GetRequiredService<IDBRepo>();
    var userQuery = provider.GetRequiredService<UserQuery>();
    var mapper = provider.GetRequiredService<UserMapper>();

    return new UserRepo(dbRepo, userQuery, mapper);
});
builder.Services.AddTransient<IPostRepo>(provider =>
{
    var dbRepo = provider.GetRequiredService<IDBRepo>();
    var postQuery = provider.GetRequiredService<PostQuery>();

    return new PostRepo(dbRepo, postQuery);
});
builder.Services.AddTransient<IFriendRepo>(provider =>
{
    var dbRepo = provider.GetRequiredService<IDBRepo>();
    var friendQuery = provider.GetRequiredService<FriendQuery>();

    return new FriendRepo(dbRepo, friendQuery);
});


// Register services
builder.Services.AddTransient<IAdminService, AdminService>();
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<IAuthService, AuthService>();
builder.Services.AddTransient<IHashService, HashService>();
builder.Services.AddTransient<IPostService, PostService>();
builder.Services.AddTransient<IFileService, FileService>();
builder.Services.AddTransient<IFriendService, FriendService>();
builder.Services.AddTransient<INotificationsService, NotificationService>();

builder.Services.Configure<FormOptions>(options =>
{
    options.MultipartBodyLengthLimit = 104857600; // 100 MB
});


// Admin Auth Middleware
builder.Services.AddScoped<AdminAuth>();
builder.Services.AddScoped<UserAuth>();
builder.Services.AddScoped<SuperAdminAuthFilter>();


// Configure CORS to allow specific frontend
builder.Services.AddCors(options =>
{
    options.AddPolicy("allow_frontend", policy =>
        policy.WithOrigins("http://192.168.0.101:3002", "http://localhost:3000", "http://192.168.1.100:3000")
            .AllowCredentials() // Allows cookies to be sent
            .AllowAnyHeader()
            .AllowAnyMethod()
            .WithExposedHeaders("auth_token")); // Exposes the auth_token header
});

// Configure JWT authentication
string adminKey = builder.Configuration["Jwt:AdminKey"] ?? throw new InvalidOperationException("Admin JWT Key is not configured in appsettings.");
string userKey = builder.Configuration["Jwt:UserKey"] ?? throw new InvalidOperationException("User JWT Key is not configured in appsettings.");
string host = builder.Configuration["Jwt:Host"] ?? throw new InvalidOperationException("JWT Host is not configured in appsettings.");

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = "yanstribe",
            ValidAudience = host,
            IssuerSigningKeys = new List<SecurityKey>
            {
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(adminKey)),
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(userKey))
            }
        };
    });

// url configuration
var vpnOn = builder.Configuration["VPN:Enabled"]?.ToLower() == "true";
Console.WriteLine($"VPN enabled: {vpnOn}");
if (vpnOn)
{
    builder.WebHost.UseUrls("http://10.123.105.3:5114", "http://localhost:5114");
}
else
{
    builder.WebHost.UseUrls("http://localhost:5114");
    //  "http://192.168.1.100:5114"
}
// Build application
var app = builder.Build();

// Configure the HTTP request pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts(); // Use HTTP Strict Transport Security for non-development environments
}

// cdn configuration
string contentRoot = app.Environment.ContentRootPath;
string cdnFolderName = app.Configuration.GetValue<string>("StaticFiles:CdnFolderName") ?? "cdn";
int cacheMaxAge = app.Configuration.GetValue<int?>("StaticFiles:CacheMaxAgeSeconds") ?? 604800;

string cdnRootPath = Path.Combine(contentRoot, cdnFolderName);

StaticFileOptions CreateStaticFileOpts(string physicalPathSegment, string requestPathSegment)
{
    return new StaticFileOptions
    {
        FileProvider = new PhysicalFileProvider(Path.Combine(cdnRootPath, physicalPathSegment)),
        RequestPath = $"/{cdnFolderName}/{requestPathSegment}",
        OnPrepareResponse = ctx =>
        {
            ctx.Context.Response.Headers.Append("Cache-Control", $"public, max-age={cacheMaxAge}, immutable");
        }
    };
}

// cdn/images/posts
app.UseStaticFiles(CreateStaticFileOpts(Path.Combine("images", "posts"), "images/posts"));

// cdn/images/profile_pics
app.UseStaticFiles(CreateStaticFileOpts(Path.Combine("images", "profile_pics"), "images/profile_pics"));

// cdn/images/stories
app.UseStaticFiles(CreateStaticFileOpts(Path.Combine("images", "stories"), "images/stories"));

// cdn/videos/posts
app.UseStaticFiles(CreateStaticFileOpts(Path.Combine("videos", "posts"), "videos/posts"));

// cdn/videos/stories
app.UseStaticFiles(CreateStaticFileOpts(Path.Combine("videos", "stories"), "videos/stories"));
// --- End Static Files Configuration ---


app.UseRouting(); // Enable routing
app.UseMiddleware<ExceptionMiddleware>();
app.UseAuthentication(); // Use authentication
app.UseAuthorization(); // Use authorization
app.MapControllers();
app.UseHttpsRedirection(); // Redirect HTTP requests to HTTPS
app.UseStaticFiles(); // Serve static files
app.UseCors("allow_frontend"); // Use CORS policy

app.MapControllers();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Admin}/{action=Index}/{id?}");

app.Run();