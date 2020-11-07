using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NovelGameLib;

namespace NovelGameLib.CLITool
{
    class Program
    {
        public const double VERSION = 1.0;

        static async Task Main(string[] args)
        {
            string line;

            Console.WriteLine($" || NovelGameLib CLI v{VERSION} ||");
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

                List<NovelGame> list = await NovelAPI.SearchByName(line);

                foreach (NovelGame game in list)
                {
                    Console.WriteLine("");
                    Console.WriteLine($"* {game.Title}");
                    Console.WriteLine($" - ブランド : {game.Brand}");
                    Console.WriteLine($" - 発売日 : {game.Release.ToString("yyyy/MM/dd")}");
                }
                Console.WriteLine("");
            }
        }
    }
}
