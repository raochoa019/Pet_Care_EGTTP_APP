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
    public partial class DateDetails : ContentPage
    {
        int user_id = (int)Application.Current.Properties["idUsuario"];
        HotelReservation reserva;
        string dia_entrada = "";
        string dia_salida = "";
        public DateDetails(HotelReservation estadia)
        {
            InitializeComponent();
            header.Children.Add(new Header());
            dateStart.MinimumDate = DateTime.Today;
            reserva = estadia;
        }

        private void Return_Clicked(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }

        private void Agendar_Clicked(object sender, EventArgs e)
        {
            agendar();
        }
        private async void agendar()
        {
            var client = new RestClient((string)Application.Current.Properties["direccionDb"]);
            string ruta = "/api/hotel/reservations";
            reserva.user_id = user_id;
            reserva.admission_day = dateStart.Date.Day.ToString() + "/" + dateStart.Date.Month.ToString() + "/" + dateStart.Date.Year.ToString();
            reserva.day_of_exit = dateEnd.Date.Day.ToString() + "/" + dateEnd.Date.Month.ToString() + "/" + dateEnd.Date.Year.ToString();
            var request = new RestRequest(ruta, Method.POST) { RequestFormat = DataFormat.Json }.AddJsonBody(reserva);
            try
            {
                var response = client.Execute(request);

                if (!(response.StatusCode == System.Net.HttpStatusCode.OK))
                {
                    await DisplayAlert("msg", response.Content, "ok");
                }
                else
                {
                    await DisplayAlert("Alerta", response.Content, "ok");
                }

            }
            catch (Exception err)
            {
                await DisplayAlert("error", err.Message, "aceptar");
            }
        }
    }
}