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

namespace PRY_LENG_PROG.components
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PetDetail : ContentView
    {
        private int pet_id;
        PetModel pet;
        public PetDetail(int id)
        {
            pet_id = id;
            InitializeComponent();

            GetPetInfo();
            //imagePet.Source(pet.species);
            SetPetInfo();
        }

        void GetPetInfo()
        {
            var petClient = new RestClient((string)Application.Current.Properties["direccionDb"]);
            string ruta = "/api/pets/" + pet_id.ToString();
            var request = new RestRequest(ruta, Method.GET);
            var queryResult = petClient.Execute(request);
            string strJson = queryResult.Content;
            pet = JsonConvert.DeserializeObject<PetModel>(strJson);
        }

        void SetPetInfo()
        {
            String defaultImage = "perro.jpg";
            if (pet.species.Equals("Gato"))
            {
                defaultImage = "gato.jpg";
            }
            imagePet.Source = defaultImage;

            pet_detail.Children.Add(
                new Label { Text = pet.name, TextColor = Color.Black, HorizontalTextAlignment = TextAlignment.Start },
                1, 0
            );

            pet_detail.Children.Add(
                new Label { Text = pet.birth_date, TextColor = Color.Black, HorizontalTextAlignment = TextAlignment.Start },
                1, 1
            );

            pet_detail.Children.Add(
                new Label { Text = pet.species, TextColor = Color.Black, HorizontalTextAlignment = TextAlignment.Start },
                1, 2
            );

            pet_detail.Children.Add(
                new Label { Text = pet.breed, TextColor = Color.Black, HorizontalTextAlignment = TextAlignment.Start },
                1, 3
            );        
        }
    }
}