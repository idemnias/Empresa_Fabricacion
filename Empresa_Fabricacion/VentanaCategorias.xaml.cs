using Empresa_Fabricacion.DAL;
using Empresa_Fabricacion.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Empresa_Fabricacion
{
    /// <summary>
    /// Lógica de interacción para VentanaCategorias.xaml
    /// </summary>
    public partial class VentanaCategorias : Window
    {
        MainWindow mainWindow;
        UnitOfWork unit = new UnitOfWork();
        Categoria categoria = new Categoria();
        List<Categoria> listacategorias = new List<Categoria>();
        public VentanaCategorias(MainWindow main)
        {
            InitializeComponent();
            DesactivarBotonesCategorias();
            ObtenerListaCategorias();
            mainWindow = main;
        }

        //limpiar grid de objeto categoria
        private void LimpiarGridCategorias()
        {
            categoria = new Categoria();
            grid_categorias.DataContext = categoria;
            DesactivarBotonesCategorias();
        }

        //obtener lista de categorias
        private void ObtenerListaCategorias()
        {
            listacategorias = unit.RepositorioCategoria.ObtenerTodo();
            lb_categorias.Items.Clear();
            foreach (var item in listacategorias)
            {
                lb_categorias.Items.Add(item.Nombre);
            }
            
        }

        //activar botones modificar y eliminar y desactivar añadir
        private void ActivarBotonesCategorias()
        {
            bt_c_añadir.Visibility = Visibility.Hidden;
            bt_c_modificar.Visibility = Visibility.Visible;
            bt_c_eliminar.Visibility = Visibility.Visible;
        }

        //desactivar botones modificar y eliminar y activar añadir
        private void DesactivarBotonesCategorias()
        {
            bt_c_añadir.Visibility = Visibility.Visible;
            bt_c_modificar.Visibility = Visibility.Hidden;
            bt_c_eliminar.Visibility = Visibility.Hidden;
        }

        //nueva categoria
        private void bt_c_nuevo_Click(object sender, RoutedEventArgs e)
        {
            LimpiarGridCategorias();
        }

        //añadir categoria
        private void bt_c_añadir_Click(object sender, RoutedEventArgs e)
        {
            unit.RepositorioCategoria.Crear(categoria);
            LimpiarGridCategorias();
            ObtenerListaCategorias();
            MessageBox.Show("Categoría nueva añadida");
        }

        //modificar categoria
        private void bt_c_modificar_Click(object sender, RoutedEventArgs e)
        {
            unit.RepositorioCategoria.Actualizar(categoria);
            ObtenerListaCategorias();
            LimpiarGridCategorias();
            DesactivarBotonesCategorias();
            MessageBox.Show("Categoría modificada");
        }

        //eliminar categoria
        private void bt_c_eliminar_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("¿Desea eliminar la categoría?", "Cancelar", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                unit.RepositorioCategoria.Eliminar(categoria);
            ObtenerListaCategorias();
            LimpiarGridCategorias();
            DesactivarBotonesCategorias();
            }
            else { MessageBox.Show("Eliminación de categoría cancelada"); }
        }

        //clic en la lista de categorias
        private void lb_categorias_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            categoria = new Categoria();
            categoria = unit.RepositorioCategoria.ObtenerUno(c => c.CategoriaId == lb_categorias.SelectedIndex+1);
            grid_categorias.DataContext = categoria;
            ActivarBotonesCategorias();
        }

        //cuando se cierra la ventana
        private void Window_Closed(object sender, EventArgs e)
        {
            mainWindow.Metodo_Gestion();
        }
    }
}
