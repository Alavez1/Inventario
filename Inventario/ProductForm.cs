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
    public partial class ProductForm : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=PAPU\SQLEXPRESS;Initial Catalog=dbIMS;Integrated Security=True;Connect Timeout=30");
        SqlCommand cm = new SqlCommand();
        SqlDataReader dr;
        public ProductForm()
        {
            InitializeComponent();
            Loadproduct();
        }

        public void Loadproduct()
        {
            int i = 0;
            dgvProduct.Rows.Clear();
            cm = new SqlCommand("SELECT * FROM tbProduct WHERE CONCAT(pid, pname,pprice,pdescription,pcategory) LIKE '%"+ txtSearch.Text +"%'" , con);
            con.Open();
            dr = cm.ExecuteReader();

            while (dr.Read())
            {
                i++;
                dgvProduct.Rows.Add(i, dr[0].ToString(), dr[1].ToString(), dr[2].ToString(), dr[3].ToString(), dr[4].ToString(), dr[5].ToString());
            }
            dr.Close();
            con.Close();
        }

            private void btnAdd_Click(object sender, EventArgs e)
        {
            ProductModule formModule = new ProductModule();
            formModule.btnSave.Enabled = true;
            formModule.btnUpdate.Enabled = false;
            formModule.ShowDialog();
            Loadproduct();
        }

        private void dgvProduct_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string colName = dgvProduct.Columns[e.ColumnIndex].Name;
            if (colName == "Edit")
            {
                ProductModule ProducModule = new ProductModule();
                ProducModule.lblPid.Text = dgvProduct.Rows[e.RowIndex].Cells[1].Value.ToString();
                ProducModule.txtPName.Text = dgvProduct.Rows[e.RowIndex].Cells[2].Value.ToString();
                ProducModule.txtPQ.Text = dgvProduct.Rows[e.RowIndex].Cells[3].Value.ToString();
                ProducModule.txtPPrice.Text = dgvProduct.Rows[e.RowIndex].Cells[4].Value.ToString();
                ProducModule.txtPdescription.Text = dgvProduct.Rows[e.RowIndex].Cells[5].Value.ToString();
                ProducModule.comboQty.Text = dgvProduct.Rows[e.RowIndex].Cells[6].Value.ToString();

                ProducModule.btnSave.Enabled = false;
                ProducModule.btnUpdate.Enabled = true;
                ProducModule.ShowDialog();

            }
            else if (colName == "Delete")
            {

                if (MessageBox.Show("¿Estas seguro en borrar este producto?", "Borrar producto", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    con.Open();
                    cm = new SqlCommand("DELETE FROM tbProduct WHERE pid LIKE '" + dgvProduct.Rows[e.RowIndex].Cells[1].Value.ToString() + "'", con);
                    cm.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("El registro a sido borrado exitosamente");

                }

            }
            Loadproduct();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            Loadproduct();
        }
    }
}
