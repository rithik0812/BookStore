using books.Repository.Interfaces;
using books.Repository;
using Microsoft.Extensions.Configuration;
using System;
using Microsoft.EntityFrameworkCore;
using books.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<BooksContext>(options =>
        options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IBooksRepository, SQLLiteBooksRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;

try
{
    // context is essentially an instance of the DB for this project, defined in the DataContext class 
    var context = services.GetRequiredService<BooksContext>();
    // this Migrate method that is used every time we run the app, is the same as db.update for any changes we want in our db
    // every time it makes an record of the update in the Migration ID table 
    await context.Database.MigrateAsync();

    // add hardcoded seed data in DB
    await AddSeedData.SeedData(context);
}
catch (Exception ex)
{
    var logger = services.GetRequiredService<ILogger<Program>>();
    logger.LogError(ex, "An error occured during migration");
}

app.Run();
