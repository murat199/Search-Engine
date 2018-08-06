using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Yazlab3_AramaMotoru2.Models
{
    public class SiteSiralaModel
    {
        public SiteSiralaModel()
        {
            SubUrlDetails = new List<SubUrlDetail>();
        }
        public List<string> Urls { get; set; }
        public List<string> Words { get; set; }
        public string OrgChat { get; set; }
        public List<SubUrlDetail> SubUrlDetails { get; set; }
        public List<UrlDetail> UrlDetails { get; set; }
        public List<string> Links { get; set; }
    }
    public class SubUrlDetail
    {
        public SubUrlDetail()
        {
            UrlDetail = new UrlDetail();
        }
        public int Id { get; set; }
        public int ParentId { get; set; }
        public string Url { get; set; }
        public int Depth { get; set; }
        public List<string> SubUrls { get; set; }
        public UrlDetail UrlDetail { get; set; }
    }
}