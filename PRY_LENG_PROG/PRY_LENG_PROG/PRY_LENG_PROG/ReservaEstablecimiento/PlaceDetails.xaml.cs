using PRY_LENG_PROG.components;
using RestSharp;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PRY_LENG_PROG.ReservaEstablecimiento
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PlaceDetails : ContentPage
    {
        private Dictionary<string, string> detalles;
        public PlaceDetails()
        {
            InitializeComponent();
            header.Children.Add(new Header());
            PetInfo("1");
        }

        private void Siguiente_Clicked(object sender, EventArgs e)
        {
            modifyInformacion();
            Navigation.PushAsync(new ExtraDetails());
        }

        private void Return_Clicked(object sender, EventArgs e)
        {
            modifyInformacion();
            Navigation.PopAsync();
        }
        private void PetInfo(string id)
        {
            var petClient = new RestClient("http://10.0.2.2:8000/");
            //var Userclient = new RestClient("http://10.0.2.2:8000");
            string ruta = "/api/pets/"+id;
            var request = new RestRequest(ruta, Method.GET);
            var queryResult = petClient.Execute(request);
            string strJason = queryResult.Content;
            /*PetModel pet = JsonConvert.DeserializeObject<PetModel>(strJason);
            if (pet != null)
            {
                nombre.Text = pet.name;
                especie.Text = pet.species;
                raza.Text = pet.breed;
            }*/
        }
        private void modifyInformacion()
        {
            detalles.Add("nombre",nombre.Text);
            detalles.Add("especie",especie.Text);
            detalles.Add("raza",raza.Text);
        }
    }
}