using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using Teledock.Abstractions;
using Teledock.dbContext;
using Teledock.Models;
using Teledock.Repositories;
using Teledock.Services;
using Teledock.dbContext.Interceptors;

namespace Teledock.DI
{
    public static  class CustomDependencyInjectionHandler
    {

        public static void AddMyDependencyGroup(this IServiceCollection Services, IConfiguration Configuration)
        {
            Services.AddControllers().AddJsonOptions(options =>
            {
                // Используем строковое представление для enum
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());

            });
            Services.AddDbContext<Db>(Options =>
                {
                    Options.UseMySql(Configuration.GetConnectionString("DbConnection"), ServerVersion.AutoDetect(Configuration.GetConnectionString("DbConnection")), m => m.MigrationsAssembly("Teledock"));
                    Options.AddInterceptors(new MyCustomInterceptorForDates());
                }
            );
    
               
            Services.AddEndpointsApiExplorer();
            Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "Client API", Version = "v1" });

                // Добавление схемы для отображения enum как строк
                options.MapType(typeof(TypeClient), () => new OpenApiSchema
                {
                    Type = "String",
                    Enum = Enum.GetValues(typeof(TypeClient))
                   .Cast<TypeClient>()
                   .Select(e => new OpenApiString(e.ToString()))  // Перечисления как строки
                   .Cast<IOpenApiAny>()
                   .ToList()
                });
                options.SchemaFilter<EnumSchemeFilter>();
            });
            Services.AddScoped<IClientRepositories, ClientRepositories>();
            Services.AddScoped<IClientService, ClientService>();
           
        }
        
    }
    public class EnumSchemeFilter : ISchemaFilter
    {
        public void Apply(OpenApiSchema schema, SchemaFilterContext context)
        {
           if(!context.Type.IsEnum)return;
            var namevalues = new OpenApiArray();
            namevalues.AddRange(Enum.GetNames(context.Type).Select(name => new OpenApiString(name)));

            schema.Extensions.Add("x-enum-varnames", namevalues);
        }
    }
}