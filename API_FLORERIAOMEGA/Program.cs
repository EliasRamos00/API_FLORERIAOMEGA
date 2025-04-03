using API_FLORERIAOMEGA.Repositories;
using API_FLORERIAOMEGA.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<DatabaseService>();
builder.Services.AddScoped<RCategorias>();
builder.Services.AddScoped<RArticulos>();
builder.Services.AddScoped<RUsuarios>();
builder.Services.AddScoped<RVentas>();
builder.Services.AddScoped<RHistoriales>();






var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
