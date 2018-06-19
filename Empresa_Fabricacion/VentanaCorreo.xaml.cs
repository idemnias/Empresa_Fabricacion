using Empresa_Fabricacion.DAL;
using Empresa_Fabricacion.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using System.Windows.Shapes;

namespace Empresa_Fabricacion
{
    /// <summary>
    /// Lógica de interacción para correo.xaml
    /// </summary>
    public partial class VentanaCorreo : Window
    {
        Empleado empleado = new Empleado();
        UnitOfWork unit = new UnitOfWork();
        public VentanaCorreo()
        {
            InitializeComponent();
        }


        //Método de enviar correo
        private void EnviarCorreo(Empleado empleado)
        {
            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient smtserver = new SmtpClient("smtp.gmail.com", 587);

                mail.From = new MailAddress("CustomComputerdam@gmail.com", "Custom Computer", Encoding.UTF8);

                mail.Subject = "Recordatorio de usuario y contraseña";
                mail.SubjectEncoding = Encoding.UTF8;
                mail.Body = "<body><h1>Usuario:</h1></p><h2>"+empleado.Usuario+"</h2>" +
                    "<h2>Contraseña:</h2></p><h3>"+empleado.Contraseña+"</h3></body>";
                mail.IsBodyHtml = true;
                mail.BodyEncoding = Encoding.UTF8;
                mail.To.Add(empleado.Correo);

                smtserver.Credentials = new System.Net.NetworkCredential("CustomComputerdam@gmail.com", "Abcd1234.");
                smtserver.EnableSsl = true;
                smtserver.Send(mail);

            }
            catch (Exception ex)
            {
                Debug.WriteLine("fallo");
            }
        }

        //clic en enviar correo
        private void bt_enviarcorreo_Click(object sender, RoutedEventArgs e)
        {
            empleado = new Empleado();
            empleado = unit.RepositorioEmpleado.ObtenerUno(c => c.Correo.Equals(tb_m_correo.Text));
            if (empleado!=null)
            {
                EnviarCorreo(empleado);
                MessageBox.Show("Mensaje enviado");
            }
            else
            {
                MessageBox.Show("No existe ese correo en nuestra base de datos");
            }
            this.Close();
        }
    }
}
