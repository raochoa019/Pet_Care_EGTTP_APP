using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RestSharp;
using PRY_LENG_PROG.components;
using PRY_LENG_PROG.Modelos;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PRY_LENG_PROG.Mascotas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class frmEditPet : ContentPage
    {
        private int pet_id;
        private PetModel pet;
        private int user_id = (int)Application.Current.Properties["idUsuario"];
        private string url = (string)Application.Current.Properties["direccionDb"];

        private List<string> lstPerros = new List<string>();
        private List<string> lstGatos = new List<string>();

        public frmEditPet(int pet_id)
        {
            this.pet_id = pet_id;
            InitializeComponent();
            fecha.MaximumDate = DateTime.Now;
            initializeLists();
            GetInfoPet(pet_id);
            fillFieldsPet();
            NavigationPage.SetHasNavigationBar(this, false);
            MessagingCenter.Send<Object>(this, "HideOsNavigationBar");
            header.Children.Add(new Header());
        }

        private void GetInfoPet(int pet_id)
        {
            var petClient = new RestClient(url);
            string ruta = "/api/pets/" + pet_id.ToString();
            var request = new RestRequest(ruta, Method.GET);
            var queryResult = petClient.Execute(request);
            string strJson = queryResult.Content;
            pet = JsonConvert.DeserializeObject<PetModel>(strJson);
        }

        private void fillFieldsPet()
        {
            nombre.Text = pet.name;
            string[] infoDate = pet.birth_date.Split('-');
            fecha.Date = new DateTime(Convert.ToInt32(infoDate[0]), Convert.ToInt32(infoDate[1]), Convert.ToInt32(infoDate[2]));
            especie.SelectedItem = pet.species;

            if(pet.species == "Gato")
            {
                foreach (string _raza in lstGatos)
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

            raza.SelectedItem = pet.breed;
            peso.Text = Convert.ToString(pet.weight);
            altura.Text = Convert.ToString(pet.height);
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

        private async void btnGuardar_Clicked(object sender, EventArgs e)
        {
            // Campos modificables
            pet.name = nombre.Text;
            pet.birth_date = preprocessDate(fecha.Date.ToShortDateString());
            Console.WriteLine(pet.birth_date);
            pet.weight = String.IsNullOrWhiteSpace(peso.Text) ? 0.0 : Convert.ToDouble(peso.Text);
            pet.height = String.IsNullOrWhiteSpace(altura.Text) ? 0.0 : Convert.ToDouble(altura.Text);

            var petClient = new RestClient(url);
            string rutaFeed = "/api/pets/" + pet_id;
            var requestFeed = new RestRequest(rutaFeed, Method.PUT) { RequestFormat = DataFormat.Json }.AddJsonBody(pet);
            try
            {
                var response = petClient.Execute(requestFeed);
                if (!(response.StatusCode == System.Net.HttpStatusCode.OK))
                {
                    await DisplayAlert("Alerta", "Asegúrese de llenar correctamente todos los campos", "ok");
                }
                else
                {
                    await DisplayAlert("Mascota " + pet.name, "Campos actualizados exitosamente", "ok");
                    await Navigation.PushAsync(new frmPets(user_id));
                }
            }
            catch (Exception err)
            {
                await DisplayAlert("error", err.Message, "aceptar");
            }

        }

        private string preprocessDate(string date)
        {
            string[] infoDate = date.Split('/');
            string mes = infoDate[0].Length < 2 ? '0' + infoDate[0] : infoDate[0];
            string dia = infoDate[1].Length < 2 ? '0' + infoDate[1] : infoDate[1];
            return infoDate[2] + '-' + mes + '-' + dia;
        }

        private void btnRegresar_Clicked(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }

    }
}