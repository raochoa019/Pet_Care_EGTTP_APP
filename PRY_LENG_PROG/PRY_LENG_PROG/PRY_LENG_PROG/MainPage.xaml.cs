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

        private void ValidarUsuario()
        {
            var Userclient = new RestClient("http://127.0.0.1:8000");
            string ruta = "/api/user/"+correo.Text;
            var request = new RestRequest(ruta, Method.GET);
            var queryResult = Userclient.Execute(request);
            string strJason = queryResult.Content;
            user = JsonConvert.DeserializeObject<List<UserModel>>(strJason);
            if(user != null)
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
            else
            {
                DisplayAlert("Alerta", "Usuario no se encuentra registrado", "ok");
            }
        }

        private void BtnEntrar_Clicked(object sender, EventArgs e)
        {
            ValidarUsuario();
        }

        private void btnExit_Clicked(object sender, EventArgs e)
        {

        }
    }
}
