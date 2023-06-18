using AdminBoard.Data;
using AdminBoard.Models.FuzzyGreenHouse;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdminBoard.Infrastructure.Services
{
    public class SubsystemService
    {
        private readonly ILogger<SubsystemService> _logger;
        private readonly FuzzyGreenhouseDbContext _context;
        private readonly VersionService _versionService;

        public SubsystemService(
            ILogger<SubsystemService> logger, 
            FuzzyGreenhouseDbContext context, 
            VersionService versionService)
        {
            _logger = logger;
            _context = context;
            _versionService = versionService;
        }

        public List<Subsystem> GetAll()
        {
            try
            {
                return _context.Subsystem.ToList();
            }
            catch (Exception e)
            {
                _logger.LogError($"Failed to get all Subsystems. Message: {e.Message}");
                throw new Exception(e.Message);
            }
        }

        public Subsystem Get(int id)
        {
            try
            {
                return _context.Subsystem.Where(e => e.SubsystemID == id).First();
            }
            catch (Exception e)
            {
                _logger.LogError($"Failed to get Subsytem with ID={id}. Message: {e.Message}");
                throw new Exception(e.Message);
            }
        }

        public Dictionary<int, string> GetNames()
        {
            try
            {
                var subsystems = GetAll();
                var namesDict = new Dictionary<int, string>();
                foreach (var subsystem in subsystems)
                    namesDict.Add(subsystem.SubsystemID, subsystem.Name);
                return namesDict;
            }
            catch (Exception e)
            {
                _logger.LogError($"Failed to get subsystem names. Message: {e.Message}");
                throw new Exception(e.Message);
            }
        }

        public void Insert(Subsystem subsystem)
        {
            try
            {
                _context.Subsystem.Add(subsystem);
                _context.SaveChanges();
                _versionService.CreateNewVersion($"Inserted new subsystem: {subsystem.Name}");
            }
            catch (Exception e)
            {
                _logger.LogError($"Failed to add new subsystem. Message: {e.Message}");
                throw new Exception(e.Message);
            }
        }

        public void Delete(int id)
        {
            try
            {
                var subSystemForDelete = _context.Subsystem.Where(e => e.SubsystemID == id).First();
                _context.Subsystem.Remove(subSystemForDelete);
                _context.SaveChanges();
                _versionService.CreateNewVersion($"Deleted subsystem: {id}");
            }
            catch (Exception e)
            {
                _logger.LogError($"Failed to delete subsysytem with ID={id}. Message: {e.Message}");
                throw new Exception(e.Message);
            }
        }

        public void Update(Subsystem subsystem)
        {
            try
            {
                var subsystemForUpdate = _context.Subsystem.Where(e => e.SubsystemID == subsystem.SubsystemID).First();
                subsystemForUpdate.Name = subsystem.Name;
                subsystemForUpdate.Description = subsystem.Description;
                _context.SaveChanges();
                _versionService.CreateNewVersion($"Updated set: {subsystem.Name}");
            }
            catch (Exception e)
            {
                _logger.LogError($"Failed to update set with ID={subsystem.SubsystemID}. Message: {e.Message}");
                throw new Exception(e.Message);
            }
        }
    }
}
