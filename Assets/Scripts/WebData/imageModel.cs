using System.Collections.Generic;

namespace WebData
{ 

    //For deserializing Image from JSON to C# object
    public class ImageBase
    {
        public _ImageQuery query {get; set;}
    }

    public class _ImageQuery
    {
        public Dictionary<string, _ImagePages>  pages { get; set; }
    }

    public class _ImagePages
    {
        public int pageid { get; set; }
        public int ns { get; set; }
        public string title { get; set; }
        public _Thumbnail thumbnail { get; set; }
        public string pageimage {get; set;}
    }

    public class _Thumbnail
    {
        public string source {get; set;}
        public float width {get; set;}
        public float height {get; set;}
    }

}  