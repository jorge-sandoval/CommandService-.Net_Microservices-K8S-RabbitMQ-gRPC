using CommandsService.Models;

namespace CommandsService.Data
{
    public interface ICommandRepository
    {
        Task<bool> SaveChanges();

        //Platforms
        Task<IEnumerable<Platform>> GetAllPlatforms();
        Task CreatePlaform(Platform platform);
        Task<bool> PlatformExists(int platformId);
        Task<bool> ExternalPlatformExists(int externalPlatformId);

        //Commands
        Task<IEnumerable<Command>> GetCommandsByPlatform(int platformId);
        Task<Command?> GetCommand(int platformId, int commandId);
        Task CreateCommand(int platformId, Command command);
    }
}
