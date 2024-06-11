using cSharpBird.Api;
using cSharpBird.API;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserStorageEF, UserStorageEFRepo>();

builder.Services.AddScoped<IChecklistService, ChecklistService>();
builder.Services.AddScoped<IChecklistStorageEF, ChecklistStorageEFRepo>();

builder.Services.AddScoped<IBirdService, BirdService>();
builder.Services.AddScoped<IBirdStorageEF, BirdStorageEFRepo>();

string connectionString = ConnectionStringHelper.GetConnectionString();

builder.Services.AddDbContext<cSharpBirdContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(policy => policy.AllowAnyMethod());

app.UseAuthorization();

app.MapControllers();

app.Run();
