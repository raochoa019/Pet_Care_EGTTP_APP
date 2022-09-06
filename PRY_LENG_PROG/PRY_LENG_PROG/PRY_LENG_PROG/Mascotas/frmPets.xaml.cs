using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PRY_LENG_PROG.components;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PRY_LENG_PROG.Mascotas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class frmPets : ContentPage
    {
        private int user_id;

        public frmPets(int id)
        {
            user_id = id;
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            MessagingCenter.Send<Object>(this, "HideOsNavigationBar");
            header.Children.Add(new Header());
            titlePage_addPet.Children.Add(new NewPet());
            showPets.Children.Add(new ListPets(user_id));
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
        }
    }
}