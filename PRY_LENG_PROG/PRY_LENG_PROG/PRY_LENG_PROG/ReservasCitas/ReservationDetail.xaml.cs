using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PRY_LENG_PROG.Modelos;
using PRY_LENG_PROG.components;
using RestSharp;
using Newtonsoft.Json;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PRY_LENG_PROG.ReservasCitas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ReservationDetail : ContentPage
    {
        ReservationDoctorModel reservation_user;
        ReservationUserModel reservation_doctor;

        string url = (string)Application.Current.Properties["direccionDb"];
        string userRol = (string)Application.Current.Properties["userRol"];
        int user_id = (int)Application.Current.Properties["idUsuario"];
        public ReservationDetail(ReservationDoctorModel reservation)
        {
            reservation_user = reservation;
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            MessagingCenter.Send<Object>(this, "HideOsNavigationBar");
            header.Children.Add(new Header());
            user_name.Text = "Doctor";
            nombre.Text = reservation.pet_name;
            user.Text = reservation.doctor_name;
            email.Text = reservation.doctor_email;
            fecha.Text = reservation.date;
            txtHeight.Text = reservation.height.ToString();
            txtWeight.Text = reservation.weight.ToString();

            if (reservation.status == "PENDIENTE") {
                rdbPendiente.IsChecked = true;
            } else
            {
                rdbTerminado.IsChecked = true;
            };
            
            coments.Text = reservation.comments;
            

        }

        public ReservationDetail(ReservationUserModel reservation)
        {
            reservation_doctor = reservation;
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            MessagingCenter.Send<Object>(this, "HideOsNavigationBar");
            header.Children.Add(new Header());
            user_name.Text = "Cliente";
            nombre.Text = reservation.pet_name;
            user.Text = reservation.client_name;
            email.Text = reservation.client_email;
            fecha.Text = reservation.date;
            txtHeight.Text = reservation.height.ToString();
            txtWeight.Text = reservation.weight.ToString();

            txtHeight.IsEnabled = true;
            txtWeight.IsEnabled = true;
            rdbPendiente.IsEnabled = true;
            rdbTerminado.IsEnabled = true;
            coments.IsEnabled = true;
            btn_guardar.IsEnabled = true;

            if (reservation.status == "PENDIENTE")
            {
                rdbPendiente.IsChecked = true;
            }
            else
            {
                rdbTerminado.IsChecked = true;
            };

            coments.Text = reservation.comments;

        }

        async void saveReservation(object sender, EventArgs e) {
            int id = reservation_doctor.id;
            double height = Convert.ToDouble(txtHeight.Text);
            double weight = Convert.ToDouble(txtWeight.Text);
            string status = "";

            if (rdbPendiente.IsChecked)
            {
                status = "PENDIENTE";

            }
            if (rdbTerminado.IsChecked)
            {
                status = "TERMINADO";
            }

            var requestJson = new ModifyReservationModel();
            requestJson.comments = coments.Text;
            requestJson.height = height;
            requestJson.weight = weight;
            requestJson.status = status;

            var client = new RestClient(url);
            string rutaFeed = "/api/reservations/" + id;
            var requestFeed = new RestRequest(rutaFeed, Method.PUT) { RequestFormat = DataFormat.Json }.AddJsonBody(requestJson);

            try
            {
                var response = client.Execute(requestFeed);

                if (!(response.StatusCode == System.Net.HttpStatusCode.OK))
                {
                    await DisplayAlert("msg", response.Content, "ok");
                }
                else
                {
                    await DisplayAlert("Alerta", response.Content, "ok");
                    await Navigation.PopAsync();
                }

            }
            catch (Exception err)
            {
                await DisplayAlert("error", err.Message, "aceptar");
            }



        }

        void regresar_Clicked(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }

    }
}