using MauiAppTempoAgora.Models;
using MauiAppTempoAgora.Services;

namespace MauiAppTempoAgora
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txt_cidade.Text))
                {
                    Tempo? t = await DataService.GetPrevisao(txt_cidade.Text);

                    if (t != null)
                    {
                        string dados_previsao =
                            $"🌍 Coordinates: {t.Lat:N2}°N, {t.Lon:N2}°E\n" +
                            $"🌅 Sunrise: {t.Sunrise}\n" +
                            $"🌇 Sunset: {t.Sunset}\n" +
                            $"🌡️ Max Temp: {t.TempMax}°C\n" +
                            $"❄️ Min Temp: {t.TempMin}°C\n" +
                            $"🌬️ Wind Speed: {t.Speed} m/s\n" +
                            $"👀 Visibility: {t.Visibility} meters\n" +
                            $"☁️ Condition: {t.Main} ({t.Description})";

                        lbl_res.Text = dados_previsao;
                    }
                    else
                    {
                        lbl_res.Text = "No weather data found for this city.";
                    }
                }
                else
                {
                    lbl_res.Text = "Please enter a city name.";
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message, "OK");
            }
        }
    }
}