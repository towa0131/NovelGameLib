using System.Threading.Tasks;

namespace NovelGameLib.Database
{
    public interface IExportableDatabase
    {  
        /// <summary>
        /// データをSQLite3データベースにエクスポートする。
        /// </summary>
        public Task<bool> ExportToSQLite3(string path);
    }
}
