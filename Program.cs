using HomeBankingMinHub.Repositories;
using HomeBankingMinHub.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using HomeBankingMinHub.Repositories.Interface;
using Microsoft.AspNetCore.Authentication.Cookies;
using HomeBankingMinHub.Services;
using HomeBankingMinHub.Services.Impl;

var builder = WebApplication.CreateBuilder(args);


// Add DbContext
builder.Services.AddDbContext<HomeBankingContext>(options =>options.UseSqlServer(builder.Configuration.GetConnectionString("HomeBankingConexion")));

// Add Controllers
builder.Services.AddControllers();
builder.Services.AddControllers().AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

//Add Services
builder.Services.AddScoped<IClientService,ClientService>();
builder.Services.AddScoped<IAccountService,AccountService>();
builder.Services.AddScoped<ICardService,CardService>();
builder.Services.AddScoped<ITransactionService,TransactionService>();
builder.Services.AddScoped<ILoanService,LoanService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddScoped<IClientRepository,ClientRepository>();
builder.Services.AddScoped<IAccountRepository,AccountRepository>();
builder.Services.AddScoped<ICardRepository,CardRepository>();
builder.Services.AddScoped<ITransactionsRepository,TransactionsRepository>();
builder.Services.AddScoped<ILoanRepository,LoanRepository>();
builder.Services.AddScoped<IClientLoanRepository,ClientLoanRepository>();

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

//Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}

//Permite usar los archivos del root
app.UseStaticFiles();
app.UseDefaultFiles();

app.UseRouting();

//Indica que la applicacion tendra autenticacion.
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();