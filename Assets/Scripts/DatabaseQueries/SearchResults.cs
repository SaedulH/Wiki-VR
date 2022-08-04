using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Graph
{
    public class SearchResults
    {

        /// <summary>
        /// Empty constructor.
        /// </summary>    
        public SearchResults()
            : this(new List<string>())
        {
        }        

        /// <summary>
        /// Default constructor.
        /// </summary> 
        public SearchResults(List<string> results)
        {

            _Results = results;
        }      

        [SerializeField]
        private List<string> _Results;
        public List<string> results1 { get { return _Results; } }
    }
}