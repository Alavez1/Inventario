using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Inventario
{
    public partial class User : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=PAPU\SQLEXPRESS;Initial Catalog=dbIMS;Integrated Security=True;Connect Timeout=30");
        SqlCommand cm = new SqlCommand();
        SqlDataReader dr;
        public User()
        {
            InitializeComponent();
            LoadUser();
        }
        public void LoadUser()
        {
            int i = 0;
            dgvUser.Rows.Clear();
            cm = new SqlCommand("SELECT * FROM tbUser", con);
            con.Open();
            dr = cm.ExecuteReader();
            while (dr.Read())
            {
                i++;
                dgvUser.Rows.Add(i,dr[0].ToString(), dr[1].ToString(), dr[2].ToString(), dr[3].ToString());
            }
            dr.Close();    
            con.Close();    
        }

        private void dgvUser_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string colName = dgvUser.Columns[e.ColumnIndex].Name;
            if (colName == "Edit")
            {
                UserModule userModule = new UserModule();
                userModule.txtuser.Text = dgvUser.Rows[e.RowIndex].Cells[1].Value.ToString();
                userModule.txtname.Text = dgvUser.Rows[e.RowIndex].Cells[2].Value.ToString();
                userModule.txtps.Text = dgvUser.Rows[e.RowIndex].Cells[3].Value.ToString();
                userModule.txtphone.Text = dgvUser.Rows[e.RowIndex].Cells[4].Value.ToString();

                userModule.btnSave.Enabled = false;
                userModule.btnUpdate.Enabled = true;
                userModule.txtuser.Enabled = false;
                userModule.ShowDialog();

            }
            else if (colName == "Delete")
            {

                if (MessageBox.Show("¿Estas seguro en borrar este usuario?", "Borrar registro", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    con.Open();
                    cm = new SqlCommand("DELETE FROM tbUser WHERE Username LIKE '" + dgvUser.Rows[e.RowIndex].Cells[1].Value.ToString() + "'", con);
                    cm.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("El registro a sido borrado exitosamente");

                }

            }
            LoadUser();
        }


        private void btnAdd_Click(object sender, EventArgs e)
        {
            UserModule module = new UserModule();
            module.btnSave.Enabled = true;
            module.btnUpdate.Enabled = false;
            module.ShowDialog();
            LoadUser(); 
        }
       
    }
}
