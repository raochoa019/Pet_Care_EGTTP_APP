﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PRY_LENG_PROG.ReservaEstablecimiento
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UpdateDateReservation : ContentPage
    {
        public UpdateDateReservation()
        {
            InitializeComponent();
        }

        private void Actualizar_Clicked(object sender, EventArgs e)
        {

        }

        private void Return_Clicked(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }
    }
}