using AdminBoard.Data;
using AdminBoard.Models.FuzzyGreenHouse;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdminBoard.Infrastructure.Services
{
    public class ValuesService
    {
        private readonly ILogger<ValuesService> _logger;
        private readonly FuzzyGreenhouseDbContext _context;
        private readonly VersionService _versionService;

        public ValuesService(ILogger<ValuesService> logger, FuzzyGreenhouseDbContext context, VersionService versionService)
        {
            _logger = logger;
            _context = context;
            _versionService = versionService;
        }

        public Value Get(int id)
        {
            try
            {
                return _context.Value.Where(e => e.ValueID == id).First();
            }
            catch (Exception e)
            {
                _logger.LogError($"Failed to get Value with ID={id}. Message: {e.Message}");
                throw new Exception(e.Message);
            }
        }

        public Value GetDisabled()
        {
            try
            {
                return _context.Value.Where(e => e.Name == "Disabled").First();
            }
            catch (Exception e)
            {
                _logger.LogError($"Failed to get disabled value. Message: {e.Message}");
                throw new Exception(e.Message);
            }
        }

        public List<Value> GetAll()
        {
            try
            {
                return _context.Value.Where(f => f.Name != "Disabled").ToList();
            }
            catch (Exception e)
            {
                _logger.LogError($"Failed to get all Values. Message: {e.Message}");
                throw new Exception(e.Message);
            }
        }

        public void Insert(Value value)
        {
            try
            {
                _context.Value.Add(value);
                _context.SaveChanges();
                _versionService.CreateNewVersion($"Inserted new value: {value.Name}");
            }
            catch (Exception e)
            {
                _logger.LogError($"Failed to insert new Value. Message: {e.Message}");
                throw new Exception(e.Message);
            }
        }

        public void Delete(int id)
        {
            try
            {
                var valueForDelete = _context.Value.Where(e => e.ValueID == id).First();
                _context.Value.Remove(valueForDelete);
                _context.SaveChanges();
                _versionService.CreateNewVersion($"Deleted value: {id}");
            }
            catch (Exception e)
            {
                _logger.LogError($"Failed to delete Value with ID={id}. Message: {e.Message}");
                throw new Exception(e.Message);
            }
        }

        public void Update(Value value)
        {
            try
            {
                var valueForUpdate = _context.Value.Where(e => e.ValueID == value.ValueID).First();
                valueForUpdate.Name = value.Name;
                valueForUpdate.Set = value.Set;
                valueForUpdate.XCoords = value.XCoords;
                valueForUpdate.YCoords = value.YCoords;
                _context.SaveChanges();
                _versionService.CreateNewVersion($"Updated value: {value.Name}");
            }
            catch (Exception e)
            {
                _logger.LogError($"Failed to update Value with ID={value.ValueID}. Message: {e.Message}");
                throw new Exception(e.Message);
            }
        }
    }
}
