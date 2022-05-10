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
      services.AddScoped<IPictureService, PictureService>();
      services.AddScoped<ICategoryService, CategoryService>();
      services.AddScoped<IUserService, UserService>();
      services.AddScoped<ILoginService, LoginService>();
      services.AddScoped<IEventService, EventService>();
      services.AddScoped<ILocationService, LocationService>();
      services.AddScoped<IAttendanceService, AttendanceService>();
      services.AddScoped<ITaskPresenceService, TaskPresenceService>();
      return services;
    }

    public static IServiceCollection AddMappingServices(this IServiceCollection services, IConfiguration config)
    {
      services.AddAutoMapper(typeof(CategoryMappings));
      services.AddAutoMapper(typeof(UserMappings));
      services.AddAutoMapper(typeof(UserLoginMappings));
      services.AddAutoMapper(typeof(EventMappings));
      services.AddAutoMapper(typeof(LocationMappings));
      services.AddAutoMapper(typeof(AttendanceMappings));
      services.AddAutoMapper(typeof(TaskPresenceMappings));
      return services;
    }
  }
}