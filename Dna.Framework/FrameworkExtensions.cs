using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Dna
{
    /// <summary>
    /// Extensions methods for DNA framework
    /// </summary>
    public static class FrameworkExtensions
    {
        /// <summary>
        /// Adds a default logger so that we can get an non-generic ILogger
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddDefaultLogger(this IServiceCollection services)
        {
            //Add a default logger
            services.AddTransient(provider => provider.GetService<ILoggerFactory>().CreateLogger("Dna"));

            //return services
            return services;
        }
    }
}
