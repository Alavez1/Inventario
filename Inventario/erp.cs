using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Inventario
{

    public partial class erp : Form
    {
        SqlConnection Conexion = new SqlConnection("Server=PAPU\\SQLEXPRESS; DataBase=PRACTICA_ERP; Integrated Security = true");
        SqlCommand cmd;//servirá para ejecutar comandos sql
        SqlDataReader dr;//lee los datos de las tablas 
        public erp()
        {
            InitializeComponent();//inicia el formulario
            this.Text = "Tienda de ropa";//Titulo de formulario
            this.Size = new Size(1200, 800); // Ajusta el tamaño
            //this.Icon = new Icon("C:/Users/52561/Downloads/icons8-wi-fi-directo-64.png/");
        }

        private void erp_Load(object sender, EventArgs e)
        {
            GrafCategorias();
            productosPreferidos();
            DashBoardDatos();//inicia el método cuando se inicie el formulario 
        }
        ArrayList categoria = new ArrayList();//Array list es una clase
        ArrayList CantProd = new ArrayList();
        ArrayList Producto = new ArrayList();
        ArrayList Cant = new ArrayList();
        private void GrafCategorias()
        {

            cmd = new SqlCommand("ProdPorCategoria", Conexion);//hace consultas sql
            cmd.CommandType = CommandType.StoredProcedure;
            Conexion.Open();
            dr = cmd.ExecuteReader();
            while (dr.Read()) //Ejecuta la lectura en las columnas
            {
                categoria.Add(dr.GetString(0));//Extrae la primera columna del SP
                CantProd.Add(dr.GetInt32(1));//Extrae la segunda columna del SP
            }                                              //Horizontal, Vertical
            chartProdxCategoria.Series[0].Points.DataBindXY(categoria, CantProd);
            dr.Close();
            Conexion.Close();
        }

        private void productosPreferidos()
        {
            Producto.Clear();//limpia los datos de producto (ArrayList)
            Cant.Clear();//Limpia los datos de la cantidad (ArrayList)

            cmd = new SqlCommand("ProdPreferidos", Conexion);//Hace la consulta sql creando un objeto
            cmd.CommandType = CommandType.StoredProcedure;//dice que el comando será de tipo procedimiento almacenado
            Conexion.Open();//abre la conexion
            dr = cmd.ExecuteReader();//Ejecuta la conexión que lee
            while (dr.Read())//Mientras haya datos en la columna
            {
                Producto.Add(dr.GetString(0));  //Obtenemos los datos de la primera columna
                Cant.Add(dr.GetInt32(1));//Obtenemos los datos de la segunda columna del sp y cada dato se añade al ArrayList "Cant"
            }
            //Graficamos los datos en dona
            chartProdPreferidos.Series[0].Points.DataBindXY(Producto, Cant);
            dr.Close();
            Conexion.Close();
        }
        private void DashBoardDatos()
        {
            //Le decimos a c# que busque y ejecute el SP dashBoard
            cmd = new SqlCommand("DashBoardDatos", Conexion);//Hace la consulta sql creando un objeto
            cmd.CommandType = CommandType.StoredProcedure;//dice que el comando será de tipo procedimiento almacenado
            //Realizamos la consulta a nuestro SP
            SqlParameter total = new SqlParameter("@totventas", 0);
            //total es parámetro de salida
            total.Direction = ParameterDirection.Output; 
            //añadimos los parámetros de salida del sp 
            SqlParameter nprod = new SqlParameter("@nprod", 0); nprod.Direction = ParameterDirection.Output;
            SqlParameter nmarca = new SqlParameter("@nmarc", 0); nmarca.Direction = ParameterDirection.Output;
            SqlParameter ncategoria = new SqlParameter("@ncateg", 0); ncategoria.Direction = ParameterDirection.Output;
            SqlParameter ncliente = new SqlParameter("@nclient", 0); ncliente.Direction = ParameterDirection.Output;
            SqlParameter nproveedores = new SqlParameter("@nprove", 0); nproveedores.Direction = ParameterDirection.Output;
            SqlParameter nempleados = new SqlParameter("@nemple", 0); nempleados.Direction = ParameterDirection.Output;
            //Referenciamos al total al comando que invoca el SP
            cmd.Parameters.Add(total);
            cmd.Parameters.Add(nprod);
            cmd.Parameters.Add(nmarca);
            cmd.Parameters.Add(ncategoria);
            cmd.Parameters.Add(ncliente);
            cmd.Parameters.Add(nproveedores);
            cmd.Parameters.Add(nempleados);

            Conexion.Open();//abre la conexión
            cmd.ExecuteNonQuery();//Ejecuta la consulta

            lblTotalVentas.Text = "$" + cmd.Parameters["@totVentas"].Value.ToString();//setea el formato para un label
            lblCantCateg.Text = cmd.Parameters["@ncateg"].Value.ToString();
            lblCantMarcas.Text = cmd.Parameters["@nmarc"].Value.ToString();
            lblCantProd.Text = cmd.Parameters["@nprod"].Value.ToString();
            lblCantClient.Text = cmd.Parameters["@nclient"].Value.ToString();
            lblCantEmple.Text = cmd.Parameters["@nemple"].Value.ToString();
            lblCantProve.Text = cmd.Parameters["@nprove"].Value.ToString();

            Conexion.Close();//cierra la conexión


        }

        private void btnBack_Click(object sender, EventArgs e)//Evento click
        {
            Form2 form = new Form2();//Declaramos una variable llamada form y le asigna una nueva instancia de la clase Form2
            form.Show();//Muestra el formulario en la pantalla
            this.Hide();//Llama al método Hide() en el objeto actual
        }
    }
}
