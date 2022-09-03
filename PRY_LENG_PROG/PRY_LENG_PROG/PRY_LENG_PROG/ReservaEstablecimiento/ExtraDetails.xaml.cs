using PRY_LENG_PROG.components;
using PRY_LENG_PROG.ReservaEstablecimiento.ModelosEstablecimiento;
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
    public partial class ExtraDetails : ContentPage
    {
        HotelReservation estadia = new HotelReservation();
        bool act = false;
        public ExtraDetails(HotelReservation lugar, bool actualizar)
        {
            InitializeComponent();
            header.Children.Add(new Header());
            estadia = lugar;
            act = actualizar;
            if (act)
            {
                alimentacion.Text = lugar.feeding;
                cuidados.Text = lugar.special_cares;
                ejercicios.Text = lugar.exercises;
                paseos.Text = lugar.rides;
            }
        }

        private void Return_Clicked(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }

        private void Siguiente_Clicked(object sender, EventArgs e)
        {
            estadia.feeding = alimentacion.Text;
            estadia.special_cares = cuidados.Text;
            estadia.exercises = ejercicios.Text;
            estadia.rides = paseos.Text;
            Navigation.PushAsync(new DateDetails(estadia,act));
        }
    }
}