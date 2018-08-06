using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Yazlab3_AramaMotoru2.Models
{
    public class SayfaUrlSiralaModel
    {
        public SayfaUrlSiralaModel()
        {
            UrlDetails = new List<UrlDetail>();
        }
        public List<UrlDetail> UrlDetails { get; set; }
        public List<string> Words { get; set; }
        public List<string> Urls { get; set; }
        public List<UrlDetail> UrlDetailsAsc { get; set; }
        public List<UrlDetail> UrlDetailsDesc { get; set; }
    }
    public class UrlDetail
    {
        public UrlDetail()
        {
            Keywords = new List<Keyword>();
        }
        public string Url { get; set; }
        public int PointSum { get; set; }
        public List<Keyword> Keywords { get; set; }
        public string SourceHtml { get; set; }
        public string CleanHtml { get; set; }
        public int PointByCount { get; set; }
        public int PointByMeta { get; set; }
        public int PointByHeader { get; set; }
        public int PointByTitle { get; set; }
        public int Depth { get; set; }
    }
    public static class PageRank
    {
        public static int GetPointByCount(List<int> counts)
        {
            int result = 1;
            foreach(int count in counts)
            {
                int num = Convert.ToInt32(count);
                num = (count == 0) ? 1 : num + 5;
                result *= num;
            }
            return result;
        }
        public static int GetPointByMeta(string html, List<string> searchWords)
        {
            int result = 0;
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(html);
            HtmlNode mdnode = doc.DocumentNode.SelectSingleNode("//meta[@name='description']");
            if (mdnode != null)
            {
                HtmlAttribute desc;
                desc = mdnode.Attributes["content"];
                foreach(var searchWord in searchWords)
                {
                    if (desc.Value.Contains(searchWord)) result += 100;
                }
            }
            return result;
        }
        public static int GetPointByHead(string html, List<string> searchWords)
        {
            int result = 0;
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(html);
            //HtmlNode mdnode = doc.DocumentNode.SelectSingleNode("//head/title");

            string xpathQuery = "//*[starts-with(name(),'h') and string-length(name()) = 2 and number(substring(name(), 2)) <= 6]";
            var nodes = doc.DocumentNode.SelectNodes(xpathQuery);
            if (nodes != null)
            {
                foreach(var node in nodes)
                {
                    foreach (var searchWord in searchWords)
                    {
                        if (node.InnerText.Contains(searchWord)) result += 100;
                    }
                }
            }
            return result;
        }
        public static int GetPointByTitle(string html, List<string> searchWords)
        {
            int result = 0;
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(html);
            HtmlNode mdnode = doc.DocumentNode.SelectSingleNode("//head/title");
            if (mdnode != null)
            {
                foreach(var searchWord in searchWords)
                {
                    if (mdnode.InnerHtml.Contains(searchWord)) result += 100;
                }
            }
            return result;
        }
    }
}