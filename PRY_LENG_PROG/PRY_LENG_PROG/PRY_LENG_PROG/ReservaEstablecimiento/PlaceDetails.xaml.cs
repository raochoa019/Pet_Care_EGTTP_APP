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
        HotelReservation hotel = new HotelReservation();
        int pet_id;
        bool act = false;

        public PlaceDetails(HotelReservation _hotel, bool actualizar, ReservationWithPetInfo res_pet = null)
        {
            InitializeComponent();
            header.Children.Add(new Header());
            hotel = _hotel;
            act = actualizar;
            pet_id = hotel.pet_id;
            sitio.Text = hotel.nombre_hotel;
            ubicacion.Text = hotel.direction;
            if (!actualizar)
            {
                PetInfo();
            }
            else
            {
                nombre.Text = res_pet.name;
                especie.Text = res_pet.species;
                raza.Text = res_pet.breed;
                Application.Current.Properties["id_reservacion"] = res_pet.id;
            }
        }

        private void Siguiente_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ExtraDetails(hotel,act));
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