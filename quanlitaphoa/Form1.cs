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

namespace quanlitaphoa
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        SqlConnection con;
        SqlCommand cmd;
        private void concection() {
            String strcon = "server = LAPTOP-CREE9M5V\\SQLEXPRESS;database = QLCH;integrated security = true";
            con = new SqlConnection(strcon);
        }
        private void btnthemmoi_Click(object sender, EventArgs e)
        {
            string itemId = txtma.Text;
            string itemName = txtten.Text;

            if (IsItemExists(itemId))
            {
                MessageBox.Show("Mat hang nay da ton tai!");
                txtma.Clear();
                txtten.Clear();
                txtgia.Clear();
                txtsoluong.Clear();
            }
            else
            {
                AddNewItemToDatabase(itemId, itemName);
                MessageBox.Show("Them mat hang thanh cong!");
            }
        }
        private bool IsItemExists(string itemId)
        {
            bool itemExists = false;
            string connectionString = "server = LAPTOP-CREE9M5V\\SQLEXPRESS;database = QLCH;integrated security = true";
            string query = "select count(*) from MatHang where id_mh = @ItemId";
            using (SqlConnection connection = new SqlConnection(connectionString))
                using(SqlCommand command = new SqlCommand(query, connection))
            {
                connection.Open();
                command.Parameters.AddWithValue("@ItemId", itemId);

                int itemCount = (int)command.ExecuteScalar();
                if(itemCount > 0)
                {
                    itemExists = true;
                }
                connection.Close();
            }
            return itemExists;

        }
        private void AddNewItemToDatabase(string itemId, string itemName)
        {
            concection();
            SqlCommand cmd = new SqlCommand("insert into MatHang values('" + txtma.Text + "','" + txtten.Text + "','" + Convert.ToDouble(txtgia.Text) + "','" + Convert.ToInt16(txtsoluong.Text) + "')", con);
            con.Open();
            int ret = cmd.ExecuteNonQuery();
            if (ret == 1)
                MessageBox.Show("Tao Them Cho may roi!");
            con.Close();
            loaddata();
            txtma.Clear();
            txtten.Clear();
            txtgia.Clear();
            txtsoluong.Clear();
        }
        private void btncapnhat_Click(object sender, EventArgs e)
        {

            concection();
            SqlCommand cmd = new SqlCommand("update MatHang set ten_mh='" + txtten.Text + "',thanh_tien='" + Convert.ToDouble(txtgia.Text) + "',Soluong='" + Convert.ToInt16(txtsoluong.Text) + "'where id_mh='" + txtma.Text + "'", con);
            con.Open();
            int ret = cmd.ExecuteNonQuery();
            if (ret == 1)
                MessageBox.Show("Cap nhat xong roi may!");
            con.Close();
            loaddata();
            txtma.Clear();
            txtten.Clear();
            txtgia.Clear();
            txtsoluong.Clear();
        }

        private void btnxoa_Click(object sender, EventArgs e)
        {
            concection();
            SqlCommand cmd = new SqlCommand("delete from MatHang where id_mh= '" + txtma.Text + "'", con);
            con.Open();
            int ret = cmd.ExecuteNonQuery();
            if (ret == 1)
                MessageBox.Show("Khong thich xoa day^^");
            con.Close();
            loaddata();
            txtma.Clear();
            txtten.Clear();
            txtgia.Clear();
            txtsoluong.Clear();
        }

        private void btntimkiem_Click(object sender, EventArgs e)
        {
            concection();
            SqlDataAdapter da = new SqlDataAdapter("select * from MatHang where ten_mh LIKE '%" + txttimkiem.Text + "%' ", con);
            DataTable dt = new DataTable();
            con.Open();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            con.Close();
            txttimkiem.Clear();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            txtma.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            txtten.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            txtgia.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            txtsoluong.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
        }

        private void loaddata()
        {
            concection();
            SqlDataAdapter da = new SqlDataAdapter("select * from MatHang", con);
            DataTable dt = new DataTable();
            con.Open();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            con.Close();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            loaddata();
        }

    }
}
