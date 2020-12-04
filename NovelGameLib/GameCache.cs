using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using NovelGameLib.Entity;

namespace NovelGameLib
{
    public class GameCache
    {
        private List<NovelGame> games = new List<NovelGame>();
        private List<Brand> brands = new List<Brand>();

        public bool isLoaded { get; set; } = false;

        public GameCache()
        {

        }

        public GameCache(List<NovelGame> games)
        {
            this.games = games;
        }

        public GameCache(List<Brand> brands)
        {
            this.brands = brands;
        }

        public GameCache(List<NovelGame> games, List<Brand> brands)
        {
            this.games = games;
            this.brands = brands;
        }

        public void AddGame(NovelGame game)
        {
            this.games.Add(game);
        }

        public void AddGames(List<NovelGame> games)
        {
            this.games.AddRange(games);
        }

        public void AddBrand(Brand brand)
        {
            this.brands.Add(brand);
        }

        public void AddBrands(List<Brand> brands)
        {
            this.brands.AddRange(brands);
        }


        public void RemoveGame(int id)
        {
            this.games.RemoveAll(x => x.Id == id);
        }

        public void RemoveBrand(int id)
        {
            this.brands.RemoveAll(x => x.Id == id);
        }

        public List<NovelGame> FindGames(int id)
        {
            return this.games.FindAll(x => x.Id == id);
        }

        public List<NovelGame> FindGamesByName(string name)
        {
            return this.FindGamesByName(name, false);
        }

        public List<NovelGame> FindGamesByName(string name, bool like)
        {
            return this.games.FindAll((x) => {
                if (x.Title == null || x.Kana == null) return false;
                else if (x.Title == name || x.Kana == name) return true;
                else if (like && (x.Title.Contains(name) || x.Kana.Contains(name))) return true;
                return false;
            });
        }

        public List<Brand> FindBrands(int id)
        {
            return this.brands.FindAll(x => x.Id == id);
        }

        public List<Brand> FindBrandsByName(string name)
        {
            return this.FindBrandsByName(name, false);
        }

        public List<Brand> FindBrandsByName(string name, bool like = false)
        {
            return this.brands.FindAll((x) => {
                if (x.Name == null || x.Kana == null) return false;
                else if (x.Name == name || x.Kana == name) return true;
                else if (like && (x.Name.Contains(name) || x.Kana.Contains(name))) return true;
                return false;
            });
        }

        public List<NovelGame> GetAllGames()
        {
            return this.games;
        }

        public List<Brand> GetAllBrands()
        {
            return this.brands;
        }

        public int CountGames()
        {
            return this.games.Count();
        }

        public int CountBrands()
        {
            return this.brands.Count();
        }

        public void Clear()
        {
            this.games.Clear();
            this.brands.Clear();
        }
    }
}
