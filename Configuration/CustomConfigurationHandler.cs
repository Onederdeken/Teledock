using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Teledock.Abstractions;
using Teledock.dbContext;
using Teledock.Repositories;
using Teledock.Services;
using Teledock.dbContext.Interceptors;
using Teledock.MediatrHandlers.ClientHandlers;
using Teledock.Commands;
using Teledock.Queries.Clients;
using Teledock.MediatrHandlers.FounderHandlers;
using Teledock.Queries.Founders;

namespace Teledock.DI
{
    public static  class CustomConfigurationHandler
    {

        public static void AddMyDependencyGroup(this IServiceCollection Services, IConfiguration Configuration)
        {
            Services.AddControllers().AddJsonOptions(options =>
            {
                // ���������� ��������� ������������� ��� enum
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());

            });
            //�������� � DI dbcontext ��� ������ � ��
            Services.AddDbContext<Db>(Options =>
                {
                    //���������� �������� �  ����������� � �� ����� appseting.json 
                    Options.UseMySql(Configuration.GetConnectionString("DbConnection"), ServerVersion.AutoDetect(Configuration.GetConnectionString("DbConnection")), m => m.MigrationsAssembly("Teledock"));
                    //��������� ���� ��������� ����������� ������� ��
                    Options.AddInterceptors(new MyCustomInterceptorForDates());
                }
            );

            Services.AddEndpointsApiExplorer();
            //��������� � ���������� swagger
            Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "Client API", Version = "v1" });
                //�������� ��������� ���������
                options.EnableAnnotations();
            });
            //�������� � DI ��������� ����������� � �������
            Services.AddScoped<IClientRepositories, ClientRepositories>();
            Services.AddScoped<IClientService, ClientService>();
            Services.AddScoped<IFounderRepository, FounderRepositories>();
            Services.AddScoped<IFounderService, FounderService>();

            //����������� handlers ��� ���������
            Services.AddMediatR(c=>c.RegisterServicesFromAssemblies(typeof(ClientCommandHandler).Assembly, typeof(ClientCommand).Assembly));
            Services.AddMediatR(c => c.RegisterServicesFromAssemblies(typeof(ClientGetAllQueryHandler).Assembly, typeof(ClientsQueries).Assembly));
            Services.AddMediatR(c => c.RegisterServicesFromAssemblies(typeof(ClientGetByIdQueryHandler).Assembly, typeof(ClientQuery).Assembly));
            Services.AddMediatR(c => c.RegisterServicesFromAssemblies(typeof(FounderCommandHandler).Assembly, typeof(FounderCommand).Assembly));
            Services.AddMediatR(c => c.RegisterServicesFromAssemblies(typeof(FounderGetAllQueriesHandler).Assembly, typeof(FoundersQueries).Assembly));
            Services.AddMediatR(c => c.RegisterServicesFromAssemblies(typeof(FounderGetByIdQueryHandler).Assembly, typeof(FounderQuery).Assembly));



        }
        public static void AddMyMidleware(this IApplicationBuilder app)
        {
            
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });


            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseEndpoints(endpoints => endpoints.MapControllers());
        }
        
    }
}