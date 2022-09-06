using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PRY_LENG_PROG.components;
using PRY_LENG_PROG.Modelos;
using Newtonsoft.Json;
using RestSharp;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PRY_LENG_PROG.Mascotas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class frmPets : ContentPage
    {
        List<PetModel> pets;
        private int user_id;

        public frmPets(int id)
        {
            user_id = id;
            InitializeComponent();
            GetPetsByUser();
            NavigationPage.SetHasNavigationBar(this, false);
            MessagingCenter.Send<Object>(this, "HideOsNavigationBar");
            header.Children.Add(new Header());
            titlePage_addPet.Children.Add(new NewPet());
            showPets.Children.Add(new ListPets(pets));
        }

        private void regresar_Clicked(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }

        private void addPet_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new frmNewPet());
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            Application.Current.Properties["regresoConsulta"] = false;
            GetPetsByUser();
            showPets.Children.Clear();
            showPets.Children.Add(new ListPets(pets));
        }

        void GetPetsByUser()
        {
            var petClient = new RestClient((string)Application.Current.Properties["direccionDb"]);
            string ruta = "/api/pets/user/" + user_id.ToString();
            var request = new RestRequest(ruta, Method.GET);
            var queryResult = petClient.Execute(request);
            string strJson = queryResult.Content;
            pets = JsonConvert.DeserializeObject<List<PetModel>>(strJson);
        }
    }
}