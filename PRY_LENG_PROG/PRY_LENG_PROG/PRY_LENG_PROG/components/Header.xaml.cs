using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PRY_LENG_PROG.components
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Header : ContentView
    {
        public Header()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            MessagingCenter.Send<Object>(this, "HideOsNavigationBar");
        }
    }
}