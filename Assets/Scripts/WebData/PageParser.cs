using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Text.RegularExpressions;

public class PageParser : MonoBehaviour
{
    // Start is called before the first frame update

    public void parseText(string content)
    {
        string spaces = content.Replace("\n\n", "\n\nflubby");
        string nosquarecontent = spaces.Replace("[[", "").Replace("]]", "").Replace("[", "").Replace("]", "");
        string nocurlycontent = Regex.Replace(nosquarecontent, @"/{([^}]*)}/g", "");
        string noUrlText = Regex.Replace(nocurlycontent, @"http[^\s]+", "");
    }

}
