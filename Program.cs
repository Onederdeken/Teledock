using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Teledock.Abstractions;
using Teledock.dbContext;
using Teledock.DI;
using Teledock.Repositories;
using Teledock.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

//DI
builder.Services.AddControllers();
            builder.Services.AddDbContext<Db>(Options=>
                Options.UseMySql(builder.Configuration.GetConnectionString("DbConnection"), ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DbConnection")), m=>m.MigrationsAssembly("Teledock.csproj")));
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddScoped<IClientRepositories, ClientRepositories>();
            builder.Services.AddScoped<IClientService, ClientService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseEndpoints(endpoints=>endpoints.MapControllers());


app.Run();

