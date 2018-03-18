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
        Proveedor proveedor = new Proveedor();
        Cliente cliente = new Cliente();
        int clienteseleccionado;
        List<Cliente> listaclientes = new List<Cliente>();
        Producto producto = new Producto();

        string rutainicial = Environment.CurrentDirectory + @"\Imagenes\";

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
        private void LimpiarEmpleados()
        {
            empleado = new Empleado();
            grid_empleado.DataContext = empleado;
            dg_empleado.ItemsSource = unit.RepositorioEmpleado.ObtenerTodo().ToList();
        }
        private void LimpiarProveedores()
        {
            proveedor = new Proveedor();
            grid_proveedor.DataContext = proveedor;
            dg_proveedor.ItemsSource = unit.RepositorioProveedor.ObtenerTodo().ToList();
        }
        private void LimpiarCliente()
        {
            cliente = new Cliente();
            grid_cliente.DataContext = cliente;
            dg_cliente.ItemsSource = unit.RepositorioCliente.ObtenerTodo().ToList();
        }

        private void LimpiarProductos()
        {
            producto = new Producto();
            grid_producto.DataContext = producto;

        }

        #endregion

//Activar o desactivar Botones
#region ACTIVARBOTONES
        private void ActivarBotonesEmpleado()
        {
            bt_e_añadir.Visibility = Visibility.Hidden;
            bt_e_modificar.Visibility = Visibility.Visible;
            bt_e_eliminar.Visibility = Visibility.Visible;
        }
        private void DesactivarBotonesEmpleado()
        {
            bt_e_añadir.Visibility = Visibility.Visible;
            bt_e_modificar.Visibility = Visibility.Hidden;
            bt_e_eliminar.Visibility = Visibility.Hidden;
        }
        private void ActivarBotonesProveedor()
        {
            bt_p_añadir.Visibility = Visibility.Hidden;
            bt_p_modificar.Visibility = Visibility.Visible;
            bt_p_eliminar.Visibility = Visibility.Visible;
        }
        private void DesactivarBotonesProveedor()
        {
            bt_p_añadir.Visibility = Visibility.Visible;
            bt_p_modificar.Visibility = Visibility.Hidden;
            bt_p_eliminar.Visibility = Visibility.Hidden;
        }
        private void ActivarBotonesCliente()
        {
            bt_c_añadir.Visibility = Visibility.Hidden;
            bt_c_modificar.Visibility = Visibility.Visible;
            bt_c_eliminar.Visibility = Visibility.Visible;
        }
        private void DesactivarBotonesCliente()
        {
            bt_c_añadir.Visibility = Visibility.Visible;
            bt_c_modificar.Visibility = Visibility.Hidden;
            bt_c_eliminar.Visibility = Visibility.Hidden;
        }

        private void ActivarProductosCliente()
        {
            lb_pr_seleccionar.Visibility = Visibility.Hidden;
            cb_pr_cliente.Visibility = Visibility.Hidden;
            bt_pr_seleccionar.Visibility = Visibility.Hidden;
            lb1.Visibility = Visibility.Visible;
            lb2.Visibility = Visibility.Visible;
            lb3.Visibility = Visibility.Visible;
            lb4.Visibility = Visibility.Visible;
            lb5.Visibility = Visibility.Visible;
            tb_pr_nombre.Visibility = Visibility.Visible;
            tb_pr_precio.Visibility = Visibility.Visible;
            tb_pr_fechaventa.Visibility = Visibility.Visible;
            cb_pr_vendido.Visibility = Visibility.Visible;
            tb_pr_Descripcion.Visibility = Visibility.Visible;
            dg_producto.Visibility = Visibility.Visible;
            bt_pr_añadir.Visibility = Visibility.Visible;
            bt_pr_modificar.Visibility = Visibility.Visible;
            bt_pr_eliminar.Visibility = Visibility.Visible;
            bt_pr_nuevo.Visibility = Visibility.Visible;

        }
        private void DesactivarProductosCliente()
        {
            lb_pr_seleccionar.Visibility = Visibility.Visible;
            cb_pr_cliente.Visibility = Visibility.Visible;
            bt_pr_seleccionar.Visibility = Visibility.Visible;
            lb1.Visibility = Visibility.Hidden;
            lb2.Visibility = Visibility.Hidden;
            lb3.Visibility = Visibility.Hidden;
            lb4.Visibility = Visibility.Hidden;
            lb5.Visibility = Visibility.Hidden;
            tb_pr_nombre.Visibility = Visibility.Hidden;
            tb_pr_precio.Visibility = Visibility.Hidden;
            tb_pr_fechaventa.Visibility = Visibility.Hidden;
            cb_pr_vendido.Visibility = Visibility.Hidden;
            tb_pr_Descripcion.Visibility = Visibility.Hidden;
            dg_producto.Visibility = Visibility.Hidden;
            bt_pr_añadir.Visibility = Visibility.Hidden;
            bt_pr_modificar.Visibility = Visibility.Hidden;
            bt_pr_eliminar.Visibility = Visibility.Hidden;
            bt_pr_nuevo.Visibility = Visibility.Hidden;
        }
        private void ActivarBotonesProductos()
        {
            bt_pr_añadir.Visibility = Visibility.Hidden;
            bt_pr_modificar.Visibility = Visibility.Visible;
            bt_pr_eliminar.Visibility = Visibility.Visible;
        }
        private void DesactivarBotonesProductos()
        {
            bt_pr_añadir.Visibility = Visibility.Visible;
            bt_pr_modificar.Visibility = Visibility.Hidden;
            bt_pr_eliminar.Visibility = Visibility.Hidden;
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
            DesactivarProductosCliente();
            RellenarComboboxClientes();
        }

        private void bt_cliente_Click(object sender, RoutedEventArgs e)
        {
            LimpiarGrids();
            grid_cliente.Visibility = Visibility.Visible;
            grid_cliente.DataContext = cliente;
            dg_cliente.ItemsSource = unit.RepositorioCliente.ObtenerTodo().ToList();
            DesactivarBotonesCliente();
        }

        private void bt_proveedor_Click(object sender, RoutedEventArgs e)
        {
            LimpiarGrids();
            grid_proveedor.Visibility = Visibility.Visible;
            grid_proveedor.DataContext = proveedor;
            dg_proveedor.ItemsSource = unit.RepositorioProveedor.ObtenerTodo().ToList();
            DesactivarBotonesEmpleado();
        }

        private void bt_empleado_Click(object sender, RoutedEventArgs e)
        {
            LimpiarGrids();
            grid_empleado.Visibility = Visibility.Visible;
            grid_empleado.DataContext = empleado;
            dg_empleado.ItemsSource = unit.RepositorioEmpleado.ObtenerTodo().ToList();
            DesactivarBotonesEmpleado();
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
                        MessageBox.Show("Sesion iniciado correctamente con la cuenta de "+aux.Nombre +" "+aux.Apellidos);
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

        private Image EnseñarImagen(string ruta)
        {
            try
            {
                Image imagen = new Image();
                BitmapImage bit = new BitmapImage();
                bit.BeginInit();
                bit.UriSource = new Uri(ruta);
                bit.EndInit();
                imagen.Source = bit;
                imagen.Width = 60;
                imagen.Height = 60;
                return imagen;
            }
            catch (Exception)
            {
                return null;
            }

        }

        private StackPanel CreacionBotones(string rutaimagen, string nombrelabel)
        {
            StackPanel stackp = new StackPanel();
            stackp.VerticalAlignment = VerticalAlignment.Center;
            stackp.HorizontalAlignment = HorizontalAlignment.Stretch;
            stackp.Orientation = Orientation.Vertical;
            stackp.Width = 80;
            stackp.Height = 80;

            Label l = new Label();
            l.Content = nombrelabel;
            l.HorizontalAlignment = HorizontalAlignment.Center;
            l.FontSize = 10;

            stackp.Children.Add(EnseñarImagen(rutaimagen));
            stackp.Children.Add(l);
            return stackp;
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
            try
            {
                empleado = (Empleado)dg_empleado.SelectedItem;
                grid_empleado.DataContext = empleado;
                ActivarBotonesEmpleado();
            }
            catch (Exception)
            {
            }
            
        }
        #endregion

//Proveedores
#region PROVEEDORES

       

        private void bt_p_nuevo_Click(object sender, RoutedEventArgs e)
        {
            proveedor = new Proveedor();
            grid_proveedor.DataContext = proveedor;
            DesactivarBotonesProveedor();
        }

        private void bt_p_añadir_Click(object sender, RoutedEventArgs e)
        {
            unit.RepositorioProveedor.Crear(proveedor);
            LimpiarProveedores();
            DesactivarBotonesProveedor();
        }

        private void bt_p_modificar_Click(object sender, RoutedEventArgs e)
        {
            unit.RepositorioProveedor.Actualizar(proveedor);
            LimpiarProveedores();
            DesactivarBotonesProveedor();
        }

        private void bt_p_eliminar_Click(object sender, RoutedEventArgs e)
        {
            unit.RepositorioProveedor.Eliminar(proveedor);
            LimpiarProveedores();
            DesactivarBotonesProveedor();
        }

        private void dg_proveedor_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                proveedor = (Proveedor)dg_proveedor.SelectedItem;
                grid_proveedor.DataContext = proveedor;
                ActivarBotonesProveedor();
            }
            catch (Exception)
            {
            }
        }

        #endregion

//Clientes
#region CLIENTES

        private void bt_c_nuevo_Click(object sender, RoutedEventArgs e)
        {
            cliente = new Cliente();
            grid_cliente.DataContext = cliente;
            DesactivarBotonesCliente();
        }

        private void bt_c_añadir_Click(object sender, RoutedEventArgs e)
        {
            unit.RepositorioCliente.Crear(cliente);
            LimpiarCliente();
            DesactivarBotonesCliente();
        }

        private void bt_c_modificar_Click(object sender, RoutedEventArgs e)
        {
            unit.RepositorioCliente.Actualizar(cliente);
            LimpiarCliente();
            DesactivarBotonesCliente();
        }

        private void bt_c_eliminar_Click(object sender, RoutedEventArgs e)
        {
            unit.RepositorioCliente.Eliminar(cliente);
            LimpiarCliente();
            DesactivarBotonesCliente();
        }

        private void dg_cliente_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                cliente = (Cliente)dg_cliente.SelectedItem;
                grid_cliente.DataContext = cliente;
                ActivarBotonesCliente();
            }
            catch (Exception)
            {
            }
        }
        #endregion

//Productos
#region PRODUCTOS

        private void RellenarComboboxClientes()
        {
            listaclientes = new List<Cliente>();
            listaclientes = unit.RepositorioCliente.ObtenerTodo();
            foreach (var item in listaclientes)
            {
                cb_pr_cliente.Items.Add(item.Nombre);
            }
        }

        private void bt_p_seleccionar_Click(object sender, RoutedEventArgs e)
        {
            cliente = listaclientes[clienteseleccionado];
            int numeroid = listaclientes[clienteseleccionado].ClienteId;
            cliente = unit.RepositorioCliente.ObtenerUno(c=>c.ClienteId == cliente.ClienteId);
            LimpiarProductos();
            DesactivarBotonesProductos();
            ActivarProductosCliente();
            grid_producto.DataContext = producto;
            dg_producto.ItemsSource = unit.RepositorioProducto.ObtenerVarios(c=>c.ClienteId == cliente.ClienteId).ToList();
        }

        private void cb_p_cliente_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            clienteseleccionado = cb_pr_cliente.SelectedIndex;
        }

        private void bt_pr_nuevo_Click(object sender, RoutedEventArgs e)
        {
            producto = new Producto();
            grid_producto.DataContext = producto;
            DesactivarProductosCliente();
            DesactivarBotonesProductos();
        }

        private void bt_pr_añadir_Click(object sender, RoutedEventArgs e)
        {
            unit.RepositorioProducto.Crear(producto);
            LimpiarProductos();
            DesactivarBotonesProductos();
        }

        private void bt_pr_modificar_Click(object sender, RoutedEventArgs e)
        {
            unit.RepositorioProducto.Actualizar(producto);
            LimpiarProductos();
            DesactivarBotonesProductos();
        }

        private void bt_pr_eliminar_Click(object sender, RoutedEventArgs e)
        {
            unit.RepositorioProducto.Eliminar(producto);
            LimpiarProductos();
            DesactivarBotonesProductos();
        }

        private void dg_producto_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                producto = (Producto)dg_producto.SelectedItem;
                grid_producto.DataContext = producto;
                ActivarBotonesProductos();
            }
            catch (Exception)
            {
            }
        }

        private void cb_p_vendido_Checked(object sender, RoutedEventArgs e)
        {
            producto.Vendido = (bool) cb_pr_vendido.IsChecked;
        }


        #endregion

    }
}
