using HealthCare_Project.Data;
using HealthCare_Project.Helper;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace HealthCare_Project.Extensions
{
    public static class ApplicationServiceExtension
    {
        public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddOpenApi();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultSqlConnection")));

            //Add AutoMapper
            services.AddAutoMapper(cfg =>
            {
                cfg.AddProfile<DoctorProfile>();
                cfg.AddProfile<PatientProfile>();
                cfg.AddProfile<AppointmentProfile>();
            });
            return services;
        }
    }
}
