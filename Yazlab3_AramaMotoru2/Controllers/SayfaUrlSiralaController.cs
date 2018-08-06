using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Yazlab3_AramaMotoru2.Models;
using Yazlab3_AramaMotoru2.Operations;

namespace Yazlab3_AramaMotoru2.Controllers
{
    public class SayfaUrlSiralaController : Controller
    {
        // GET: SayfaUrlSirala
        public ActionResult Index(string urls, string words)
        {
            SayfaUrlSiralaModel model = new SayfaUrlSiralaModel();
            model.Words = StringOperations.GetListBySplit(words, ',');
            model.Urls = StringOperations.GetListBySplit(urls, ',');
            foreach(var url in model.Urls)
            {
                UrlDetail urldetail = new UrlDetail();
                urldetail.Url = url;
                urldetail.SourceHtml = SiteSource.GetHtml(url).ToLower();
                urldetail.SourceHtml = HtmlPack.GetHtmlExludePopup(urldetail.SourceHtml);
                urldetail.CleanHtml = SiteSource.GetCleanHtml(urldetail.SourceHtml).ToLower();
                foreach(var word in model.Words)
                {
                    Keyword keyword = new Keyword();
                    keyword.Url = url;
                    keyword.Word = word;
                    keyword.Count = 0;
                    List<string> languageCompatibles = new List<string>();
                    languageCompatibles = StringOperations.GetLanguageLowerCompatible(word);
                    languageCompatibles = StringOperations.GetDifferentWords(languageCompatibles);
                    foreach (var languageCompatible in languageCompatibles)
                    {
                        keyword.Count += StringOperations.GetCountWordInSentence(urldetail.CleanHtml, languageCompatible);
                    }
                    urldetail.Keywords.Add(keyword);
                }
                List<int> countList = new List<int>();
                foreach(var keyword in urldetail.Keywords)
                {
                    countList.Add(keyword.Count);
                }
                urldetail.PointByCount = PageRank.GetPointByCount(countList);
                urldetail.PointByMeta = PageRank.GetPointByMeta(urldetail.SourceHtml, model.Words);
                urldetail.PointByHeader = PageRank.GetPointByHead(urldetail.SourceHtml, model.Words);
                urldetail.PointByTitle = PageRank.GetPointByTitle(urldetail.SourceHtml, model.Words);
                urldetail.PointSum += urldetail.PointByCount + urldetail.PointByMeta + urldetail.PointByHeader + urldetail.PointByTitle;
                model.UrlDetails.Add(urldetail);
            }
            model.UrlDetailsAsc = model.UrlDetails.OrderBy(u => u.PointSum).ToList();
            model.UrlDetailsDesc = model.UrlDetails.OrderByDescending(u => u.PointSum).ToList();
            return View(model);
        }
    }
}