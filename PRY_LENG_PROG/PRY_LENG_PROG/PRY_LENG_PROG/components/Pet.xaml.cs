using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PRY_LENG_PROG.ReservasCitas;
using PRY_LENG_PROG.Modelos;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PRY_LENG_PROG.components
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Pet : ContentView
    {
        private PetModel pet = new PetModel();
        public Pet(PetModel pet, string image)
        {
            this.pet = pet;   
            InitializeComponent();
            petName.Text = pet.name;
            petImage.Source = image;
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new PetReservation(this.pet.id));
        }
    }
}