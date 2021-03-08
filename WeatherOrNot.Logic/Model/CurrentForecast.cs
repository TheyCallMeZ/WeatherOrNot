using System.Linq;
using HtmlAgilityPack;
using ScrapySharp.Extensions;

namespace WeatherOrNot.Logic.Model
{
    /// <summary>
    /// Current forecase for a given zip code on weather.gov
    /// </summary>
    public class CurrentForecast
    {
        
        public string shortDescription { get; set; }
        public string farenheit { get; set; }
        public string celcius { get; set; }
        public string humidity { get; set; }
        public string windSpeed { get; set; }
        public string barometer { get; set; }
        public string dewpoint { get; set; }
        public string visibilty { get; set; }
        public string lastUpdate { get; set; }

        /// <summary>
        /// Parse the HTML Input scraped from weather.gov
        /// </summary>
        /// <param name="htmlSource">html string</param>
        public CurrentForecast(string htmlSource)
        {
            //Create an HTML document and scrape the text off the page
            var html = new HtmlDocument();
            html.LoadHtml(htmlSource);

            shortDescription = html.DocumentNode.CssSelect(".myforecast-current").ToArray()[0].InnerText;
            farenheit = html.DocumentNode.CssSelect(".myforecast-current-lrg").ToArray()[0].InnerText;
            celcius = html.DocumentNode.CssSelect(".myforecast-current-sm").ToArray()[0].InnerText;
            //JQuery Selector "#current_conditions_detail table tbody tr td:nth-child(2)" is functional but does not want to recognize the ":" character as a valid selector in ScrapySharp
            //So we have to cheat and get all of the td elements getting all of the "odd" numbered ones
            humidity = html.DocumentNode.CssSelect("#current_conditions_detail td").ToArray()[1].InnerText;
            windSpeed = html.DocumentNode.CssSelect("#current_conditions_detail td").ToArray()[3].InnerText;
            barometer = html.DocumentNode.CssSelect("#current_conditions_detail td").ToArray()[5].InnerText;
            dewpoint = html.DocumentNode.CssSelect("#current_conditions_detail td").ToArray()[7].InnerText;
            visibilty = html.DocumentNode.CssSelect("#current_conditions_detail td").ToArray()[9].InnerText;
            lastUpdate = html.DocumentNode.CssSelect("#current_conditions_detail td").ToArray()[11].InnerText.Trim();
        }
    }
}