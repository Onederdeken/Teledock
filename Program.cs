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
builder.Services.AddMyDependencyGroup(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
    });
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseEndpoints(endpoints=>endpoints.MapControllers());


app.Run();

