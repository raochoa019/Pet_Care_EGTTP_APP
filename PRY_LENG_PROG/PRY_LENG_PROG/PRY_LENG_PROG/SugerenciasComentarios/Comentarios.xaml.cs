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

namespace PRY_LENG_PROG.SugerenciasComentarios
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Comentarios : ContentPage
    {
        List<DatosConsultaComentarios> ListComentarios = new List<DatosConsultaComentarios>();
        public Comentarios()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            MessagingCenter.Send<Object>(this, "HideOsNavigationBar");

        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            GetComments();
        }

        private void GetComments()
        {
            var Userclient = new RestClient("http://10.0.2.2:8000");
            string ruta = "/api/comments";
            var request = new RestRequest(ruta, Method.GET);
            var queryResult = Userclient.Execute(request);
            string strJason = queryResult.Content;
            ListComentarios = JsonConvert.DeserializeObject<List<DatosConsultaComentarios>>(strJason);


            
            
        } 


        private void regresar_Clicked(object sender, EventArgs e)
        {
            
        }

        private void siguiente_Clicked(object sender, EventArgs e)
        {
            DisplayAlert("msg", rating.Value.ToString(), "ok");            
        }
    }
}