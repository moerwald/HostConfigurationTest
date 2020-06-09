using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace HostConfiugration.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public async Task TestMethod1()
        {
            var builder = Host.CreateDefaultBuilder()
                //.ConfigureAppConfiguration((context, configBuilder) =>
                //{
                //    configBuilder.SetBasePath(Directory.GetCurrentDirectory());
                //    configBuilder.AddJsonFile("appsettings_abc.json", optional: true);
                //})
                .ConfigureHostConfiguration(( configBuilder) =>
                {
                    var basePath = Directory.GetCurrentDirectory();
                    configBuilder.SetBasePath(basePath);
                    configBuilder.AddJsonFile(
                        Path.Combine(basePath,"hostsettings.json"), optional: false);
                    var root = configBuilder.Build();
                    var section = root.GetSection("AppConfig");
                    var providers = root.Providers.ToList();
                })
              .ConfigureServices((hostContext, services) =>
              {
                  var section = hostContext.Configuration.GetSection("AppConfig");
                  Assert.IsNotNull(section.Value, "Appconfig sections was not loaded");
              });
            await builder.RunConsoleAsync();
        }
    }
}
