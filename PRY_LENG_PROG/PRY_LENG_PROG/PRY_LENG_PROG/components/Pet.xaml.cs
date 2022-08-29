using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PRY_LENG_PROG.ReservasCitas;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PRY_LENG_PROG.components
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Pet : ContentView
    {
        private int id;
        public Pet(int _id, string name, string image)
        {
            id = _id;
            InitializeComponent();
            petName.Text = name;
            petImage.Source = image;
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new PetReservation(id));
        }
    }
}