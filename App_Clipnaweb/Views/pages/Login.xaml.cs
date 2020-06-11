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
    public partial class Login : ContentPage
    {
        [Obsolete]
        public Login()
        {
            InitializeComponent();
            this.Padding = Device.OnPlatform(
                new Thickness(5, 10, 5, 5),
                new Thickness(5),
                new Thickness(5)
                );

            if (Application.Current.Properties.ContainsKey("login") && Application.Current.Properties.ContainsKey("senha"))
            {
                txtLogin.Text = Application.Current.Properties["login"].ToString();
                txtSenha.Text = Application.Current.Properties["senha"].ToString();
                lembrarmeSwitch.IsToggled = true;
                lembrarmeSwitch.OnColor = Color.Gray;
                lembrarmeSwitch.ThumbColor = Color.Green;
            }
            else
            {
                lembrarmeSwitch.IsToggled = false;
                lembrarmeSwitch.ThumbColor = Color.Gray;
            }

            loginButton.Clicked += Login_ButtonClicked;
        }

        [Obsolete]
        private async void Login_ButtonClicked(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtLogin.Text))
            {
                await DisplayAlert("Erro", "Digite um login válido!", "Aceitar");
                txtLogin.Focus();
                return;
            }

            if (String.IsNullOrEmpty(txtSenha.Text))
            {
                await DisplayAlert("Erro", "Digite uma senha válida!", "Aceitar");
                txtSenha.Focus();
                return;
            }

            this.LogarAsync();
        }

        [Obsolete]
        private async void LogarAsync()
        {

            try
            {
                using (var client = new HttpClient())
                {
                    waitActivityIndicator.IsRunning = true;

                    var loginRequest = new DtoUser
                    {
                        login = txtLogin.Text,
                        senha = txtSenha.Text,
                    };

                    var jsonRequest = JsonConvert.SerializeObject(loginRequest);
                    var httpContent = new StringContent(jsonRequest, Encoding.UTF8, "application/json");

                    string uri = "http://www.clipnaweb.com.br/WS/Admin/Login/GetLogin";

                    var retorno = await client.PostAsync(uri, httpContent);

                    var resultString = await retorno.Content.ReadAsStringAsync();

                    if (retorno.StatusCode == System.Net.HttpStatusCode.BadRequest)
                    {
                        await DisplayAlert("Erro", "Usuário ou Senha Incorretos", "Ok");
                        waitActivityIndicator.IsRunning = false;
                        return;
                    }

                    var user = JsonConvert.DeserializeObject<DtoResultLogin>(resultString);

                    await DisplayAlert("Olá " + user.NM_Login.ToUpper() + "!", "Seja bem-vindo ao Clipping " + user.Cliente.ToUpper() + "!", "Acessar");

                    //foreach (var x in posts)
                    //{
                    //    if (x.nM_Login != "") { };
                    //}

                    //var user = jsonconvert.deserializeobject<user>(resp);
                    //user.senha = txtsenha.text;
                    //this.VerificarRemember(user);

                    var DadosUsuarioLogado = new DtoUser();
                    DadosUsuarioLogado.NomeBanco = user.NomeBanco;
                    DadosUsuarioLogado.login = user.NM_Login;
                    DadosUsuarioLogado.senha = txtSenha.Text;
                    DadosUsuarioLogado.NomeCliente = user.Cliente;

                    if (Application.Current.Properties.ContainsKey("login") && Application.Current.Properties.ContainsKey("senha") && lembrarmeSwitch.IsToggled) {
                        Application.Current.Properties["login"] = txtLogin.Text;
                        Application.Current.Properties["senha"] = txtSenha.Text;
                    }
                    else if(lembrarmeSwitch.IsToggled) {
                        Application.Current.Properties.Add("login", txtLogin.Text);
                        Application.Current.Properties.Add("senha", txtSenha.Text);
                    }

                    await Application.Current.SavePropertiesAsync();

                    waitActivityIndicator.IsRunning = false;
                    Application.Current.MainPage = new NavigationPage(new FiltroTodas(DadosUsuarioLogado)) { BarBackgroundColor = Color.Black, BarTextColor = Color.White };

                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Erro", ex.Message, "Erro ao logar...");
                waitActivityIndicator.IsRunning = false;
                return;
            }


        }

        private void OnToggled(object sender, ToggledEventArgs e)
        {
            if(e.Value == false)
            {
                Application.Current.Properties.Remove("login");
                Application.Current.Properties.Remove("senha");
            }
            else
            {
                lembrarmeSwitch.OnColor = Color.Gray;
                lembrarmeSwitch.ThumbColor = Color.Green;
            }
        }

    }
}