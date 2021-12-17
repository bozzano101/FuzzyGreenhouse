using AdminBoard.Infrastructure.Services;
using AdminBoard.Models.FuzzyGreenHouse;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminBoard.Models
{
    public class ValueViewModel
    {
        public int ValueID { get; set; }
        public string Name { get; set; }
        public string XCoords { get; set; }
        public string YCoords { get; set; }
        public List<SelectListItem> AllSets { get; set; }
        public string SelectedSet { get; set; }
        public string SelectedSetName { get; set; }

        public ValueViewModel() { }

        public ValueViewModel(List<Set> Sets)
        {
            AllSets = new List<SelectListItem>();
            Sets.ForEach(e => AllSets.Add(new SelectListItem { Text = e.Name, Value = e.SetID.ToString() }));
        }

        public ValueViewModel(Value model, List<Set> Sets)
        {
            
            ValueID = model.ValueID;
            Name = model.Name;
            // Coords string should be joined from list with comma separator
            XCoords = String.Join(',', model.XCoords);
            YCoords = String.Join(',', model.YCoords);

            // Prepare all set select element
            AllSets = new List<SelectListItem>();
            Sets.ForEach(e => AllSets.Add(new SelectListItem { Text = e.Name, Value = e.SetID.ToString() }));

            SelectedSet = model.SetID.ToString();

        }

        public Value ConvertToValue(Set set)
        {
            return new Value
            {
                Name = Name,
                ValueID = ValueID,
                XCoords = XCoords.Split(',').ToList().Select(float.Parse).ToList(),
                YCoords = YCoords.Split(',').ToList().Select(float.Parse).ToList(),
                Set = set
            };
        }
    }
}
