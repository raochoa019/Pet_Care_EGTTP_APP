using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RestSharp;
using PRY_LENG_PROG.components;
using PRY_LENG_PROG.Modelos;
using Syncfusion.SfChart.XForms;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PRY_LENG_PROG.Mascotas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class frmControlPet : ContentPage
    {
        private int pet_id;
        private PetModel pet;
        private List<ReservationModel> reservations;
        private int user_id = (int)Application.Current.Properties["idUsuario"];
        private string url = (string)Application.Current.Properties["direccionDb"];

        public frmControlPet(int pet_id)
        {
            this.pet_id = pet_id;
            InitializeComponent();
            GetInfoPet();
            GetInfoReservationsPet();
            lblNombre.Text += pet.name;
            lblEdad.Text += formatAge(calculateDays());
            NavigationPage.SetHasNavigationBar(this, false);
            MessagingCenter.Send<Object>(this, "HideOsNavigationBar");
            header.Children.Add(new Header());
            this.BindingContext = new ViewModel(reservations);
            initializeCharts();
        }

        private void initializeCharts()
        {
            //Initializing column series
            ColumnSeries series_peso = new ColumnSeries();
            series_peso.SetBinding(ChartSeries.ItemsSourceProperty, "DataWeight");
            series_peso.XBindingPath = "month";
            series_peso.YBindingPath = "info";
            chartPeso.Series.Add(series_peso);

            ColumnSeries series_altura = new ColumnSeries();
            series_altura.SetBinding(ChartSeries.ItemsSourceProperty, "DataHeight");
            series_altura.XBindingPath = "month";
            series_altura.YBindingPath = "info";
            chartAltura.Series.Add(series_altura);
        }

        private void GetInfoPet()
        {
            var petClient = new RestClient(url);
            string ruta = "/api/pets/" + pet_id;
            var request = new RestRequest(ruta, Method.GET);
            var queryResult = petClient.Execute(request);
            string strJson = queryResult.Content;
            pet = JsonConvert.DeserializeObject<PetModel>(strJson);
        }

        private void GetInfoReservationsPet()
        {
            var petClient = new RestClient(url);
            string ruta = "/api/reservations/pet/" + pet_id;
            var request = new RestRequest(ruta, Method.GET);
            var queryResult = petClient.Execute(request);
            string strJson = queryResult.Content;
            reservations = JsonConvert.DeserializeObject<List<ReservationModel>>(strJson);
        }

        private int calculateDays()
        {
            DateTime now = DateTime.Now;
            string[] infoDate = pet.birth_date.Split('-');
            DateTime then = new DateTime(Convert.ToInt32(infoDate[0]), Convert.ToInt32(infoDate[1]), Convert.ToInt32(infoDate[2]));

            TimeSpan ts = now.Subtract(then);
            return ts.Days;
        }

        private string formatAge(int totalDays)
        {
            int anios = totalDays / 365;
            int dias = totalDays % 365;
            return anios + " años, " + dias + " días.";
        }

        private void btnRegresar_Clicked(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }

        private void btnAltura_Clicked(object sender, EventArgs e)
        {
            chartPeso.IsVisible = false;
            chartAltura.IsVisible = true;
            btnAltura.IsEnabled = false;
            btnPeso.IsEnabled = true;
        }

        private void btnPeso_Clicked(object sender, EventArgs e)
        {
            chartAltura.IsVisible = false;
            chartPeso.IsVisible = true;
            btnPeso.IsEnabled = false;
            btnAltura.IsEnabled = true;
        }
    }
}