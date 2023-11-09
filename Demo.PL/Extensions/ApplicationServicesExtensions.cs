using Demo.BAL;
using Demo.BAL.Interfaces;
using Demo.BAL.Repositries;
using Microsoft.Extensions.DependencyInjection;

namespace Demo.PL.Extensions
{
    public static class ApplicationServicesExtensions
    {
        public static IServiceCollection AddAppServicesExtensions(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();

          
            //services.AddScoped<IDepartmentRepositry, DepartmentRepositry>();
            //services.AddScoped<IEmployeeRepositry, EmployeeReprositry>();
            return services;    

        }
    }
}
