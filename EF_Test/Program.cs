using Domin.Data;
using Microsoft.Extensions.DependencyInjection;
using Program.Applicaction;
using Program.ConfigureServices;

namespace Program
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            try
            {
                var serviceCollection = new ServiceCollection();
                serviceCollection.AddServices();

                var serviceProvider = serviceCollection.BuildServiceProvider();

                var dbContext = serviceProvider.GetService<AppDbContext>();
                if (dbContext == null)
                {
                    Console.WriteLine("AppDbContext could not be resolved!");
                    return;
                }

                var app = serviceProvider.GetService<App>();
                await app?.Run(); // تعديل هنا لاستخدام await
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
    }

   
  
}
