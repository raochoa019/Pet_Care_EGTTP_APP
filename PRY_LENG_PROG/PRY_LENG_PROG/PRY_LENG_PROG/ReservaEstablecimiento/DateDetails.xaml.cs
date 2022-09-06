using PRY_LENG_PROG.components;
using PRY_LENG_PROG.ReservaEstablecimiento.ModelosEstablecimiento;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Globalization;
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
        int id_reserva;
        HotelReservation reserva;
        bool act = false;
        public DateDetails(HotelReservation estadia,bool actualizar)
        {
            InitializeComponent();
            header.Children.Add(new Header());
            NavigationPage.SetHasNavigationBar(this, false);
            MessagingCenter.Send<Object>(this, "HideOsNavigationBar");
            reserva = estadia;
            act = actualizar;
            if (act)
            {

                string[] inicio = estadia.admission_day.Split('/');
                string[] fin = estadia.day_of_exit.Split('/');
                dateStart.Date = new DateTime(Convert.ToInt32(inicio[2]), Convert.ToInt32(inicio[1]),Convert.ToInt32(inicio[0])).Date;
                dateEnd.Date = new DateTime(Convert.ToInt32(fin[2]), Convert.ToInt32(fin[1]), Convert.ToInt32(fin[0])).Date;
            }
            else
            {
                dateStart.MinimumDate = DateTime.Now;
            }
        }

        private void Return_Clicked(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }

        private void Agendar_Clicked(object sender, EventArgs e)
        {
            if (!act)
                agendar();
            else
                actualizar();
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
                    Application.Current.Properties["regresoReservaHotel"] = true;
                    await Navigation.PopAsync();
                }

            }
            catch (Exception err)
            {
                await DisplayAlert("error", err.Message, "aceptar");
            }
            
            
        }
        private async void actualizar()
        {
            var client = new RestClient((string)Application.Current.Properties["direccionDb"]);
            id_reserva = (int)Application.Current.Properties["id_reservacion"];
            string ruta = "/api/hotel/reservations/"+id_reserva;
            reserva.user_id = user_id;
            reserva.admission_day = dateStart.Date.Day.ToString() + "/" + dateStart.Date.Month.ToString() + "/" + dateStart.Date.Year.ToString();
            reserva.day_of_exit = dateEnd.Date.Day.ToString() + "/" + dateEnd.Date.Month.ToString() + "/" + dateEnd.Date.Year.ToString();
            var request = new RestRequest(ruta, Method.PUT) { RequestFormat = DataFormat.Json }.AddJsonBody(reserva);
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
                    Application.Current.Properties["regresoActualizacionReservaHotel"] = true;
                    await Navigation.PopAsync();
                }

            }
            catch (Exception err)
            {
                await DisplayAlert("error", err.Message, "aceptar");
            }
        }
    }
}