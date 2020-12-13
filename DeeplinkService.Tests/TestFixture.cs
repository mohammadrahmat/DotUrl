using DotUrl.Elastic;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.IO;

namespace DeeplinkService.Tests
{
    public class TestFixture
    {
        public ServiceProvider ServiceProvider { get; private set; }

        public TestFixture()
        {
            var services = new ServiceCollection();
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile(
                    path: "appsettings.json",
                    optional: false,
                    reloadOnChange: true
                )
                .Build();
            services.AddSingleton<IConfiguration>(configuration);
            services.AddElasticsearch(configuration);
            ServiceProvider = services.BuildServiceProvider();
        }
    }
}
