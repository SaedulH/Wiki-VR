using UnityEngine;
using MwParserFromScratch;
using System.Text.RegularExpressions;
using TMPro;

namespace WebData
{
    public class PageParser : MonoBehaviour
    {   

        [SerializeField]
        private TextMeshProUGUI Old;
        [SerializeField]
        private TextMeshProUGUI New;

        // Parses Wikipedia text into plain text
        public string parseText(string content)
        {   
            Regex regexcurly = new Regex("{{[^}]+}}");
            Regex regexfile = new Regex("File[^\\n]+\\n");
                Regex regexthumb = new Regex("thumb[^\\n]+\\n");
                Regex regexleft = new Regex("left[^\\n]+\\n");
                Regex regexalt = new Regex("alt[^\\n]+\\n");
                Regex regexupright = new Regex("upright[^\\n]+\\n");

            var parser = new WikitextParser();

            var ast = parser.Parse(content);

            content = ast.ToPlainText();

            content = content.Substring(content.IndexOf("==\\n")).Replace("==\\n", "");
            content = content.Replace("\\n","\n");

            content = regexcurly.Replace(content, "");

            content = content.Replace("{", "").Replace("}", "").Replace("\"", "").Replace("\\","\"");

            content = content.Replace("[[", "").Replace("]]", "").Replace("[", "").Replace("]", "");

            content = Regex.Replace(content, @"http[^\s]+", "");

            content = regexfile.Replace(content, "");

            content = regexthumb.Replace(content, "");

            content = regexleft.Replace(content, "");

            content = regexalt.Replace(content, "");

            content = regexupright.Replace(content, "");

            return content;

        }
        
        // Parses infobox to correct format (unused)
        public void parseInfobox(string infobox)
        {
            infobox = infobox.Split("\\n\\n")[0];

            infobox = infobox.Substring(infobox.IndexOf("Infobox"));

            infobox = infobox.Replace("\\n","\n");

            infobox = infobox.Replace("[[", "").Replace("]]", "").Replace("[", "").Replace("]", "");
            //return infobox;
            New.text = infobox;
        }

    }

}
