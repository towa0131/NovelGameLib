namespace NovelGameLib
{
    public class Brand
    {
        public int? Id { get; set; }

        public string? Name { get; set; }

        public string? Kana { get; set; }

        public string? Maker { get; set; }

        public string? MakerKana { get; set; }

        public string? Url { get; set; }

        public MakerType? Kind { get; set; }

        public bool? Lost { get; set; }

        public bool? DirectLink { get; set; }

        public int? Median { get; set; }

        public string? Twitter { get; set; }
    }

    public enum MakerType
    {
        CORPORATION, CIRCLE
    }
}
