using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using AngleSharp.Html.Parser;

namespace NovelGameLib
{
    public class NovelAPI
    {

        public const string SEARCH_URL = "https://erogamescape.dyndns.org/~ap2/ero/toukei_kaiseki/kensaku.php?category=game&word_category=name&word={0}";
        public async static Task<List<NovelGame>> SearchByName(string word)
        {
            List<NovelGame> result = new List<NovelGame>();

            string url = string.Format(SEARCH_URL, word);

            string res = await GetHtml(url);
            HtmlParser parser = new HtmlParser();
            var doc = await parser.ParseDocumentAsync(res);

            var list = doc.QuerySelectorAll("#result td")
                .Select(x => x.TextContent.Trim().Replace("OHP", ""));

            var newList = list.Select((v, i) => new { v, i })
                .GroupBy(x => x.i / 6)
                .Select(g => g.Select(x => x.v));

            foreach (var val in newList)
            {
                var element = val.ToList();
                result.Add(new NovelGame()
                {
                    Title = element[0],
                    Brand = element[1],
                    Release = DateTime.Parse(element[2])
                });
            }

            return result;
        }

        private async static Task<string> GetHtml(string uri)
        {
            using (HttpClient client = new HttpClient())
            {
                return await client.GetStringAsync(uri);
            }
        }
    }
}
