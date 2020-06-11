using App_Clipnaweb.Models.Dto;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App_Clipnaweb.Views.pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListMaterias : ContentPage
    {
        public ICommand TapCommand => new Command<string>(async (url) => await Launcher.OpenAsync(url));

        [Obsolete]
        public ListMaterias(DtoFiltro filtro)
        {
            InitializeComponent();

            BindingContext = this;

            this.Padding = Device.OnPlatform(
                new Thickness(5, 10, 5, 5),
                new Thickness(5),
                new Thickness(5)
                );

            imgLogoCliente.Source = "http://www.clipnaweb.com.br/clipping/CNW/imagens/" + filtro.Cliente + "/logo.png";

            lblClipping.Text = "Clipping de notícias";

            this.MontaListagemMaterias(filtro);
        }

        private async void MontaListagemMaterias(DtoFiltro filtro)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-us", false);

            waitActivityIndicator.IsVisible = true;
            waitActivityIndicator.IsRunning = true;

            List<DtoMateria> itens = new List<DtoMateria>();
            try
            {
                using (var client = new HttpClient())
                {
                    var dadosRequest = new DtoFiltro()
                    {
                        Assunto = filtro.Assunto,
                        ChkImpresso = filtro.ChkImpresso,
                        ChkTv = filtro.ChkTv,
                        ChkRd = filtro.ChkRd,
                        ChkOnline = filtro.ChkOnline,
                        ChkInter = filtro.ChkInter,
                        ChkMSocial = filtro.ChkMSocial,
                        Palavra = filtro.Palavra,
                        DataIni = new DateTime(Convert.ToDateTime(filtro.DataIni).Year, Convert.ToDateTime(filtro.DataIni).Month, Convert.ToDateTime(filtro.DataIni).Day),
                        DataFim = new DateTime(Convert.ToDateTime(filtro.DataFim).Year, Convert.ToDateTime(filtro.DataFim).Month, Convert.ToDateTime(filtro.DataFim).Day),
                        NomeBanco = filtro.NomeBanco,
                        Cliente = filtro.Cliente
                    };

                    var jsonRequest = JsonConvert.SerializeObject(dadosRequest);
                    var httpContent = new StringContent(jsonRequest, Encoding.UTF8, "application/json");

                    string uri = "http://www.clipnaweb.com.br/WS/Admin/Login/GetMaterias";

                    HttpResponseMessage retorno = await client.PostAsync(uri, httpContent);

                    var resultString = await retorno.Content.ReadAsStringAsync();

                    if (retorno.StatusCode == System.Net.HttpStatusCode.BadRequest)
                    {
                        await DisplayAlert("Erro", "Ocorreu um erro ao retonar as matérias", "Ok");
                        return;
                    }
                    if (resultString != "[]")
                    {
                        noNews.IsVisible = false;
                        lvMaterias.ItemsSource = JsonConvert.DeserializeObject<List<DtoMateria>>(resultString);
                        int countNews = JsonConvert.DeserializeObject<List<DtoMateria>>(resultString).Count();

                        waitActivityIndicator.IsVisible = false;
                        waitActivityIndicator.IsRunning = false;

                        if (filtro.DataIni != filtro.DataFim)
                        {
                            lblDataPesquisa.Text = "Veicula(s) " + countNews + " Notícia(s) entre: " + Convert.ToDateTime(filtro.DataIni).Day.ToString() + "/" + Convert.ToDateTime(filtro.DataIni).Month.ToString() + "/" + Convert.ToDateTime(filtro.DataIni).Year.ToString() + " à " + Convert.ToDateTime(filtro.DataFim).Day.ToString() + "/" + Convert.ToDateTime(filtro.DataFim).Month.ToString() + "/" + Convert.ToDateTime(filtro.DataFim).Year.ToString();
                        }
                        else
                        {
                            lblDataPesquisa.Text = "Veicula(s) " + countNews + " Notícia(s) no dia: " + Convert.ToDateTime(filtro.DataIni).Day.ToString() + "/" + Convert.ToDateTime(filtro.DataIni).Month.ToString() + "/" + Convert.ToDateTime(filtro.DataIni).Year.ToString();
                        }
                    }
                    else
                    {
                        waitActivityIndicator.IsVisible = false;
                        waitActivityIndicator.IsRunning = false;
                        noNews.IsVisible = true;
                    }

                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Erro", ex.Message, "Erro ao listar Matérias...");
                return;
            }
        }

        private async void OnTappedTitulo(object sender, EventArgs e)
        {
            Label lblClicked = (Label)sender;
            var item = (TapGestureRecognizer)lblClicked.GestureRecognizers[0];
            var url = item.CommandParameter;
            await Browser.OpenAsync(url.ToString(), BrowserLaunchMode.SystemPreferred);
        }
    }
}