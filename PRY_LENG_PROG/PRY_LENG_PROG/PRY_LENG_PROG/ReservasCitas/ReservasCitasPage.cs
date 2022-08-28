﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PRY_LENG_PROG.components;

using Xamarin.Forms;

namespace PRY_LENG_PROG.ReservasCitas
{
    public class ReservasCitasPage : ContentPage
    {
        public ReservasCitasPage()
        {
            Content = new StackLayout
            {
                Children = {
                    new Header(),
                    new NewPet(),
                    new Pet("Lupita","perro.jpg")
                }
            };
        }
    }
}