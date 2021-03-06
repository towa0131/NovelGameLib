﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SqlKata;
using AngleSharp.Html.Dom;
using AngleSharp.Dom;
using NovelGameLib.Entity;
using NovelGameLib.Utils;
using NovelGameLib.Database;

namespace NovelGameLib
{
    public class NovelGameAPI
    {
        private IDatabase database;

        public NovelGameAPI(IDatabase database)
        {
            this.database = database;
        }

        /// <summary>
        /// ブランドを全て取得する。
        /// </summary>
        /// <returns>ブランドのリスト。</returns>
        public async Task<List<Brand>> GetAllBrands()
        {
            return await this.database.GetAllBrands();
        }

        /// <summary>
        /// ブランド名でブランドを検索する。
        /// </summary>
        /// <param name="name">ブランド名</param>
        /// <returns>ブランド。ブランドが存在しないとき <see langword="null"/> 。</returns>
        public async Task<Brand?> SearchBrandByName(string name)
        {
            return await this.database.SearchBrandByName(name);
        }

        /// <summary>
        /// ブランドIDでブランドを検索する。
        /// </summary>
        /// <param name="id">ブランドID</param>
        /// <returns>ブランド。ブランドが存在しないとき <see langword="null"/> 。</returns>
        public async Task<Brand?> SearchBrandById(int id)
        {
            return await this.database.SearchBrandById(id);
        }

        /// <summary>
        /// ブランド名が部分一致する全てのブランドを検索する。
        /// </summary>
        /// <param name="name">ブランド名</param>
        /// <returns>ブランドのリスト。</returns>
        public async Task<List<Brand>> SearchBrands(string name)
        {
            return await this.database.SearchBrands(name);
        }

        /// <summary>
        /// ノベルゲームを全て取得する。
        /// </summary>
        /// <returns>ノベルゲームのリスト。</returns>
        public async Task<List<NovelGame>> GetAllGames()
        {
            return await this.database.GetAllGames();
        }

        /// <summary>
        /// ゲームタイトルでノベルゲームを検索する。
        /// </summary>
        /// <param name="name">ゲームタイトル</param>
        /// <returns>ノベルゲーム。ゲームが存在しないとき <see langword="null"/> 。</returns>
        public async Task<NovelGame?> SearchGameByName(string name)
        {
            return await this.database.SearchGameByName(name);
        }

        /// <summary>
        /// ゲームIDでノベルゲームを検索する。
        /// </summary>
        /// <param name="id">ゲームID</param>
        /// <returns>ノベルゲーム。ゲームが存在しないとき <see langword="null"/> 。</returns>
        public async Task<NovelGame?> SearchGameById(int id)
        {
            return await this.database.SearchGameById(id);
        }

        /// <summary>
        /// ゲームタイトルが部分一致する全てのノベルゲームを検索する。
        /// </summary>
        /// <param name="name">ゲームタイトル</param>
        /// <returns>ノベルゲームのリスト。</returns>
        public async Task<List<NovelGame>> SearchGames(string name)
        {
            return await this.database.SearchGames(name);
        }
    }
}
