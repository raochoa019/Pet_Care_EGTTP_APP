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
using PRY_LENG_PROG.Modelos;
using PRY_LENG_PROG.ReservaEstablecimiento.ModelosEstablecimiento;

namespace PRY_LENG_PROG.ReservaEstablecimiento
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PlaceDetails : ContentPage
    {
        string name = "";
        string direction = "";
        int pet_id;
        private Dictionary<string, string> detalles = new Dictionary<string, string>();

        public PlaceDetails(string nombre, string direccion, int pet)
        {
            InitializeComponent();
            header.Children.Add(new Header());
            pet_id = pet;
            name = nombre;
            direction = direccion;
            sitio.Text = nombre;
            ubicacion.Text = direccion;
            PetInfo();
        }

        private void Siguiente_Clicked(object sender, EventArgs e)
        {
            HotelReservation lugar = new HotelReservation();
            lugar.pet_id = pet_id;
            lugar.nombre_hotel = name;
            lugar.direction = direction;

            Navigation.PushAsync(new ExtraDetails(lugar));
        }

        private void Return_Clicked(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }
        private void PetInfo()
        {
            var petClient = new RestClient((string)Application.Current.Properties["direccionDb"]);
            string ruta = "/api/pets/"+pet_id;
            var request = new RestRequest(ruta, Method.GET);
            var queryResult = petClient.Execute(request);
            string strJason = queryResult.Content;
            PetModel pet = JsonConvert.DeserializeObject<PetModel>(strJason);
            if (pet != null)
            {
                nombre.Text = pet.name;
                especie.Text = pet.species;
                raza.Text = pet.breed;
            }
        }
    }
}