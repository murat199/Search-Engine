using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HtmlAgilityPack;

namespace Yazlab3_AramaMotoru2.Operations
{
    public static class HtmlPack
    {
        public static string GetHtmlExludePopup(string html)
        {
            string result = html;
            HtmlDocument document = new HtmlDocument();
            document.OptionOutputAsXml = true;
            document.LoadHtml(html);
            HtmlNode[] nodes = document.DocumentNode.Descendants("div").Where(d => d.Attributes.Contains("class") && d.Attributes["class"].Value.Contains("modal-dialog")).ToArray();
            foreach(var node in nodes)
            {
                node.ParentNode.Remove();
            }
            result = document.DocumentNode.OuterHtml;
            return result;
        }
        public static string GetHtmlExludeCommentLine(string html)
        {
            string result = "";
            return result;
        }
    }
}