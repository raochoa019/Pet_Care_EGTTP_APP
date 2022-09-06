﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PRY_LENG_PROG.components;
using PRY_LENG_PROG.Modelos;
using Newtonsoft.Json;
using RestSharp;

using Xamarin.Forms;

namespace PRY_LENG_PROG.ReservasCitas
{
    public class ReservasCitasPage : ContentPage
    {
        private int user_id = (int)Application.Current.Properties["idUsuario"];
        StackLayout layout = new StackLayout();
        private List<PetModel> pets;

        public ReservasCitasPage()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            MessagingCenter.Send<Object>(this, "HideOsNavigationBar");
            GetPetsByUser();
            Content = new StackLayout
            {
                Children = {
                    new Header(),
                    new NewPet(),
                    new ListPets(pets)
                }
            };
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            Console.WriteLine("Refresh");
            if (layout.Children.Count > 0)
            {
                layout.Children.Clear();
                Console.WriteLine("Borrando");
            }

            layout.Children.Add(new Header());
            layout.Children.Add(new NewPet());
            layout.Children.Add(new ListPets(pets));
            
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