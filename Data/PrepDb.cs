using Azure.Core.Pipeline;
using CommandsService.Models;
using CommandsService.SyncDataServices.Grpc;

namespace CommandsService.Data
{
    public static class PrepDb
    {
        public static void PrepPopulation(IApplicationBuilder applicationBuilder)
        {
            using( var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var grpcClient = serviceScope.ServiceProvider.GetService<IPlatformDataClient>();

                var platforms = grpcClient?.ReturnAllPlatforms();

                SeedData
                (
                    serviceScope.ServiceProvider.GetService<ICommandRepository>(),
                    platforms
                );
            }
        }

        private static async void SeedData(ICommandRepository commandRepository, IEnumerable<Platform> platforms)
        {
            Console.WriteLine("Seeding new platforms...");
            
            foreach(var plat in platforms)
            {
                var exists = await commandRepository.ExternalPlatformExists(plat.ExternalId);

                if(!exists)
                {
                    await commandRepository.CreatePlaform(plat);
                }
            }

            await commandRepository.SaveChanges();
        }
    }
}