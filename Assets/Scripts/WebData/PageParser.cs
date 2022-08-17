using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
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

        public string parseText(string content)
        {   
            Regex regexcurly = new Regex("{{[^}]+}}");

            Regex regexfile = new Regex("File[^\\n]+\\n");

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

            return content;

        }

    }
}
