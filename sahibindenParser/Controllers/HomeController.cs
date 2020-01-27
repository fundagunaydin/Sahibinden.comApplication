using HtmlAgilityPack;
using sahibindenParser.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace sahibindenParser.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            List<Parser> list;
            string url = "https://www.sahibinden.com/audi";
            HtmlWeb web = new HtmlWeb();
            HtmlAgilityPack.HtmlDocument document = web.Load(url);
            var node = document.DocumentNode.SelectNodes("//*[@id='searchResultsTable']/tbody[@class='searchResultsRowClass']/tr");

            list = new List<Parser>();
            for (int a = 1; a < node.Count; a++)
            {
                var resim = document.DocumentNode.SelectSingleNode("//*[@id='searchResultsTable']/tbody[@class='searchResultsRowClass']/tr[" + a.ToString() + "]/td[@class='searchResultsLargeThumbnail']/a/img");
                var seri = document.DocumentNode.SelectNodes("//*[@id='searchResultsTable']/tbody[@class='searchResultsRowClass']/tr[" + a.ToString() + "]/td[@class='searchResultsTagAttributeValue']");
                var baslik = document.DocumentNode.SelectSingleNode("//*[@id='searchResultsTable']/tbody[@class='searchResultsRowClass']/tr[" + a.ToString() + "]/td[@class='searchResultsTitleValue ']/a");
                var yil = document.DocumentNode.SelectNodes("//*[@id='searchResultsTable']/tbody[@class='searchResultsRowClass']/tr[" + a.ToString() + "]/td[@class='searchResultsAttributeValue']"); //km,renk,yıl Burada
                var fiyat = document.DocumentNode.SelectSingleNode("//*[@id='searchResultsTable']/tbody[@class='searchResultsRowClass']/tr[" + a.ToString() + "]/td[@class='searchResultsPriceValue']");
                var İlanTarihi = document.DocumentNode.SelectSingleNode("//*[@id='searchResultsTable']/tbody[@class='searchResultsRowClass']/tr[" + a.ToString() + "]/td[@class='searchResultsDateValue']");
                var il = document.DocumentNode.SelectSingleNode("//*[@id='searchResultsTable']/tbody[@class='searchResultsRowClass']/tr[" + a.ToString() + "]/td[@class='searchResultsLocationValue']");

                if (seri != null && baslik != null)
                {
                    list.Add(new Parser() { Baslik = baslik.InnerText.Trim(), Seri = seri[0].InnerText.ToString().Trim(), Model = seri[1].InnerText.ToString().Trim(), Yil = yil[0].InnerText.Trim(), Km = yil[1].InnerText.ToString().Trim(), Renk = yil[2].InnerText.ToString().Trim(), Fiyat = fiyat.InnerText.Trim(), Ilan_Tarihi = İlanTarihi.InnerText.Trim(), Il_Ilce = il.InnerText.Trim() });

                }

            }

            return View(list);
        }

       
    }
}