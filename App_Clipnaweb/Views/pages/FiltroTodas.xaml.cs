using App_Clipnaweb.Models.Dto;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App_Clipnaweb.Views.pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FiltroTodas : ContentPage
    {
        private static String _nomeBanco = string.Empty;
        private static String _nomeCliente = string.Empty;
        private static DateTime _dataIni;
        private static DateTime _dataFim;

        [Obsolete]
        public FiltroTodas(DtoUser DadosUsuarioLogado)
        {
            InitializeComponent();

            _nomeBanco = DadosUsuarioLogado.NomeBanco;
            _nomeCliente = DadosUsuarioLogado.NomeCliente;
            _dataIni = Convert.ToDateTime(DateTime.Today.ToShortDateString());
            _dataFim = Convert.ToDateTime(DateTime.Today.ToShortDateString());

            this.Padding = Device.OnPlatform(
                new Thickness(5, 10, 5, 5),
                new Thickness(5),
                new Thickness(5)
                );

            this.MontaAssuntosAsync(DadosUsuarioLogado.NomeBanco);

            this.MontaMidiasPermitidasAsync(DadosUsuarioLogado);

            imgLogoCliente.Source = "http://www.clipnaweb.com.br/clipping/CNW/imagens/"+ _nomeCliente + "/logo.png";

            BtnRefine.Clicked += BtnRefine_ButtonClicked;

        }

        private async void MontaMidiasPermitidasAsync(DtoUser DadosUsuarioLogado)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    waitActivityIndicator.IsVisible = true;
                    waitActivityIndicator.IsRunning = true;

                    var loginRequest = new DtoUser
                    {
                        login = DadosUsuarioLogado.login,
                        senha = DadosUsuarioLogado.senha,
                    };

                    var jsonRequest = JsonConvert.SerializeObject(loginRequest);
                    var httpContent = new StringContent(jsonRequest, Encoding.UTF8, "application/json");

                    string uri = "http://www.clipnaweb.com.br/WS/Admin/Login/GetMidias";

                    HttpResponseMessage retorno = await client.PostAsync(uri, httpContent);

                    var resultString = await retorno.Content.ReadAsStringAsync();

                    if (retorno.StatusCode == System.Net.HttpStatusCode.BadRequest)
                    {
                        await DisplayAlert("Erro", "Ocorreu um erro ao retonar as mídias permitidas", "Ok");
                        waitActivityIndicator.IsRunning = false;
                        return;
                    }

                    var midias = JsonConvert.DeserializeObject<List<DtoResultLogin>>(resultString);

                    foreach (DtoResultLogin x in midias)
                    {
                        if (x.NM_Funcionalidade == "Impresso")
                            chkJr.IsVisible = true;
                        else if (x.NM_Funcionalidade == "TV")
                            chkTv.IsVisible = true;
                        else if (x.NM_Funcionalidade == "RD")
                            chkRd.IsVisible = true;
                        else if (x.NM_Funcionalidade == "Online")
                            chkOnline.IsVisible = true;
                        else if (x.NM_Funcionalidade == "Inter")
                            chkInter.IsVisible = true;
                        else if (x.NM_Funcionalidade == "MSocial")
                            chkMSocial.IsVisible = true;
                    }

                    waitActivityIndicator.IsVisible = false;
                    waitActivityIndicator.IsRunning = false;

                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Erro", ex.Message, "Erro ao listar mídias...");
                waitActivityIndicator.IsVisible = false;
                waitActivityIndicator.IsRunning = false;
                return;
            }
        }

        private async void MontaAssuntosAsync(String NomeBanco)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    waitActivityIndicator.IsVisible = true;
                    waitActivityIndicator.IsRunning = true;

                    var dadosRequest = NomeBanco;

                    var jsonRequest = JsonConvert.SerializeObject(dadosRequest);
                    var httpContent = new StringContent(jsonRequest, Encoding.UTF8, "application/json");

                    string uri = "http://www.clipnaweb.com.br/WS/Admin/Login/GetAssuntos";

                    HttpResponseMessage retorno = await client.PostAsync(uri, httpContent);

                    var resultString = await retorno.Content.ReadAsStringAsync();

                    if (retorno.StatusCode == System.Net.HttpStatusCode.BadRequest)
                    {
                        await DisplayAlert("Erro", "Ocorreu um erro ao retonar as mídias permitidas", "Ok");
                        waitActivityIndicator.IsVisible = false;
                        waitActivityIndicator.IsRunning = false;
                        return;
                    }

                    var assuntos = JsonConvert.DeserializeObject<List<DtoAssunto>>(resultString);

                    foreach (DtoAssunto x in assuntos)
                    {
                        pckAssunto.Items.Add(x.BTN_Label);
                    }

                    waitActivityIndicator.IsVisible = false;
                    waitActivityIndicator.IsRunning = false;

                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Erro", ex.Message, "Erro ao listar mídias...");
                waitActivityIndicator.IsVisible = false;
                waitActivityIndicator.IsRunning = false;
                return;
            }
        }

        private void OnDateSelectedDataIni(object sender, DateChangedEventArgs e)
        {
            _dataIni = new DateTime(e.NewDate.Year, e.NewDate.Month, e.NewDate.Day);
        }

        private void OnDateSelectedDataFim(object sender, DateChangedEventArgs e)
        {
            _dataFim = new DateTime(e.NewDate.Year, e.NewDate.Month, e.NewDate.Day);
        }

        private async void BtnRefine_ButtonClicked(object sender, EventArgs e)
        {
            waitActivityIndicator.IsVisible = true;
            waitActivityIndicator.IsRunning = true;

            if (_dataFim < _dataIni)
            {
                await DisplayAlert("Ops!", "A data de fim não pode ser maior que a data início da pesquisa!", "Entendi!");
                DataIni.Focus();
                waitActivityIndicator.IsVisible = false;
                waitActivityIndicator.IsRunning = false;
                return;
            }

            DtoFiltro filtro = new DtoFiltro()
            {
                Assunto = pckAssunto.SelectedIndex != -1 ? pckAssunto.Items[pckAssunto.SelectedIndex].ToString() : "Clipping do Dia",
                ChkImpresso = chkJr.Checked ? true : false,
                ChkTv = chkTv.Checked ? true : false,
                ChkRd = chkRd.Checked ? true : false,
                ChkOnline = chkOnline.Checked ? true : false,
                ChkInter = chkInter.Checked ? true : false,
                ChkMSocial = chkMSocial.Checked ? true : false,
                Palavra = txtPalavra.Text,
                NomeBanco = _nomeBanco,
                Cliente = _nomeCliente,
                DataIni = _dataIni,
                DataFim = _dataFim
            };

            waitActivityIndicator.IsVisible = false;
            waitActivityIndicator.IsRunning = false;
            await Navigation.PushAsync(new ListMaterias(filtro));
        }
    }
}