using Greenployee.CORE.Business;
using Greenployee.MODELS.Data;
using Greenployee.MODELS.Model;
using Microsoft.EntityFrameworkCore;

var myAllowEspecificOrigins = "myAllowEspecificOrigins";

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"), b => b.MigrationsAssembly("Greenployee.API"));
});

builder.Services.AddScoped<IAnotacaoBusiness, AnotacaoBusiness>();
builder.Services.AddScoped<IMetaBusiness, MetaBusiness>();
builder.Services.AddScoped<IOrdemServicoBusiness, OrdemServicoBusiness>();
builder.Services.AddScoped<IMetaBusiness, MetaBusiness>();
builder.Services.AddScoped<IOrdemServicoItemBusiness, OrdemServicoItemBusiness>();
builder.Services.AddScoped<IPessoaBusiness, PessoaBusiness>();
builder.Services.AddScoped<IPessoaMetaBusiness, PessoaMetaBusiness>();

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: myAllowEspecificOrigins, 
                      builder => 
                      { 
                          builder.WithOrigins("http://localhost:4200").AllowAnyMethod().AllowAnyHeader();
                      });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(myAllowEspecificOrigins);

app.UseAuthorization();

app.MapControllers();

app.Run();
