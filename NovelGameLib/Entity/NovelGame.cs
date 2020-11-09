using System;

namespace NovelGameLib.Entity
{
    public class NovelGame
    {
        public int? Id { get; set; }

        public string? Title { get; set; }

        public string? Kana { get; set; }

        public DateTime? SellDay { get; set; }

        public int? BrandId { get; set; }

        public int? Median { get; set; }

        public int? Stdev { get; set; }

        public int? Getchu { get; set; } 

        public string? OHP { get; set; }

        public string? Model { get; set; }

        public bool? Rating { get; set; }

        public int? Gyutto { get; set; }

        public string? Fanza { get; set; }
    }
}
