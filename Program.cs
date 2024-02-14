using HomeBankingMinHub.Repositories;
using HomeBankingMinHub.Models;
using HomeBankingMinHub.Controllers;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using HomeBankingMinHub.Repositories.Interface;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);


// Add DbContext
builder.Services.AddDbContext<HomeBankingContext>(options =>options.UseSqlServer(builder.Configuration.GetConnectionString("HomeBankingConexion")));

// Add Controllers
builder.Services.AddControllers();
builder.Services.AddControllers().AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddScoped<IClientRepository,ClientRepository>();
builder.Services.AddScoped<IAccountRepository,AccountRepository>();
builder.Services.AddScoped<ICardRepository,CardRepository>();

//Authentication
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
.AddCookie(options =>{options.ExpireTimeSpan = TimeSpan.FromMinutes(10);options.LoginPath = new PathString("/index.html");});

//Authorization
builder.Services.AddAuthorization(options =>{options.AddPolicy("ClientOnly", policy => policy.RequireClaim("Client"));});


var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<HomeBankingContext>();
    DbInitializer.Initialize(context);
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}

app.UseStaticFiles();

app.UseDefaultFiles();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
