using System;
using System.Collections.Generic;
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

        public List<ExtendedForecast> extendedForecast { get; set; }

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

            extendedForecast = new List<ExtendedForecast>();

            //Loop through the "tombstones" as they are called (vertical panes) to get 3/4 of the details
            foreach (var exDetail in html.DocumentNode.CssSelect("div.tombstone-container").ToArray())
            {
                extendedForecast.Add(new ExtendedForecast
                {
                    periodName = exDetail.CssSelect(".period-name").ToArray()[0].InnerText.Replace("Night", " Night"), //Unfortunately the space gets stripped out here but not in the long description section. So better to fix it now.
                    shortDescription = exDetail.CssSelect(".short-desc").ToArray()[0].InnerText,
                    temp_hl = exDetail.CssSelect(".temp").ToArray()[0].InnerText
                });

            }

            //Loop through the long table to get the extended forecast text
            foreach (var longDesc in html.DocumentNode.CssSelect("div#detailed-forecast-body div.row-forecast").ToArray())
            {
                    var exForecast = extendedForecast.First(ef =>
                        ef.periodName == longDesc.CssSelect("div.forecast-label").ToArray()[0].InnerText);
                    exForecast.longDescrition = longDesc.CssSelect("div.forecast-text").ToArray()[0].InnerText;
            }
            
        }
    }
}