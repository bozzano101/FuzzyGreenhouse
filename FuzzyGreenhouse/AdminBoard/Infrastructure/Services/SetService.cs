using AdminBoard.Data;
using AdminBoard.Models.FuzzyGreenHouse;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdminBoard.Infrastructure.Services
{
    public class SetService
    {
        private readonly ILogger<SetService> _logger;
        private readonly FuzzyGreenhouseDbContext _context;
        private readonly VersionService _versionService;

        public SetService(ILogger<SetService> logger, FuzzyGreenhouseDbContext context, VersionService versionService)
        {
            _logger = logger;
            _context = context;
            _versionService = versionService;
        }

        public List<Set> GetAll()
        {
            try
            {
                return _context.Set.Include(e => e.Values).Include(e => e.Subsystems).ToList();
            }
            catch (Exception e)
            {
                _logger.LogError($"Failed to get all Set. Message: {e.Message}");
                throw new Exception(e.Message);
            }
        }

        public Set Get(int id)
        {
            try
            {
                return _context.Set.Where(e => e.SetID == id).First();
            }
            catch (Exception e)
            {
                _logger.LogError($"Failed to get Set with ID={id}. Message: {e.Message}");
                throw new Exception(e.Message);
            }
        }

        public Dictionary<int,string> GetNames()
        {
            try
            {
                var sets = GetAll();
                var namesDict = new Dictionary<int, string>();
                foreach (var set in sets)
                    namesDict.Add(set.SetID, set.Name);
                return namesDict;
            }
            catch (Exception e)
            {
                _logger.LogError($"Failed to get set names. Message: {e.Message}");
                throw new Exception(e.Message);
            }
        }

        public void Insert(Set set)
        {
            try
            {
                _context.Set.Add(set);
                _context.SaveChanges();
                _versionService.CreateNewVersion($"Inserted new set: {set.Name}");
            }
            catch (Exception e)
            {
                _logger.LogError($"Failed to add new set. Message: {e.Message}");
                throw new Exception(e.Message);
            }
        }

        public void Delete(int id)
        {
            try
            {
                var setForDelete = _context.Set.Where(e => e.SetID == id).First();
                _context.Set.Remove(setForDelete);
                _context.SaveChanges();
                _versionService.CreateNewVersion($"Deleted set: {id}");
            }
            catch (Exception e)
            {
                _logger.LogError($"Failed to delete set with ID={id}. Message: {e.Message}");
                throw new Exception(e.Message);
            }
        }

        public void Update(Set set)
        {
            try
            {
                var setForUpdate = _context.Set.Where(e => e.SetID == set.SetID).First();
                setForUpdate.Name = set.Name;
                setForUpdate.Type = set.Type;
                _context.SaveChanges();
                _versionService.CreateNewVersion($"Updated set: {set.Name}");
            }
            catch (Exception e)
            {
                _logger.LogError($"Failed to update set with ID={set.SetID}. Message: {e.Message}");
                throw new Exception(e.Message);
            }
        }
    }
}
