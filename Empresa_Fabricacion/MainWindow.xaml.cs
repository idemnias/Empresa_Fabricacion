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
        Empleado usuarioactivo = new Empleado();
        Fabricacion fabricacion = new Fabricacion();
        public MainWindow()
        {
            InitializeComponent();
            LimpiarGrids();
            LimpiarBotones();
        }

//Limpiar grids y botones
#region LIMPIAR GRIDS Y BOTONES
        private void LimpiarGrids()
        {
            grid_fabricacion.Visibility = Visibility.Hidden;
            grid_material.Visibility = Visibility.Hidden;
            grid_producto.Visibility = Visibility.Hidden;
            grid_cliente.Visibility = Visibility.Hidden;
            grid_proveedor.Visibility = Visibility.Hidden;
            grid_empleado.Visibility = Visibility.Hidden;
            grid_inicio.Visibility = Visibility.Hidden;
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
        }

        private void LimpiarInicio()
        {
            tb_usuario.Text = "";
            tb_contraseña.Password = "";
        }

/* Limpiar objetos */
        public void LimpiarEmpleados()
        {
            empleado = new Empleado();
            grid_empleado.DataContext = empleado;
            dg_empleado.ItemsSource = unit.RepositorioEmpleado.ObtenerTodo().ToList();
        }

        #endregion
    
//Activar o desactivar Botones
#region ACTIVARBOTONES
    public void ActivarBotonesEmpleado()
        {
            bt_e_añadir.Visibility = Visibility.Hidden;
            bt_e_modificar.Visibility = Visibility.Visible;
            bt_e_eliminar.Visibility = Visibility.Visible;
        }
        public void DesactivarBotonesEmpleado()
        {
            bt_e_añadir.Visibility = Visibility.Visible;
            bt_e_modificar.Visibility = Visibility.Hidden;
            bt_e_eliminar.Visibility = Visibility.Hidden;
        }
        #endregion

//Clicks Clases
#region CLICKS CLASES
        private void bt_inicio_Click(object sender, RoutedEventArgs e)
        {
            LimpiarGrids();
            grid_inicio.Visibility = Visibility.Visible;
            LimpiarBotones();
        }
        private void bt_fabricacion_Click(object sender, RoutedEventArgs e)
        {
            LimpiarGrids();
            grid_fabricacion.Visibility = Visibility.Visible;
        }

        private void bt_material_Click(object sender, RoutedEventArgs e)
        {
            LimpiarGrids();
            grid_material.Visibility = Visibility.Visible;
        }

        private void bt_producto_Click(object sender, RoutedEventArgs e)
        {
            LimpiarGrids();
            grid_producto.Visibility = Visibility.Visible;
        }

        private void bt_cliente_Click(object sender, RoutedEventArgs e)
        {
            LimpiarGrids();
            grid_cliente.Visibility = Visibility.Visible;
        }

        private void bt_proveedor_Click(object sender, RoutedEventArgs e)
        {
            LimpiarGrids();
            grid_proveedor.Visibility = Visibility.Visible;
        }

        private void bt_empleado_Click(object sender, RoutedEventArgs e)
        {
            LimpiarGrids();
            grid_empleado.Visibility = Visibility.Visible;
            grid_empleado.DataContext = empleado;
            dg_empleado.ItemsSource = unit.RepositorioEmpleado.ObtenerTodo().ToList();
        }

        #endregion

//Inicio
#region INICIO
        private void bt_acceder_Click(object sender, RoutedEventArgs e)
        {
            Empleado aux = unit.RepositorioEmpleado.ObtenerUno(c => c.Usuario.Equals(tb_usuario.Text));
            if (aux!=null)
            {
                if (aux.Contraseña.Equals(tb_contraseña.Password))
                {
                    PonerBotones();
                    if (aux.TipoCuenta.Equals("Administrador"))
                    {
                        bt_empleado.Visibility = Visibility.Visible;
                    }
                    usuarioactivo = aux;
                }
                else
                {
                    MessageBox.Show("Password incorrecto");
                }
            }
            else
            {
                MessageBox.Show("No hay este usuario");
            }
            LimpiarInicio();
        }


        #endregion

//Empleados
#region EMPLEADOS
        private void bt_e_añadir_Click(object sender, RoutedEventArgs e)
        {
            unit.RepositorioEmpleado.Crear(empleado);
            LimpiarEmpleados();
            DesactivarBotonesEmpleado();
        }

        private void bt_e_modificar_Click(object sender, RoutedEventArgs e)
        {
            unit.RepositorioEmpleado.Actualizar(empleado);
            LimpiarEmpleados();
            DesactivarBotonesEmpleado();
        }

        private void bt_e_nuevo_Click(object sender, RoutedEventArgs e)
        {
            empleado = new Empleado();
            grid_empleado.DataContext = empleado;
            DesactivarBotonesEmpleado();
        }

        private void bt_e_eliminar_Click(object sender, RoutedEventArgs e)
        {
            unit.RepositorioEmpleado.Eliminar(empleado);
            LimpiarEmpleados();
            DesactivarBotonesEmpleado();
        }

        private void dg_empleado_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            empleado = (Empleado) dg_empleado.SelectedItem;
            grid_empleado.DataContext = empleado;
            ActivarBotonesEmpleado();
        }
#endregion

    }
}
