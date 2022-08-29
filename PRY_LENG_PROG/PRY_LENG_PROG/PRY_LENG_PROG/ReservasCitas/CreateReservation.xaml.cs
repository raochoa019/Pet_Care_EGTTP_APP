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

        public CreateReservation(int id_vet,int id_pet)
        {
            pet_id = id_pet;
            vet_id = id_vet;

            InitializeComponent();
            header.Children.Add(new Header());

            GetPetInfo();
            SetPetInfo();

            GetVetInfo();
            SetVetInfo();



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
            string ruta = "/api/pets/"+pet_id.ToString();
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
    }
}