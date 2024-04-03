
using Microsoft.EntityFrameworkCore;
using Persistence;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

// 1. DbContext

builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseSqlServer(
        configuration.GetConnectionString("DefaultConnection"),
        sqlServerOptionsAction: sqlOptions =>
        {
            sqlOptions.EnableRetryOnFailure();
        });
});


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();



app.Run();