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
    class ListaViewModel : BaseViewModel
    {

        public Command BorrarCommand { get; }
        public Command UpdateCommand { get; }

        Page Page;
        List<Pagos> listapagos;
        Pagos listapagosselected;
        Pagos _yourSelectedItem;

        public List<Pagos> ListaPagos
        {
            get => listapagos;
            set { SetProperty(ref listapagos, value); }
        }

        public Pagos ListaPagosSelected
        {
            get => listapagosselected;
            set { SetProperty(ref listapagosselected, value); }
        }

        public Pagos YourSelectedItem
        {
            get
            {
                return _yourSelectedItem;
            }

            set
            {
                _yourSelectedItem = value;
                OnPropertyChanged("YourSelectedItem");

            }
        }

        public ListaViewModel(Page pag)
        {
            Page = pag;

            //PuestoSelected = new Puestos();

            BorrarCommand = new Command(DeleteCommand);
            UpdateCommand = new Command(ActCommand);

            SQLiteConnection conexion = new SQLiteConnection(App.UbicacionDB);
            conexion.CreateTable<Pagos>();
            ListaPagos = conexion.Table<Pagos>().OrderByDescending(c => c.Fecha).ToList();



        }

        private async void DeleteCommand()
        {

            string x = Convert.ToString(_yourSelectedItem.id_pago);

            

            if (x != null)
            {
                Page.DisplayAlert("Aviso", "" + _yourSelectedItem.Descripcion + " ha sido eliminado de la lista de Pagos", "Ok");

                SQLiteConnection conexion = new SQLiteConnection(App.UbicacionDB);
                var borrarpersonas = conexion.Query<Pagos>($"Delete FROM Pagos WHERE id_pago = '" + x + "' ");
                conexion.Close();
            }
            else
            {
                Page.DisplayAlert("Aviso", "No ha seleccionado ningun elemento para borrar!", "Ok");
            }

            

        }

        private async void ActCommand()
        {

            string x = Convert.ToString(_yourSelectedItem.id_pago);



            if (x != null)
            {
                Page.DisplayAlert("Aviso", "" + _yourSelectedItem.Descripcion + " ha sido eliminado de la lista de personas", "Ok");

                var pago = new Pagos
                {
                    Descripcion = _yourSelectedItem.Descripcion,
                    Monto = _yourSelectedItem.Monto,
                    Fecha = _yourSelectedItem.Fecha,
                    Photo_recibo = _yourSelectedItem.Photo_recibo,
                    foto_ruta = _yourSelectedItem.foto_ruta


                };
                var datos = new MainPage();
                datos.BindingContext = pago;
                await Page.Navigation.PushAsync(datos);

            }
            else
            {
                Page.DisplayAlert("Aviso", "No ha seleccionado ningun elemento para Modificar!", "Ok");

                
            }

            
        }

    }
}

