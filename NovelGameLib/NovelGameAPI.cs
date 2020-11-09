using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SqlKata;
using AngleSharp.Html.Dom;
using AngleSharp.Dom;
using NovelGameLib.Entity;

namespace NovelGameLib
{
    public class NovelGameAPI
    {
        public const string POST_URL = "https://erogamescape.dyndns.org/~ap2/ero/toukei_kaiseki/sql_for_erogamer_form.php";

        public async static Task<List<Brand>> GetAllBrands()
        {
            var document = await NetworkUtil.PostQuery(new Query().From("brandlist"), POST_URL);

            List<Brand> brands = ReadBrandTable(document);

            return brands;
        }

        public async static Task<Brand?> SearchBrandByName(string name)
        {
            Query query = new Query()
                        .From("brandlist")
                        .Where("brandname", name)
                        .OrWhere("brandfurigana", name);
            var document = await NetworkUtil.PostQuery(query, POST_URL);

            List<Brand> brands = ReadBrandTable(document);

            if (brands.Count() == 0) return null;
            else return brands.First();
        }

        public async static Task<Brand?> SearchBrandById(int id)
        {
            Query query = new Query()
                        .From("brandlist")
                        .Where("id", id);
            var document = await NetworkUtil.PostQuery(query, POST_URL);

            List<Brand> brands = ReadBrandTable(document);

            if (brands.Count() == 0) return null;
            else return brands.First();
        }

        public async static Task<List<Brand>> SearchBrands(string name)
        {
            Query query = new Query()
                        .From("brandlist")
                        .WhereLike("brandname", $"%{name}%")
                        .OrWhereLike("brandfurigana", $"%{name}%");
            var document = await NetworkUtil.PostQuery(query, POST_URL);

            List<Brand> brands = ReadBrandTable(document);

            return brands;
        }

        public async static Task<List<NovelGame>> GetAllGames()
        {
            var document = await NetworkUtil.PostQuery(new Query().From("gamelist"), POST_URL);

            List<NovelGame> games = ReadGameTable(document);

            return games;
        }

        public async static Task<NovelGame?> SearchGameByName(string name)
        {
            Query query = new Query()
                        .From("gamelist")
                        .Where("gamename", name)
                        .OrWhere("furigana", name);
            var document = await NetworkUtil.PostQuery(query, POST_URL);

            List<NovelGame> games = ReadGameTable(document);

            if (games.Count() == 0) return null;
            else return games.First();
        }

        public async static Task<NovelGame?> SearchGameById(int id)
        {
            Query query = new Query()
                        .From("gamelist")
                        .Where("id", id);
            var document = await NetworkUtil.PostQuery(query, POST_URL);

            List<NovelGame> games = ReadGameTable(document);

            if (games.Count() == 0) return null;
            else return games.First();
        }

        public async static Task<List<NovelGame>> SearchGames(string name)
        {
            Query query = new Query()
                        .From("gamelist")
                        .WhereLike("gamename", $"%{name}%")
                        .OrWhereLike("furigana", $"%{name}%");
            var document = await NetworkUtil.PostQuery(query, POST_URL);

            List<NovelGame> games = ReadGameTable(document);

            return games;
        }

        private static List<Brand> ReadBrandTable(IHtmlDocument document)
        {
            var brands = document.QuerySelectorAll("tr")
                .Skip(1)
                .Select(element =>
                {
                    var td = element.GetElementsByTagName("td");

                    Brand brand = new Brand()
                    {
                        Id = ParseInt(td[0]),
                        Name = td[1].TextContent,
                        Kana = td[2].TextContent,
                        Maker = td[3].TextContent,
                        MakerKana = td[4].TextContent,
                        Url = td[5].TextContent,
                        Kind = td[7].TextContent == "CORPORATION" ? MakerType.CORPORATION : MakerType.CIRCLE,
                        Lost = ParseBool(td[8]),
                        DirectLink = ParseBool(td[9]),
                        Median = ParseInt(td[10]),
                        Twitter = td[12].TextContent
                    };

                    return brand;
                });

            return brands.ToList();
        }

        private static List<NovelGame> ReadGameTable(IHtmlDocument document)
        {
            var games = document.QuerySelectorAll("tr")
                .Skip(1)
                .Select(element =>
                {
                    var td = element.GetElementsByTagName("td");

                    NovelGame game = new NovelGame()
                    {
                        Id = ParseInt(td[0]),
                        Title = td[1].TextContent,
                        Kana = td[2].TextContent,
                        SellDay = DateTime.Parse(td[3].TextContent),
                        BrandId = ParseInt(td[4]),
                        Median = ParseInt(td[5]),
                        Stdev = ParseInt(td[6]),
                        Getchu = ParseInt(td[14]),
                        OHP = td[15].TextContent,
                        Model = td[16].TextContent,
                        Rating = ParseBool(td[18]),
                        Gyutto = ParseInt(td[26]),
                        Fanza = td[27].TextContent
                    };

                    return game;
                });

            return games.ToList();
        }

        private static int? ParseInt(IElement element)
        {
            int result;

            if (!int.TryParse(element.TextContent, out result))
            {
                return null;
            }

            return result;
        }

        private static bool ParseBool(IElement flag)
        {
            if (flag.TextContent == "f") return false;
            else return true;
        }
    }
}
