using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Threading.Tasks;

namespace Restaurant.Data.Extensions
{
    public static class HostExtensions
    {
        public static async Task InitializeApplication(this IHost host)
        {
            using var scope = host.Services.CreateScope();
            var databaseInitializer = scope.ServiceProvider.GetRequiredService<DbInitializer>();

            await databaseInitializer.InitializeAsync();
        }
    }
}
