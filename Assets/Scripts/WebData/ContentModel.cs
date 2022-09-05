using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;

namespace WebData
{   

    //For deserializing List of Content from wikitext to C# objects
    public class ContentBase
    {
        public _Parse parse {get; set;}
    }

    public class _Parse
    {
        public string title {get; set;}
        public int pageid {get; set;}
        public _Section[] sections {get; set;}
    }
    public class _Section
    {
        public string line {get; set;} 
        public string number {get; set;}
        public int index {get; set;}
    }
}
