using System.Text;
using DotNetEnv;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using server.managers;
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

builder.Services.AddControllersWithViews();


// string dbConn = Env.GetString("DB_CONN", "defaultStr");
// builder.Services.AddScoped<UserManager>(provider => new UserManager(dbConn));

// Views
// builder.Services.AddTransient<HomeView>();
// builder.Services.AddTransient<UserView>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}




app.UseStaticFiles();

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.UseAuthentication();



// app.MapStaticAssets();

// Map all Controllers automatically
app.MapControllers();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Admin}/{action=Index}/{id?}");

app.Run();
