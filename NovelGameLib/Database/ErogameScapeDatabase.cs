using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SQLite;
using SqlKata;
using SqlKata.Execution;
using SqlKata.Compilers;
using AngleSharp.Html.Dom;
using NovelGameLib.Entity;
using NovelGameLib.Utils;

namespace NovelGameLib.Database
{
    public class ErogameScapeDatabase : IDatabase, IExportableDatabase
    {
        private const string POST_URL = "https://erogamescape.dyndns.org/~ap2/ero/toukei_kaiseki/sql_for_erogamer_form.php";

        public async Task<List<Brand>> GetAllBrands()
        {
            var document = await NetworkUtil.PostQuery(new Query().From("brandlist"), POST_URL);
            List<Brand> brands = ReadBrandTable(document);

            return brands;
        }

        public async Task<Brand?> SearchBrandByName(string name)
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

        public async Task<Brand?> SearchBrandById(int id)
        {
            Query query = new Query()
                        .From("brandlist")
                        .Where("id", id);

            var document = await NetworkUtil.PostQuery(query, POST_URL);
            List<Brand> brands = ReadBrandTable(document);

            if (brands.Count() == 0) return null;
            else return brands.First();
        }

        public async Task<List<Brand>> SearchBrands(string name)
        {
            Query query = new Query()
                    .From("brandlist")
                    .WhereLike("brandname", $"%{name}%")
                    .OrWhereLike("brandfurigana", $"%{name}%");

            var document = await NetworkUtil.PostQuery(query, POST_URL);
            List<Brand> brands = ReadBrandTable(document);

            return brands;
        }

        public async Task<List<NovelGame>> GetAllGames()
        {
            var document = await NetworkUtil.PostQuery(new Query().From("gamelist"), POST_URL);
            List<NovelGame> games = ReadGameTable(document);

            return games;
        }

        public async Task<NovelGame?> SearchGameByName(string name)
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

        public async Task<NovelGame?> SearchGameById(int id)
        {
            Query query = new Query()
                        .From("gamelist")
                        .Where("id", id);

            var document = await NetworkUtil.PostQuery(query, POST_URL);
            List<NovelGame> games = ReadGameTable(document);

            if (games.Count() == 0) return null;
            else return games.First();
        }

        public async Task<List<NovelGame>> SearchGames(string name)
        {
            Query query = new Query()
                        .From("gamelist")
                        .WhereLike("gamename", $"%{name}%")
                        .OrWhereLike("furigana", $"%{name}%");

            var document = await NetworkUtil.PostQuery(query, POST_URL);
            List<NovelGame> games = ReadGameTable(document);

            return games;
        }

        public async Task<bool> ExportToSQLite3(string path)
        {
            List<Brand> brands = await this.GetAllBrands();
            List<NovelGame> games = await this.GetAllGames();

            var connectionstring = new SQLiteConnectionStringBuilder
            {
                DataSource = path
            };

            SQLiteConnection connection = new SQLiteConnection(connectionstring.ToString());
            SqliteCompiler compiler = new SqliteCompiler();

            connection.Open();

            QueryFactory db = new QueryFactory(connection, compiler);

            SQLiteCommand command = new SQLiteCommand(connection);

            command.CommandText = "CREATE TABLE IF NOT EXISTS games(" +
                "Id INTEGER, " +
                "Title TEXT, " +
                "Kana TEXT, " +
                "SellDay TIMESTAMP, " +
                "BrandId INTEGER, " +
                "Median INTEGER, " +
                "Stdev INTEGER, " +
                "Getchu INTEGER, " +
                "OHP TEXT, " +
                "Model TEXT, " +
                "Rating INTEGER, " +
                "Gyutto INTEGER, " +
                "Fanza TEXT)";
            command.ExecuteNonQuery();

            command.CommandText = "CREATE TABLE IF NOT EXISTS brands(" +
                "Id INTEGER, " +
                "Name TEXT, " +
                "Kana TEXT, " +
                "Maker TEXT, " +
                "MakerKana TEXT, " +
                "Url TEXT, " +
                "Kind INTEGER, " +
                "Lost INTEGER, " +
                "DirectLink INTEGER, " +
                "Median INTEGER, " +
                "Twitter TEXT)";
            command.ExecuteNonQuery();

            foreach (NovelGame game in games)
            {
                int affected = db.Query("games").Insert(game);
                if (!Convert.ToBoolean(affected)) return false;
            }

            foreach(Brand brand in brands)
            {
                int affected = db.Query("brands").Insert(brand);
                if (!Convert.ToBoolean(affected)) return false;
            }

            return true;
        }

        private List<Brand> ReadBrandTable(IHtmlDocument document)
        {
            var brands = document.QuerySelectorAll("tr")
                .Skip(1)
                .Select(element =>
                {
                    var td = element.GetElementsByTagName("td");

                    Brand brand = new Brand()
                    {
                        Id = ElementParser.ParseInt(td[0]),
                        Name = td[1].TextContent,
                        Kana = td[2].TextContent,
                        Maker = td[3].TextContent,
                        MakerKana = td[4].TextContent,
                        Url = td[5].TextContent,
                        Kind = td[7].TextContent == "CORPORATION" ? MakerType.CORPORATION : MakerType.CIRCLE,
                        Lost = ElementParser.ParseBool(td[8]),
                        DirectLink = ElementParser.ParseBool(td[9]),
                        Median = ElementParser.ParseInt(td[10]),
                        Twitter = td[12].TextContent
                    };

                    return brand;
                });

            return brands.ToList();
        }

        private List<NovelGame> ReadGameTable(IHtmlDocument document)
        {
            var games = document.QuerySelectorAll("tr")
                .Skip(1)
                .Select(element =>
                {
                    var td = element.GetElementsByTagName("td");

                    NovelGame game = new NovelGame()
                    {
                        Id = ElementParser.ParseInt(td[0]),
                        Title = td[1].TextContent,
                        Kana = td[2].TextContent,
                        SellDay = DateTime.Parse(td[3].TextContent),
                        BrandId = ElementParser.ParseInt(td[4]),
                        Median = ElementParser.ParseInt(td[5]),
                        Stdev = ElementParser.ParseInt(td[6]),
                        Getchu = ElementParser.ParseInt(td[14]),
                        OHP = td[15].TextContent,
                        Model = td[16].TextContent,
                        Rating = ElementParser.ParseBool(td[18]),
                        Gyutto = ElementParser.ParseInt(td[26]),
                        Fanza = td[27].TextContent
                    };

                    return game;
                });

            return games.ToList();
        }
    }
}
