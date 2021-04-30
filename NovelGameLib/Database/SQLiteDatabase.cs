using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using SqlKata;
using SqlKata.Execution;
using SqlKata.Compilers;
using NovelGameLib.Entity;
using NovelGameLib.Utils;

namespace NovelGameLib.Database
{
    public class SQLiteDatabase : IDatabase
    {
        QueryFactory db;

        public SQLiteDatabase(string path)
        {
            var connectionstring = new SQLiteConnectionStringBuilder
            {
                DataSource = path
            };

            SQLiteConnection connection = new SQLiteConnection(connectionstring.ToString());
            SqliteCompiler compiler = new SqliteCompiler();

            connection.Open();

            this.db = new QueryFactory(connection, compiler);
        }

        public async Task<List<Brand>> GetAllBrands()
        {
            List<Brand> brands = this.db.Query("brands").Get<Brand>().ToList();

            return brands;
        }

        public async Task<Brand?> SearchBrandByName(string name)
        {
            List<Brand> brands = this.db.Query("brands")
                                        .Where("Name", name)
                                        .OrWhere("Kana", name)
                                        .Get<Brand>()
                                        .ToList();

            if (brands.Count() == 0) return null;
            else return brands.First();
        }

        public async Task<Brand?> SearchBrandById(int id)
        {
            List<Brand> brands = this.db.Query("brands")
                                        .Where("Id", id)
                                        .Get<Brand>()
                                        .ToList();

            if (brands.Count() == 0) return null;
            else return brands.First();
        }

        public async Task<List<Brand>> SearchBrands(string name)
        {
            List<Brand> brands = this.db.Query("brands")
                                        .WhereLike("Name", $"%{name}%")
                                        .OrWhereLike("Kana", $"%{name}%")
                                        .Get<Brand>()
                                        .ToList();

            return brands;
        }

        public async Task<List<NovelGame>> GetAllGames()
        {
            List<NovelGame> games = this.db.Query("games").Get<NovelGame>().ToList();

            return games;
        }

        public async Task<NovelGame?> SearchGameByName(string name)
        {
            List<NovelGame> games = this.db.Query("games")
                                            .Where("Title", name)
                                            .OrWhere("Kana", name)
                                            .Get<NovelGame>()
                                            .ToList();

            if (games.Count() == 0) return null;
            else return games.First();
        }

        public async Task<NovelGame?> SearchGameById(int id)
        {
            List<NovelGame> games = this.db.Query("games")
                                            .Where("Id", id)
                                            .Get<NovelGame>()
                                            .ToList();

            if (games.Count() == 0) return null;
            else return games.First();
        }

        public async Task<List<NovelGame>> SearchGames(string name)
        {
            List<NovelGame> games = this.db.Query("games")
                                        .WhereLike("Title", $"%{name}%")
                                        .OrWhereLike("Kana", $"%{name}%")
                                        .Get<NovelGame>()
                                        .ToList();

            return games;
        }

    }
}
