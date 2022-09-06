using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PRY_LENG_PROG.components;

using Xamarin.Forms;

namespace PRY_LENG_PROG.ReservasCitas
{
    public class ReservasCitasPage : ContentPage
    {
        private int user_id = (int)Application.Current.Properties["idUsuario"];
        StackLayout layout = new StackLayout();
        public ReservasCitasPage()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            MessagingCenter.Send<Object>(this, "HideOsNavigationBar");
            
            Content = layout;
            /*Content = new StackLayout
            {
                Children = {
                    new Header(),
                    new NewPet(),
                    new ListPets(user_id)
                }
            };*/
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
            layout.Children.Add(new ListPets(user_id));
            
        }
    }
}