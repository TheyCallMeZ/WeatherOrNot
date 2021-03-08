using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherOrNot.Logic.Model
{
    public class ExtendedForecast
    {
        public string periodName { get; set; }
        public string shortDescription { get; set; }
        public string temp_hl { get; set; }
        public string longDescrition { get; set; }

    }
}
