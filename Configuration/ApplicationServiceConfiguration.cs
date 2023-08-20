using Microsoft.Extensions.DependencyInjection;
using ACRPhone.Webhook.Repositories;

namespace ACRPhone.Webhook.Configuration
{
    public static class ApplicationServiceConfiguration
    {
        public static void AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IRecordingRepository, RecordingRepository>();
        }
    }
}
