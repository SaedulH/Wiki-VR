namespace WebData
{ 
    //For deserializing Featured Article from wikitext to C# object
    public class FeaturedBase
    {
        public _Tfa tfa {get; set;}
    }

    public class _Tfa
    {
        public string displaytitle {get; set;}
    }

    //For deserializing Random Article from wikitext to C# object

    public class RandomBase
    {
        public _RandomQuery query {get;set;}
    }

    public class _RandomQuery
    {
        public _Random[] random {get; set;}
    } 

    public class _Random
    {
        public string title {get; set;}
    }
}
