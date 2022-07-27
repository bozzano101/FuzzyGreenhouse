using AdminBoard.Models.FuzzyGreenHouse;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminBoard.Models
{
    public class RuleViewModel : Rule
    {
        public string InputValue1Representation { get; set; }
        public string InputValue2Representation { get; set; }
        public string OutputValueRepresentation { get; set; }
        public string SubsystemRepresentation { get; set; }

        public List<SelectListItem> InputList1 { get; } = new List<SelectListItem>();
        public List<SelectListItem> InputList2 { get; } = new List<SelectListItem>();
        public List<SelectListItem> OutputList { get; } = new List<SelectListItem>();
        public List<SelectListItem> SubsystemList { get; } = new List<SelectListItem>();

        public RuleViewModel() { }

        public RuleViewModel(Rule rule)
            :base(rule.InputValue1, rule.InputValue2, rule.OutputValue, rule.Operator, rule.Subsystem) { 
        }

        public RuleViewModel(Rule rule, List<Set> sets)
            :base(rule.InputValue1, rule.InputValue2, rule.OutputValue, rule.Operator, rule.Subsystem)
        {
            RuleID = rule.RuleID;

            var inputValue1Set = sets.Where(e => e.SetID == rule.InputValue1.SetID).First();
            InputValue1Representation = inputValue1Set.Name + " - " + rule.InputValue1.Name;

            var inputValue2Set = sets.Where(e => e.SetID == rule.InputValue2.SetID).First();
            InputValue2Representation = inputValue2Set.Name + " - " + rule.InputValue2.Name;

            var outputValueSet = sets.Where(e => e.SetID == rule.OutputValue.SetID).First();
            OutputValueRepresentation = outputValueSet.Name + " - " + rule.OutputValue.Name;
        }

        public Rule ConvertToRule()
        {
            Rule rule = new Rule
            {
                InputValue1ID = Convert.ToInt32(InputValue1Representation),
                InputValue2ID = Convert.ToInt32(InputValue2Representation),
                OutputValueID = Convert.ToInt32(OutputValueRepresentation),
                SubsystemID = Convert.ToInt32(SubsystemRepresentation),
                Operator = this.Operator,
                RuleID = this.RuleID
            };

            return rule;
        }
    }
}
