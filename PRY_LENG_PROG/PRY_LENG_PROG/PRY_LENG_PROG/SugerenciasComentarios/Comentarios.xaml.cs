using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;
using Newtonsoft.Json;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using PRY_LENG_PROG.SugerenciasComentarios.ModelosComentario;
using System.Threading;

namespace PRY_LENG_PROG.SugerenciasComentarios
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Comentarios : ContentPage
    {

        List<DatosConsultaComentarios> ListComentarios = new List<DatosConsultaComentarios>();
        DatosConsultaComentarios comentarioSeleccionado = new DatosConsultaComentarios();
        string url = (string)Application.Current.Properties["direccionDb"];
        int idUser = (int)Application.Current.Properties["idUsuario"];


        public Comentarios()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            MessagingCenter.Send<Object>(this, "HideOsNavigationBar");

        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            spinner.IsRunning = true;
            Thread hilo = new Thread(GetComments);
            hilo.Start();

        }

        private void GetComments()
        {
            var Userclient = new RestClient("http://127.0.0.1:8000");
            string ruta = "/api/comments";
            var request = new RestRequest(ruta, Method.GET);
            var queryResult = Userclient.Execute(request);
            string strJason = queryResult.Content;

            Device.BeginInvokeOnMainThread(() =>
            {
                ListComentarios = JsonConvert.DeserializeObject<List<DatosConsultaComentarios>>(strJason);
                if (ListComentarios != null)
                {
                    foreach (var i in ListComentarios)
                    {
                        int startIndex = 0;
                        int endIndex = i.created_at.Length - 17;
                        i.fecha = i.created_at.Substring(startIndex, endIndex);
                    }
                    ListComentarios.Reverse();
                    this.listView.ItemsSource = ListComentarios;
                }
                this.spinner.IsRunning = false;
            });
        }

        private void regresar_Clicked(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }

        private void addComments_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new AddComment(new DatosConsultaComentarios(),0));
        }

        private void listView_SwipeEnded(object sender, Syncfusion.ListView.XForms.SwipeEndedEventArgs e)
        {
            DatosConsultaComentarios commentarioSelec = (DatosConsultaComentarios)e.ItemData;
            comentarioSeleccionado = commentarioSelec;

        }

        private async void eliminar_Clicked(object sender, EventArgs e)
        {
            if(idUser == comentarioSeleccionado.id_user)
            {
                bool respuesta = await DisplayAlert("Alerta", "Está seguro que desea eliminar el comentario", "No", "Si");
                if (!respuesta)
                {
                    EliminarComentario();
                }
            }
            else
            {
                await DisplayAlert("msg", "No puede eliminar este comentario", "ok");
            }
            listView.ResetSwipe();
        }

        private async void EliminarComentario()
        {
            var userClient = new RestClient(url);
            string rutaFeed = "/api/comments/"+ comentarioSeleccionado.id;
            var requestFeed = new RestRequest(rutaFeed, Method.DELETE);

            var response = userClient.Execute(requestFeed);

            if (!(response.StatusCode == System.Net.HttpStatusCode.OK))
            {
                await DisplayAlert("msg", response.Content, "ok");
            }
            else
            {
                await DisplayAlert("Alerta", response.Content, "ok");
                Thread lista = new Thread(GetComments);
                lista.Start();
                listView.RefreshView();
            }

        }

        private void editar_Clicked(object sender, EventArgs e)
        {
            EditarComentario();
        }

        private async void EditarComentario()
        {
            //await Navigation.PushAsync(new AddComment(comentarioSeleccionado,1));
        }
    }
}