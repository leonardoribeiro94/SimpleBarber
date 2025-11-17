using SimpleBarber.Api.Domain;
using SimpleBarber.Api.Extensions.cs;
using SimpleBarber.Api.Infrastructure.Repositories;
using SimpleBarber.Api.Services;
using SimpleBarber.Api.Settings;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();

// Add services to the container.

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerConfiguration();

// dependency injections
builder.Services.AddScoped<JwtServices, JwtServices>();
builder.Services.AddScoped<IdentityService, IdentityService>();
builder.Services.AddScoped<ServiceRepository, ServiceRepository>();
builder.Services.AddScoped<CustomerRepository, CustomerRepository>();
builder.Services.AddScoped<AppointmentRepository, AppointmentRepository>();

//add dbcontext
var cnnstring = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddCustomDbContext(cnnstring ?? "");

// add identity service
builder.Services.AddCustomIdentity();

//add jwt configurations
var jwtSettings = builder.Configuration.GetSection("TokenConfiguration").Get<JwtTokenSettings>();
builder.Services.AddCustomJwtAuthentication(jwtSettings);

builder.Services.Configure<JwtTokenSettings>(
    builder.Configuration.GetSection("TokenConfiguration"));

var app = builder.Build();

// Configure the HTTP request pipeline.
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