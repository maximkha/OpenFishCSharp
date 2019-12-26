using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace OpenFishPort
{
    class OpenFish
    {
        //IP, PORT, COUNTRY CODE
        public List<Tuple<string, int, string>> Proxies = new List<Tuple<string, int, string>>();
        static Random rnd = new Random();

        private string GetHtml(string url)
        {
            string html = string.Empty;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.AutomaticDecompression = DecompressionMethods.GZip;

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                html = reader.ReadToEnd();
            }
            return html;
        }

        public OpenFish()
        {
            string html = GetHtml("https://www.sslproxies.org/");
            HtmlDocument htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(html);

            HtmlNodeCollection IpNodes = htmlDoc.DocumentNode.SelectNodes("//table[@id=\"proxylisttable\"]/tbody//td[1]");
            HtmlNodeCollection PortNodes = htmlDoc.DocumentNode.SelectNodes("//table[@id=\"proxylisttable\"]/tbody//td[2]");
            HtmlNodeCollection CountryNodes = htmlDoc.DocumentNode.SelectNodes("//table[@id=\"proxylisttable\"]/tbody//td[3]");

            if (IpNodes.Count != PortNodes.Count || IpNodes.Count != CountryNodes.Count) throw new Exception("Node length mismatch");

            for (int i = 0; i < IpNodes.Count; i++)
            {
                Proxies.Add(new Tuple<string, int, string>(IpNodes[i].InnerText, int.Parse(PortNodes[i].InnerText), CountryNodes[i].InnerText));
            }
        }

        public Tuple<string, int, string> GetRandom()
        {
            int r = rnd.Next(Proxies.Count);
            return Proxies[r];
        }
    }
}
