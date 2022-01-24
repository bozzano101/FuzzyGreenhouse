using System;
using System.Collections.Generic;
using System.Text;

namespace FuzzyLib
{
    /// <summary>
    /// This class represents fuzzy set. It can contains many fuzzy values and manipulate with them
    /// </summary>
    public class FuzzyOutputSet
    {
        /// <summary>
        /// Constructor for FuzzyOutputSet class
        /// </summary>
        /// <param name="setId"> Database ID of set</param>
        /// <param name="setName">Name of fuzzy set</param>
        public FuzzyOutputSet(int setId, string setName)
        {
            Id = setId;
            Name = setName;
            Values = new List<FuzzyOutput>();
        }

        /// <summary>
        /// This method will add new FuzzyOutput to collection
        /// </summary>
        /// <param name="value"> FuzzyOutput that will be added</param>
        public void AddValue(FuzzyOutput value)
        {
            Values.Add(value);
        }

        /// <summary>
        /// This property represents fuzzy set retrieved from database
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// This property represents fuzzy set name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// This property represents all values for this particular set
        /// </summary>
        public List<FuzzyOutput> Values { get; set; }

    }
}
