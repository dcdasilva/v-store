using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using VStore.Api.Domain.Services;
using VStoreApi.Domain.Contracts;
using VStoreApi.Infrastructure.Contracts;
using VStoreApi.Infrastructure.Data;
using VStoreApi.Infrastructure.Extensions;
using VStoreApi.Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);
builder.AddJwt();
builder.Services.AddDbContext<VStoreContext>(options =>
{
  var connectionString = builder.Configuration.GetConnectionString("MySql");
  if (!string.IsNullOrEmpty(connectionString))
  {
    options.UseMySQL(connectionString);
  }
});
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IVehicleService, VehicleService>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddVStoreSwagger();
builder.Services.AddAuthorization();

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();
app.RegisterAuthEndpoints();
app.RegisterVehicleEndpoints();

if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI(options =>
  {
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    options.RoutePrefix = string.Empty;
  });
}

app.Run();