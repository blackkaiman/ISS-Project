using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using YonderfulApi.Mappings;
using YonderfulApi.Service;
using api.Mappings;
using api.Service;

namespace YonderfulApi.Extensions
{
  public static class ApplicationServiceExtensions
  {
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
    {
      services.AddScoped<ITaskService, TaskService>();
      services.AddScoped<IUserService, UserService>();
      services.AddScoped<ILoginService, LoginService>();
      services.AddScoped<ITaskPresenceService, TaskPresenceService>();
      return services;
    }

    public static IServiceCollection AddMappingServices(this IServiceCollection services, IConfiguration config)
    {
      services.AddAutoMapper(typeof(TaskMappings));
      services.AddAutoMapper(typeof(UserMappings));
      services.AddAutoMapper(typeof(UserLoginMappings));
      services.AddAutoMapper(typeof(TaskPresenceMappings));
      return services;
    }
  }
}