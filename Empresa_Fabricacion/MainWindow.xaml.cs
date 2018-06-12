using Empresa_Fabricacion.DAL;
using Empresa_Fabricacion.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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
        Material material = new Material();
        int clienteseleccionado;
        int productoseleccionado;
        int fabricacioneseleccionado=1;
        List<Cliente> listaclientes = new List<Cliente>();
        List<Proveedor> listaproveedores = new List<Proveedor>();
        Producto producto = new Producto();
        List<Producto> listaproductos = new List<Producto>();
        List<Material> listamateriales = new List<Material>();
        List<Fabricacion> listafabricaciones = new List<Fabricacion>();

        string rutainicial = Environment.CurrentDirectory + @"\Imagenes\";

        public MainWindow()
        {
            InitializeComponent();
            LimpiarGrids();
            LimpiarBotones();
            grid_inicio.Visibility = Visibility.Visible;
            bt_inicio.Content = CreacionBotones(rutainicial + "ICONO.png", "INICIO",80);
            bt_cliente.Content = CreacionBotones(rutainicial + "cliente.png", "CLIENTE",80);
            bt_material.Content = CreacionBotones(rutainicial + "Material.png", "MATERIAL",80);
            bt_fabricacion.Content = CreacionBotones(rutainicial + "fabricacion.png", "FABRICACION",80);
            bt_empleado.Content = CreacionBotones(rutainicial + "empleado.png", "EMPLEADO",80);
            bt_producto.Content = CreacionBotones(rutainicial + "producto.png", "PRODUCTO",80);
            bt_proveedor.Content = CreacionBotones(rutainicial + "proveedor.png", "PROVEEDOR",80);
            bt_m_gestion.Content = CreacionBotones(rutainicial + "materialgestion.png", "MATERIAL GESTION",110);
            bt_m_utilizacion.Content = CreacionBotones(rutainicial + "materialutilizado.png", "UTILIZAR MATERIAL",110);
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
            grid_material_gestion.Visibility = Visibility.Hidden;
            grid_material_utilizado.Visibility = Visibility.Hidden;
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
            tb_pr_fechaventa.Text = "";
            dg_producto.ItemsSource = unit.RepositorioProducto.ObtenerTodo().ToList();
        }

        private void LimpiarFabricacion()
        {
            fabricacion = new Fabricacion();
            grid_fabricacion.DataContext = fabricacion;
            tb_f_fechainicio.Text = "";
            tb_f_fechafinal.Text = "";
            dg_fabricacion.ItemsSource = unit.RepositorioFabricacion.ObtenerTodo().ToList();
        }
        private void LimpiarMaterial()
        {
            material = new Material();
            grid_material_gestion.DataContext = material;
            BitmapImage bit = new BitmapImage();
            imagen_materiales.Source = bit;
            RellenarComboboxProveedores();
            dg_material.ItemsSource = unit.RepositorioMaterial.ObtenerTodo().ToList();
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
            ug_producto.Visibility = Visibility.Visible;
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
            ug_producto.Visibility = Visibility.Hidden;
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

        private void ActivarFabricacionProducto()
        {
            lb_f_seleccionar.Visibility = Visibility.Hidden;
            cb_f_producto.Visibility = Visibility.Hidden;
            bt_f_seleccionar.Visibility = Visibility.Hidden;
            lb1f.Visibility = Visibility.Visible;
            lb2f.Visibility = Visibility.Visible;
            lb3f.Visibility = Visibility.Visible;
            ug_fabricacion.Visibility = Visibility.Visible;
            cb_f_fabricado.Visibility = Visibility.Visible;
            tb_f_fechainicio.Visibility = Visibility.Visible;
            tb_f_fechafinal.Visibility = Visibility.Visible;
            dg_fabricacion.Visibility = Visibility.Visible;
            bt_f_añadir.Visibility = Visibility.Visible;
            bt_f_modificar.Visibility = Visibility.Visible;
            bt_f_eliminar.Visibility = Visibility.Visible;
            bt_f_nuevo.Visibility = Visibility.Visible;

        }
        private void DesactivarFabricacionProducto()
        {
            lb_f_seleccionar.Visibility = Visibility.Visible;
            cb_f_producto.Visibility = Visibility.Visible;
            bt_f_seleccionar.Visibility = Visibility.Visible;
            lb1f.Visibility = Visibility.Hidden;
            lb2f.Visibility = Visibility.Hidden;
            lb3f.Visibility = Visibility.Hidden;
            ug_fabricacion.Visibility = Visibility.Hidden;
            cb_f_fabricado.Visibility = Visibility.Hidden;
            tb_f_fechainicio.Visibility = Visibility.Hidden;
            tb_f_fechafinal.Visibility = Visibility.Hidden;
            dg_fabricacion.Visibility = Visibility.Hidden;
            bt_f_añadir.Visibility = Visibility.Hidden;
            bt_f_modificar.Visibility = Visibility.Hidden;
            bt_f_eliminar.Visibility = Visibility.Hidden;
            bt_f_nuevo.Visibility = Visibility.Hidden;
        }

        private void ActivarBotonesFabricacion()
        {
            bt_f_añadir.Visibility = Visibility.Hidden;
            bt_f_modificar.Visibility = Visibility.Visible;
            bt_f_eliminar.Visibility = Visibility.Visible;
        }
        private void DesactivarBotonesFabricacion()
        {
            bt_f_añadir.Visibility = Visibility.Visible;
            bt_f_modificar.Visibility = Visibility.Hidden;
            bt_f_eliminar.Visibility = Visibility.Hidden;
        }

        private void ActivarBotonesMaterial()
        {
            bt_m_añadir.Visibility = Visibility.Hidden;
            bt_m_modificar.Visibility = Visibility.Visible;
            bt_m_eliminar.Visibility = Visibility.Visible;
        }
        private void DesactivarBotonesMaterial()
        {
            bt_m_añadir.Visibility = Visibility.Visible;
            bt_m_modificar.Visibility = Visibility.Hidden;
            bt_m_eliminar.Visibility = Visibility.Hidden;
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
            LimpiarFabricacion();
            DesactivarFabricacionProducto();
            RellenarComboboxFabricacion();
        }

        private void bt_material_Click(object sender, RoutedEventArgs e)
        {
            LimpiarGrids();
            grid_material .Visibility = Visibility.Visible;
        }

        private void bt_producto_Click(object sender, RoutedEventArgs e)
        {
            LimpiarGrids();
            grid_producto.Visibility = Visibility.Visible;
            LimpiarProductos();
            DesactivarProductosCliente();
            RellenarComboboxClientes();
        }

        private void bt_cliente_Click(object sender, RoutedEventArgs e)
        {
            LimpiarGrids();
            grid_cliente.Visibility = Visibility.Visible;
            LimpiarCliente();
            grid_cliente.DataContext = cliente;
            dg_cliente.ItemsSource = unit.RepositorioCliente.ObtenerTodo().ToList();
            DesactivarBotonesCliente();
        }

        private void bt_proveedor_Click(object sender, RoutedEventArgs e)
        {
            LimpiarGrids();
            grid_proveedor.Visibility = Visibility.Visible;
            LimpiarProveedores();
            grid_proveedor.DataContext = proveedor;
            dg_proveedor.ItemsSource = unit.RepositorioProveedor.ObtenerTodo().ToList();
            DesactivarBotonesEmpleado();
        }

        private void bt_empleado_Click(object sender, RoutedEventArgs e)
        {
            LimpiarGrids();
            grid_empleado.Visibility = Visibility.Visible;
            LimpiarEmpleados();
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
                    }
                    else if (aux.TipoCuenta.Equals("Trabajador"))
                    {
                        bt_empleado.Visibility = Visibility.Collapsed;
                        bt_cliente.Visibility = Visibility.Collapsed;
                    }
                    else
                    {
                        bt_empleado.Visibility = Visibility.Collapsed;
                        bt_fabricacion.Visibility = Visibility.Collapsed;
                        bt_proveedor.Visibility = Visibility.Collapsed;
                    }
                    MessageBox.Show("Sesion iniciado correctamente con la cuenta de " + aux.Nombre + " " + aux.Apellidos);
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

        private Image EnseñarImagen(string ruta, int peso)
        {
            try
            {
                Image imagen = new Image();
                BitmapImage bit = new BitmapImage();
                bit.BeginInit();
                bit.UriSource = new Uri(ruta);
                bit.EndInit();
                imagen.Source = bit;
                imagen.Width = peso;
                imagen.Height = peso;
                return imagen;
            }
            catch (Exception)
            {
                return null;
            }

        }

        private StackPanel CreacionBotones(string rutaimagen, string nombrelabel, int peso)
        {
            try
            {
                StackPanel stackp = new StackPanel();
                stackp.VerticalAlignment = VerticalAlignment.Center;
                stackp.HorizontalAlignment = HorizontalAlignment.Stretch;
                stackp.Orientation = Orientation.Vertical;
                stackp.Width = peso;
                stackp.Height = peso;

                Label l = new Label();
                l.Content = nombrelabel;
                l.HorizontalAlignment = HorizontalAlignment.Center;
                l.FontSize = 11;

                stackp.Children.Add(EnseñarImagen(rutaimagen, peso - 20));
                stackp.Children.Add(l);
                return stackp;
            }
            catch (Exception)
            {
                return null;
            }
            
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
            cb_pr_cliente.Items.Clear();
            listaclientes = new List<Cliente>();
            listaclientes = unit.RepositorioCliente.ObtenerTodo();
            foreach (var item in listaclientes)
            {
                cb_pr_cliente.Items.Add(item.Nombre + " "+item.Apellidos);
            }
            if (cb_pr_cliente != null) cb_pr_cliente.SelectedIndex = 0;
        }

        private void bt_p_seleccionar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                cliente = listaclientes[clienteseleccionado];
                int numeroid = listaclientes[clienteseleccionado].ClienteId;
                cliente = unit.RepositorioCliente.ObtenerUno(c => c.ClienteId == cliente.ClienteId);
                LimpiarProductos();
                ActivarProductosCliente();
                DesactivarBotonesProductos();
                grid_producto.DataContext = producto;
                dg_producto.ItemsSource = unit.RepositorioProducto.ObtenerVarios(c => c.ClienteId == cliente.ClienteId).ToList();
            }
            catch (Exception)
            {

            }
            
        }

        private void cb_p_cliente_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            clienteseleccionado = cb_pr_cliente.SelectedIndex;
        }

        private void bt_pr_nuevo_Click(object sender, RoutedEventArgs e)
        {
            producto = new Producto();
            tb_pr_fechaventa.Text = "";
            grid_producto.DataContext = producto;
            DesactivarBotonesProductos();
        }

        private void bt_pr_añadir_Click(object sender, RoutedEventArgs e)
        {
            producto.ClienteId = cliente.ClienteId;
            if (tb_pr_fechaventa.Text == "") { producto.FechaVenta = DateTime.Today; }
            else { producto.FechaVenta = Convert.ToDateTime(tb_pr_fechaventa.Text); }            
            unit.RepositorioProducto.Crear(producto);
            LimpiarProductos();
            DesactivarBotonesProductos();
            dg_producto.ItemsSource = unit.RepositorioProducto.ObtenerVarios(c => c.ClienteId == cliente.ClienteId).ToList();
        }

        private void bt_pr_modificar_Click(object sender, RoutedEventArgs e)
        {
            if (tb_pr_fechaventa.Text == "") { producto.FechaVenta = DateTime.Today; }
            else { producto.FechaVenta = Convert.ToDateTime(tb_pr_fechaventa.Text); }
            unit.RepositorioProducto.Actualizar(producto);
            LimpiarProductos();
            DesactivarBotonesProductos();
            dg_producto.ItemsSource = unit.RepositorioProducto.ObtenerVarios(c => c.ClienteId == cliente.ClienteId).ToList();
        }

        private void bt_pr_eliminar_Click(object sender, RoutedEventArgs e)
        {
            producto.ClienteId = cliente.ClienteId;
            if (tb_pr_fechaventa.Text == "") { producto.FechaVenta = DateTime.Today; }
            else { producto.FechaVenta = Convert.ToDateTime(tb_pr_fechaventa.Text); }
            unit.RepositorioProducto.Eliminar(producto);
            LimpiarProductos();
            DesactivarBotonesProductos();
            dg_producto.ItemsSource = unit.RepositorioProducto.ObtenerVarios(c => c.ClienteId == cliente.ClienteId).ToList();
        }

        private void dg_producto_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                producto = (Producto)dg_producto.SelectedItem;
                grid_producto.DataContext = producto;
                tb_pr_fechaventa.Text = producto.FechaVenta.ToString();
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

//Fabricacion
#region FABRICACION

        private void RellenarComboboxFabricacion()
        {
            cb_f_producto.Items.Clear();
            listaproductos = new List<Producto>();
            listaproductos = unit.RepositorioProducto.ObtenerTodo();
            foreach (var item in listaproductos)
            {
                cb_f_producto.Items.Add(item.Nombre);
            }
            if (cb_f_producto != null) cb_f_producto.SelectedIndex = 0;
        }

        private void cb_f_producto_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            productoseleccionado = cb_f_producto.SelectedIndex;
        }

        private void bt_f_seleccionar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                producto = listaproductos[productoseleccionado];
                int numeroid = listaproductos[productoseleccionado].ProductoId;
                producto = unit.RepositorioProducto.ObtenerUno(c => c.ProductoId == producto.ProductoId);
                LimpiarFabricacion();
                ActivarFabricacionProducto();
                DesactivarBotonesFabricacion();
                grid_fabricacion.DataContext = fabricacion;
                dg_fabricacion.ItemsSource = unit.RepositorioFabricacion.ObtenerVarios(c => c.ProductoId == producto.ProductoId).ToList();
            }
            catch (Exception)
            {

            }
        }

        private void bt_f_nuevo_Click(object sender, RoutedEventArgs e)
        {
            fabricacion = new Fabricacion();
            tb_f_fechainicio.Text = "";
            tb_f_fechafinal.Text = "";
            grid_fabricacion.DataContext = fabricacion;
            DesactivarBotonesFabricacion();
        }

        private void bt_f_añadir_Click(object sender, RoutedEventArgs e)
        {
            fabricacion.ProductoId = producto.ProductoId;
            if (tb_f_fechainicio.Text == "") { fabricacion.FechaInicio = DateTime.Today; }
            else { fabricacion.FechaInicio = Convert.ToDateTime(tb_f_fechainicio.Text); }
            if (tb_f_fechafinal.Text == "") { fabricacion.FechaAcaba = DateTime.Today; }
            else { fabricacion.FechaAcaba = Convert.ToDateTime(tb_f_fechafinal.Text); }
            unit.RepositorioFabricacion.Crear(fabricacion);
            LimpiarFabricacion();
            DesactivarBotonesFabricacion();
            dg_fabricacion.ItemsSource = unit.RepositorioFabricacion.ObtenerVarios(c => c.ProductoId == producto.ProductoId).ToList();
        }

        private void bt_f_modificar_Click(object sender, RoutedEventArgs e)
        {
            if (tb_f_fechainicio.Text == "") { fabricacion.FechaInicio = DateTime.Today; }
            else { fabricacion.FechaInicio = Convert.ToDateTime(tb_f_fechainicio.Text); }
            if (tb_f_fechafinal.Text == "") { fabricacion.FechaAcaba = DateTime.Today; }
            else { fabricacion.FechaAcaba = Convert.ToDateTime(tb_f_fechafinal.Text); }
            unit.RepositorioFabricacion.Actualizar(fabricacion);
            LimpiarFabricacion();
            DesactivarBotonesFabricacion();
            dg_fabricacion.ItemsSource = unit.RepositorioFabricacion.ObtenerVarios(c => c.ProductoId == producto.ProductoId).ToList();
        
        }

        private void bt_f_eliminar_Click(object sender, RoutedEventArgs e)
        {
            fabricacion.ProductoId = producto.ProductoId;
            if (tb_f_fechainicio.Text == "") { fabricacion.FechaInicio = DateTime.Today; }
            else { fabricacion.FechaInicio = Convert.ToDateTime(tb_f_fechainicio.Text); }
            if (tb_f_fechainicio.Text == "") { fabricacion.FechaInicio = DateTime.Today; }
            else { fabricacion.FechaInicio = Convert.ToDateTime(tb_f_fechainicio.Text); }
            unit.RepositorioFabricacion.Eliminar(fabricacion);
            LimpiarFabricacion();
            DesactivarBotonesFabricacion();
            dg_fabricacion.ItemsSource = unit.RepositorioFabricacion.ObtenerVarios(c => c.ProductoId == producto.ProductoId).ToList();
        }

        private void dg_fabricacion_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                fabricacion = (Fabricacion)dg_fabricacion.SelectedItem;
                grid_fabricacion.DataContext = fabricacion;
                tb_f_fechainicio.Text = fabricacion.FechaInicio.ToString();
                tb_f_fechafinal.Text = fabricacion.FechaAcaba.ToString();
                ActivarBotonesFabricacion();
            }
            catch (Exception)
            {
            }
        }

        private void cb_f_fabricado_Checked(object sender, RoutedEventArgs e)
        {
            fabricacion.Fabricado = (bool)cb_f_fabricado.IsChecked;
        }

        #endregion

//Materiales
#region MATERIALES

        private void bt_m_gestion_Click(object sender, RoutedEventArgs e)
        {
            LimpiarGrids();
            grid_material_gestion.Visibility = Visibility.Visible;
            LimpiarMaterial();
            grid_material_gestion.DataContext = material;
            RellenarComboboxProveedores();
            DesactivarBotonesMaterial();
        }

        private void bt_m_utilizacion_Click(object sender, RoutedEventArgs e)
        {
            LimpiarGrids();
            grid_material_utilizado.Visibility = Visibility.Visible;
            sp_materiales.Children.Clear();
            sp_proveedores.Children.Clear();
            GenerarBotones();
            RellenarComboboxFabricacionId();
            if (cb_fabricacionid != null && listafabricaciones.Count != 0) cb_fabricacionid.SelectedIndex = 0;
        }

        private void RellenarComboboxFabricacionId()
        {
            cb_fabricacionid.Items.Clear();
            listafabricaciones = new List<Fabricacion>();
            listafabricaciones = unit.RepositorioFabricacion.ObtenerTodo();
            foreach (var item in listafabricaciones)
            {
                cb_fabricacionid.Items.Add(item.FabricacionId);
            }
        }

        private BitmapImage EnseñarBit(string ruta)
        {
            try
            {
                BitmapImage bit = new BitmapImage();
                bit.BeginInit();
                bit.UriSource = new Uri(ruta);
                bit.EndInit();
                return bit;
            }
            catch (Exception)
            {
                return null;
            }
            
            
        }       

        #endregion

//Materiales - Gestion
#region MATERIALES - GESTION

        private void RellenarComboboxProveedores()
        {
            cb_m_proveedor.Items.Clear();
            listaproveedores = new List<Proveedor>();
            listaproveedores = unit.RepositorioProveedor.ObtenerTodo();
            foreach (var item in listaproveedores)
            {
                cb_m_proveedor.Items.Add(item.ProveedorId + "--> " + item.Nombre);
            }
            if (cb_m_proveedor != null) cb_m_proveedor.SelectedIndex = 0;
        }

        private void tb_m_proveedor_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                proveedor.ProveedorId = listaproveedores[cb_m_proveedor.SelectedIndex].ProveedorId;
            }
            catch (Exception)
            {
            }
            
        }

        private void bt_m_nuevo_Click(object sender, RoutedEventArgs e)
        {
            material = new Material();
            grid_material_gestion.DataContext = material;
            BitmapImage bit = new BitmapImage();
            imagen_materiales.Source = bit;
            RellenarComboboxProveedores();
            DesactivarBotonesMaterial();
        }

        private void bt_m_añadir_Click(object sender, RoutedEventArgs e)
        {
            material.ProveedorId = proveedor.ProveedorId;
            material.Foto = tb_m_foto.Text;
            unit.RepositorioMaterial.Crear(material);
            LimpiarMaterial();
            DesactivarBotonesMaterial();
        }

        private void bt_m_modificar_Click(object sender, RoutedEventArgs e)
        {
            material.ProveedorId = proveedor.ProveedorId;
            material.Foto = tb_m_foto.Text;
            unit.RepositorioMaterial.Actualizar(material);
            LimpiarMaterial();
            DesactivarBotonesMaterial();
        }

        private void bt_m_eliminar_Click(object sender, RoutedEventArgs e)
        {
            unit.RepositorioMaterial.Eliminar(material);
            LimpiarMaterial();
            DesactivarBotonesMaterial();
        }

        private void dg_material_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                material = (Material)dg_material.SelectedItem;
                grid_material_gestion.DataContext = material;
                imagen_materiales.Source = EnseñarBit(material.Foto);
                ActivarBotonesMaterial();
            }
            catch (Exception)
            {
            }
        }

        private void bt_m_elegir_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.OpenFileDialog explorador = new System.Windows.Forms.OpenFileDialog();
            explorador.InitialDirectory = Environment.CurrentDirectory + @"\Imagenes";
            explorador.Filter = "Image Files(*.BMP;*.JPG;*.GIF;*.PNG)|*.BMP;*.JPG;*.GIF;*.PNG|All files (*.*)|*.*";
            explorador.ShowDialog();
            tb_m_foto.Text = explorador.FileName.ToString();
            imagen_materiales.Source = EnseñarBit(tb_m_foto.Text);
        }


        #endregion

//Materiales - Utilizado
#region MATERIALES - UTILIZADO

        private void cb_fabricacionid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                fabricacioneseleccionado = listafabricaciones.ElementAt(cb_fabricacionid.SelectedIndex).FabricacionId;
            }
            catch (Exception)
            {
            }
            
        }

        //Creacion de botones de proveedores
        public void GenerarBotones()
        {
            try

            {

                while ( sp_proveedores.Children.Count > 0)
                {
                    sp_proveedores.Children.RemoveAt(sp_proveedores.Children.Count - 1);
                }
                List<Proveedor> proveedores = new List<Proveedor>();
                proveedores = unit.RepositorioProveedor.ObtenerTodo();
                for (int i = 0; i < proveedores.Count; i++)
                {
                    Button n = new Button();
                    n.Content = proveedores[i].Nombre;
                    n.Height = 40;
                    n.Width = 130;
                    n.Background = Brushes.LightBlue;
                    n.Margin = new Thickness(2);
                    n.Click += proveedor_click;
                    sp_proveedores.Children.Add(n);

                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        //Click  en una categoria y creacion de botones de productos
        private void proveedor_click(object sender, RoutedEventArgs e)
        {

            List<Material> listmateriales = new List<Material>();
            Proveedor cat = new Proveedor();



            this.sp_materiales.Children.Clear();
            var aux = e.OriginalSource;
            if (aux.GetType() == typeof(Button))
            {
                var a = sender as Button;
                cat = unit.RepositorioProveedor.ObtenerUno(d => d.Nombre.Equals(a.Content.ToString()));
                listmateriales = unit.RepositorioMaterial.ObtenerVarios(c => c.ProveedorId.Equals(cat.ProveedorId));

                for (int i = 0; i < listmateriales.Count; i++)
                {

                    //crear boton
                    Button b = new Button();
                    b.Width = 125;
                    b.Height = 125;
                    b.Margin = new Thickness(2);
                    b.Content = CreacionBotones(listmateriales[i].Foto, listmateriales[i].Nombre, 105);
                    b.Name = "P_" + listmateriales[i].MaterialId;
                    b.Click += producto_click;

                    //mirar stock
                    if (listmateriales[i].Stock > 0)
                    {
                        b.Background = Brushes.LightBlue;
                    }
                    else
                    {
                        b.Background = Brushes.LightSalmon;
                    }

                    this.sp_materiales.Children.Add(b);
                }
            }
        }

        //Cuando clickamos a un producto para añadirlo a lineaventa
        private void producto_click(object sender, RoutedEventArgs e)
        {

            var aux = e.OriginalSource;
            if (aux.GetType() == typeof(Button))
            {
                Button b = (Button)aux;

                String[] btname = b.Name.Split('_');
                int cx = Convert.ToInt32(btname[1].Trim());
                material = unit.RepositorioMaterial.ObtenerUno(c => c.MaterialId == cx);

                if (material.Stock < 1)
                {
                    MessageBox.Show("El producto no tiene stock", "ERROR", MessageBoxButton.OK, MessageBoxImage.Stop);
                }
                else
                {
                        material.Stock--;
                        unit.RepositorioMaterial.Actualizar(material);
                        listamateriales.Add(material);

                    //fabricacion.Materiales = listamateriales;

                    dg_material_aplicado.ItemsSource = "";
                    dg_material_aplicado.ItemsSource = listamateriales;
                }
            }
        }

        //Clickar en delete en datagrid
        public void Click_delete(object sender, RoutedEventArgs e)
        {
            try
            {
                if (MessageBox.Show("¿Desea eliminar el producto?", "Cancelar", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    Material materialaux = listamateriales.ElementAt(dg_material_aplicado.SelectedIndex);
                    material = unit.RepositorioMaterial.ObtenerUno(c => c.MaterialId==materialaux.MaterialId);
                    material.Stock--;
                    unit.RepositorioMaterial.Actualizar(materialaux);
                    listamateriales.RemoveAt(dg_material_aplicado.SelectedIndex);
                    //listamateriales.Add(material);
                    //fabricacion.Materiales = listamateriales;
                    dg_material_aplicado.ItemsSource = "";
                    dg_material_aplicado.ItemsSource = listamateriales;

                }
                else
                {
                    MessageBox.Show("Cancelada eliminacion");
                }
            }
            catch (Exception)
            {
            }

        }

        //Crear Factura
        public void CrearFactura(Fabricacion fabricacion)
        {    
            StreamWriter escritura;
            /*siempre se mira si existe*/
            String factura = "  Lista de Materiales de Fabricacion N " + fabricacion.FabricacionId + ".txt";
            if (!File.Exists(factura))
            {
                escritura = new StreamWriter(factura, true, Encoding.Default);
                foreach (var item in fabricacion.Materiales)
                {
                    escritura.WriteLine("  Articulo------" + item.Nombre + "  Precio------" + item.Precio);
                }
                escritura.WriteLine("");
                escritura.WriteLine("  Producto -------" + fabricacion.Productos.Nombre);
                escritura.WriteLine("");
                escritura.WriteLine("  Cliente -------" + fabricacion.Productos.Clientes.Nombre+" "+ fabricacion.Productos.Clientes.Apellidos);

                /*escritura de lineas con WriteLine*/
                /*IMPORTANTE al acabar de escribir el txt*/
                escritura.Close();
                Process proceso = new Process();
                proceso.StartInfo.FileName = factura;
                proceso.Start();
            }

            else
            {
                /*Error de que ya esta cargado*/
            }
        }


        private void bt_aplicar_fabricacion_Click(object sender, RoutedEventArgs e)
        {
            listamateriales.Clear();
            dg_material_aplicado.ItemsSource = "";
            fabricacion = new Fabricacion();
            fabricacion = unit.RepositorioFabricacion.ObtenerUno(c => c.FabricacionId == fabricacioneseleccionado);
            // dg_material_aplicado.ItemsSource = fabricacion.Materiales;
            foreach (var item in fabricacion.Materiales)
            {
                listamateriales.Add(item);
            }
            dg_material_aplicado.ItemsSource = listamateriales;
            listafabricaciones.Count();
        }

        private void bt_aplicarcambios_Click(object sender, RoutedEventArgs e)
        {
            fabricacion.Materiales = listamateriales;
            unit.RepositorioFabricacion.Actualizar(fabricacion);
            dg_material_aplicado.ItemsSource = "";
            RellenarComboboxFabricacionId();
            if (cb_fabricacionid != null && listafabricaciones.Count != 0) cb_fabricacionid.SelectedIndex = 0;
        }

        private void bt_generar_materiales_Click(object sender, RoutedEventArgs e)
        {
            CrearFactura(fabricacion);
        }
        #endregion
    }
}
