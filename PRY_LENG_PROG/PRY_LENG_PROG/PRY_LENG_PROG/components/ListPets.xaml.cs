using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RestSharp;
using PRY_LENG_PROG.Modelos;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PRY_LENG_PROG.components
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListPets : ContentView
    {
        List<PetModel> pets = new List<PetModel>();
        private int user_id;

        public ListPets(int user_id)
        {
            this.user_id = user_id;
            InitializeComponent();

            GetPetsByUser(this.user_id);
            ShowPets();
        }

        void GetPetsByUser(int user_id)
        {
            var petClient = new RestClient("http://127.0.0.1:8000");
            string ruta = "/api/pets/user/" + user_id.ToString();
            var request = new RestRequest(ruta, Method.GET);
            var queryResult = petClient.Execute(request);
            string strJson = queryResult.Content;
            pets = JsonConvert.DeserializeObject<List<PetModel>>(strJson);
        }

        void ShowPets()
        {
            foreach(var pet in pets)
            {
                String defaultImage = "perro.jpg";
                if (pet.species.Equals("Gato")) {
                    defaultImage = "gato.png";
                } 

                PanelPets.Children.Add(new Pet(pet.id, pet.name, defaultImage));
            }
        }
    }
}