using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PRY_LENG_PROG.Modelos;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PRY_LENG_PROG.components
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListPets : ContentView
    {
        List<PetModel> pets;
        IEnumerable<PetModel> petsSource = new List<PetModel>();

        public ListPets(List<PetModel> pets)
        {
            this.pets = pets;
            petsSource = this.pets;
            InitializeComponent();
            ShowPets();
        }

        void ShowPets()
        {
            PanelPets.Children.Clear();
            foreach(var pet in petsSource)
            {
                String defaultImage = "perro.jpg";
                if (pet.species.Equals("Gato")) {
                    defaultImage = "gato.jpg";
                } 

                PanelPets.Children.Add(new Pet(pet, defaultImage));
            }
        }

        private void searchPet_TextChanged(object sender, TextChangedEventArgs e)
        {
            petsSource = pets.Where(pet => pet.name.ToLower().StartsWith(e.NewTextValue.ToLower()));
            ShowPets();
        }
    }
}