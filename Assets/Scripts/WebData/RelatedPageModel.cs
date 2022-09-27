using System.Collections.Generic;

namespace WebData
{ 

    public class RelatedBase
    {
        public _RelatedQuery query {get; set;}
    }
    public class _RelatedQuery
    {
        public Dictionary<string, _RelatedPages>  pages { get; set; }
    }

    public class _RelatedPages
    {
        public int pageid { get; set; }
        public int ns { get; set; }
        public string title { get; set; }
        public _Links[] links { get; set; }
    }

    public class _Links
    {
        public int ns {get; set;}
        public string title {get; set;}
    }
}