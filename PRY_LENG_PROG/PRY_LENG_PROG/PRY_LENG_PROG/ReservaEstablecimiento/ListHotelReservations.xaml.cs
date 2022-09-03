using Newtonsoft.Json;
using PRY_LENG_PROG.components;
using PRY_LENG_PROG.ReservaEstablecimiento.ModelosEstablecimiento;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PRY_LENG_PROG.ReservaEstablecimiento
{
    [XamlCompilation(XamlCompilationOptions.Compile)]

    public partial class ListHotelReservations : ContentPage
    {
        List<ReservationWithPetInfo> reservaciones = new List<ReservationWithPetInfo>();
        string id_user = Application.Current.Properties["idUsuario"].ToString();
        public ListHotelReservations()
        {
            InitializeComponent();
            header.Children.Add(new Header());
        }

        private void GetReservations()
        {
            var petClient = new RestClient((string)Application.Current.Properties["direccionDb"]);
            string ruta = "/api/hotel/reservations/user-pet/" + id_user;
            var request = new RestRequest(ruta, Method.GET);
            var queryResult = petClient.Execute(request);
            string strJson = queryResult.Content;
            reservaciones = JsonConvert.DeserializeObject<List<ReservationWithPetInfo>>(strJson);
            Device.BeginInvokeOnMainThread(() =>
            {
                if (reservaciones != null)
                {
                    listView.ItemsSource = reservaciones;
                }
            });
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            GetReservations();
        }

        private async void listView_ItemTapped(object sender, Syncfusion.ListView.XForms.ItemTappedEventArgs e)
        {
            ReservationWithPetInfo hotelSeleccionado = (ReservationWithPetInfo)e.ItemData;
            string action = await DisplayActionSheet("Seleccione un opcion", "Cancelar", null,"Actualizar", "Eliminar");
            if (action.Equals("Actualizar"))
            {
                HotelReservation hotel = new HotelReservation();
                hotel.nombre_hotel = hotelSeleccionado.nombre_hotel;
                hotel.direction = hotelSeleccionado.direction;
                hotel.pet_id = hotelSeleccionado.pet_id;
                hotel.exercises = hotelSeleccionado.exercises;
                hotel.feeding = hotelSeleccionado.feeding;
                hotel.rides = hotelSeleccionado.rides;
                hotel.special_cares = hotelSeleccionado.special_cares;
                hotel.admission_day = hotelSeleccionado.admission_day;
                hotel.day_of_exit = hotelSeleccionado.day_of_exit;

                await Navigation.PushAsync(new PlaceDetails(hotel, true, hotelSeleccionado));
            }
            else
            {
                await DisplayAlert("Eliminar", "¿Esta seguro que desea eliminar esta reserva?", "Si", "No");
                DeleteReservation(hotelSeleccionado.id);
            }
            
        }
        
        private void Return_Clicked(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }

        private void listView_ItemHolding(object sender, Syncfusion.ListView.XForms.ItemHoldingEventArgs e)
        {
            ReservationWithPetInfo hotelSeleccionado = (ReservationWithPetInfo)e.ItemData;
            DisplayAlert("Prueba", "Id: "+hotelSeleccionado.id, "ok");
        }
        private async void DeleteReservation(int id)
        {
            var hotelClient = new RestClient((string)Application.Current.Properties["direccionDb"]);
            string ruta = "/api/hotel/reservations/" + id;
            var request = new RestRequest(ruta, Method.DELETE);
            try
            {
                var response = hotelClient.Execute(request);

                if (!(response.StatusCode == System.Net.HttpStatusCode.OK))
                {
                    await DisplayAlert("msg", response.Content, "ok");
                    
                }
                else
                {
                    await DisplayAlert("Alerta", response.Content, "ok");
                    Thread lista = new Thread(GetReservations);
                    lista.Start();
                    listView.RefreshView();
                }

            }
            catch (Exception err)
            {
                await DisplayAlert("error", err.Message, "aceptar");
            }
        }
    }
}