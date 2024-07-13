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

namespace supermarket
{
    public partial class seller : Form
    {
        public seller()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\DELL\OneDrive\Documents\marketdb.mdf;Integrated Security=True;Connect Timeout=30");
        private void label7_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                string query = "INSERT INTO sellerTb VALUES('" + sellid.Text + "','" + sellname.Text + "','" + sellage.Text + "','"+sellphone.Text+"','"+sellpass.Text+"')";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("seller details added succesfully");
                con.Close();
                populate();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                if (sellid.Text == "" || sellname.Text == "" || sellage.Text == "" || sellphone.Text == "" || sellpass.Text == "")
                {
                    MessageBox.Show("missing information");
                }
                else
                {
                    con.Open();
                    string query = "UPDATE sellerTb SET sellname='" + sellname.Text + "',sellage='" + sellage.Text + "',sellphone='" + sellphone.Text + "',sellpass='" + sellpass.Text + "'where sellid=" + sellid.Text + ";";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("update successfuly");
                    con.Close();
                    populate();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                if (sellid.Text == "")
                {
                    MessageBox.Show("select the seller to delete");
                }
                else
                {
                    con.Open();
                    string query = "delete from sellerTb where sellid=" + sellid.Text + "";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("delete sucessfuly");
                    con.Close();
                    populate();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void selldgv_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            sellid.Text = selldgv.SelectedRows[0].Cells[0].Value.ToString();
            sellname.Text = selldgv.SelectedRows[0].Cells[1].Value.ToString();
            sellage.Text = selldgv.SelectedRows[0].Cells[2].Value.ToString();
            sellphone.Text = selldgv.SelectedRows[0].Cells[3].Value.ToString();
            sellpass.Text = selldgv.SelectedRows[0].Cells[4].Value.ToString();
        }
        private void populate()
        {
            con.Open();
            string query = "SELECT *FROM sellerTb";
            SqlDataAdapter sda = new SqlDataAdapter(query, con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            selldgv.DataSource = ds.Tables[0];
            con.Close();
        }
            private void seller_Load(object sender, EventArgs e)
        {
            populate();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //selling sell = new selling();
           // sell.Show();
           // this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            category cat = new category();
            cat.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            product prod = new product();
            prod.Show();
            this.Hide();
        }

        private void label9_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 log = new Form1();
            log.Show();
        }
    }
}
