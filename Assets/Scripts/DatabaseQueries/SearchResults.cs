using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Graph
{
    public class SearchResults
    {

        // Empty constructor.   
        public SearchResults()
            : this(new List<string>())
        {
        }        

        // Default constructor.
        public SearchResults(List<string> results)
        {

            _Results = results;
        }      

        [SerializeField]
        private List<string> _Results;
        public List<string> results1 { get { return _Results; } }
    }
}