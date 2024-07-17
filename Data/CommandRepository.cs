using CommandsService.Models;
using Microsoft.EntityFrameworkCore;

namespace CommandsService.Data
{
    public class CommandRepository : ICommandRepository
    {
        private readonly AppDbContext _context;

        public CommandRepository(AppDbContext context)
        {
            _context = context;  
        }

        public async Task CreatePlaform(Platform platform)
        {
            if ( platform == null)
            {
                throw new ArgumentNullException( nameof(platform) );
            }

            await _context.PlatForms.AddAsync(platform);
        }

        public async Task<IEnumerable<Platform>> GetAllPlatforms()
        {
            return await _context.PlatForms.ToListAsync();
        }

        public async Task<bool> PlatformExists(int platformId)
        {
            return await _context.PlatForms.AnyAsync(p => p.Id == platformId);
        }

        public async Task CreateCommand(int platformId, Command command)
        {
            if (command == null)
            {
                throw new ArgumentNullException( nameof(command) );
            }

            command.PlatformId = platformId;
            await _context.Commands.AddAsync(command);   
        }
        
        public async Task<Command?> GetCommand(int platformId, int commandId)
        {
            return await _context.Commands
                        .Where(c => c.PlatformId == platformId && c.Id == commandId)
                        .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Command>> GetCommandsByPlatform(int platformId)
        {
            return await _context.Commands.Where(c => c.PlatformId == platformId).ToListAsync();
        }

        public async Task<bool> SaveChanges()
        {
            return await _context.SaveChangesAsync() >= 0;
        }
    }
}