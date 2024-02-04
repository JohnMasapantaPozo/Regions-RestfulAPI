using Microsoft.EntityFrameworkCore;
using RestfulDEMO.API.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


/* DB Context class injection:
    Add connection string to the DB context class.
    Now the DB Context Class should be available to be used accross all the application from any controler and from any repository.
*/

builder.Services.AddDbContext<RestfulDbContextA>(
    (options) => options.UseSqlServer(builder.Configuration.GetConnectionString("RestFulDEMOConnectionString"))
    );

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

app.Run();
