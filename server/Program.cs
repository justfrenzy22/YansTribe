using DotNetEnv;
using server.managers;
using server.views;

var builder = WebApplication.CreateBuilder(args);

Env.Load();

// Console.WriteLine("DB_CONN: " + Env.GetString("DB_CONN"));
// Add services to the container.

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



// app.MapStaticAssets();

// Map all Controllers automatically
app.MapControllers();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
