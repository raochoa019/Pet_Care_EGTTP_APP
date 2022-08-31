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
    public partial class ChooseVet : ContentPage
    {

        List<UserModel> listVets = new List<UserModel>();
        public ChooseVet(int id_pet)
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            MessagingCenter.Send<Object>(this, "HideOsNavigationBar");

            header.Children.Add(new Header());
            GetVets();
            // List<StackLayout> list = new List<StackLayout>();

            foreach (UserModel vet in listVets) {
                StackLayout sl = new StackLayout { Padding = new Thickness(0,20) };
                // Event tap
                var tapEvent = new TapGestureRecognizer();
                
                tapEvent.Tapped += (s, e) =>
                {
                    Navigation.PushAsync(new CreateReservation(vet.id,id_pet));
                };

                sl.GestureRecognizers.Add(tapEvent);

                // Create Grid Layout 

                var gridRow = new Grid
                {
                    RowDefinitions = {
                        new RowDefinition { Height = GridLength.Auto },
                        new RowDefinition { Height = GridLength.Auto }
                    }
                };

                var gridColumn = new Grid
                {
                    ColumnDefinitions = {
                        new ColumnDefinition { Width = new GridLength(100) },
                        new ColumnDefinition { Width = new GridLength(200) }
                    },
                };

                gridColumn.Children.Add(
                    new Image { Source = "veterinario.gif", WidthRequest = 50}, 0 , 0
                );

                gridColumn.Children.Add(
                    gridRow, 1, 0
                );

                gridRow.Children.Add(
                    new Label { Text = vet.name, TextColor = Color.Black }, 0, 0
                );

                gridRow.Children.Add(
                    new Label { Text = vet.email, TextColor = Color.Black }, 0, 1
                );

                sl.Children.Add(gridColumn);

                vets.Children.Add(sl);
            }

        }

        void GetVets() {
            var vetsClient = new RestClient("http://127.0.0.1:8000");
            string ruta = "/api/vets";
            var request = new RestRequest(ruta, Method.GET);
            var queryResult = vetsClient.Execute(request);
            string strJason = queryResult.Content;
            listVets = JsonConvert.DeserializeObject<List<UserModel>>(strJason);
            // Console.WriteLine(listVets);
        }

        void regresar_Clicked(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }

    }
}