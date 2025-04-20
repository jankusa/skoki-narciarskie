using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MvcSkoki.Data;
using System.Globalization;
using System.Data.SQLite;
using MvcSkoki;

var cultureInfo = CultureInfo.InvariantCulture;
CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;

SQLiteConnection connection = new SQLiteConnection("Data Source=MvcSkoki.Data.db;Version=3;");
connection.Open();
DatabasesLoader.ClearAllTables(connection);
DatabasesLoader.LoadCsvDataToTable(connection, "Sezon", "dane/Sezon.csv");
DatabasesLoader.LoadCsvDataToTable(connection, "Skoczek", "dane/Skoczkowie.csv");
DatabasesLoader.LoadCsvDataToTable(connection, "Skocznia", "dane/Skocznie.csv");
DatabasesLoader.LoadCsvDataToTable(connection, "Konkurs", "dane/Konkursy.csv");
DatabasesLoader.LoadCsvDataToTable(connection, "Punktacja", "dane/Punkty.csv");
connection.Close();

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<MvcSkokiContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("MvcSkokiContext") ?? throw new InvalidOperationException("Connection string 'MvcSkokiContext' not found.")));

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSession();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseSession(); // Use session middleware before UseAuthorization
app.UseAuthorization();
app.Use(async (ctx, next) =>
{
    await next();

    if(ctx.Response.StatusCode == 404 && !ctx.Response.HasStarted)
    {
        //Re-execute the request so the user gets the error page
        string originalPath = ctx.Request.Path.Value;
        ctx.Items["originalPath"] = originalPath;
        ctx.Request.Path = "/IO/Logowanie";
        await next();
    }
});

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();