using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace Yazlab3_AramaMotoru2.Operations
{
    public static class Synonym
    {
        private static string GetHtml(string word)
        {
            string source = "";
            using (WebClient client = new WebClient())
            {
                client.Encoding = Encoding.UTF8;
                source = client.DownloadString(String.Format("http://www.es-anlam.com/kelime/{0}", word)).ToString();
                source = HttpUtility.HtmlDecode(source);
            }
            return source;
        }
        public static List<string> GetSynonym(string word)
        {
            string html = GetHtml(word);
            var synonyms = new List<string>();
            int startIndex = html.IndexOf("<h2 style=\"font-size:24px\" id=\"esanlamlar\">");
            int endIndex;
            if (startIndex >= 0)
            {
                startIndex = html.Substring(startIndex).IndexOf("<strong>") + startIndex;
                endIndex = html.Substring(startIndex).IndexOf("</strong>");
                string source = html.Substring(startIndex, endIndex);
                source = source.Replace(" ", "").Replace("<strong>", "");
                string[] sourceSplits = source.Split(',');
                foreach(string sourceSplit in sourceSplits)
                {
                    synonyms.Add(sourceSplit);
                }
            }
            return synonyms;
        }
    }
}