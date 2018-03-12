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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Empresa_Fabricacion
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        UnitOfWork unit = new UnitOfWork();
        Empleado empleado = new Empleado();
        public MainWindow()
        {
            InitializeComponent();
            LimpiarGrids();
            LimpiarBotones();
            grid_empleado.DataContext = unit.RepositorioEmpleado.ObtenerTodo();
        }

        //limpiar grids y botones
#region LIMPIAR GRIDS Y BOTONES
        private void LimpiarGrids()
        {
            grid_fabricacion.Visibility = Visibility.Hidden;
            grid_material.Visibility = Visibility.Hidden;
            grid_producto.Visibility = Visibility.Hidden;
            grid_cliente.Visibility = Visibility.Hidden;
            grid_proveedor.Visibility = Visibility.Hidden;
            grid_empleado.Visibility = Visibility.Hidden;
        }
        private void LimpiarBotones()
        {
            bt_fabricacion.Visibility = Visibility.Hidden;
            bt_material.Visibility = Visibility.Hidden;
            bt_producto.Visibility = Visibility.Hidden;
            bt_cliente.Visibility = Visibility.Hidden;
            bt_proveedor.Visibility = Visibility.Hidden;
            bt_empleado.Visibility = Visibility.Hidden;
        }

        private void PonerBotones()
        {
            bt_fabricacion.Visibility = Visibility.Visible;
            bt_material.Visibility = Visibility.Visible;
            bt_producto.Visibility = Visibility.Visible;
            bt_cliente.Visibility = Visibility.Visible;
            bt_proveedor.Visibility = Visibility.Visible;
            bt_empleado.Visibility = Visibility.Visible;
        }

#endregion
        private void bt_inicio_Click(object sender, RoutedEventArgs e)
        {
            LimpiarGrids();
            LimpiarBotones();
        }

        private void bt_acceder_Click(object sender, RoutedEventArgs e)
        {
            Empleado aux = unit.RepositorioEmpleado.ObtenerUno(c => c.Usuario.Equals(tb_usuario.Text));
            if (aux!=null)
            {
                if (aux.Contraseña.Equals(tb_contraseña.Password))
                {

                }
            }
            else
            {
                MessageBox.Show("No hay este usuario");
            }
        }
    }
}
