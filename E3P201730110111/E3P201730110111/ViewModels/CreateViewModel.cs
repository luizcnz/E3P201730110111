using System;
using System.Collections.Generic;
using System.Text;

using System.Threading.Tasks;
using Xamarin.Forms;
using Plugin.Media;
using System.IO;
using E3P201730110111.Models;
using SQLite;

namespace E3P201730110111.ViewModels
{
    class CreateViewModel : BaseViewModel
    {
        public Command ListaCommand { get; }        
        public Command TomarFotoCommand { get; }
        public Command EnviarCommand { get; }


        Page Page;

        int id;
        string desc;
        int monto;
        DateTime fecha;
        byte[] foto;
        string imagen;


        public byte[] Foto { get => foto; set { foto = value; } }

        public int ID
        {
            get => id;
            set { SetProperty(ref id, value); }
        }

        public string Desc
        {
            get => desc;
            set { SetProperty(ref desc, value); }
        }

        public string Imagen
        {
            get => imagen;
            set { SetProperty(ref imagen, value); }
        }

        public int Monto
        {
            get => monto;
            set { SetProperty(ref monto, value); }
        }
        public DateTime Fecha
        {
            get => fecha;
            set { SetProperty(ref fecha, value); }
        }


        public CreateViewModel(Page pag)
        {
            Page = pag;

            //PuestoSelected = new Puestos();

            Cargar();

            EnviarCommand = new Command(OnSaveClicked);
            TomarFotoCommand = new Command(TomarPhoto);
            ListaCommand = new Command(VerListaCommand);
        }

        private async Task Cargar()
        {
            //Cargo = Puestos.ObtenerCargos().OrderBy(c => c.value).ToList();

        }


        public async void OnSaveClicked(object obj)
        {

            if (Foto == null || string.IsNullOrEmpty(Desc) || Monto == 0 || Fecha == null)
            {
                await Page.DisplayAlert("Mensaje", "No deben haber campos vacíos", "Ok");
            }
            else
            {
                await Page.DisplayAlert("Mensaje", "Los Datos se han capturado", "Ok");
                if (ID==null)
                {
                    Int32 resultado = 0;
                    var pago = new Pagos
                    {
                        Descripcion = Desc,
                        Monto = Monto,
                        Fecha = Fecha,
                        Photo_recibo = Foto,
                        foto_ruta = Imagen
                    };

                    using (SQLiteConnection conexion = new SQLiteConnection(App.UbicacionDB))
                    {
                        conexion.CreateTable<Pagos>();
                        resultado = conexion.Insert(pago);

                        if (resultado > 0)
                            Page.DisplayAlert("Aviso", "Adicionado", "Ok");
                        else
                            Page.DisplayAlert("Aviso", "Error", "Ok");
                    }
                }
                else
                {
                    

                    SQLiteConnection conexion = new SQLiteConnection(App.UbicacionDB);
                    var update = conexion.Query<Pagos>($"Update Localizacion SET Descripcion = '" + Desc + "', Monto = '" + Monto + "', Fecha = '" + Fecha + "',Photo_recibo = '" + Desc + "', foto_ruta = '" + Desc + "' WHERE id_pago = '" + ID + "'");
                    conexion.Close();

                    Page.DisplayAlert("Aviso", "Se ha Actualizado Con Exito", "Ok");
                }
                
            }

        }

        public async void VerListaCommand(object obj)
        {
            await Page.Navigation.PushAsync(new ListaPagos());
        }

        private async void TomarPhoto()

        {
            var fotoTomada = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions());

            if (fotoTomada != null)
            {
                //variable utilizada para almacenar la foto tomada en formado 
                Foto = null;
                MemoryStream memoryStream = new MemoryStream();// creamos un flujo de memoria temporal

                fotoTomada.GetStream().CopyTo(memoryStream);
                Foto = memoryStream.ToArray();

                // se muestra la imagen pero aun no se guarda
                Imagen = fotoTomada.Path;
            }
        }
    }
}

