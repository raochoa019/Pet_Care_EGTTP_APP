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
    public partial class ExtraDetails : ContentPage
    {
        Dictionary<string, string> adicionales;
        public ExtraDetails()
        {
            InitializeComponent();
            header.Children.Add(new Header());
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
            modifyInformacion();
            Navigation.PopAsync();
        }

        private void Siguiente_Clicked(object sender, EventArgs e)
        {
            modifyInformacion();
            Navigation.PushAsync(new DateDetails());
        }
        
        private void modifyInformacion()
        {
            adicionales.Add("alimentacion", alimentacion.Text);
            adicionales.Add("cuidados", cuidados.Text);
            adicionales.Add("ejercicios", ejercicios.Text);
            adicionales.Add("paseos", paseos.Text);
        }
    }
}