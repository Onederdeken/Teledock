using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Teledock.Abstractions;
using Teledock.dbContext;
using Teledock.Repositories;
using Teledock.Services;

namespace Teledock.DI
{
    public static  class CustomDependencyInjectionHandler
    {
        public static IServiceCollection AddMyDependencyGroup(WebApplicationBuilder builder)
        {
            builder.Services.AddControllers();
            builder.Services.AddDbContext<Db>(Options=>
                Options.UseMySql(builder.Configuration.GetConnectionString("DbConnection"), ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DbConnection")), m=>m.MigrationsAssembly("Teledock.csproj")));
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddSingleton<IClientRepositories, ClientRepositories>();
            builder.Services.AddSingleton<IClientService, ClientService>();
            return builder.Services;
        }
        
    }
}