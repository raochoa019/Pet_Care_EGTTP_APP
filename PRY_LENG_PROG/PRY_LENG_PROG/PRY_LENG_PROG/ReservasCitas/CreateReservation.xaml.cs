using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PRY_LENG_PROG.components;
using Newtonsoft.Json;
using RestSharp;
using PRY_LENG_PROG.Modelos;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PRY_LENG_PROG.ReservasCitas

{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CreateReservation : ContentPage
    {

        PetModel pet;
        UserModel vet;
        private int pet_id;
        private int vet_id;
        List<ReservationModel> reservations;
        //List<int> times;

        public CreateReservation(int id_vet, int id_pet)
        {
            pet_id = id_pet;
            vet_id = id_vet;
            //times = new List<int> { 9, 10, 11, 12, 13, 14, 15, 16, 17 };

            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            MessagingCenter.Send<Object>(this, "HideOsNavigationBar");

            header.Children.Add(new Header());

            GetPetInfo();
            SetPetInfo();

            GetVetInfo();
            SetVetInfo();

            GetReservationsDoctor();

            var date = DateTime.Now;

            datePicker.MinimumDate = date;
            datePicker.MaximumDate = date.AddDays(30);

            timePicker.ItemsSource = new int[]{ 9, 10, 11, 12, 13, 14, 15, 16, 17 };

            

        }

        void regresar_Clicked(object sender, EventArgs e) {
            Navigation.PopAsync();
        }

        void enviar_Clicked(object sender, EventArgs e)
        {
            RegisterReservation();
        }

        private async void RegisterReservation()
        {

            DateTime date = new DateTime(datePicker.Date.Year, datePicker.Date.Month, datePicker.Date.Day,(int)timePicker.SelectedItem,0,0);
            Console.WriteLine(date.ToString("yyyy-MM-dd HH:mm:ss"));
            ReservationModel reserva = new ReservationModel();
            reserva.doctor_id = vet_id;
            reserva.pet_id = pet_id;
            reserva.date = date.ToString("yyyy-MM-dd HH:mm:ss");
            reserva.height = 0;
            reserva.weight = 0;

            var userClient = new RestClient("http://127.0.0.1:8000");
            string rutaFeed = "/api/reservations";
            var requestFeed = new RestRequest(rutaFeed, Method.POST) { RequestFormat = DataFormat.Json }.AddJsonBody(reserva);
            try
            {
                var response = userClient.Execute(requestFeed);

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

        void SetPetInfo() {
            pet_detail.Children.Add(
                new Label { Text = pet.name, TextColor = Color.Black, HorizontalTextAlignment = TextAlignment.Start },
                1, 0
            );

            pet_detail.Children.Add(
                new Label { Text = pet.species, TextColor = Color.Black, HorizontalTextAlignment = TextAlignment.Start },
                1, 1
            );

            pet_detail.Children.Add(
                new Label { Text = pet.breed, TextColor = Color.Black, HorizontalTextAlignment = TextAlignment.Start },
                1, 2
            );
        }

        void SetVetInfo() {
            vet_detail.Children.Add(
                new Label { Text = vet.name, TextColor = Color.Black, HorizontalTextAlignment = TextAlignment.Start },
                1, 0
            );

            vet_detail.Children.Add(
                new Label { Text = vet.email, TextColor = Color.Black, HorizontalTextAlignment = TextAlignment.Start },
                1, 1
            );
        }

        void GetPetInfo() {
            var petClient = new RestClient("http://127.0.0.1:8000");
            string ruta = "/api/pets/" + pet_id.ToString();
            var request = new RestRequest(ruta, Method.GET);
            var queryResult = petClient.Execute(request);
            string strJson = queryResult.Content;
            pet = JsonConvert.DeserializeObject<PetModel>(strJson);
        }

        void GetVetInfo()
        {
            var vetClient = new RestClient("http://127.0.0.1:8000");
            string ruta = "/api/users/" + vet_id.ToString();
            var request = new RestRequest(ruta, Method.GET);
            var queryResult = vetClient.Execute(request);
            string strJson = queryResult.Content;
            vet = JsonConvert.DeserializeObject<UserModel>(strJson);
        }

        void GetReservationsDoctor() {
            var reservationClient = new RestClient("http://127.0.0.1:8000");
            string ruta = "/api/reservations/doctor/" + vet_id.ToString();
            var request = new RestRequest(ruta, Method.GET);
            var queryResult = reservationClient.Execute(request);
            string strJson = queryResult.Content;
            reservations = JsonConvert.DeserializeObject<List<ReservationModel>>(strJson);
        }

        List<int> GetTimesAllowed(DateTime date) {
            List<int> times = new List<int> { 9, 10, 11, 12, 13, 14, 15, 16, 17 };

            foreach (ReservationModel res in reservations) {
                DateTime resDate = Convert.ToDateTime(res.date);

                if (resDate.Date.Equals(date.Date)) {
                    times.Remove(resDate.Hour);
                }
                
            }

            return times;
        }

        void DateSelected(object sender, FocusEventArgs e) {
            Console.WriteLine(datePicker.Date.ToString());
            timePicker.ItemsSource.Clear();
            timePicker.ItemsSource = GetTimesAllowed(datePicker.Date);
        }
    }
}