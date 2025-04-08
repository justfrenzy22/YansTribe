using System.Text;
using dal.interfaces.db;
using dal.interfaces.repo;
using dal.interfaces.service;
using dal.queries;
using dal.repo;
using dal.services.admin;
using dal.services.user;

using DotNetEnv;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using server.mapper;
using server.services;
using server.views;

var builder = WebApplication.CreateBuilder(args);

// Load environment variables
Env.Load();

// Connection string
string connString = "Server=localhost;database=yanstribe;Integrated Security=True;TrustServerCertificate=True";

// Register application services
builder.Services.AddControllersWithViews();

// Register views
builder.Services.AddTransient<UserView>();

// Register mappers
builder.Services.AddTransient<AdminMapper>();
builder.Services.AddTransient<UserMapper>();

// Register queries
builder.Services.AddTransient<AdminQuery>();
builder.Services.AddTransient<UserQuery>();

// Register repositories
builder.Services.AddTransient<IDBRepo>(prov => new DBRepo(connString));
builder.Services.AddTransient<IAdminRepo>(provider =>
{
    var dbRepo = provider.GetRequiredService<IDBRepo>();
    var adminQuery = provider.GetRequiredService<AdminQuery>();
    var userQuery = provider.GetRequiredService<UserQuery>();

    return new AdminRepo(dbRepo, adminQuery, userQuery);
});
builder.Services.AddTransient<IUserRepo>(provider =>
{
    var dbRepo = provider.GetRequiredService<IDBRepo>();
    var userQuery = provider.GetRequiredService<UserQuery>();

    return new UserRepo(dbRepo, userQuery);
});

// Register services
builder.Services.AddTransient<IAdminService, AdminService>();
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<IAuthService, AuthService>();
builder.Services.AddTransient<IHashService, HashService>();

// Configure CORS to allow specific frontend
builder.Services.AddCors(options =>
{
    options.AddPolicy("allow_frontend", policy =>
        policy.WithOrigins("http://192.168.0.101:3002")
            .AllowCredentials()
            .AllowAnyHeader()
            .AllowAnyMethod()
            .WithExposedHeaders("testaccess"));
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
if (vpnOn)
{
    builder.WebHost.UseUrls("http://10.123.105.3:5114", "http://localhost:5114");
}
else
{
    builder.WebHost.UseUrls("http://localhost:5114");
}

// VPN configuration
// var vpn = new desktop_app.config.VPN();
// var status = await Task.Run(() => vpn.get());

// if (status == desktop_app.config.status.Disconnected)
// {
//     builder.WebHost.UseUrls("http://localhost:5114");
// }
// else if (status == desktop_app.config.status.Connected)
// {
//     builder.WebHost.UseUrls("http://10.123.105.3:5114", "http://localhost:5114");
// }
// else
// {
//     throw new Exception("There was a problem checking the VPN status. Check the VPN config class.");
// }

// Build application
var app = builder.Build();

// Configure the HTTP request pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts(); // Use HTTP Strict Transport Security for non-development environments
}

app.UseMiddleware<server.middleware.ExceptionMiddleware>();
app.UseHttpsRedirection(); // Redirect HTTP requests to HTTPS
app.UseStaticFiles(); // Serve static files
app.UseRouting(); // Enable routing
app.UseCors("allow_frontend"); // Use CORS policy
app.UseAuthentication(); // Use authentication
app.UseAuthorization(); // Use authorization

// Map all controllers automatically
app.MapControllers();

// Map default route
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Admin}/{action=Index}/{id?}");

// Run the application
app.Run();


// using System.Text;

// using dal.interfaces.db;
// using dal.interfaces.repo;
// using dal.interfaces.service;
// using dal.queries;
// using dal.repo;
// using dal.services.admin;
// using dal.services.user;

// using DotNetEnv;
// using Microsoft.AspNetCore.Authentication.JwtBearer;
// using Microsoft.IdentityModel.Tokens;
// using server.mapper;
// using server.services;
// using server.views;

// var builder = WebApplication.CreateBuilder(args);

// Env.Load();

// //

// // string connString = "Server=ACER/justf;Database=test;";
// builder.Services.AddTransient<UserView>();

// builder.Services.AddControllersWithViews();


// string connString = "Server=localhost;database=yanstribe;Integrated Security=True;TrustServerCertificate=True";

// builder.Services.AddTransient<IAdminService, AdminService>();
// builder.Services.AddTransient<IUserService, UserService>();
// builder.Services.AddTransient<AdminQuery>();
// builder.Services.AddTransient<UserQuery>();
// builder.Services.AddTransient<IAdminRepo>(provider =>
// {
//     var dbRepo = provider.GetRequiredService<IDBRepo>();
//     var adminQuery = provider.GetRequiredService<AdminQuery>();
//     var userQuery = provider.GetRequiredService<UserQuery>();

//     return new AdminRepo(dbRepo, adminQuery, userQuery);
// });
// builder.Services.AddTransient<IUserRepo>(provider =>
// {
//     var dbRepo = provider.GetRequiredService<IDBRepo>();
//     var userQuery = provider.GetRequiredService<UserQuery>();

//     return new UserRepo(dbRepo, userQuery);
// });

// builder.Services.AddTransient<AdminMapper>();
// builder.Services.AddTransient<UserMapper>();

// builder.Services.AddTransient<IDBRepo>(prov => new DBRepo(connString));
// builder.Services.AddTransient<IAuthService, AuthService>();
// builder.Services.AddTransient<IHashService, HashService>();

// builder.Services.AddCors(options =>
//     {
//         options.AddPolicy("allow_frontend",
//         policy => policy
//             .WithOrigins("http://192.168.0.101:3002")
//             .AllowCredentials()
//             .AllowAnyHeader()
//             .AllowAnyMethod()
//             .WithExposedHeaders("testaccess")
//         );
//     });


// // JWT
// // credit to https://auth0.com/blog/how-to-validate-jwt-dotnet/; https://medium.com/@softsusanta/how-to-implement-jwt-token-authentication-in-asp-net-core-api-833385ad60cc
// string adminKey = builder.Configuration["Jwt:AdminKey"] ?? throw new InvalidOperationException("Admin JWT Key is not configured in appsettings.");
// string userKey = builder.Configuration["Jwt:UserKey"] ?? throw new InvalidOperationException("User JWT Key is not configured in appsettings.");
// string host = builder.Configuration["Jwt:Host"] ?? throw new InvalidOperationException("JWT Host is not configured in appsettings.");
// builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
//     .AddJwtBearer(options =>
//     {
//         options.TokenValidationParameters = new TokenValidationParameters
//         {
//             ValidateIssuer = true,
//             ValidateAudience = true,
//             ValidateLifetime = true,
//             ValidateIssuerSigningKey = true,
//             ValidIssuer = "yanstribe",
//             ValidAudience = host,
//             IssuerSigningKeys = new List<SecurityKey> { new SymmetricSecurityKey(Encoding.UTF8.GetBytes(adminKey)), new SymmetricSecurityKey(Encoding.UTF8.GetBytes(userKey)) }
//         };
//     });


// // credit to myself for VPN config https://github.com/justfrenzy22/desktop_app

// var vpn = new desktop_app.config.VPN();

// var status = await Task.Run(() => vpn.get());

// if (status == desktop_app.config.status.Disconnected)
// {
//     builder.WebHost.UseUrls("http://localhost:5114");
// }
// else if (status == desktop_app.config.status.Connected)
// {
//     builder.WebHost.UseUrls("http://10.123.105.3:5114", "http://localhost:5114");
// }
// else
// {
//     throw new Exception("There was problem checking the VPN status. Check the VPN config class.");
// }


// var app = builder.Build();

// // Configure the HTTP request pipeline.
// if (!app.Environment.IsDevelopment())
// {
//     app.UseExceptionHandler("/Home/Error");
//     // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
//     app.UseHsts();
// }


// app.UseStaticFiles();

// app.UseHttpsRedirection();

// app.UseRouting();

// app.UseAuthorization();

// app.UseAuthentication();

// //use CORS
// app.UseCors("allow_frontend");
// // app.UseCors(builder =>
// //     builder.WithOrigins("*")
// //         .AllowAnyHeader()
// //         .AllowAnyMethod()
// //         .AllowCredentials());


// // app.MapStaticAssets();

// // Map all Controllers automatically
// app.MapControllers();

// app.MapControllerRoute(
//     name: "default",
//     pattern: "{controller=Admin}/{action=Index}/{id?}");

// app.Run();
