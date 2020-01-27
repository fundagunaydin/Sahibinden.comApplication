using HtmlAgilityPack;
using sahibindenParser.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace sahibindenParser.Controllers
{
    public class ParserController : Controller
    {
        public string trimText(string text) {
            char[] arrayText = text.ToCharArray();
            string first = "";
            string last="";
            string result=" ";
            string str = new string(arrayText);
            for (int i=1;i<arrayText.Length;i++)
            {
                if (char.IsUpper(arrayText[i]))
                {
                    first = str.Substring(0, i);
                    last = str.Substring(i);
                    result = first + "+" + last;                        
                } 
            }
            return result;
        }
        // GET: Parser
        public ActionResult Index()
        {

            return View();
        }
        [HttpPost]
        public JsonResult getMapLocations(string url,bool urlCheck)
        {

            string firstUrl = "https://www.sahibinden.com/";
          
           if(urlCheck)
            {
                firstUrl = url;
            }
            else
            {
                firstUrl = firstUrl + url;
            }
            
          
            List<Parser> list;
            int[] array = new int[5];
            HtmlWeb web = new HtmlWeb(); 
            HtmlAgilityPack.HtmlDocument document = web.Load(firstUrl);
            var node = document.DocumentNode.SelectNodes("//*[@id='searchResultsTable']/tbody[@class='searchResultsRowClass']/tr");
           
            if (node!=null)
            {
                list = new List<Parser>();
                for (int a = 1; a < node.Count; a++)
                {
                    var uri = "";
                    var resim = "";



                    var seri = document.DocumentNode.SelectNodes("//*[@id='searchResultsTable']/tbody[@class='searchResultsRowClass']/tr[" + a.ToString() + "]/td[@class='searchResultsTagAttributeValue']");

                    if (seri != null) {
                        uri = document.DocumentNode.SelectSingleNode("//*[@id='searchResultsTable']/tbody[@class='searchResultsRowClass']/tr[" + a.ToString() + "]/td[@class='searchResultsLargeThumbnail']/a").Attributes["href"].Value;
                        resim = document.DocumentNode.SelectSingleNode("//*[@id='searchResultsTable']/tbody[@class='searchResultsRowClass']/tr[" + a.ToString() + "]/td[@class='searchResultsLargeThumbnail']/a/img").Attributes["src"].Value;
                        }
                    
                    var baslik = document.DocumentNode.SelectSingleNode("//*[@id='searchResultsTable']/tbody[@class='searchResultsRowClass']/tr[" + a.ToString() + "]/td[@class='searchResultsTitleValue ']/a");
                    var yil = document.DocumentNode.SelectNodes("//*[@id='searchResultsTable']/tbody[@class='searchResultsRowClass']/tr[" + a.ToString() + "]/td[@class='searchResultsAttributeValue']"); //km,renk,yıl Burada
                    var fiyat = document.DocumentNode.SelectSingleNode("//*[@id='searchResultsTable']/tbody[@class='searchResultsRowClass']/tr[" + a.ToString() + "]/td[@class='searchResultsPriceValue']");
                    var İlanTarihi = document.DocumentNode.SelectSingleNode("//*[@id='searchResultsTable']/tbody[@class='searchResultsRowClass']/tr[" + a.ToString() + "]/td[@class='searchResultsDateValue']");
                    var il = document.DocumentNode.SelectSingleNode("//*[@id='searchResultsTable']/tbody[@class='searchResultsRowClass']/tr[" + a.ToString() + "]/td[@class='searchResultsLocationValue']");

                    if (seri != null && baslik != null)
                    {
                        if (seri.Count<2)
                        {
                            list.Add(new Parser() { Resim = resim.ToString(), Url = uri.ToString(), Baslik = baslik.InnerText.Trim(), Seri = "", Model = seri[0].InnerText.ToString().Trim(), Yil = yil[0].InnerText.Trim(), Km = yil[1].InnerText.ToString().Trim(), Renk = yil[2].InnerText.ToString().Trim(), Fiyat = fiyat.InnerText.Trim(), Ilan_Tarihi = İlanTarihi.InnerText.Trim(), Il_Ilce = trimText(il.InnerText.Trim()) });

                        }
                        else
                        {

                            list.Add(new Parser() { Resim = resim.ToString(), Url = uri.ToString(), Baslik = baslik.InnerText.Trim(), Seri = seri[0].InnerText.ToString().Trim(), Model = seri[1].InnerText.ToString().Trim(), Yil = yil[0].InnerText.Trim(), Km = yil[1].InnerText.ToString().Trim(), Renk = yil[2].InnerText.ToString().Trim(), Fiyat = fiyat.InnerText.Trim(), Ilan_Tarihi = İlanTarihi.InnerText.Trim(), Il_Ilce = trimText(il.InnerText.Trim()) });
                        }
                    }
                  
                
                }
                return Json(list, JsonRequestBehavior.AllowGet);
            }
            else
            {

                return Json(false, JsonRequestBehavior.AllowGet);
            }

            

          
        }
       
      
    }

}