using API.Data;
using API.Helpers;
using API.Interfaces;
using API.Services;
using Microsoft.EntityFrameworkCore;

namespace API.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<DataContext>(o =>
            {
                o.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });

            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IUserRepository,  UserRepository>(); 
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.Configure<CloudinarySettings>(configuration.GetSection("CloudinarySettings"));
            services.AddScoped<IPhotoService, PhotoService>();
            services.AddScoped<LogUserActivity>();
            services.AddScoped<ILikeRepository, LikeRepository>();

            services.AddCors();

            return services;
        }

    }
}
