using Newtonsoft.Json;
using PRY_LENG_PROG.Modelos;
using RestSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PRY_LENG_PROG
{
    public partial class MainPage : ContentPage
    {
        List<UserModel> user = new List<UserModel>();
        public MainPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            MessagingCenter.Send<Object>(this, "HideOsNavigationBar");
        }

        protected override void OnAppearing()
        {
            correo.Text = "";
            password.Text = "";
            base.OnAppearing();
        }

        private void ValidarUsuario()
        {
            var correoStr = correo.Text;
            var Userclient = new RestClient("http://127.0.0.1:8000");
            string ruta = "/api/user/" + correoStr;
            var request = new RestRequest(ruta, Method.GET);
            var queryResult = Userclient.Execute(request);

            if (queryResult.StatusCode == System.Net.HttpStatusCode.OK)
            {
                string strJason = queryResult.Content;
                user = JsonConvert.DeserializeObject<List<UserModel>>(strJason);
                if (user is null || user.Count == 0)
                {
                    DisplayAlert("Alerta", "Usuario no se encuentra registrado", "ok");
                }
                else
                {
                    if (user.FirstOrDefault().email == correo.Text)
                    {
                        UserModel usuario = user.FirstOrDefault();
                        Navigation.PushAsync(new Menu(usuario));
                    }
                    else
                    {
                        DisplayAlert("Alerta", "Credenciales incorrectas", "ok");
                    }
                }
            }
            else
            {
                DisplayAlert("Alerta", "Credenciales incorrectas", "ok");
            }
        }

        private void BtnEntrar_Clicked(object sender, EventArgs e)
        {

            if ((correo.Text != null || correo.Text != "") && (password.Text != null || password.Text != ""))
            {
                ValidarUsuario();
            }
            else
            {
                DisplayAlert("Alerta", "Credenciales incorrectas", "ok");
            }
        }
        private async void btnExit_Clicked(object sender, EventArgs e)
        {
            bool respuesta = await DisplayAlert("Alerta", "¿Esta seguro que desea salir?", "No", "Si");
            if (!respuesta)
            {
                System.Diagnostics.Process.GetCurrentProcess().Kill();// Matar aplicación 
            }
        }
    }
}
