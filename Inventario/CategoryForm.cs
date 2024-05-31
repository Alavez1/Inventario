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

namespace Inventario
{
    public partial class CategoryForm : Form
    {

        SqlConnection con = new SqlConnection(@"Data Source=PAPU\SQLEXPRESS;Initial Catalog=dbIMS;Integrated Security=True;Connect Timeout=30");
        SqlCommand cm = new SqlCommand();
        SqlDataReader dr;
        public CategoryForm()
        {
            InitializeComponent();
            LoadCategory();
        }
        public void LoadCategory()
        {
            int i = 0;
            dgvCategory.Rows.Clear();
            cm = new SqlCommand("SELECT * FROM tbCategory", con);
            con.Open();
            dr = cm.ExecuteReader();
            while (dr.Read())
            {
                i++;
                dgvCategory.Rows.Add(i, dr[0].ToString(), dr[1].ToString());
            }
            dr.Close();
            con.Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            CategoryModule formModule = new CategoryModule();
            formModule.btnSave.Enabled = true;
            formModule.btnUpdate.Enabled=false;
            formModule.ShowDialog();    
            LoadCategory();

        }

        private void dgvCategory_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string colName = dgvCategory.Columns[e.ColumnIndex].Name;
            if (colName == "Edit")
            {
                CategoryModule CategoryModule = new CategoryModule();
                CategoryModule.lblcatid.Text = dgvCategory.Rows[e.RowIndex].Cells[1].Value.ToString();
                CategoryModule.txtCatName.Text = dgvCategory.Rows[e.RowIndex].Cells[2].Value.ToString();


                CategoryModule.btnSave.Enabled = false;
                CategoryModule.btnUpdate.Enabled = true;
                CategoryModule.ShowDialog();

            }
            else if (colName == "Delete")
            {

                if (MessageBox.Show("¿Estas seguro en borrar esta categoria?", "Borrar categoria", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    con.Open();
                    cm = new SqlCommand("DELETE FROM tbCategory WHERE catid LIKE '" + dgvCategory.Rows[e.RowIndex].Cells[1].Value.ToString() + "'", con);
                    cm.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("El registro a sido borrado exitosamente");

                }

            }
            LoadCategory();
        }

        private void CategoryForm_Load(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
