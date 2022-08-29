using PRY_LENG_PROG.SugerenciasComentarios.ModelosComentario;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PRY_LENG_PROG.SugerenciasComentarios
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddComment : ContentPage
    {
        string url = (string)Application.Current.Properties["direccionDb"];
        int user_id = (int)Application.Current.Properties["idUsuario"];
        string name_user = (string)Application.Current.Properties["nameUser"];

        public AddComment()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            MessagingCenter.Send<Object>(this, "HideOsNavigationBar");

        }

        private async void RegisterComments()
        {
            DatosRegistroComentario comment = new DatosRegistroComentario();
            comment.id_user = user_id;
            comment.name_user = name_user;
            comment.rating = (int)rating.Value;
            comment.title = tituloComentario.Text;
            comment.comment = bodyComentario.Text;

            var userClient = new RestClient(url);
            string rutaFeed = "/api/comments";
            var requestFeed = new RestRequest(rutaFeed, Method.POST) { RequestFormat = DataFormat.Json }.AddJsonBody(comment);
            try
            {
                var response = userClient.Execute(requestFeed);

                if (!(response.StatusCode == System.Net.HttpStatusCode.OK))
                {
                    await DisplayAlert("msg", response.Content, "ok");
                }
                else
                {
                    await DisplayAlert("Alerta", response.Content, "ok");
                    await Navigation.PopAsync();
                }

            }
            catch (Exception err)
            {
                await DisplayAlert("error", err.Message, "aceptar");
            }
        }

        private void enviar_Clicked(object sender, EventArgs e)
        {
            RegisterComments();
        }

        private void regresar_Clicked(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }
    }
}