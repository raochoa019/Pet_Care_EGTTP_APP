using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;
using PRY_LENG_PROG.components;
using PRY_LENG_PROG.Modelos;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PRY_LENG_PROG.Mascotas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class frmNewPet : ContentPage
    {
        private int user_id = (int)Application.Current.Properties["idUsuario"];
        private string url = (string)Application.Current.Properties["direccionDb"];

        private List<string> lstPerros = new List<string>();
        private List<string> lstGatos = new List<string>();

        public frmNewPet()
        {
            InitializeComponent();
            initializeLists();
            NavigationPage.SetHasNavigationBar(this, false);
            MessagingCenter.Send<Object>(this, "HideOsNavigationBar");
            header.Children.Add(new Header());
        }

        private void initializeLists()
        {
            lstPerros.Add("Dalmata");
            lstPerros.Add("Golden");
            lstPerros.Add("Husky");
            lstPerros.Add("Labrador");
            lstPerros.Add("French Poodle");
            lstPerros.Add("Otro");

            lstGatos.Add("Persa");
            lstGatos.Add("Romano");
            lstGatos.Add("Siames");
            lstGatos.Add("Angora");
            lstGatos.Add("Garfield");
            lstGatos.Add("Otro");
        }

        void especieSelectedIndexChanged(object sender, EventArgs e)
        {
            var picker = (Picker)sender;
            int selectedIndex = picker.SelectedIndex;

            if (selectedIndex != -1)
            {
                string speciesSelected = picker.Items[selectedIndex];

                raza.Items.Clear();
                if (speciesSelected == "Gato")
                {         
                    foreach(string _raza in lstGatos)
                    {
                        raza.Items.Add(_raza);
                    }
                }
                else
                {
                    foreach (string _raza in lstPerros)
                    {
                        raza.Items.Add(_raza);
                    }
                }
            }
        }

        private void fillRazaPicker()
        {

        }

        private void regresar_Clicked(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }

        private async void btnRegistrar_Clicked(object sender, EventArgs e)
        {
            PetModel pet = new PetModel();
            pet.user_id = user_id;
            pet.name = nombre.Text;
            pet.species = (string)especie.SelectedItem;
            pet.breed = (string)raza.SelectedItem;           
            if (fecha.Date != null) pet.birth_date = fecha.Date;
            if (peso.Text != "") pet.weight = Convert.ToDouble(peso.Text);       
            if (altura.Text != "") pet.height = Convert.ToDouble(altura.Text);

            var petClient = new RestClient(url);
            string rutaFeed = "/api/pets";
            var requestFeed = new RestRequest(rutaFeed, Method.POST) { RequestFormat = DataFormat.Json }.AddJsonBody(pet);
            try
            {
                var response = petClient.Execute(requestFeed);
                if (!(response.StatusCode == System.Net.HttpStatusCode.Created))
                {
                    await DisplayAlert("Alerta", "Asegúrese de llenar correctamente todos los campos", "ok");
                }
                else
                {
                    await DisplayAlert("Registro", "Mascota registrada exitosamente", "ok");
                    await Navigation.PushAsync(new frmPets(user_id));
                }
            }
            catch (Exception err)
            {
                await DisplayAlert("error", err.Message, "aceptar");
            }
        }
    }
}