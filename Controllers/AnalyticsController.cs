using HtmlAgilityPack;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Net;
using System.Text;
using System.IO;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using ElectricityAPI1.Models;

namespace ElectricityAPI1.Controllers
{
    public class AnalyticsController : Controller
    {
        private static async Task<string> CallUrl(string fullUrl)
        {
            LogSystem.log("Scraping website started.");
            HttpClient client = new HttpClient();
            var response = await client.GetStringAsync(fullUrl);
            if (response == string.Empty) LogSystem.logError("Response is empty!");
            else LogSystem.log("Response got successfully");
            return response;
        }
        public IActionResult Index()
        {
            string url = "https://data.gov.lt/dataset/siame-duomenu-rinkinyje-pateikiami-atsitiktinai-parinktu-1000-buitiniu-vartotoju-automatizuotos-apskaitos-elektriniu-valandiniai-duomenys";
            var response = CallUrl(url).Result;
            downloadFiles(ParseHtml(response, url));
            return View();
        }
        private List<string> ParseHtml(string html, string url)
        {
            LogSystem.log("Response parsing started.");
            int lastMonthsCount = 2;
            var downloadLinks = new List<string>();
            string mainUrl = string.Empty;
            string tempLink = string.Empty;

            HtmlDocument htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(html);

            mainUrl = url.Substring(0, url.IndexOf(".lt") + 3);

            for (int i = 0; i < lastMonthsCount; i++)
            {
                tempLink = htmlDoc.DocumentNode.SelectNodes(String.Format(
                                  "//table[@id='resource-table']/tbody/tr[last()-{0}]/td[last()]/div/a[last()]", i))[0].Attributes["href"].Value;
                if(!tempLink.StartsWith(mainUrl))
                {
                    tempLink = String.Format("{0}{1}", mainUrl, tempLink);
                }
                LogSystem.log($"Response link got: {tempLink}");
                downloadLinks.Add(tempLink);
            }
            
            return downloadLinks;
        }
        private void downloadFiles(List<string> links)
        {
            LogSystem.log("Downloading files started.");
            string fileName = string.Empty;
            using var client = new HttpClient();
            try
            {
                foreach (var link in links)
                {
                    fileName = String.Format("Files/{0}", Path.GetFileName(link));
                    using var s = client.GetStreamAsync(link);
                    using var fs = new FileStream(fileName, FileMode.OpenOrCreate);
                    s.Result.CopyTo(fs);
                }
            }
            catch (Exception ex)    
            {
                LogSystem.logError($"Exception caught when downloading files: {ex}");
                throw ex;
            }
            
        }
    }
}
