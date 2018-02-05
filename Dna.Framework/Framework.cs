using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace Dna
{
    /// <summary>
    /// the main entry point into the Dna Framework library
    /// </summary>
    public static class Framework
    {
        #region private Members
        /// <summary>
        /// dependency injection service provider
        /// </summary>
        private static IServiceProvider ServiceProvider;
        #endregion

        #region Public Properties
        /// <summary>
        /// dependency injection service provider
        /// </summary
        public static IServiceProvider Provider => ServiceProvider;

        /// <summary>
        /// Gets the configuration for the framework environment
        /// </summary>
        public static IConfiguration Configuration => Provider.GetService<IConfiguration>();

        /// <summary>
        /// Gets the default logger for the framework
        /// </summary>
        public static ILogger Logger => Provider.GetService<ILogger>();

        /// <summary>
        /// Gets framework environment of this class
        /// </summary>
        public static FrameworkEnvironment Environment => Provider.GetService<FrameworkEnvironment>();



        #endregion

        #region public Methods
        /// <summary>
        /// Should be called at the very start of application to configure 
        /// and setup the Dna Framework
        /// </summary>
        /// <param name="configure">The Action to add custom configuration</param>
        /// <param name="injection">The action to inject services into the service collection</param>
        public static void Startup(Action<IConfigurationBuilder> configure = null, Action<IServiceCollection, IConfiguration> injection = null)
        {
            #region Init
            //create a new list of dependencies
            var services = new ServiceCollection();
            #endregion

            #region Environment
            // Create environment Details
            var environment = new FrameworkEnvironment();

            //inject environment into services
            services.AddSingleton(environment);

            #endregion

            #region Configuration

            //Add config for file.json
            var configurationBuilder = new ConfigurationBuilder()
                //add environment vars
                .AddEnvironmentVariables()
                .SetBasePath(Directory.GetCurrentDirectory())
                //add app setting json files
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{environment.Configuration}.json", optional: true, reloadOnChange: true);

            //let custom config happen
            configure?.Invoke(configurationBuilder);

            //Inject configuration into services
            var configuration = configurationBuilder.Build();
            services.AddSingleton<IConfiguration>(configuration);
            #endregion

            #region Logging
            //Add logging as default
            services.AddLogging(options => {

                //setup loggers from configurations
                options.AddConfiguration(configuration.GetSection("Logging"));

                //add console logger
                options.AddConsole();

                //add debug logger
                options.AddDebug();
                // add file logger
                options.AddFile("log.txt");

            });

            // add default logger
            services.AddDefaultLogger();
            #endregion

            #region Custom Services and Building
            //allow custom service injection
            injection?.Invoke(services, configuration);

            //Build the service provider
            ServiceProvider = services.BuildServiceProvider();
            #endregion

            //log startup complete
            Logger.LogCriticalSource($"Dna Framework Started in {environment.Configuration}");
        }
        #endregion
      
    }

    public class Test { }
}
