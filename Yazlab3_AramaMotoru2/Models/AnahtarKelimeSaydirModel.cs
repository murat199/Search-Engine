using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Yazlab3_AramaMotoru2.Models
{
    public class AnahtarKelimeSaydirModel
    {
        public AnahtarKelimeSaydirModel()
        {
            keywords = new List<Keyword>();
        }
        public List<string> words { get; set; }
        public List<string> urls { get; set; }
        public List<Keyword> keywords { get; set; }
    }
    public class Keyword
    {
        public string Word { get; set; }
        public string Url { get; set; }
        public int Count { get; set; }
    }
}