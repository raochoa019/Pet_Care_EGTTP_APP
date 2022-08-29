using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PRY_LENG_PROG.components;


using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PRY_LENG_PROG.ReservasCitas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PetReservation : ContentPage
    {
        private int pet_id;
        public PetReservation(int id)
        {
            pet_id = id;
            Console.WriteLine(id);
            InitializeComponent();
            header.Children.Add(new Header());
            petInfo.Children.Add(new PetDetail());
        }

        void agendarCita(object sender, EventArgs e) {
            Navigation.PushAsync(new ChooseVet(pet_id));
        }
    }
}