using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;
using Newtonsoft.Json;
using PRY_LENG_PROG.components;
using PRY_LENG_PROG.Modelos;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PRY_LENG_PROG.ReservasCitas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PetReservation : ContentPage
    {
        private int pet_id;
        private PetModel pet;
        private int user_id = (int)Application.Current.Properties["idUsuario"];
        private string url = (string)Application.Current.Properties["direccionDb"];
        public PetReservation(int pet_id)
        {
            this.pet_id = pet_id;
            //Console.WriteLine(id);
            InitializeComponent();
            GetInfoPet();
            NavigationPage.SetHasNavigationBar(this, false);
            MessagingCenter.Send<Object>(this, "HideOsNavigationBar");
            header.Children.Add(new Header());
            petInfo.Children.Add(new PetDetail(pet));
        }

        void agendarCita(object sender, EventArgs e) {
            Navigation.PushAsync(new ChooseVet(pet_id));
        }

        void goToHistorical(object sender, EventArgs e)
        {
            Navigation.PushAsync(new HistoricalReservation(pet_id));
        }

        private void estadia(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Hoteles(pet_id));
        }

        private void control(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Mascotas.frmControlPet(pet_id));
        }

        private void regresar_Clicked(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }

        private async void btnEliminar_Clicked(object sender, EventArgs e)
        {
            bool respuesta = await DisplayAlert("Alerta", "Está seguro que desea eliminar la mascota", "Si", "No");
            if (respuesta)
            {
                EliminarMascota();
            }
        }

        private async void EliminarMascota()
        {
            var petClient = new RestClient(url);
            string rutaFeed = "/api/pets/" + pet_id;
            var requestFeed = new RestRequest(rutaFeed, Method.DELETE);

            var response = petClient.Execute(requestFeed);

            if (!(response.StatusCode == System.Net.HttpStatusCode.OK))
            {
                await DisplayAlert("Eliminación", "Hubo un error. No se pudo eliminar la mascota", "ok");
            }
            else
            {
                await DisplayAlert("Eliminación", "La mascota se ha eliminado exitosamente", "ok");
                await Navigation.PopAsync();
            }
        }

        private void btnEditar_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Mascotas.frmEditPet(pet_id));
        }
        
        protected override void OnAppearing()
        {
            base.OnAppearing();
            Application.Current.Properties["regresoReservaHotel"] = false;
            Application.Current.Properties["regresoActualizacionReservaHotel"] = false;
            GetInfoPet();
            petInfo.Children.Clear();
            petInfo.Children.Add(new PetDetail(pet));
        }

        void GetInfoPet()
        {
            var petClient = new RestClient((string)Application.Current.Properties["direccionDb"]);
            string ruta = "/api/pets/" + pet_id;
            var request = new RestRequest(ruta, Method.GET);
            var queryResult = petClient.Execute(request);
            string strJson = queryResult.Content;
            pet = JsonConvert.DeserializeObject<PetModel>(strJson);
        }
    }
}