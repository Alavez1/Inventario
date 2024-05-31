using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.SqlClient;
using System.Windows.Forms;
namespace Inventario
{

    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();//Inicia el formulario
        }
        public void logins()//método que verifica el login
        {
            try//Estructura de control para captar errores
            {
                //hace la conexión con la BD
                SqlConnection Conexion = new SqlConnection("Server=PAPU\\SQLEXPRESS; DataBase=codigos; Integrated Security = true");
                {   
                    Conexion.Open();//Abre la conexión
                    //comando para acceder a los usuarios
                    using (SqlCommand cmd = new SqlCommand("SELECT usuario, pass FROM usuarios WHERE usuario = '"+ txtUser.Text +"'AND pass='"+txtPass.Text+"'", Conexion))
                    {
                        SqlDataReader dr = cmd.ExecuteReader();//Lee las columnas de la BD
                        if (dr.Read())//si encuentra un usuario hace esto
                        {
                            MessageBox.Show("Login Exitoso");//Mensaje en pantalla que manda al otro formulario
                        }
                        else//en caso de que no encuentre el usuario 
                        {
                            MessageBox.Show("Datos Erroneos");//Mensaje en pantalla que indica que los datos no son correctos
                        }
                    }
                }            
            }
            catch(Exception ex) //Guardamos el error en una variable
            {
                MessageBox.Show(ex.ToString());//Mensaje que muestra el posible error y lo convierte en string
            }
        }

        private void button1_Click(object sender, EventArgs e)//Evento click CRM
        {
           Main main = new Main();//Declaramos una variable llamada main y le asigna una nueva instancia de la clase Main
            main.Show();//Muestra el formulario en la pantalla
            this.Hide();//Llama al método Hide() en el objeto actualA
        }

        private void button2_Click(object sender, EventArgs e)//Evento click ERP
        {
            erp erpY = new erp();//Declaramos la variable y le asignamos una nueva instancia
            erpY.Show();//Muestra el formulario en pantalla 
            this.Hide();//Llama al método Hide() en el objeto actual
        }
    }
}
