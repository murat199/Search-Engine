using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Yazlab3_AramaMotoru2.Models;
using Yazlab3_AramaMotoru2.Operations;

namespace Yazlab3_AramaMotoru2.Controllers
{
    public class SiteSiralaController : Controller
    {
        // GET: SiteSiralama
        public ActionResult Index(string urls, string words)
        {
            int counter = 0;
            int depthId1 = 0, depthId2 = 0, depthId3 = 0;
            int id = 0;
            SiteSiralaModel model = new SiteSiralaModel();
            model.Urls = StringOperations.GetListBySplit(urls, ',');
            model.Words = StringOperations.GetListBySplit(words, ',');
            model.Links = new List<string>();
            foreach (var url in model.Urls)
            {
                id++;
                SiteSiralaFunctions.SiteOrderByDepth(url, 1, id, model.Words);
                foreach (var link in SiteSiralaFunctions.links)
                {
                    model.Links.Add(link);
                }
                foreach (var subUrlDetail in SiteSiralaFunctions.subUrlDetails)
                {
                    model.SubUrlDetails.Add(subUrlDetail);
                }
                SiteSiralaFunctions.ClearValues();
            }
            foreach (var subUrlDetail in model.SubUrlDetails)
            {
                counter++;
                if (subUrlDetail.Depth == 1)
                {
                    subUrlDetail.Id = counter;
                    depthId1 = subUrlDetail.Id;
                    subUrlDetail.ParentId = 0;
                }
                else if (subUrlDetail.Depth == 2)
                {
                    subUrlDetail.Id = counter;
                    depthId2 = subUrlDetail.Id;
                    subUrlDetail.ParentId = depthId1;
                }
                else if (subUrlDetail.Depth == 3)
                {
                    subUrlDetail.Id = counter;
                    depthId3 = subUrlDetail.Id;
                    subUrlDetail.ParentId = depthId2;
                }
            }
            model.SubUrlDetails = SiteSiralaFunctions.GetSubUrlDetailSort(model.SubUrlDetails);
            model.OrgChat = SiteSiralaFunctions.GetOrgChat(model.SubUrlDetails);
            return View(model);
        }
    }
}