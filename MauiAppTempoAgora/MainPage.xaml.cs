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
                lbl_erro.IsVisible = false;
                lbl_res.Text = "Carregando...";

                if (!string.IsNullOrWhiteSpace(txt_cidade.Text))
                {
                    Tempo? t = await DataService.GetPrevisao(txt_cidade.Text);

                    if (t != null)
                    {
                        lbl_res.Text = $"🌍 Latitude: {t.Lat}\n" +
                                       $"🌍 Longitude: {t.Lon}\n" +
                                       $"🌅 Nascer do Sol: {t.Sunrise}\n" +
                                       $"🌇 Pôr do Sol: {t.Sunset}\n" +
                                       $"🌡️ Temp Máx: {t.TempMax}°C\n" +
                                       $"🌡️ Temp Min: {t.TempMin}°C\n" +
                                       $"☁️ Descrição do Clima: {t.Description}\n" +
                                       $"💨 Velocidade do Vento: {t.Speed} m/s\n" +
                                       $"👀 Visibilidade: {t.Visibility} metros";
                    }
                    else
                    {
                        lbl_res.Text = "Sem dados de previsão.";
                    }
                }
                else
                {
                    lbl_erro.Text = "Preencha a cidade.";
                    lbl_erro.IsVisible = true;
                    lbl_res.Text = "";
                }
            }
            catch (Exception ex)
            {
                lbl_erro.Text = ex.Message;
                lbl_erro.IsVisible = true;
                lbl_res.Text = "";
            }
        }
    }
}
