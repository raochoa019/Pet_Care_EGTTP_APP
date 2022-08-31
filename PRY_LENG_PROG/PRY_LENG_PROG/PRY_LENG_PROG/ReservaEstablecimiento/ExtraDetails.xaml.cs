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
        Dictionary<string, string> adicionales = new Dictionary<string, string>();
        HotelReservation estadia = new HotelReservation();
        
        public ExtraDetails(HotelReservation lugar)
        {
            InitializeComponent();
            header.Children.Add(new Header());
            estadia = lugar;
            if (adicionales.Count > 0)
            {
                alimentacion.Text = adicionales["alimentacion"];
                cuidados.Text = adicionales["cuidados"];
                ejercicios.Text = adicionales["ejercicios"];
                paseos.Text = adicionales["paseos"];
            }
        }

        private void Return_Clicked(object sender, EventArgs e)
        {
            adicionales.Clear();
            modifyInformacion();
            Navigation.PopAsync();
        }

        private void Siguiente_Clicked(object sender, EventArgs e)
        {
            modifyInformacion();
            estadia.feeding = alimentacion.Text;
            estadia.special_cares = cuidados.Text;
            estadia.exercises = ejercicios.Text;
            estadia.rides = paseos.Text;
            Navigation.PushAsync(new DateDetails(estadia));
        }
        
        private void modifyInformacion()
        {
            if (alimentacion.Text != null) 
                adicionales.Add("alimentacion", alimentacion.Text);
            if (cuidados.Text != null)
                adicionales.Add("cuidados", cuidados.Text);
            if (ejercicios.Text != null)
                adicionales.Add("ejercicios", ejercicios.Text);
            if (paseos.Text != null)
                adicionales.Add("paseos", paseos.Text);
        }
    }
}