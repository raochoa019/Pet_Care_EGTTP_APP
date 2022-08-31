using PRY_LENG_PROG.Modelos;
using PRY_LENG_PROG.SugerenciasComentarios;
using PRY_LENG_PROG.Mascotas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PRY_LENG_PROG.ReservasCitas;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using PRY_LENG_PROG.ReservaEstablecimiento;

namespace PRY_LENG_PROG
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Menu : ContentPage
    {
        UserModel userAccount = new UserModel();
        public Menu(UserModel usuario)
        {
            this.userAccount = usuario;
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("NjU2MDY2QDMyMzAyZTMxMmUzMG9DL1A0cUFxU0kyd1BuZVNvRFpLNDFjditvdGU1ZUMwOXpKODhMazhCQzQ9");
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            MessagingCenter.Send<Object>(this, "HideOsNavigationBar");

            Application.Current.Properties["direccionDb"] = "http://127.0.0.1:8000";
            Application.Current.Properties["idUsuario"] = userAccount.id;
            Application.Current.Properties["nameUser"] = userAccount.name;


            List<Carrusel> images = new List<Carrusel>();
            images.Add(new Carrusel() { referencia = "https://www.centroveterinariobarbanza.es/cuidados-para-cachorros_img8860t1.jpg" });
            images.Add(new Carrusel() { referencia = "https://hvc.cat/wp-content/uploads/2020/12/3-infecciones-en-perros-y-que-hacer-en-cada-caso.jpg" });
            images.Add(new Carrusel() { referencia = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcREBGhuF-y6ctphzDQ9rvNF79CYYBB-_pl3U-KCsamCNZ5qrmNsNQD3x0WhdQRYK9cDGbE&usqp=CAU" });
            images.Add(new Carrusel() { referencia = "https://static3.elcorreo.com/www/multimedia/202201/14/media/cortadas/perreteweb-kva-U1605387236324pF-624x385@RC.jpg" });
            images.Add(new Carrusel() { referencia = "https://www.prensalibre.com/wp-content/uploads/2019/06/mascotas-preguntas-veterinario-perros-gatos-4.jpg?quality=52&w=760&h=430&crop=1" });
            
            crsOpcionesUsuario.ItemsSource = images;

            Device.StartTimer(TimeSpan.FromSeconds(4), (Func<bool>)(() =>
            {
                crsOpcionesUsuario.Position = (crsOpcionesUsuario.Position + 1) % images.Count;
                return true;
            }));
        }

        private void cita_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ReservasCitasPage());
        }

        private void mascota_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new frmPets(userAccount.id));
        }

        private void sugerencia_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Comentarios());
        }
    }
}