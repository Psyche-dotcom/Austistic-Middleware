using AlpaStock.Api.Extension;
using Austistic.Api.Seed;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureLibrary(builder.Configuration);
builder.Services.ConfigureDb(builder.Configuration);
builder.Services.ConfigureServices(builder.Configuration);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();


var app = builder.Build();
Seeder.SeedData(app).Wait();
// Configure the HTTP request pipeline.
/*if (app.Environment.IsDevelopment())
{*/
app.UseSwagger();
    app.UseSwaggerUI();
/*}*/

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
