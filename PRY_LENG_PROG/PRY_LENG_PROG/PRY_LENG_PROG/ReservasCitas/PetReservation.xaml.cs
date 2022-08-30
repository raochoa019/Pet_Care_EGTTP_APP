using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;
using PRY_LENG_PROG.components;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PRY_LENG_PROG.ReservasCitas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PetReservation : ContentPage
    {
        private int pet_id;
        private int user_id = (int)Application.Current.Properties["idUsuario"];
        private string url = (string)Application.Current.Properties["direccionDb"];
        public PetReservation(int id)
        {
            pet_id = id;
            //Console.WriteLine(id);
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            MessagingCenter.Send<Object>(this, "HideOsNavigationBar");
            header.Children.Add(new Header());
            petInfo.Children.Add(new PetDetail(pet_id));
        }

        void agendarCita(object sender, EventArgs e) {
            Navigation.PushAsync(new ChooseVet(pet_id));
        }

        private void estadia(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Hoteles(pet_id));
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
                await Navigation.PushAsync(new Mascotas.frmPets(user_id));
            }

        }
    }
}