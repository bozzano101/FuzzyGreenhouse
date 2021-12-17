using AdminBoard.Data;
using AdminBoard.Models.FuzzyGreenHouse;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdminBoard.Infrastructure.Services
{
    public class RuleService
    {
        private readonly ILogger<RuleService> _logger;
        private readonly FuzzyGreenhouseDbContext _context;

        public RuleService(ILogger<RuleService> logger, FuzzyGreenhouseDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public List<Rule> GetAll()
        {
            try
            {
                return _context.Rule.ToList();
            }
            catch (Exception e)
            {
                _logger.LogError($"Failed to get all rules. Message: {e.Message}");
                throw new Exception(e.Message);
            }
        }

        public Rule Get(int id)
        {
            try
            {
                return _context.Rule.Where(e => e.RuleID == id).First();
            }
            catch (Exception e)
            {
                _logger.LogError($"Failed to get rule with ID={id}. Message: {e.Message}");
                throw new Exception(e.Message);
            }
        }

        public void Insert(Rule rule)
        {
            try
            {
                _context.Rule.Add(rule);
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                _logger.LogError($"Failed to add new rule. Message: {e.Message}");
                throw new Exception(e.Message);
            }
        }

        public void Delete(int id)
        {
            try
            {
                var ruleForDelete = _context.Rule.Where(e => e.RuleID == id).First();
                _context.Rule.Remove(ruleForDelete);
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                _logger.LogError($"Failed to delete rule with ID={id}. Message: {e.Message}");
                throw new Exception(e.Message);
            }
        }

        public void Update(Rule rule)
        {
            try
            {
                var ruleForUpdate = _context.Rule.Where(e => e.RuleID == rule.RuleID).First();
                // TODO
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                _logger.LogError($"Failed to update rule with ID={rule.RuleID}. Message: {e.Message}");
                throw new Exception(e.Message);
            }
        }
    }
}
