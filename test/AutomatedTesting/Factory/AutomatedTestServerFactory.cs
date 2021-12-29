namespace AutomatedTesting.Factory
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.IO;
    using System.Linq;
    using Microsoft.AspNetCore;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Hosting.Server.Features;
    using Microsoft.AspNetCore.Mvc.Testing;
    using Microsoft.AspNetCore.TestHost;
    using Microsoft.Extensions.Configuration;

    /// <summary>
    /// Web Application Factory for End to End based testing e.g. using Selenium or Playwright.
    /// </summary>
    /// <typeparam name="TStartup">A Startup type.</typeparam>
    public class AutomatedTestServerFactory<TStartup> : WebApplicationFactory<TStartup>
        where TStartup : class
    {
        private const string _LocalhostBaseAddress = "https://localhost";

        private string _environment;

        private IWebHost _host;

        private Action<IWebHostBuilder> _webhostConfiguration;

        /// <summary>
        /// Initializes a new instance of the <see cref="AutomatedTestServerFactory{TStartup}"/> class.
        /// Creates a new WebApplication Factory with environment, logging and webhost override.
        /// </summary>
        /// <param name="environment">Set the target environment.</param>
        /// <param name="webhostConfiguration">Override the webhost configuration.</param>
        public AutomatedTestServerFactory(string environment, Action<IWebHostBuilder> webhostConfiguration)
        {
            ClientOptions.BaseAddress = new Uri(_LocalhostBaseAddress);

            _webhostConfiguration = webhostConfiguration;

            _environment = environment;

            CreateServer(CreateWebHostBuilder());
        }

        public string RootUri { get; private set; }

        protected override TestServer CreateServer(IWebHostBuilder builder)
        {
            // Load server URL from hosting.json file so that the web server runs on the correct port for testing.
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("hosting.json", optional: true)
                .Build();

            _host = builder
                .UseConfiguration(config)
                .Build();

            _host.Start();

            RootUri = _host.ServerFeatures.Get<IServerAddressesFeature>().Addresses.LastOrDefault();

            // There doesn't seem to be a need to return a test server instance.  If so it would be different to what is created previously.
            // Attempting to use the same instance causes errors, creating new instances creates slightly different web servers.
            return null;
        } 

        protected override IWebHostBuilder CreateWebHostBuilder()
        {
            // Have to set environment here otherwise it is set to Production
            if (!string.IsNullOrWhiteSpace(_environment))
            {
                Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", _environment);
            }

            var builder = WebHost.CreateDefaultBuilder(Array.Empty<string>());

            builder.UseStartup<TStartup>();

            // Apply ConfigureServices changes to server.
            _webhostConfiguration?.Invoke(builder);

            return builder;
        }

        [ExcludeFromCodeCoverage]
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            if (disposing)
            {
                _host?.Dispose();
            }
        }
    }
}
