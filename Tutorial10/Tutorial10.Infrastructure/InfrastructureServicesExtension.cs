using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Tutorial10.Application.Repositories;
using Tutorial10.Infrastructure.Repositories;

namespace Tutorial10.Infrastructure;

public static class InfrastructureServicesExtension
{
    public static void RegisterInfraServices(this IServiceCollection services, IConfigurationManager configuration)
    {
        services.AddDbContext<ClinicContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"), b => b.MigrationsAssembly("Tutorial10")));

        services.AddScoped<IDoctorRepository, DoctorRepository>();
        services.AddScoped<IPatientRepository, PatientRepository>();
        services.AddScoped<IPrescriptionRepository, PrescriptionRepository>();
        services.AddScoped<IMedicationRepository, MedicationRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
    }
}
