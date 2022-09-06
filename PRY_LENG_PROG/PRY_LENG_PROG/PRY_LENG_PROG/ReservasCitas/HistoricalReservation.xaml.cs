using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using RestSharp;
using Newtonsoft.Json;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using PRY_LENG_PROG.components;
using PRY_LENG_PROG.Modelos;

namespace PRY_LENG_PROG.ReservasCitas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HistoricalReservation : ContentPage
    {
        List<ReservationDoctorModel> reservations = new List<ReservationDoctorModel>();
        string url = (string)Application.Current.Properties["direccionDb"];
        int pet_id;

        public HistoricalReservation(int id_pet)
        {
            pet_id = id_pet;
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            MessagingCenter.Send<Object>(this, "HideOsNavigationBar");
            //header.Children.Add(new Header());
            //spinner.IsRunning = true;
            //GetReservations();
        }

        protected override void OnAppearing()
        {
            if (header.Children.Count > 0) {
                header.Children.Clear();
            }
            header.Children.Add(new Header());
            spinner.IsRunning = true;
            GetReservations();
        }

        private void GetReservations() {
            var client = new RestClient(url);
            string ruta = "/api/reservations/pet/" + pet_id+"/all";
            var request = new RestRequest(ruta, Method.GET);
            var queryResult = client.Execute(request);
            string strJson = queryResult.Content;
            
            reservations = JsonConvert.DeserializeObject<List<ReservationDoctorModel>>(strJson);
            if (reservations != null) {
                this.listView.ItemsSource = reservations;
            }
            spinner.IsRunning = false;
        }

        void goToReservationDetail(object sender, EventArgs e) {
            
        }

        void regresar_Clicked(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }

        private void listView_ItemTapped(object sender, Syncfusion.ListView.XForms.ItemTappedEventArgs e)
        {
            Navigation.PushAsync(new ReservationDetail((ReservationDoctorModel)e.ItemData));
        }
    }
}