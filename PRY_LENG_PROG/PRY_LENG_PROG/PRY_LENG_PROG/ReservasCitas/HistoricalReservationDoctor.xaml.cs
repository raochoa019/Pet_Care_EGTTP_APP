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
    public partial class HistoricalReservationDoctor : ContentPage
    {
        List<ReservationUserModel> reservations = new List<ReservationUserModel>();
        string url = (string)Application.Current.Properties["direccionDb"];
        int vet_id = (int)Application.Current.Properties["idUsuario"];

        public HistoricalReservationDoctor()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            MessagingCenter.Send<Object>(this, "HideOsNavigationBar");
            //header.Children.Add(new Header());

            //spinner.IsRunning = true;
            //GetReservations();
        }

        protected override void OnAppearing() {
            if (header.Children.Count > 0)
            {
                header.Children.Clear();
            }
            header.Children.Add(new Header());
            spinner.IsRunning = true;
            GetReservations();
        }

        private void GetReservations() {
            var client = new RestClient(url);
            string ruta = "/api/reservations/doctor/" + vet_id;
            var request = new RestRequest(ruta, Method.GET);
            var queryResult = client.Execute(request);
            string strJson = queryResult.Content;
            
            reservations = JsonConvert.DeserializeObject<List<ReservationUserModel>>(strJson);
            if (reservations != null) {
                this.listView.ItemsSource = reservations;
            }
            spinner.IsRunning = false;
        }

        private void listView_ItemTapped(object sender, Syncfusion.ListView.XForms.ItemTappedEventArgs e)
        {
            Navigation.PushAsync(new ReservationDetail((ReservationUserModel)e.ItemData));
        }

        void regresar_Clicked(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }
    }
}