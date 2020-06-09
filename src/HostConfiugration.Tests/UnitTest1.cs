using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace HostConfiugration.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public async Task TestMethod1()
        {
            var builder = new HostBuilder()
                .ConfigureAppConfiguration((context, configBuilder) =>
                {
                    configBuilder.AddJsonFile("appsettings_abc.json", optional: true);
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
