using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Yazlab3_AramaMotoru2.Models
{
    public class SemantikAnalizModel
    {
        public SemantikAnalizModel()
        {
            Urls = new List<string>();
            Words = new List<string>();
            Synonyms = new List<EsAnlamliSozcuk>();
            SubUrlDetails = new List<SubUrlDetail>();
        }
        public List<string> Urls { get; set; }
        public List<string> Words { get; set; }
        public string OrgChat { get; set; }
        public List<SubUrlDetail> SubUrlDetails { get; set; }
        public List<UrlDetail> UrlDetails { get; set; }
        public List<string> Links { get; set; }
        public List<EsAnlamliSozcuk> Synonyms { get; set; }
    }
    public class EsAnlamliSozcuk
    {
        public EsAnlamliSozcuk()
        {
            Words = new List<string>();
        }
        public string Source { get; set; }
        public List<string> Words { get; set; }
        public string WordFirst { get; set; }
    }
}