using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Inventario
{
    public partial class UserModule : Form
    {
        SqlConnection con=new SqlConnection(@"Data Source=PAPU\SQLEXPRESS;Initial Catalog=dbIMS;Integrated Security=True;Connect Timeout=30");
        SqlCommand cm = new SqlCommand();
        public UserModule()
        {
            InitializeComponent();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            this.Dispose(); 
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtps.Text != txtRepass.Text)
                {
                    MessageBox.Show("La contraseñas no coinciden", "Peligro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                if (MessageBox.Show("¿Estas seguro que quieres guardar a este usuario?", "Guardar Registro", MessageBoxButtons.YesNo,MessageBoxIcon.Question)==DialogResult.Yes)
                {
                    cm = new SqlCommand("INSERT INTO tbUser(Username,Fullname,Password,Phone)VALUES(@Username,@Fullname,@Password,@Phone)", con);
                    cm.Parameters.AddWithValue("@Username",txtuser.Text);
                    cm.Parameters.AddWithValue("@Fullname", txtname.Text);
                    cm.Parameters.AddWithValue("@Password", txtps.Text);
                    cm.Parameters.AddWithValue("@Phone", txtphone.Text);
                    con.Open();
                    cm.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("Usuario guardado correctamente");
                    Clear();    
                }
            }
            catch (Exception ex) 
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            Clear();
            btnClear.Enabled = true;
            btnUpdate.Enabled = false;
        }
        public void Clear() { 
        txtuser.Clear();   
        txtname.Clear();
        txtps.Clear();
        txtphone.Clear();
        txtRepass.Clear();  
     
        }

        private void txtuser_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtname_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtps_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtphone_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtps.Text != txtRepass.Text)
                {
                    MessageBox.Show("La contraseñas no coinciden", "Peligro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                if (MessageBox.Show("¿Estas seguro que quieres actualizar a este usuario?", "Actualizar Registro", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    cm = new SqlCommand("UPDATE tbUser SET Fullname=@Fullname, Password=@Password, Phone=@Phone WHERE Username LIKE '"+ txtuser.Text +"'", con);
                   
                    cm.Parameters.AddWithValue("@Fullname", txtname.Text);
                    cm.Parameters.AddWithValue("@Password", txtps.Text);
                    cm.Parameters.AddWithValue("@Phone", txtphone.Text);
                    con.Open();
                    cm.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("Usuario actualizado correctamente");
                    this.Dispose(); 
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void UserModule_Load(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void txtRepass_TextChanged(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }
    }
}
