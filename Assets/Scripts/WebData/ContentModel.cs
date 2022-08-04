using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;

namespace WebData
{   

    public class Base
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
        public int index {get; set;}
        public string line {get; set;} 
    }

    public class PageTextModel
    {
        public string wikitext {get; set;}
    }
}
