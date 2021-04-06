using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Http;
using SqlKata;
using SqlKata.Compilers;
using AngleSharp.Html.Parser;
using AngleSharp.Html.Dom;

namespace NovelGameLib.Utils
{
    static class NetworkUtil
    {
        public async static Task<IHtmlDocument> PostQuery(Query query, string uri)
        {
            var parameters = new Dictionary<string, string>()
            {
                { "sql", ToRawSQL(query) }
            };

            var content = new FormUrlEncodedContent(parameters);

            using (HttpClient client = new HttpClient())
            {
                var response = await client.PostAsync(uri, content);
                HtmlParser parser = new HtmlParser();
                return await parser.ParseDocumentAsync(await response.Content.ReadAsStringAsync());
            }
        }

        private static string ToRawSQL(Query query)
        {
            var compiler = new PostgresCompiler();
            SqlResult result = compiler.Compile(query);

            return result.ToString();
        }
    }
}
