using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NovelGameLib.Entity;

namespace NovelGameLib.CLITool
{
    class Program
    {
        public const double VERSION = 1.0;

        static async Task Main(string[] args)
        {
            string line;

            Console.WriteLine($" || NovelGameLib CLI v{VERSION:f1} ||");
            Console.WriteLine("タイトルを入力し、ゲームを検索");
            Console.WriteLine("exit でアプリケーションを終了");

            while (true)
            {
                Console.Write(">> ");
                line = Console.ReadLine();

                if ((line == null) || (line == "exit"))
                {
                    Console.WriteLine("終了しています...");
                    break;
                }

                List<NovelGame> games = await NovelGameAPI.SearchGames(line);

                foreach (NovelGame game in games)
                {
                    Console.WriteLine($" - {game.Title} ({game.Kana})");
                    Console.WriteLine($"  ID : {game.Id}");
                    Console.WriteLine($"  Release : {game.SellDay?.ToString("yyyy/MM/dd")}");
                    if (game.BrandId != null)
                    {
                        Brand brand = await NovelGameAPI.SearchBrandById(game.BrandId.Value);
                        if (brand != null)
                        {
                            Console.WriteLine($"  Brand : {brand.Name} ({brand.Kana})");
                            Console.WriteLine($"   ID : {brand.Id}");
                            Console.WriteLine($"   URL : {brand.Url}");
                            Console.WriteLine($"   Twitter : {brand.Twitter}");
                        }
                    }
                    Console.WriteLine($"  HP : {game.OHP}");
                    Console.WriteLine($"  Brand ID : {game.BrandId}");
                    Console.WriteLine($"  Getchu : {game.Getchu}");
                    Console.WriteLine($"  Model : {game.Model}");
                    Console.WriteLine($"  Rating : {game.Rating}");
                    Console.WriteLine($"  Gyutto : {game.Gyutto}");
                    Console.WriteLine($"  Fanza : {game.Fanza}");
                    Console.WriteLine("");
                }

            }
        }
    }
}
