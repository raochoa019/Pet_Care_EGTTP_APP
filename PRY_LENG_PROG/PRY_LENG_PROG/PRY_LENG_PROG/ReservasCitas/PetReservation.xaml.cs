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
        public PetReservation()
        {
            InitializeComponent();
            header.Children.Add(new Header());
            petInfo.Children.Add(new PetDetail());
        }
    }
}