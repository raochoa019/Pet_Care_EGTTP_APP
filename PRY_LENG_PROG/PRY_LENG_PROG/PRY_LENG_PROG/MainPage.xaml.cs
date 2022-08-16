using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PRY_LENG_PROG
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void boton_Prueba_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Page1());

        }
    }
}
