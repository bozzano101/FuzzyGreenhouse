using System;
using System.Collections.Generic;
using System.Text;

namespace FuzzyLib
{
    /// <summary>
    /// This class represents fuzzy set. It can contains many fuzzy values and manipulate with them
    /// </summary>
    public class FuzzyInputSet
    {
        /// <summary>
        /// Constructor for FuzzyInputSet class
        /// </summary>
        /// <param name="id">Id from database</param>
        /// <param name="name">Name of fuzzy set</param>
        public FuzzyInputSet(int id, string name)
        {
            Id = id;
            Name = name;
            Values = new List<FuzzyInput>();
        }

        /// <summary>
        /// This method will add new FuzzyInput to collection
        /// </summary>
        /// <param name="value"> FuzzyInput that will be added</param>
        public void AddValue(FuzzyInput value)
        {
            Values.Add(value);
        }

        /// <summary>
        /// This method will update Mu value in all FuzzyInput objects that are part of this FuzzyInputSet object
        /// </summary>
        /// <param name="value"> Float number to update to</param>
        public void RecalculateMu(float value)
        {
            Values.ForEach(e => e.RecalculateMu(value));
        }

        /// <summary>
        /// This property represents id from database
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// This property represents fuzzy set name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// This property represents all values for this particular set
        /// </summary>
        public List<FuzzyInput> Values { get; set; }
    }
}
