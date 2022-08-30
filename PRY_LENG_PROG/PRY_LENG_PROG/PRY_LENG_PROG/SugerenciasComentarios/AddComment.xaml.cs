using Newtonsoft.Json;
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

        DatosConsultaComentarioById commentxId = new DatosConsultaComentarioById(); 
        DatosConsultaComentarios comentarioSeleccionado = new DatosConsultaComentarios();
        int flag;

        public AddComment(DatosConsultaComentarios cmmt, int validar)
        {
            this.comentarioSeleccionado = cmmt;
            this.flag = validar;
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            MessagingCenter.Send<Object>(this, "HideOsNavigationBar");

        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            if(flag == 1)
            {
                tituloPosteo.Text = "Editar Comentario";
                GetCommentToEdit();
            }
            else
            {
                tituloPosteo.Text = "Postear Comentario";
            }
        }

        private void GetCommentToEdit()
        {
            var Userclient = new RestClient("http://127.0.0.1:8000");
            string ruta = "/api/comment/"+ comentarioSeleccionado.id.ToString();
            var request = new RestRequest(ruta, Method.GET);
            var queryResult = Userclient.Execute(request);
            string strJason = queryResult.Content;
            commentxId = JsonConvert.DeserializeObject<DatosConsultaComentarioById>(strJason);
            if(commentxId != null)
            {
                tituloComentario.Text = commentxId.title;
                rating.Value = commentxId.rating;
                bodyComentario.Text = commentxId.comment;

            }
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

        private async void EditComments()
        {
            DatosRegistroComentario comment = new DatosRegistroComentario();
            comment.id_user = comentarioSeleccionado.id_user;
            comment.name_user = comentarioSeleccionado.name_user;
            comment.rating = (int)rating.Value;
            comment.title = tituloComentario.Text;
            comment.comment = bodyComentario.Text;

            var userClient = new RestClient(url);
            string rutaFeed = "/api/comments/"+comentarioSeleccionado.id.ToString();
            var requestFeed = new RestRequest(rutaFeed, Method.PUT) { RequestFormat = DataFormat.Json }.AddJsonBody(comment);
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
            if(flag == 1)
            {
                EditComments();
            }
            else
            {
                RegisterComments();
            }
        }

        private void regresar_Clicked(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }
    }
}