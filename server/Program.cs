using System.Text;
using dal.admin;
using dal.interfaces;
using dal.queries;
using dal.repo;
using dal.user;
using DotNetEnv;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using server.managers;
using server.services;
using server.views;

var builder = WebApplication.CreateBuilder(args);

Env.Load();

// Console.WriteLine("DB_CONN: " + Env.GetString("DB_CONN"));
// // Add services to the container.
// builder.Services.AddAuthentication(options =>
// {
//     options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
//     options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
// })
// .AddJwtBearer(options =>
// {
// #pragma warning disable CS8604 // Possible null reference argument.
//     options.TokenValidationParameters = new TokenValidationParameters
//     {
//         ValidateIssuer = true,
//         ValidateAudience = true,
//         ValidateLifetime = true,
//         ValidateIssuerSigningKey = true,
//         ValidIssuer = builder.Configuration["Jwt:Issuer"],
//         ValidAudience = builder.Configuration["Jwt:Audience"],
//         IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
//     };
// #pragma warning restore CS8604 // Possible null reference argument.
// });


// dbi546373_facebook

//

// string connString = "Server=ACER/justf;Database=test;";
builder.Services.AddTransient<UserView>();

builder.Services.AddControllersWithViews();


string connString = "Server=localhost;database=yanstribe;Integrated Security=True;TrustServerCertificate=True";

builder.Services.AddTransient<IAdminService, AdminService>();
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<AdminQuery>();
builder.Services.AddTransient<UserQuery>();
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

builder.Services.AddTransient<IDBRepo>(prov => new DBRepo(connString));
builder.Services.AddTransient<IAuthService, AuthService>();

builder.Services.AddCors(options =>
    {
        options.AddPolicy("AllowAll",
        policy => policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()
    );
    });

// JWT
// credit to https://auth0.com/blog/how-to-validate-jwt-dotnet/; https://medium.com/@softsusanta/how-to-implement-jwt-token-authentication-in-asp-net-core-api-833385ad60cc


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
            // IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)),
            IssuerSigningKeys = new List<SecurityKey> { new SymmetricSecurityKey(Encoding.UTF8.GetBytes(adminKey)), new SymmetricSecurityKey(Encoding.UTF8.GetBytes(userKey)) }
        };
    });





// builder.Services.AddAuthentication(options =>
// {
//     options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
//     options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
// }).AddJwtBearer(options =>
// {
//     var key = builder.Configuration["Jwt:Key"];
//     if (string.IsNullOrEmpty(key))
//     {
//         throw new InvalidOperationException("JWT Key is not configured in appsettings.");
//     }

//     options.TokenValidationParameters = new TokenValidationParameters
//     {
//         ValidateIssuer = true,
//         ValidateAudience = true,
//         ValidateLifetime = true,
//         ValidateIssuerSigningKey = true,
//         ValidIssuer = builder.Configuration["Jwt:Issuer"],
//         ValidAudience = builder.Configuration["Jwt:Audience"],
//         IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key))
//     };
// });

// string dbConn = Env.GetString("DB_CONN", "defaultStr");
// builder.Services.AddScoped<UserManager>(provider => new UserManager(dbConn));

// Views
// builder.Services.AddTransient<HomeView>();
// builder.Services.AddTransient<UserView>();


builder.WebHost.UseUrls("http://10.123.105.3:5114", "http://localhost:5114");


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseCors("AllowAll");


app.UseStaticFiles();

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.UseAuthentication();

// CORS
// app.UseCors(builder =>
//     builder.WithOrigins("*")
//         .AllowAnyHeader()
//         .AllowAnyMethod()
//         .AllowCredentials());


// app.MapStaticAssets();

// Map all Controllers automatically
app.MapControllers();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Admin}/{action=Index}/{id?}");

app.Run();
