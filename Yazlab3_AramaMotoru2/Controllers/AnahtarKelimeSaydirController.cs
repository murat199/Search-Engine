using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Yazlab3_AramaMotoru2.Models;
using Yazlab3_AramaMotoru2.Operations;

namespace Yazlab3_AramaMotoru2.Controllers
{
    public class AnahtarKelimeSaydirController : Controller
    {
        public ActionResult Index(string urls, string words)
        {
            if (urls == "" || words == "")
                return HttpNotFound();
            AnahtarKelimeSaydirModel model = new AnahtarKelimeSaydirModel();
            model.urls = StringOperations.GetListBySplit(urls, ',');
            model.words = StringOperations.GetListBySplit(words, ',');
            foreach (var url in model.urls)
            {
                string html = SiteSource.GetHtml(url);
                html = HtmlPack.GetHtmlExludePopup(html);
                string cleanHtml = SiteSource.GetCleanHtml(html).ToLower();
                foreach (var word in model.words)
                {
                    List<string> compatibleWords = new List<string>();
                    compatibleWords = StringOperations.GetLanguageLowerCompatible(word);
                    compatibleWords = StringOperations.GetDifferentWords(compatibleWords);
                    Keyword keyword = new Keyword();
                    keyword.Word = word;
                    keyword.Url = url;
                    keyword.Count = 0;
                    foreach (string compatibleWord in compatibleWords)
                    {
                        keyword.Count += StringOperations.GetCountWordInSentence(cleanHtml, compatibleWord);
                    }
                    model.keywords.Add(keyword);
                }
            }
            return View(model);
        }
    }
}