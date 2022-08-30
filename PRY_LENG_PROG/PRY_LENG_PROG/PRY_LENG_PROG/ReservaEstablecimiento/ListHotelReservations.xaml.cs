using Newtonsoft.Json;
using PRY_LENG_PROG.components;
using PRY_LENG_PROG.ReservaEstablecimiento.ModelosEstablecimiento;
using RestSharp;
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
            if (reservaciones != null)
            {
                listView.ItemsSource = reservaciones;
            }
            
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            GetReservations();
        }

        private void listView_ItemTapped(object sender, Syncfusion.ListView.XForms.ItemTappedEventArgs e)
        {
            ReservationWithPetInfo hotelSeleccionado = (ReservationWithPetInfo)e.ItemData;
            DisplayAlert("Actualizar", hotelSeleccionado.nombre_hotel, "cerrar");
            //Navigation.PushAsync(new PlaceDetails(hotelSeleccionado.nombreHotel, hotelSeleccionado.direccion, pet));
        }

        private void Return_Clicked(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }
    }
}