using System.Collections.Generic;
using System.Threading.Tasks;
using NovelGameLib.Entity;

namespace NovelGameLib.Database
{
    public interface IDatabase
    {
        /// <summary>
        /// ブランドを全て取得する。
        /// </summary>
        /// <returns>ブランドのリスト。</returns>
        public Task<List<Brand>> GetAllBrands();

        /// <summary>
        /// ブランド名でブランドを検索する。
        /// </summary>
        /// <param name="name">ブランド名</param>
        /// <returns>ブランド。ブランドが存在しないとき <see langword="null"/> 。</returns>
        public Task<Brand?> SearchBrandByName(string name);

        /// <summary>
        /// ブランドIDでブランドを検索する。
        /// </summary>
        /// <param name="id">ブランドID</param>
        /// <returns>ブランド。ブランドが存在しないとき <see langword="null"/> 。</returns>
        public Task<Brand?> SearchBrandById(int id);

        /// <summary>
        /// ブランド名が部分一致する全てのブランドを検索する。
        /// </summary>
        /// <param name="name">ブランド名</param>
        /// <returns>ブランドのリスト。</returns>
        public Task<List<Brand>> SearchBrands(string name);

        /// <summary>
        /// ノベルゲームを全て取得する。
        /// </summary>
        /// <returns>ノベルゲームのリスト。</returns>
        public Task<List<NovelGame>> GetAllGames();

        /// <summary>
        /// ゲームタイトルでノベルゲームを検索する。
        /// </summary>
        /// <param name="name">ゲームタイトル</param>
        /// <returns>ノベルゲーム。ゲームが存在しないとき <see langword="null"/> 。</returns>
        public Task<NovelGame?> SearchGameByName(string name);

        /// <summary>
        /// ゲームIDでノベルゲームを検索する。
        /// </summary>
        /// <param name="id">ゲームID</param>
        /// <returns>ノベルゲーム。ゲームが存在しないとき <see langword="null"/> 。</returns>
        public Task<NovelGame?> SearchGameById(int id);

        /// <summary>
        /// ゲームタイトルが部分一致する全てのノベルゲームを検索する。
        /// </summary>
        /// <param name="name">ゲームタイトル</param>
        /// <returns>ノベルゲームのリスト。</returns>
        public Task<List<NovelGame>> SearchGames(string name);
    }
}
