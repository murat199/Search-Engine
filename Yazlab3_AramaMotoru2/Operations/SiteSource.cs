using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Script.Serialization;

namespace Yazlab3_AramaMotoru2.Operations
{
    public class SiteSource
    {
        public static string GetHtml(string url)
        {
            string source = "";
            using (WebClient client = new WebClient())
            {
                try
                {
                    client.Encoding = Encoding.UTF8;
                    source = client.DownloadString(url).ToString();
                    source = HttpUtility.HtmlDecode(source);
                }
                catch (Exception)
                {
                    return "";
                }
            }
            return source;
        }
        public static string GetCleanHtml(string source)
        {
            return Regex.Replace(source, "<[^>]*>", " ");
        }
        public static int GetCountWordInSite(string word, string url, List<string> compliantWords)
        {
            if (word == null || url == null)
                return 0;
            int sourceLength = 0, targetLength = 0, result = 0;
            string sourceHtml = GetHtml(url);
            string sourceCleanHtml = GetCleanHtml(sourceHtml).ToLower();
            foreach (var compliantWord in compliantWords)
            {
                sourceLength = sourceCleanHtml.Length;
                targetLength = sourceCleanHtml.Replace(compliantWord, null).Length;
                result += (sourceLength - targetLength) / (word.Length);
            }
            return result;
        }
        public static List<string> GetSubUrls(string html)
        {
            if (html == null)
                return null;
            var r = new Regex("<a.*?href=\"(.*?)\".*?>");
            var output = r.Matches(html);
            var subUrls = new List<string>();
            foreach (var item in output)
            {
                subUrls.Add((item as Match).Groups[1].Value);
            }
            return subUrls;
        }
        public static List<string> GetUrlsWithoutExtenscions(List<string> urls, string siteUrl)
        {
            List<string> extenscions = new List<string>(new string[] { "pdf", "file","#", "jpg", "png", " ", "xls","docx" });
            List<string> compatibleUrls= new List<string>();
            bool hasExtenscion = false;
            compatibleUrls.Add(siteUrl);
            foreach (var url in urls)
            {
                foreach(var extenscion in extenscions)
                {
                    if (url.Contains(extenscion) || (!url.Contains(siteUrl)))
                    {
                        hasExtenscion = true;
                        break;
                    }
                }
                if (!hasExtenscion)
                    compatibleUrls.Add(String.Format("{0}", url));
                hasExtenscion = false;
            }
            return compatibleUrls;
        }
    }
}