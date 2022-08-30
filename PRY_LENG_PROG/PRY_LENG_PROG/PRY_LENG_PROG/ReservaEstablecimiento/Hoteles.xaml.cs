using PRY_LENG_PROG.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PRY_LENG_PROG
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Hoteles : ContentPage
    {
        List<HotelModel> hoteles = new List<HotelModel>();
        public Hoteles()
        {
            InitializeComponent();
            hoteles.Add(new HotelModel {nombreHotel = "Hotel de las Flores", direccion = "Av. El Rey 333 y Mul Mul" });
            hoteles.Add(new HotelModel { nombreHotel = "Hotel Oro Blue Hotel Resort", direccion = "Espejo y Av. Cevallos" });
            hoteles.Add(new HotelModel { nombreHotel = "Hostal Grand Hotel", direccion = "Rocafuerte y Lalama"});
            hoteles.Add(new HotelModel { nombreHotel = "Hotel Sangay", direccion = "	laza Isidro Ayora 100" });
            hoteles.Add(new HotelModel { nombreHotel = "Hotel Oro Verde", direccion = "Ordóñez Laso s/n" });
            hoteles.Add(new HotelModel { nombreHotel = "Hotel Las Américas", direccion = "Mariano Cueva 1359" });
            hoteles.Add(new HotelModel { nombreHotel = "Hotel Grand Hotel Guayaquil", direccion = "Boyacá y 10 de Agosto" });

        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            listView.ItemsSource = hoteles;
        }

        private void listView_ItemTapped(object sender, Syncfusion.ListView.XForms.ItemTappedEventArgs e)
        {
            HotelModel hotelSeleccionado = (HotelModel)e.ItemData;
            //DisplayAlert("msg", hotelSeleccionado.nombreHotel, "ok");
        }
    }
}