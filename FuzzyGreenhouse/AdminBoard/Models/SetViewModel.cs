using AdminBoard.Models.FuzzyGreenHouse;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace AdminBoard.Models
{
    public class SetViewModel
    {
        public SetViewModel() { }

        public SetViewModel(List<Subsystem> subsystems) {
            AllSubsystems = new List<SelectListItem>();
            subsystems.ForEach(e => AllSubsystems.Add(new SelectListItem(e.Name, e.SubsystemID.ToString())));
        }

        public Set ConvertToSet(List<Subsystem> subsystems)
        {
            return new Set
            {
                Name = Name,
                Type = Type,
                Subsystems = subsystems
            };
        }

        public string Name { get; set; }
        public SetType Type { get; set; }
        public List<SelectListItem> AllSubsystems { get; set; }
        public List<string> SelectedSubsystems { get; set; }


    }
}
