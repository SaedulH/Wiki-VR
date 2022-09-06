using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WebData{

    public class PageViewBase
    {
        public _Items[] items {get; set;}
    }

    public class _Items
    {
        public int views {get; set;}
    }
}
