using AdminBoard.Data;
using AdminBoard.Models.FuzzyGreenHouse;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdminBoard.Infrastructure.Services
{
    public class VariableService
    {
        private readonly ILogger<VariableService> _logger;
        private readonly FuzzyGreenhouseDbContext _context;

        public VariableService(ILogger<VariableService> logger, FuzzyGreenhouseDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public List<Set> GetAll()
        {
            try
            {
                return _context.Set.ToList();
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

        public void Insert(Set set)
        {
            try
            {
                _context.Set.Add(set);
                _context.SaveChanges();
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
            }
            catch (Exception e)
            {
                _logger.LogError($"Failed to update set with ID={set.SetID}. Message: {e.Message}");
                throw new Exception(e.Message);
            }
        }
    }
}
