using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;

namespace WebData
{ 

    //For deserializing Sections from wikitext to C# object
    public class SectionBase
    {
        public _Parsesection parse {get; set;}
    }

    public class _Parsesection 
    {
        public object wikitext {get; set;}
    }


    //For deserializing Introduction from wikitext to C# object
    public class IntroBase
    {
        public _Query query {get; set;}
    }

    public class _Query
    {
        public Dictionary<string, _Pages>  pages { get; set; }
    }

    public class _Pages
    {
        public int pageid { get; set; }
        public int ns { get; set; }
        public string title { get; set; }
        public string extract { get; set; }
    }
}  