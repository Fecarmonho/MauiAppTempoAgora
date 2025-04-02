using MauiAppTempoAgora.Models;
using Newtonsoft.Json.Linq;
using System.Globalization;

namespace MauiAppTempoAgora.Services
{
    public class DataService
    {
        private static readonly string apiKey = "86be87189644e8464d3d019a2918a733";

        public static async Task<Tempo?> GetPrevisao(string cidade)
        {
            if (string.IsNullOrWhiteSpace(cidade))
                return null;

            string url = $"https://api.openweathermap.org/data/2.5/weather?q={cidade}&units=metric&appid={apiKey}&lang=pt";

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage resp = await client.GetAsync(url);

                    if (!resp.IsSuccessStatusCode)
                    {
                        if (resp.StatusCode == System.Net.HttpStatusCode.NotFound)
                        {
                            throw new Exception("Cidade não encontrada. Verifique o nome digitado.");
                        }
                        else
                        {
                            throw new Exception($"Erro ao buscar dados: {resp.ReasonPhrase}");
                        }
                    }

                    string json = await resp.Content.ReadAsStringAsync();
                    var rascunho = JObject.Parse(json);

                    return new Tempo
                    {
                        Lat = rascunho["coord"]?["lat"]?.Value<double>(),
                        Lon = rascunho["coord"]?["lon"]?.Value<double>(),
                        Description = rascunho["weather"]?[0]?["description"]?.ToString(),
                        Main = rascunho["weather"]?[0]?["main"]?.ToString(),
                        TempMin = rascunho["main"]?["temp_min"]?.Value<double>(),
                        TempMax = rascunho["main"]?["temp_max"]?.Value<double>(),
                        Speed = rascunho["wind"]?["speed"]?.Value<double>(),
                        Visibility = rascunho["visibility"]?.Value<int>(),
                        Sunrise = rascunho["sys"]?["sunrise"]?.Value<long>() != null
                            ? DateTimeOffset.FromUnixTimeSeconds(rascunho["sys"]["sunrise"].Value<long>()).ToLocalTime().ToString()
                            : null,
                        Sunset = rascunho["sys"]?["sunset"]?.Value<long>() != null
                            ? DateTimeOffset.FromUnixTimeSeconds(rascunho["sys"]["sunset"].Value<long>()).ToLocalTime().ToString()
                            : null
                    };
                }
                catch (HttpRequestException)
                {
                    throw new Exception("Sem conexão com a internet. Verifique sua rede.");
                }
                catch (Exception ex)
                {
                    throw new Exception($"Erro inesperado: {ex.Message}");
                }
            }
        }
    }
}
