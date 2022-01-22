using Academy.Application.Services.Implementations;
using Academy.Application.Services.Interfaces;
using Academy.Data.Repositories;
using Academy.Domain.IRepositories;
using Microsoft.Extensions.DependencyInjection;
using Academy.Application.Senders;

namespace Academy.IoC
{
   public static class DependencyContainer
    {
        public static void SetDependencyInjection(this IServiceCollection service)
        {
            //services
            service.AddScoped<IUserService,UserService>();
            service.AddScoped<IViewRenderService, RenderViewToString>();
            service.AddTransient<IGoogleRecaptcha, GoogleRecaptcha>();
            service.AddScoped<IPermissionService, PermissionService>();
            service.AddScoped<IArticleService, ArticleService>();
            service.AddScoped<ICourseService, CourseService>();
            service.AddScoped<ISubscribeService, SubscribeService>();
            service.AddScoped<IOrderService, OrderService>();

            //repositories
            service.AddScoped<IUserRepository,UserRepository>();
            service.AddScoped<IPermissionRepository, PermissionRepository>();
            service.AddScoped<IArticleRepository, ArticleRepository>();
            service.AddScoped<ICourseRepository, CourseRepository>();
            service.AddScoped<ISubscribeRepository, SubscribeRepository>();
            service.AddScoped<IOrderRepository, OrderRepository>();
        }
    }
}
