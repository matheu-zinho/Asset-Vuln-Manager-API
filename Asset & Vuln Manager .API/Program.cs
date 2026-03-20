using Asset___Vuln_Manager_.API.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// Direciona o caminho que o arquivo do banco de dados será criado(AddDbContext:Registra o serviço no sistema ; UseSqlite:Configura o contexto para usar o SQLite como provedor de banco de dados ; Data source )
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=vulnabilityassets.db"));

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
