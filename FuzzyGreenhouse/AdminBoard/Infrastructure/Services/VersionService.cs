using AdminBoard.Data;
using AdminBoard.Models.FuzzyGreenHouse;
using Microsoft.Extensions.Logging;
using System.Linq;

namespace AdminBoard.Infrastructure.Services
{
    public class VersionService
    {
        private readonly ILogger<VersionService> _logger;
        private readonly FuzzyGreenhouseDbContext _context;

        public VersionService(ILogger<VersionService> logger, FuzzyGreenhouseDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public Version GetLatest()
        {
            try
            {
                return _context.Set<Version>()
                    .OrderByDescending(e => e.CreatedDate)
                    .First();
            }
            catch (System.Exception e)
            {
                _logger.LogError($"Failed to get latest version. Message: {e.Message}");
                throw new System.Exception(e.Message);
            }
        } 

        public void CreateNewVersion(string description)
        {
            try
            {
                _context.Version.Add(new Version(description));
                _context.SaveChanges();
            }
            catch (System.Exception e)
            {
                _logger.LogError($"Failed to get insert new version. Message: {e.Message}");
                throw new System.Exception(e.Message);
            }
        }
    }
}
