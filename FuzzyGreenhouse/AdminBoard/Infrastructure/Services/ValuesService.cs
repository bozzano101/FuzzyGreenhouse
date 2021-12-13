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

        public ValuesService(ILogger<ValuesService> logger, FuzzyGreenhouseDbContext context)
        {
            _logger = logger;
            _context = context;
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

        public List<Value> GetAll()
        {
            try
            {
                return _context.Value.ToList();
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
            }
            catch (Exception e)
            {
                _logger.LogError($"Failed to update Value with ID={value.ValueID}. Message: {e.Message}");
                throw new Exception(e.Message);
            }
        }
    }
}
