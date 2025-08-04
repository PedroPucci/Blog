using Blog.Application.UnitOfWork;
using Blog.Exrensions.SwaggerDocumentation;
using Blog.Infrastracture.Connections;
using Blog.Infrastracture.RepositoryUoW;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text.Json.Serialization;

namespace Blog.Exrensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(opt =>
            {
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

                opt.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "API Blog",
                    Description = @"
                    Crie um sistema básico de blog onde os usuários podem visualizar, criar, editar e excluir postagens. 
                    O projeto deve utilizar os princípios de orientação a objetos, seguir os princípios SOLID, integrar o Entity Framework para manipulação de dados 
                    e incluir uma comunicação 
                    simples usando WebSockets para notificar os usuários sobre novas postagens em tempo real.
                ",
                });


                opt.OperationFilter<CustomOperationDescriptions>();
            });

            services.AddDbContext<DataContext>(opt =>
            {
                opt.UseNpgsql(config.GetConnectionString("WebApiDatabase"));
            });

            services.AddCors(opt =>
            {
                opt.AddPolicy("CorsPolicy", policy =>
                {
                    policy.AllowAnyMethod().AllowAnyHeader().WithOrigins("http://localhost:4200");
                });
            });

            services.AddScoped<IRepositoryUoW, RepositoryUoW>();
            //services.AddScoped<TokenService>();
            //services.AddScoped<BCryptoAlgorithm>();
            services.AddScoped<IUnitOfWorkService, UnitOfWorkService>();

            services.AddMvc().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
            });

            return services;
        }
    }
}