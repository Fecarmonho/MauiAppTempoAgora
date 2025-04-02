namespace MauiAppTempoAgora.Models
{
    public class Tempo
    {
        public double? Lon { get; set; }
        public double? Lat { get; set; }
        public double? TempMin { get; set; }
        public double? TempMax { get; set; }
        public int? Visibility { get; set; }
        public double? Speed { get; set; }
        public string? Main { get; set; }
        public string? Description { get; set; }
        public string? Sunrise { get; set; }
        public string? Sunset { get; set; }
    }
}
