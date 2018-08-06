using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Yazlab3_AramaMotoru2.Models;

namespace Yazlab3_AramaMotoru2.Operations
{
    public static class SiteSiralaFunctions
    {
        public static List<string> links = new List<string>();
        public static List<SubUrlDetail> subUrlDetails = new List<SubUrlDetail>();
        public static void SiteOrderByDepth(string url, int depth, int id, List<string> words)
        {
            if (depth == 3)
                return;
            string html = SiteSource.GetHtml(url).ToLower();
            string cleanHtml = SiteSource.GetCleanHtml(html);
            List<string> htmlLinks = SiteSource.GetSubUrls(html);
            htmlLinks = SiteSource.GetUrlsWithoutExtenscions(htmlLinks, url);
            
            foreach(string htmlLink in htmlLinks)
            {
                if (!links.Contains(htmlLink))
                {
                    string htmlUrl = "";
                    string cleanHtmlUrl = "";
                    if (htmlLink == html)
                    {
                        htmlUrl = html;
                        htmlUrl = HtmlPack.GetHtmlExludePopup(htmlUrl);
                        cleanHtmlUrl = cleanHtml;
                    }
                    else
                    {
                        htmlUrl = SiteSource.GetHtml(htmlLink).ToLower();
                        htmlUrl = HtmlPack.GetHtmlExludePopup(htmlUrl);
                        cleanHtmlUrl = SiteSource.GetCleanHtml(htmlUrl);
                    }
                    SubUrlDetail subUrlDetail = new SubUrlDetail();
                    subUrlDetail.Depth = depth;
                    subUrlDetail.Id = id;
                    subUrlDetail.Url = htmlLink;
                    subUrlDetail.ParentId = id - 1;
                    subUrlDetail.UrlDetail.Depth = depth;
                    subUrlDetail.UrlDetail.SourceHtml = htmlUrl;
                    subUrlDetail.UrlDetail.CleanHtml = cleanHtmlUrl;
                    subUrlDetail.UrlDetail.Url = htmlLink;
                    foreach (var word in words)
                    {
                        Keyword keyword = new Keyword();
                        keyword.Url = htmlLink;
                        keyword.Word = word.ToString();
                        keyword.Count = 0;
                        List<string> languageCompatibles = new List<string>();
                        languageCompatibles = StringOperations.GetLanguageLowerCompatible(keyword.Word);
                        languageCompatibles = StringOperations.GetDifferentWords(languageCompatibles);
                        foreach (var languageCompatible in languageCompatibles)
                        {
                            keyword.Count += StringOperations.GetCountWordInSentence(cleanHtmlUrl, languageCompatible);
                        }
                        subUrlDetail.UrlDetail.Keywords.Add(keyword);
                    }
                    List<int> countList = new List<int>();
                    foreach (var keyword in subUrlDetail.UrlDetail.Keywords)
                    {
                        countList.Add(keyword.Count);
                    }
                    subUrlDetail.UrlDetail.PointByCount = PageRank.GetPointByCount(countList);
                    subUrlDetail.UrlDetail.PointSum += subUrlDetail.UrlDetail.PointByCount;
                    subUrlDetails.Add(subUrlDetail);

                    links.Add(htmlLink);
                    SiteOrderByDepth(htmlLink, depth + 1, id + 1, words);
                }
            }
        }
        public static void ClearValues()
        {
            links.Clear();
            subUrlDetails.Clear();
        }
        public static List<SubUrlDetail> GetSubUrlDetailSort(List<SubUrlDetail> subUrlDetails)
        {
            List<SubUrlDetail> entity = new List<SubUrlDetail>();
            List<SubUrlDetail> subUrlDepth1 = subUrlDetails.Where(s => s.Depth == 1).OrderByDescending(s => s.UrlDetail.PointSum).ToList();
            List<SubUrlDetail> subUrlDepth2 = subUrlDetails.Where(s => s.Depth == 2).ToList();
            List<SubUrlDetail> subUrlDepth3 = subUrlDetails.Where(s => s.Depth == 3).ToList();
            foreach (var depthList1 in subUrlDepth1)
            {
                entity.Add(depthList1);
                subUrlDepth2 = subUrlDetails.Where(s => s.Depth == 2 && s.ParentId == depthList1.Id).OrderByDescending(s => s.UrlDetail.PointSum).ToList();
                foreach (var depthList2 in subUrlDepth2)
                {
                    entity.Add(depthList2);
                    subUrlDepth3 = subUrlDetails.Where(s => s.Depth == 3 && s.ParentId == depthList2.Id).OrderByDescending(s => s.UrlDetail.PointSum).ToList();
                    foreach (var depthList3 in subUrlDepth3)
                    {
                        entity.Add(depthList3);
                    }
                }
            }
            return entity;
        }
        public static string GetOrgChat(List<SubUrlDetail> subUrlDetails)
        {
            string result = "<div class=\"tree\">";
            List<SubUrlDetail> subUrlDepth1 = subUrlDetails.Where(s => s.Depth == 1).OrderByDescending(s => s.UrlDetail.PointSum).ToList();
            List<SubUrlDetail> subUrlDepth2 = subUrlDetails.Where(s => s.Depth == 2).ToList();
            List<SubUrlDetail> subUrlDepth3 = subUrlDetails.Where(s => s.Depth == 3).ToList();
            if (subUrlDepth1 != null) result += "<ul>";
            foreach (var depthList1 in subUrlDepth1)
            {
                result += "<li><a href=\"#\"><img src=\"/Content/img/87x90.png\">" + String.Format("Id : {0}", depthList1.Id.ToString()) + "</a>";
                subUrlDepth2 = subUrlDetails.Where(s => s.Depth == 2 && s.ParentId == depthList1.Id).OrderByDescending(s => s.UrlDetail.PointSum).ToList();
                if (subUrlDepth2 != null) result += "<ul>";
                foreach (var depthList2 in subUrlDepth2)
                {
                    result += "<li><a href=\"#\"><img src=\"/Content/img/87x90.png\">" + String.Format("Id : {0}", depthList2.Id.ToString()) + "</a>";
                    subUrlDepth3 = subUrlDetails.Where(s => s.Depth == 3 && s.ParentId == depthList2.Id).OrderByDescending(s => s.UrlDetail.PointSum).ToList();
                    if (subUrlDepth3 != null) result += "<ul>";
                    foreach (var depthList3 in subUrlDepth3)
                    {
                        result += "<li><a href=\"#\"><img src=\"/Content/img/87x90.png\">" + String.Format("Id : {0}", depthList2.Id.ToString()) + "</a>";
                    }
                    if (subUrlDepth3 != null) result += "</ul>";
                }
                if (subUrlDepth2 != null) result += "</ul>";
                result += "</li>";
            }
            if (subUrlDepth1 != null) result += "</ul>";
            result += "</div>";
            return result;
        }
    }
}