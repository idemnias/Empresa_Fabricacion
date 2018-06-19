using Empresa_Fabricacion.DAL;
using Empresa_Fabricacion.Model;
using iText.IO.Image;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Mail;
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
        Categoria categoria = new Categoria();
        int clienteseleccionado;
        int productoseleccionado;
        int categoriaseleccionado;
        int fabricacioneseleccionado=1;
        System.Windows.Style estilo;
        bool materialencontrado = false;
        bool fabricacionaplicada = false;
        List<Cliente> listaclientes = new List<Cliente>();
        List<Proveedor> listaproveedores = new List<Proveedor>();
        List<Categoria> listacategorias = new List<Categoria>();
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
            imagen_inicio.Source = EnseñarBit(rutainicial + "iconofactura.png");
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
            cb_f_trabajadoractivo.IsChecked = false;
            dg_fabricacion.ItemsSource = unit.RepositorioFabricacion.ObtenerTodo().ToList();
        }
        private void LimpiarMaterial()
        {
            material = new Material();
            grid_material_gestion.DataContext = material;
            BitmapImage bit = new BitmapImage();
            imagen_materiales.Source = bit;
            RellenarComboboxProveedores();
            RellenarComboboxCategoria();
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
            bt_pr_generarfactura.Visibility = Visibility.Visible;

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
            bt_pr_generarfactura.Visibility = Visibility.Hidden;
        }

        private void ActivarBotonesProductos()
        {
            bt_pr_añadir.Visibility = Visibility.Hidden;
            bt_pr_modificar.Visibility = Visibility.Visible;
            bt_pr_eliminar.Visibility = Visibility.Visible;
            bt_pr_generarfactura.Visibility = Visibility.Visible;
        }
        private void DesactivarBotonesProductos()
        {
            bt_pr_añadir.Visibility = Visibility.Visible;
            bt_pr_modificar.Visibility = Visibility.Hidden;
            bt_pr_eliminar.Visibility = Visibility.Hidden;
            bt_pr_generarfactura.Visibility = Visibility.Hidden;
        }

        private void ActivarFabricacionProducto()
        {
            lb_f_seleccionar.Visibility = Visibility.Hidden;
            cb_f_producto.Visibility = Visibility.Hidden;
            bt_f_seleccionar.Visibility = Visibility.Hidden;
            lb1f.Visibility = Visibility.Visible;
            lb2f.Visibility = Visibility.Visible;
            lb3f.Visibility = Visibility.Visible;
            lb4f.Visibility = Visibility.Visible;
            lb5f.Visibility = Visibility.Visible;
            ug_fabricacion.Visibility = Visibility.Visible;
            ug_trabajador_activo.Visibility = Visibility.Visible;
            cb_f_fabricado.Visibility = Visibility.Visible;
            tb_f_fechainicio.Visibility = Visibility.Visible;
            tb_f_fechafinal.Visibility = Visibility.Visible;
            dg_fabricacion.Visibility = Visibility.Visible;
            bt_f_añadir.Visibility = Visibility.Visible;
            bt_f_modificar.Visibility = Visibility.Visible;
            bt_f_eliminar.Visibility = Visibility.Visible;
            bt_f_nuevo.Visibility = Visibility.Visible;
            cb_f_trabajadoractivo.Visibility = Visibility.Visible;
            cb_f_clienteid.Visibility = Visibility.Visible;

        }
        private void DesactivarFabricacionProducto()
        {
            lb_f_seleccionar.Visibility = Visibility.Visible;
            cb_f_producto.Visibility = Visibility.Visible;
            bt_f_seleccionar.Visibility = Visibility.Visible;
            lb1f.Visibility = Visibility.Hidden;
            lb2f.Visibility = Visibility.Hidden;
            lb3f.Visibility = Visibility.Hidden;
            lb4f.Visibility = Visibility.Hidden;
            lb5f.Visibility = Visibility.Hidden;
            ug_trabajador_activo.Visibility = Visibility.Hidden;
            ug_fabricacion.Visibility = Visibility.Hidden;
            cb_f_fabricado.Visibility = Visibility.Hidden;
            tb_f_fechainicio.Visibility = Visibility.Hidden;
            tb_f_fechafinal.Visibility = Visibility.Hidden;
            dg_fabricacion.Visibility = Visibility.Hidden;
            bt_f_añadir.Visibility = Visibility.Hidden;
            bt_f_modificar.Visibility = Visibility.Hidden;
            bt_f_eliminar.Visibility = Visibility.Hidden;
            bt_f_nuevo.Visibility = Visibility.Hidden;
            cb_f_trabajadoractivo.Visibility = Visibility.Hidden;
            cb_f_clienteid.Visibility = Visibility.Hidden;

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

        //inicio
        private void bt_inicio_Click(object sender, RoutedEventArgs e)
        {
            LimpiarGrids();
            grid_inicio.Visibility = Visibility.Visible;
            LimpiarBotones();
        }
        
        //fabricacion
        private void bt_fabricacion_Click(object sender, RoutedEventArgs e)
        {
            LimpiarGrids();
            grid_fabricacion.Visibility = Visibility.Visible;
            LimpiarFabricacion();
            DesactivarFabricacionProducto();
            RellenarComboboxFabricacion();
        }

        //material
        private void bt_material_Click(object sender, RoutedEventArgs e)
        {
            LimpiarGrids();
            grid_material .Visibility = Visibility.Visible;
        }

        //producto
        private void bt_producto_Click(object sender, RoutedEventArgs e)
        {
            LimpiarGrids();
            grid_producto.Visibility = Visibility.Visible;
            LimpiarProductos();
            DesactivarProductosCliente();
            RellenarComboboxClientes();
        }

        //cliente
        private void bt_cliente_Click(object sender, RoutedEventArgs e)
        {
            LimpiarGrids();
            grid_cliente.Visibility = Visibility.Visible;
            LimpiarCliente();
            grid_cliente.DataContext = cliente;
            dg_cliente.ItemsSource = unit.RepositorioCliente.ObtenerTodo().ToList();
            DesactivarBotonesCliente();
        }

        //proveedor
        private void bt_proveedor_Click(object sender, RoutedEventArgs e)
        {
            LimpiarGrids();
            grid_proveedor.Visibility = Visibility.Visible;
            LimpiarProveedores();
            grid_proveedor.DataContext = proveedor;
            dg_proveedor.ItemsSource = unit.RepositorioProveedor.ObtenerTodo().ToList();
            DesactivarBotonesEmpleado();
        }

        //empleado
        private void bt_empleado_Click(object sender, RoutedEventArgs e)
        {
            LimpiarGrids();
            grid_empleado.Visibility = Visibility.Visible;
            LimpiarEmpleados();
            cb_u_tipocuenta.SelectedIndex = 0;
            grid_empleado.DataContext = empleado;
            dg_empleado.ItemsSource = unit.RepositorioEmpleado.ObtenerTodo().ToList();
            DesactivarBotonesEmpleado();
        }

        #endregion

//Inicio
#region INICIO
        //acceder al inicio
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
                    else if (aux.TipoCuenta.Equals("Vendedor"))
                    {
                        bt_empleado.Visibility = Visibility.Collapsed;
                        bt_fabricacion.Visibility = Visibility.Collapsed;
                        bt_proveedor.Visibility = Visibility.Collapsed;
                    }
                    else { }
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

        //Enseñar imagen
        private System.Windows.Controls.Image EnseñarImagen(string ruta, int peso)
        {
            try
            {
                System.Windows.Controls.Image imagen = new System.Windows.Controls.Image();
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

        //metodo para crear stackpanel
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

        //Olvidar usuario o contraseña
        private void OlvidarContraseña(object sender, MouseButtonEventArgs e)
        {
            VentanaCorreo ventana = new VentanaCorreo();
            ventana.ShowDialog();
        }



        #endregion

//Empleados
#region EMPLEADOS

        //nuevo empleado
        private void bt_e_nuevo_Click(object sender, RoutedEventArgs e)
        {
            empleado = new Empleado();
            grid_empleado.DataContext = empleado;
            cb_u_tipocuenta.SelectedIndex = 0;
            DesactivarBotonesEmpleado();
        }

        //añadir empleado
        private void bt_e_añadir_Click(object sender, RoutedEventArgs e)
        {
            unit.RepositorioEmpleado.Crear(empleado);
            LimpiarEmpleados();
            DesactivarBotonesEmpleado();
            MessageBox.Show("Empleado nuevo añadido");
        }

        //modificar empleado
        private void bt_e_modificar_Click(object sender, RoutedEventArgs e)
        {
            unit.RepositorioEmpleado.Actualizar(empleado);
            LimpiarEmpleados();
            DesactivarBotonesEmpleado();
            MessageBox.Show("Empleado modificado");
        }

        //eliminar empleado
        private void bt_e_eliminar_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("¿Desea eliminar el empleado?", "Cancelar", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                unit.RepositorioEmpleado.Eliminar(empleado);
                LimpiarEmpleados();
                DesactivarBotonesEmpleado();
            }
            else { MessageBox.Show("Eliminación de empleado cancelada"); }
            
        }

        //clic en datagrid de empleado
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

        //nuevo proveedor
        private void bt_p_nuevo_Click(object sender, RoutedEventArgs e)
        {
            proveedor = new Proveedor();
            grid_proveedor.DataContext = proveedor;
            DesactivarBotonesProveedor();
        }
        
        //añadir proveedor
        private void bt_p_añadir_Click(object sender, RoutedEventArgs e)
        {
            unit.RepositorioProveedor.Crear(proveedor);
            LimpiarProveedores();
            DesactivarBotonesProveedor();
            MessageBox.Show("Proveedor nuevo añadido");
        }

        //modificar proveedor
        private void bt_p_modificar_Click(object sender, RoutedEventArgs e)
        {
            unit.RepositorioProveedor.Actualizar(proveedor);
            LimpiarProveedores();
            DesactivarBotonesProveedor();
            MessageBox.Show("Proveedor modificado");
        }

        //eliminar proveedor
        private void bt_p_eliminar_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("¿Desea eliminar el proveedor?", "Cancelar", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                unit.RepositorioProveedor.Eliminar(proveedor);
                LimpiarProveedores();
                DesactivarBotonesProveedor();
            }
            else { MessageBox.Show("Eliminación de proveedor cancelada"); }
        }

        //clic en datagrid de proveedor
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

        //nuevo cliente
        private void bt_c_nuevo_Click(object sender, RoutedEventArgs e)
        {
            cliente = new Cliente();
            grid_cliente.DataContext = cliente;
            DesactivarBotonesCliente();
        }

        //añadir nuevo cliente
        private void bt_c_añadir_Click(object sender, RoutedEventArgs e)
        {
            unit.RepositorioCliente.Crear(cliente);
            LimpiarCliente();
            DesactivarBotonesCliente();
            MessageBox.Show("Cliente nuevo añadido");
        }

        //modificar cliente
        private void bt_c_modificar_Click(object sender, RoutedEventArgs e)
        {
            unit.RepositorioCliente.Actualizar(cliente);
            LimpiarCliente();
            DesactivarBotonesCliente();
            MessageBox.Show("Cliente modificado");
        }

        //eliminar cliente
        private void bt_c_eliminar_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("¿Desea eliminar el cliente?", "Cancelar", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                unit.RepositorioCliente.Eliminar(cliente);
                LimpiarCliente();
                DesactivarBotonesCliente();
            }
            else { MessageBox.Show("Eliminación de cliente cancelada"); }
        }

        //clic en datagrid de cliente
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

        //rellenar combobox de clientes
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

        //aceptar cliente
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

        //clic en combobox de clientes
        private void cb_p_cliente_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            clienteseleccionado = cb_pr_cliente.SelectedIndex;
        }

        //nuevo producto
        private void bt_pr_nuevo_Click(object sender, RoutedEventArgs e)
        {
            producto = new Producto();
            tb_pr_fechaventa.Text = "";
            grid_producto.DataContext = producto;
            DesactivarBotonesProductos();
        }

        //añadir producto
        private void bt_pr_añadir_Click(object sender, RoutedEventArgs e)
        {
            if (producto == null) { producto = new Producto(); }
            producto.ClienteId = cliente.ClienteId;

            if (tb_pr_fechaventa.Text == "") { producto.FechaVenta = DateTime.Today; }
            else { producto.FechaVenta = Convert.ToDateTime(tb_pr_fechaventa.Text); }   
            
            unit.RepositorioProducto.Crear(producto);
            LimpiarProductos();
            DesactivarBotonesProductos();
            dg_producto.ItemsSource = unit.RepositorioProducto.ObtenerVarios(c => c.ClienteId == cliente.ClienteId).ToList();
            MessageBox.Show("Producto nuevo añadido");
        }

        //modificar producto
        private void bt_pr_modificar_Click(object sender, RoutedEventArgs e)
        {
            if (tb_pr_fechaventa.Text == "") { producto.FechaVenta = DateTime.Today; }
            else { producto.FechaVenta = Convert.ToDateTime(tb_pr_fechaventa.Text); }

            unit.RepositorioProducto.Actualizar(producto);
            LimpiarProductos();
            DesactivarBotonesProductos();
            dg_producto.ItemsSource = unit.RepositorioProducto.ObtenerVarios(c => c.ClienteId == cliente.ClienteId).ToList();
            MessageBox.Show("Producto modificado");
        }

        //eliminar producto
        private void bt_pr_eliminar_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("¿Desea eliminar el producto?", "Cancelar", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                producto.ClienteId = cliente.ClienteId;

                if (tb_pr_fechaventa.Text == "") { producto.FechaVenta = DateTime.Today; }
                else { producto.FechaVenta = Convert.ToDateTime(tb_pr_fechaventa.Text); }

                unit.RepositorioProducto.Eliminar(producto);
                LimpiarProductos();
                DesactivarBotonesProductos();
                dg_producto.ItemsSource = unit.RepositorioProducto.ObtenerVarios(c => c.ClienteId == cliente.ClienteId).ToList();
            }
            else { MessageBox.Show("Eliminación de producto cancelada"); }
        }

        //clic al datagrid de producto
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

        //chequear el boton de vendido
        private void cb_p_vendido_Checked(object sender, RoutedEventArgs e)
        {
            producto.Vendido = (bool) cb_pr_vendido.IsChecked;
        }

        //clic a generar factura
        private void bt_pr_generarfactura_Click(object sender, RoutedEventArgs e)
        {
            fabricacion = unit.RepositorioFabricacion.ObtenerUno(x => x.ProductoId == producto.ProductoId);
            GenerarFactura();
            LimpiarProductos();
            DesactivarBotonesProductos();
            dg_producto.ItemsSource = unit.RepositorioProducto.ObtenerVarios(c => c.ClienteId == cliente.ClienteId).ToList();
        }

        #endregion

//Fabricacion
#region FABRICACION

        //rellenar combobox de fabricacion
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

        //rellenar combobox de cliente
        private void RellenarComboboxClienteId()
        {
            cb_f_clienteid.Items.Clear();
            listaclientes = new List<Cliente>();
            listaclientes = unit.RepositorioCliente.ObtenerTodo();
            foreach (var item in listaclientes)
            {
                cb_f_clienteid.Items.Add(item.Nombre + " " + item.Apellidos);
            }
            if (cb_f_clienteid != null)
            {
                clienteseleccionado = Regresarcliente();
                cliente = listaclientes[clienteseleccionado];
                cb_f_clienteid.SelectedIndex = clienteseleccionado;
            }
        }

        //Devolver posicion de cliente preseleccionado en producto
        private int Regresarcliente()
        {
            int clientedeproducto = 0;
            cliente = unit.RepositorioCliente.ObtenerUno(c => c.ClienteId == producto.ClienteId);
            for (int i = 0; i < listaclientes.Count; i++)
            {
                if (listaclientes[i].ClienteId == producto.ClienteId)
                {
                    clientedeproducto = i;
                }
            }
            return clientedeproducto;
        }

        //clic en combobox de clientes
        private void cb_f_clienteid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            cliente = listaclientes[cb_f_clienteid.SelectedIndex];
        }

        //clic en combobox de producto
        private void cb_f_producto_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            productoseleccionado = cb_f_producto.SelectedIndex;
        }

        //clic boton cuando aceptas el producto
        private void bt_f_seleccionar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                producto = listaproductos[productoseleccionado];
                int numeroid = listaproductos[productoseleccionado].ProductoId;
                producto = unit.RepositorioProducto.ObtenerUno(c => c.ProductoId == producto.ProductoId);
                LimpiarFabricacion();
                RellenarComboboxClienteId();
                ActivarFabricacionProducto();
                DesactivarBotonesFabricacion();
                grid_fabricacion.DataContext = fabricacion;
                dg_fabricacion.ItemsSource = unit.RepositorioFabricacion.ObtenerVarios(c => c.ProductoId == producto.ProductoId).ToList();
            }
            catch (Exception)
            {

            }
        }

        //Comprobar empleado si esta en la fabricacion
        private bool comprobarempleadoidenfabricacion(Fabricacion fabricacion)
        {
            bool estaempleado = false;
            foreach (var item in fabricacion.Empleados)
            {
                if (item.EmpleadoId == usuarioactivo.EmpleadoId)
                {
                    estaempleado = true;
                }
            }
            return estaempleado;
        }

        //nueva fabricacion
        private void bt_f_nuevo_Click(object sender, RoutedEventArgs e)
        {
            fabricacion = new Fabricacion();
            tb_f_fechainicio.Text = "";
            tb_f_fechafinal.Text = "";
            cb_f_trabajadoractivo.IsChecked = false;
            cb_f_clienteid.SelectedIndex = clienteseleccionado;
            grid_fabricacion.DataContext = fabricacion;
            DesactivarBotonesFabricacion();
        }

        //añadir fabricacion
        private void bt_f_añadir_Click(object sender, RoutedEventArgs e)
        {
            if (fabricacion == null) { fabricacion = new Fabricacion(); }
            fabricacion.ProductoId = producto.ProductoId;
            fabricacion.ClienteId = cliente.ClienteId;
            if (tb_f_fechainicio.Text == "") { fabricacion.FechaInicio = DateTime.Today; }
            else { fabricacion.FechaInicio = Convert.ToDateTime(tb_f_fechainicio.Text); }
            if (tb_f_fechafinal.Text == "") { fabricacion.FechaAcaba = DateTime.Today; }
            else { fabricacion.FechaAcaba = Convert.ToDateTime(tb_f_fechafinal.Text); }

            //agregar fabricacion a empleado
            if (cb_f_trabajadoractivo.IsChecked == true)
            {
                fabricacion.Empleados.Add(usuarioactivo);
                unit.RepositorioFabricacion.Crear(fabricacion);
                usuarioactivo.FabricacionId = fabricacion.FabricacionId;
                unit.RepositorioEmpleado.Actualizar(usuarioactivo);
            }
            else
            {
                unit.RepositorioFabricacion.Crear(fabricacion);
            }

            LimpiarFabricacion();
            cb_f_clienteid.SelectedIndex = clienteseleccionado;
            DesactivarBotonesFabricacion();
            dg_fabricacion.ItemsSource = unit.RepositorioFabricacion.ObtenerVarios(c => c.ProductoId == producto.ProductoId).ToList();
            MessageBox.Show("Fabricación nueva añadida");
        }

        //modificar fabricacion
        private void bt_f_modificar_Click(object sender, RoutedEventArgs e)
        {
            if (tb_f_fechainicio.Text == "") { fabricacion.FechaInicio = DateTime.Today; }
            else { fabricacion.FechaInicio = Convert.ToDateTime(tb_f_fechainicio.Text); }

            if (tb_f_fechafinal.Text == "") { fabricacion.FechaAcaba = DateTime.Today; }
            else { fabricacion.FechaAcaba = Convert.ToDateTime(tb_f_fechafinal.Text); }

            fabricacion.ClienteId = cliente.ClienteId;

            //agregar fabricacion a empleado
            if (comprobarempleadoidenfabricacion(fabricacion) == false)
            {
                if (cb_f_trabajadoractivo.IsChecked == true)
                {
                    fabricacion.Empleados.Add(usuarioactivo);
                    usuarioactivo.FabricacionId = fabricacion.FabricacionId;
                }
            }
            else if(cb_f_trabajadoractivo.IsChecked == false){ usuarioactivo.FabricacionId = null; }

            unit.RepositorioEmpleado.Actualizar(usuarioactivo);
            unit.RepositorioFabricacion.Actualizar(fabricacion);
            LimpiarFabricacion();
            cb_f_clienteid.SelectedIndex = clienteseleccionado;
            DesactivarBotonesFabricacion();
            dg_fabricacion.ItemsSource = unit.RepositorioFabricacion.ObtenerVarios(c => c.ProductoId == producto.ProductoId).ToList();
            MessageBox.Show("Fabricación modificada");
        }

        //eliminar fabricacion
        private void bt_f_eliminar_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("¿Desea eliminar la fabricación?", "Cancelar", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                if (tb_f_fechainicio.Text == "") { fabricacion.FechaInicio = DateTime.Today; }
                else { fabricacion.FechaInicio = Convert.ToDateTime(tb_f_fechainicio.Text); }

                if (tb_f_fechainicio.Text == "") { fabricacion.FechaInicio = DateTime.Today; }
                else { fabricacion.FechaInicio = Convert.ToDateTime(tb_f_fechainicio.Text); }

                unit.RepositorioFabricacion.Eliminar(fabricacion);
                LimpiarFabricacion();
                cb_f_clienteid.SelectedIndex = clienteseleccionado;
                DesactivarBotonesFabricacion();
                dg_fabricacion.ItemsSource = unit.RepositorioFabricacion.ObtenerVarios(c => c.ProductoId == producto.ProductoId).ToList();
            }
            else { MessageBox.Show("Eliminación de fabricación cancelada"); }
        }

        //clic en datagrid de fabricación
        private void dg_fabricacion_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                fabricacion = (Fabricacion)dg_fabricacion.SelectedItem;
                grid_fabricacion.DataContext = fabricacion;
                tb_f_fechainicio.Text = fabricacion.FechaInicio.ToString();
                tb_f_fechafinal.Text = fabricacion.FechaAcaba.ToString();
                for (int i = 0; i < listaclientes.Count; i++)
                {
                    if(fabricacion.ClienteId == listaclientes[i].ClienteId)
                    {
                        cliente = listaclientes[i];
                    }
                }
                cb_f_clienteid.Text = cliente.Nombre+" "+cliente.Apellidos;
                if (usuarioactivo.FabricacionId == fabricacion.FabricacionId) { cb_f_trabajadoractivo.IsChecked = true; }
                else { cb_f_trabajadoractivo.IsChecked = false; }
                ActivarBotonesFabricacion();
            }
            catch (Exception)
            {
            }
        }

        //clic en checkbox de fabricado
        private void cb_f_fabricado_Checked(object sender, RoutedEventArgs e)
        {
            fabricacion.Fabricado = (bool)cb_f_fabricado.IsChecked;
        }

        #endregion

//Materiales
#region MATERIALES

        //clic en boton de gestión de materiales
        private void bt_m_gestion_Click(object sender, RoutedEventArgs e)
        {
            Metodo_Gestion();
        }

        //método de gestion para llamarlo desde otras ventanas
        public void Metodo_Gestion()
        {
            LimpiarGrids();
            grid_material_gestion.Visibility = Visibility.Visible;
            LimpiarMaterial();
            grid_material_gestion.DataContext = material;
            RellenarComboboxProveedores();
            RellenarComboboxCategoria();
            DesactivarBotonesMaterial();
        }

        //clic en boton de materiales utilizados
        private void bt_m_utilizacion_Click(object sender, RoutedEventArgs e)
        {
            LimpiarGrids();
            grid_material_utilizado.Visibility = Visibility.Visible;
            sp_materiales.Children.Clear();
            sp_categorias.Children.Clear();
            GenerarBotones();
            RellenarComboboxFabricacionId();          
            if (cb_fabricacionid != null && listafabricaciones.Count != 0)
            {
                cb_fabricacionid.SelectedIndex = 0;
                fabricacioneseleccionado = listafabricaciones.ElementAt(cb_fabricacionid.SelectedIndex).FabricacionId;
            }
            bt_aplicarcambios.Visibility = Visibility.Hidden;
            bt_generar_materiales.Visibility = Visibility.Hidden;
            dg_material_aplicado.ItemsSource = "";
            fabricacionaplicada = false;
            
        }

        //rellenar combobox de fabricaciónid
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

        //método para devolver imagen pasandole un string
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

        //rellenar combobox de categoria
        private void RellenarComboboxCategoria()
        {
            cb_categoria.Items.Clear();
            listacategorias = new List<Categoria>();
            listacategorias = unit.RepositorioCategoria.ObtenerTodo();
            if (cb_categoria!=null)
            {
                foreach (var item in listacategorias)
                {
                    cb_categoria.Items.Add(item.CategoriaId+"-->"+item.Nombre);
                }
                cb_categoria.SelectedIndex = 0;
                categoriaseleccionado = listacategorias.ElementAt(0).CategoriaId;
            }
        }
        

        #endregion

//Materiales - Gestion
#region MATERIALES - GESTION

        //rellenar combobox de proveedores
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

        //clic en combobox de proveedores
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

        //nuevo material
        private void bt_m_nuevo_Click(object sender, RoutedEventArgs e)
        {
            material = new Material();
            grid_material_gestion.DataContext = material;
            BitmapImage bit = new BitmapImage();
            imagen_materiales.Source = bit;
            RellenarComboboxProveedores();
            RellenarComboboxCategoria();
            DesactivarBotonesMaterial();
        }

        //añadir material
        private void bt_m_añadir_Click(object sender, RoutedEventArgs e)
        {
            material.ProveedorId = proveedor.ProveedorId;
            material.CategoriaId = categoriaseleccionado;

            if (tb_m_foto.Text != "")
            {
                material.Foto = tb_m_foto.Text;
            }
            else { material.Foto = Environment.CurrentDirectory + @"\Imagenes\imagendefecto.png"; }
            
            unit.RepositorioMaterial.Crear(material);
            LimpiarMaterial();
            DesactivarBotonesMaterial();
            MessageBox.Show("Material nuevo añadido");
        }

        //modificar material
        private void bt_m_modificar_Click(object sender, RoutedEventArgs e)
        {
            material.ProveedorId = proveedor.ProveedorId;
            material.Foto = tb_m_foto.Text;
            material.CategoriaId = listacategorias.ElementAt(cb_categoria.SelectedIndex).CategoriaId;
            unit.RepositorioMaterial.Actualizar(material);
            LimpiarMaterial();
            DesactivarBotonesMaterial();
            MessageBox.Show("Material modificado");
        }

        //eliminar material
        private void bt_m_eliminar_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("¿Desea eliminar el material?", "Cancelar", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                unit.RepositorioMaterial.Eliminar(material);
                LimpiarMaterial();
                DesactivarBotonesMaterial();
            }
            else { MessageBox.Show("Eliminación de material cancelado"); }
        }

        //clic en datagrid de materiales
        private void dg_material_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                material = (Material)dg_material.SelectedItem;
                grid_material_gestion.DataContext = material;
                imagen_materiales.Source = EnseñarBit(material.Foto);
                cb_m_proveedor.SelectedIndex = material.ProveedorId-1;
                cb_categoria.SelectedIndex = material.CategoriaId-1;
                ActivarBotonesMaterial();
            }
            catch (Exception)
            {
            }
        }

        //clic en boton de elegir imagen
        private void bt_m_elegir_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.OpenFileDialog explorador = new System.Windows.Forms.OpenFileDialog();
            explorador.InitialDirectory = Environment.CurrentDirectory + @"\Imagenes";
            explorador.Filter = "Image Files(*.BMP;*.JPG;*.GIF;*.PNG)|*.BMP;*.JPG;*.GIF;*.PNG|All files (*.*)|*.*";
            explorador.ShowDialog();
            tb_m_foto.Text = explorador.FileName.ToString();
            imagen_materiales.Source = EnseñarBit(tb_m_foto.Text);
        }

        //clic en combobox de categoria
        private void cb_categoria_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                categoriaseleccionado = listacategorias.ElementAt(cb_categoria.SelectedIndex).CategoriaId;
            }
            catch (Exception)
            {
            }
            
        }

        //clic en modificar categorias
        private void bt_m_modificarcategorias_Click(object sender, RoutedEventArgs e)
        {
            VentanaCategorias ventanaCategorias = new VentanaCategorias(this);
            ventanaCategorias.ShowDialog();
        }

        #endregion

//Materiales - Utilizado
#region MATERIALES - UTILIZADO

        //clic en combobox de fabricacion id
        private void cb_fabricacionid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                fabricacioneseleccionado = listafabricaciones.ElementAt(cb_fabricacionid.SelectedIndex).FabricacionId;
                bt_aplicarcambios.Visibility = Visibility.Hidden;
                bt_generar_materiales.Visibility = Visibility.Hidden;
                fabricacionaplicada = false;
            
            }
            catch (Exception)
            {
            }            
        }

        //private LinearGradientBrush GradienteBoton()
        //{
        ////    Create a diagonal linear gradient with four stops.   
        //    LinearGradientBrush myLinearGradientBrush =
        //        new LinearGradientBrush();
        //    myLinearGradientBrush.StartPoint = new Point(0.5, 0);
        //    myLinearGradientBrush.EndPoint = new Point(0.5, 1);
        //    myLinearGradientBrush.GradientStops.Add(
        //        new GradientStop(Colors.DarkBlue, 0.0));
        //    myLinearGradientBrush.GradientStops.Add(
        //        new GradientStop(Colors.LightSteelBlue, 0.3));
        //    myLinearGradientBrush.GradientStops.Add(
        //       new GradientStop(Colors.White, 0.5));
        //    myLinearGradientBrush.GradientStops.Add(
        //        new GradientStop(Colors.LightSteelBlue, 0.7));
        //    myLinearGradientBrush.GradientStops.Add(
        //        new GradientStop(Colors.DarkBlue, 1.0));
        //    return myLinearGradientBrush;
        //}

        //Creacion de botones de proveedores
        public void GenerarBotones()
        {
            try

            {
                while (sp_categorias.Children.Count > 0)
                {
                    sp_categorias.Children.RemoveAt(sp_categorias.Children.Count - 1);
                }
                estilo = this.FindResource("botonazul") as System.Windows.Style;

                List<Categoria> categorias = new List<Categoria>();
                categorias = unit.RepositorioCategoria.ObtenerTodo();
                for (int i = 0; i < categorias.Count; i++)
                {
                    Button n = new Button();
                    
                    n.Content = categorias[i].Nombre;
                    n.Height = 40;
                    n.Width = 130;
                    n.Margin = new Thickness(2);
                    n.Click += categoria_click;
                    n.Style = estilo;
                    sp_categorias.Children.Add(n);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        //Click  en una categoria y creacion de botones de productos
        private void categoria_click(object sender, RoutedEventArgs e)
        {

            List<Material> listmateriales = new List<Material>();
            Categoria cat = new Categoria();

            estilo = this.FindResource("botonproductosi") as System.Windows.Style;
            System.Windows.Style estilo2 = this.FindResource("botonproductono") as System.Windows.Style;

            this.sp_materiales.Children.Clear();
            var aux = e.OriginalSource;
            if (aux.GetType() == typeof(Button))
            {
                var a = sender as Button;
                cat = unit.RepositorioCategoria.ObtenerUno(d => d.Nombre.Equals(a.Content.ToString()));
                listmateriales = unit.RepositorioMaterial.ObtenerVarios(c => c.CategoriaId.Equals(cat.CategoriaId));

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
                        b.Style = estilo;
                    }
                    else
                    {
                        b.Style = estilo2;
                    }

                    this.sp_materiales.Children.Add(b);
                }
            }
        }

        //Cuando clickamos a un producto para añadirlo a lineaventa
        private void producto_click(object sender, RoutedEventArgs e)
        {
            if (fabricacionaplicada == true)
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
                        materialencontrado = false;
                        foreach (var item in listamateriales)
                        {
                            if (item.MaterialId == material.MaterialId)
                            {
                                materialencontrado = true;
                                item.Cantidad++;
                                material.Calcularpreciototal();
                            }
                        }
                        if (materialencontrado == false)
                        {
                            material.Cantidad = 1;
                            material.PrecioTotal = material.Precio;
                            listamateriales.Add(material);
                        }
                        //fabricacion.Materiales = listamateriales;

                        dg_material_aplicado.ItemsSource = "";
                        dg_material_aplicado.ItemsSource = listamateriales;
                    }
                }
            }
            else { MessageBox.Show("Tiene que estar en una fabricacion", "ERROR", MessageBoxButton.OK, MessageBoxImage.Stop); }
        }

        //Clickar en delete en datagrid
        public void Click_delete(object sender, RoutedEventArgs e)
        {
            try
            {
                if (MessageBox.Show("¿Desea eliminar el material de la fabricación?", "Cancelar", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    Material materialaux = listamateriales.ElementAt(dg_material_aplicado.SelectedIndex);
                    material = unit.RepositorioMaterial.ObtenerUno(c => c.MaterialId==materialaux.MaterialId);
                    material.Stock--;
                    unit.RepositorioMaterial.Actualizar(materialaux);
                    if (listamateriales.ElementAt(dg_material_aplicado.SelectedIndex).Cantidad == 1)
                    {
                        listamateriales.RemoveAt(dg_material_aplicado.SelectedIndex);
                    }
                    else { listamateriales.ElementAt(dg_material_aplicado.SelectedIndex).Cantidad--; }
                    //listamateriales.Add(material);
                    //fabricacion.Materiales = listamateriales;
                    dg_material_aplicado.ItemsSource = "";
                    dg_material_aplicado.ItemsSource = listamateriales;

                }
                else
                {
                    MessageBox.Show("Eliminación cancelada");
                }
            }
            catch (Exception)
            {
            }

        }

        //Crear Factura
        public void CrearRecibo(Fabricacion fabricacion)
        {   
            /*siempre se mira si existe*/
            String factura = "Lista de Materiales de Fabricacion N " + fabricacion.FabricacionId + ".txt";
            bool borrar = true;

            if (!File.Exists(factura))
            {
                borrar = true;
                EscribirRecibo(fabricacion, factura,borrar);
            }

            else
            {
                if (MessageBox.Show("¿Desea sobreescribir la factura?", "Cancelar", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    borrar = false;
                EscribirRecibo(fabricacion, factura, borrar);
                }
                else
                {
                    MessageBox.Show("Sobreescritura cancelada");
                }
            }
        }

        //creacion de txt de factura
        private void EscribirRecibo(Fabricacion fabricacion, string factura, bool borrar)
        {
            StreamWriter escritura;
            escritura = new StreamWriter(factura, borrar, Encoding.Default);
            double total = 0;
            producto = new Producto();
            producto = unit.RepositorioProducto.ObtenerUno(c => c.ProductoId == fabricacion.ProductoId);
            Material montaje = new Material();

            escritura.WriteLine("  Factura Nº" + fabricacion.FabricacionId+" con fecha "+DateTime.Today);
            escritura.WriteLine("");
            escritura.WriteLine("");

            escritura.WriteLine("  Producto -------" + fabricacion.Productos.Nombre);
            escritura.WriteLine("");
            escritura.WriteLine("  Cliente -------" + fabricacion.Productos.Clientes.Nombre + " " + fabricacion.Productos.Clientes.Apellidos);
            escritura.WriteLine("");
            escritura.WriteLine("---------------------------------------------------------------------------------------------------------------------------------");
            escritura.WriteLine("");
            escritura.WriteLine("Listado de productos");
            escritura.WriteLine("");

            foreach (var item in fabricacion.Materiales)
            {
                if (item.Nombre != "Montaje")
                {
                    escritura.WriteLine(item.Cantidad + " Articulo------" + item.Nombre + escribirespaciosenblanco(item.Nombre.Length) + item.PrecioTotal + "€");
                }
                else
                {
                    montaje = item;
                }
                total = total + item.PrecioTotal;
            }
            escritura.WriteLine("");
            escritura.WriteLine(montaje.Cantidad +" Servicios-----Montaje" + escribirespaciosenblanco(7) + montaje.PrecioTotal + "€");
            if (producto.Precio!=total)
            {
                if (MessageBox.Show("¿El precio del producto y el total de recibo no coinciden, quiere modificarlo?", "Cancelar", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    producto.Precio = total;
                    unit.RepositorioProducto.Actualizar(producto);
                }
                else
                {
                    MessageBox.Show("Precio de producto no modificado");
                }
            }
            escritura.WriteLine("");
            escritura.WriteLine(escribirespaciosenblanco(0)+"   Total -------" + producto.Precio+ "€");
            escritura.WriteLine("");
            escritura.WriteLine("---------------------------------------------------------------------------------------------------------------------------------");
            escritura.WriteLine("");
            escritura.WriteLine("  Número de trabajadores durante el proceso de fabricación --------" + fabricacion.Empleados.Count);
            foreach (var item in fabricacion.Empleados)
            {
                escritura.WriteLine("");
                escritura.WriteLine("  Trabajador --------" + item.Nombre + " "+ item.Apellidos);
            }
           


            /*escritura de lineas con WriteLine*/
            /*IMPORTANTE al acabar de escribir el txt*/
            escritura.Close();
            Process proceso = new Process();
            proceso.StartInfo.FileName = factura;
            proceso.Start();
        }

        //Escribir espacios en blanco
        private string escribirespaciosenblanco(int numero)
        {
            string espacios = "";
            numero = 105 - numero;
            for (int i= 0; i < numero; i++)
            {
                espacios = espacios + " ";
            }
            

            return espacios;
        }

        //clic en aplicar fabricacion
        private void bt_aplicar_fabricacion_Click(object sender, RoutedEventArgs e)
        {
            listamateriales = new List<Material>();
            dg_material_aplicado.ItemsSource = "";

            fabricacion = new Fabricacion();
            fabricacion = listafabricaciones.Where(c => c.FabricacionId == fabricacioneseleccionado).FirstOrDefault();
            cliente = unit.RepositorioCliente.ObtenerUno(c => c.ClienteId == fabricacion.ClienteId);

            foreach (var item in fabricacion.Materiales)
            {
                listamateriales.Add(item);
            }
            
            dg_material_aplicado.ItemsSource = listamateriales;
            bt_aplicarcambios.Visibility = Visibility.Visible;
            bt_generar_materiales.Visibility = Visibility.Visible;
            fabricacionaplicada = true;
        }

        //Aplicar cambios en fabricacion
        private void bt_aplicarcambios_Click(object sender, RoutedEventArgs e)
        {
            fabricacion.Materiales = listamateriales;
            unit.RepositorioFabricacion.Actualizar(fabricacion);
            dg_material_aplicado.ItemsSource = "";
            RellenarComboboxFabricacionId();
            if (cb_fabricacionid != null && listafabricaciones.Count != 0)
            {
                cb_fabricacionid.SelectedIndex = 0;
                fabricacioneseleccionado = listafabricaciones.ElementAt(cb_fabricacionid.SelectedIndex).FabricacionId;
            }
            fabricacionaplicada = false;
            MessageBox.Show("Cambios aplicados");

        }

        //generar recibo
        private void bt_generar_materiales_Click(object sender, RoutedEventArgs e)
        {
            CrearRecibo(fabricacion);
        }

        #endregion

#region PDF

        //generar factura en pdf
        private void GenerarFactura()
        {
            String imgFile = Environment.CurrentDirectory + @"\Imagenes\iconofactura.png";
            String imgFileAgua = Environment.CurrentDirectory + @"\Imagenes\marca de agua2.png";
            ImageData data = ImageDataFactory.Create(imgFile);
            ImageData dataAgua = ImageDataFactory.Create(imgFileAgua);
            iText.Layout.Element.Image image = new iText.Layout.Element.Image(data);
            iText.Layout.Element.Image imageAgua = new iText.Layout.Element.Image(dataAgua);


            string carpetaDestino = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string nombreArchivo = "Factura" + DateTime.Today.Day.ToString() + "-" + DateTime.Today.Month.ToString() + "-" + DateTime.Today.Year.ToString() + "-"
               + DateTime.Today.Hour.ToString() + ";" + DateTime.Today.Minute.ToString() + "-" + cliente.Apellidos + cliente.Nombre;

            if (!File.Exists(nombreArchivo))
            {
                string archivo = System.IO.Path.Combine(carpetaDestino, nombreArchivo + ".pdf");

                try
                {
                    using (var writer = new PdfWriter(archivo))
                    {
                        using (var pdf = new PdfDocument(writer))
                        {
                            var doc = new Document(pdf);

                            //marca de agua
                            imageAgua.SetFixedPosition(0, 0);
                            imageAgua.ScaleToFit(595, 842);
                            doc.Add(imageAgua);

                            //Tabla para poder poner la imagen con el titulo en la misma linea
                            iText.Layout.Element.Table t = new iText.Layout.Element.Table(2);
                            Cell cell = new Cell();
                            cell.SetBorder(null);
                            iText.Layout.Element.Paragraph p = new iText.Layout.Element.Paragraph();
                            iText.Layout.Element.Paragraph c = new iText.Layout.Element.Paragraph();
                            t.SetBorder(null);
                            p.SetFontColor(iText.Kernel.Colors.ColorConstants.RED);
                            p.SetBold();
                            p.SetFontSize(20);
                            p.SetUnderline();
                            p.Add("Custom Computer");
                            c.SetFontSize(10);
                            c.Add("\nDavid Blanco Cortiñas");
                            c.Add("\nCIF G-541565487");
                            c.Add("\n" + @" Direccion: C\ lepando Nº3");
                            cell.Add(p);
                            cell.Add(c);
                            t.AddCell(cell);

                            Cell cell2 = new Cell();
                            cell2.SetBorder(null);
                            image.SetMarginLeft(280);
                            image.SetHeight(40);
                            image.SetWidth(40);
                            cell2.Add(image);
                            t.AddCell(cell2);
                            doc.Add(t);

                            //Cabeceras
                            iText.Layout.Element.Paragraph v = new iText.Layout.Element.Paragraph();
                            v.SetFontSize(13);
                            v.SetBold();
                            v.SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER);
                            v.Add("\nDatos del vendedor, cliente y producto");
                            doc.Add(v);

                            //Datos vendedor 
                            iText.Layout.Element.Table t3 = new iText.Layout.Element.Table(2);
                            t3.SetBorder(null);
                            Cell cell4 = new Cell();
                            cell4.SetBorder(null);
                            iText.Layout.Element.Paragraph z = new iText.Layout.Element.Paragraph();
                            z.SetFontSize(12);
                            z.Add("Empleado: " + usuarioactivo.Nombre + " " + usuarioactivo.Apellidos + "\n");
                            z.Add("Fecha: " + producto.FechaVenta.ToShortDateString());
                            cell4.Add(z);
                            t3.AddCell(cell4);

                            Cell cell5 = new Cell();
                            cell5.SetBorder(null);
                            iText.Layout.Element.Paragraph z1 = new iText.Layout.Element.Paragraph();
                            z1.SetFontSize(12);
                            z1.SetMarginLeft(210);
                            z1.Add("Cliente: " + cliente.Nombre + " " + cliente.Apellidos + "\n");
                            z1.Add("NIF: " + cliente.NIF);
                            if (producto.Vendido == true)
                            {
                                z1.Add("Producto entregado: " + "'" + producto.Nombre + "'" + "\n");
                            }
                            else
                            {
                                z1.Add("Producto no entregado " + "'" + producto.Nombre + "'" + "\n");
                            }

                            z1.Add("Precio Final: " + producto.Precio + " €");
                            cell5.Add(z1);
                            t3.AddCell(cell5);
                            doc.Add(t3);

                            iText.Layout.Element.Paragraph r = new iText.Layout.Element.Paragraph();
                            r.SetFontSize(13);
                            r.SetBold();
                            r.SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER);
                            r.Add("\nDetalle de la compra");
                            doc.Add(r);

                            //Tabla datos venta
                            iText.Layout.Element.Paragraph l;
                            iText.Layout.Element.Table t2 = new iText.Layout.Element.Table(5);
                            t2.SetWidth(520);
                            t2.SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER);

                            Cell cell3 = new Cell();
                            l = new iText.Layout.Element.Paragraph();
                            l.SetBold();
                            l.Add("Cantidad");
                            cell3.SetBackgroundColor(iText.Kernel.Colors.WebColors.GetRGBColor("#DDDDDD"));
                            cell3.Add(l);

                            cell3.SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER);

                            Cell cell6 = new Cell();
                            l = new iText.Layout.Element.Paragraph();
                            l.SetBold();
                            l.Add("Concepto");
                            cell6.SetBackgroundColor(iText.Kernel.Colors.WebColors.GetRGBColor("#DDDDDD"));
                            cell6.Add(l);
                            cell6.SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER);

                            Cell cell7 = new Cell();
                            l = new iText.Layout.Element.Paragraph();
                            l.SetBold();
                            l.Add("Proveedor");
                            cell7.SetBackgroundColor(iText.Kernel.Colors.WebColors.GetRGBColor("#DDDDDD"));
                            cell7.Add(l);
                            cell7.SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER);

                            Cell cell8 = new Cell();
                            l = new iText.Layout.Element.Paragraph();
                            l.SetBold();
                            l.Add("Precio/unidad");
                            cell8.SetBackgroundColor(iText.Kernel.Colors.WebColors.GetRGBColor("#DDDDDD"));
                            cell8.Add(l);
                            cell8.SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER);

                            Cell cell9 = new Cell();
                            l = new iText.Layout.Element.Paragraph();
                            l.SetBold();
                            l.Add("Precio Total");
                            cell9.SetBackgroundColor(iText.Kernel.Colors.WebColors.GetRGBColor("#DDDDDD"));
                            cell9.Add(l);
                            cell9.SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER);

                            t2.AddHeaderCell(cell3);
                            t2.AddHeaderCell(cell6);
                            t2.AddHeaderCell(cell7);
                            t2.AddHeaderCell(cell8);
                            t2.AddHeaderCell(cell9);

                            Material montaje = new Material();
                            foreach (var item in fabricacion.Materiales)
                            {
                                iText.Layout.Element.Paragraph nombreproducto = new iText.Layout.Element.Paragraph();
                                nombreproducto.SetFontColor(iText.Kernel.Colors.ColorConstants.BLUE);
                                nombreproducto.Add(item.Nombre);
                                iText.Layout.Element.Paragraph precioproductototal = new iText.Layout.Element.Paragraph();
                                precioproductototal.SetBold();
                                precioproductototal.Add(item.PrecioTotal + " €");
                                if (item.Nombre != "Montaje")
                                {
                                    t2.AddCell(item.Cantidad.ToString());
                                    t2.AddCell(nombreproducto);
                                    t2.AddCell(item.Proveedores.Nombre);
                                    t2.AddCell(item.Precio.ToString());
                                    t2.AddCell(precioproductototal);
                                }
                                else { montaje = item; }
                            }
                       
                            for (int i = 0; i < 5; i++){ t2.AddCell(""); }

                            //mano de obra o descuento
                            iText.Layout.Element.Paragraph nombremanodeobra = new iText.Layout.Element.Paragraph();
                            nombremanodeobra.SetFontColor(iText.Kernel.Colors.ColorConstants.BLUE);
                            nombremanodeobra.Add(montaje.Nombre);
                            iText.Layout.Element.Paragraph preciototalmontaje = new iText.Layout.Element.Paragraph();
                            preciototalmontaje.SetBold();
                            preciototalmontaje.Add(montaje.PrecioTotal + " €");
                            t2.AddCell(montaje.Cantidad.ToString());
                            t2.AddCell(nombremanodeobra);
                            t2.AddCell(montaje.Proveedores.Nombre);
                            t2.AddCell(montaje.Precio.ToString());
                            t2.AddCell(preciototalmontaje);
                            

                            doc.Add(t2);

                            //Precio total e iva

                            iText.Layout.Element.Table t4 = new iText.Layout.Element.Table(2);
                            t4.SetMarginTop(50);
                            t4.SetHorizontalAlignment(iText.Layout.Properties.HorizontalAlignment.RIGHT);
                            Cell cellivaytotal = new Cell();
                            iText.Layout.Element.Paragraph a = new iText.Layout.Element.Paragraph();
                            a.SetBold();
                            a.SetFontSize(12);
                            //calcular iva
                            double iva= producto.Precio*0.21;

                            a.Add("Base imponible            " + (producto.Precio - iva) + " €" + "\n");
                            a.Add("21% IVA:                  " + iva +" €"+"\n");
                            a.Add("Total factura :         " + producto.Precio + " €" + "\n");
                            cellivaytotal.Add(a);
                            t4.AddCell(cellivaytotal);

                            doc.Add(t4);

                            doc.Close();

                            Process.Start(archivo);
                        }
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show("Pdf con mismo nombre abierto. Cierrelo primero.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }






        #endregion
       
    }
}
