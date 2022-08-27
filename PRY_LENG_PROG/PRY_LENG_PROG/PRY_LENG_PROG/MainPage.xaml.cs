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
            NavigationPage.SetHasNavigationBar(this, false);
            MessagingCenter.Send<Object>(this, "HideOsNavigationBar");

        }

        private void BtnEntrar_Clicked(object sender, EventArgs e)
        {

        }

        private void btnExit_Clicked(object sender, EventArgs e)
        {

        }
    }
}
