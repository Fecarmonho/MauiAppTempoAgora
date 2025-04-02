using MauiAppTempoAgora.Models;
using Newtonsoft.Json.Linq;
using System.Globalization;

namespace MauiAppTempoAgora.Services
{
    public class DataService
    {
        public static async Task<Tempo?> GetPrevisao(string cidade)
        {
            try
            {
                string chave = "86be87189644e8464d3d019a2918a733";
                string url = $"https://api.openweathermap.org/data/2.5/weather?q={cidade}&units=metric&appid={chave}";

                using HttpClient client = new HttpClient();
                HttpResponseMessage resp = await client.GetAsync(url);

                if (resp.IsSuccessStatusCode)
                {
                    string json = await resp.Content.ReadAsStringAsync();
                    var rascunho = JObject.Parse(json);

                    if (rascunho == null ||
                        rascunho["sys"] == null ||
                        rascunho["weather"] == null ||
                        rascunho["main"] == null ||
                        rascunho["wind"] == null)
                    {
                        return null;
                    }

                    DateTime time = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
                    DateTime sunrise = time.AddSeconds((double)rascunho["sys"]!["sunrise"]!).ToLocalTime();
                    DateTime sunset = time.AddSeconds((double)rascunho["sys"]!["sunset"]!).ToLocalTime();

                    return new Tempo
                    {
                        Lat = (double?)rascunho["coord"]?["lat"],
                        Lon = (double?)rascunho["coord"]?["lon"],
                        Description = (string?)rascunho["weather"]?[0]?["description"],
                        Main = (string?)rascunho["weather"]?[0]?["main"],
                        TempMin = (double?)rascunho["main"]?["temp_min"],
                        TempMax = (double?)rascunho["main"]?["temp_max"],
                        Speed = (double?)rascunho["wind"]?["speed"],
                        Visibility = (int?)rascunho["visibility"],
                        Sunrise = sunrise.ToString("HH:mm"),
                        Sunset = sunset.ToString("HH:mm"),
                    };
                }
                return null;
            }
            catch
            {
                return null;
            }
        }
    }
}