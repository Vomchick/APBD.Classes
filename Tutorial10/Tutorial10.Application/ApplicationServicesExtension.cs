using Microsoft.Extensions.DependencyInjection;
using System.IdentityModel.Tokens.Jwt;
using Tutorial10.Application.Services;
using Tutorial10.Application.Services.Interfaces;

namespace Tutorial10.Application;

public static class ApplicationServicesExtension
{
    public static void RegisterApplicationServices(this IServiceCollection app)
    {
        app.AddScoped<IPatientService, PatientService>();
        app.AddScoped<IPrescriptionService, PrescriptionService>();
        app.AddScoped<IAuthService, AuthService>();
        app.AddSingleton<JwtHelper>();
    }
}