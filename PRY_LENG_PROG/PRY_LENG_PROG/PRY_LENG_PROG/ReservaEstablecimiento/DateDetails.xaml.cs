using PRY_LENG_PROG.components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PRY_LENG_PROG.ReservaEstablecimiento
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DateDetails : ContentPage
    {
        public DateDetails()
        {
            InitializeComponent();
            header.Children.Add(new Header());
            dateStart.MinimumDate = DateTime.Today;
        }

        private void Return_Clicked(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }

        private void Siguiente_Clicked(object sender, EventArgs e)
        {

        }
    }
}