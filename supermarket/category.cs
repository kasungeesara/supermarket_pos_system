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

namespace supermarket
{
    public partial class category : Form
    {
        public category()
        {
            InitializeComponent();
        }
      
        private void label7_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\DELL\OneDrive\Documents\marketdb.mdf;Integrated Security=True;Connect Timeout=30");
        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                string query = "INSERT INTO categoryTb VALUES('" + catid.Text + "','" + catname.Text + "','" + catdes.Text + "')";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("category added succesfully");
                con.Close();
                populate();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void populate()
        {
            con.Open();
            string query = "SELECT *FROM categoryTb";
            SqlDataAdapter sda = new SqlDataAdapter(query,con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            catdgv.DataSource = ds.Tables[0];
            con.Close();
        }
        private void category_Load(object sender, EventArgs e)
        {
            populate();
        }

        private void catdgv_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            catid.Text = catdgv.SelectedRows[0].Cells[0].Value.ToString();
            catname.Text = catdgv.SelectedRows[0].Cells[1].Value.ToString();
            catdes.Text = catdgv.SelectedRows[0].Cells[2].Value.ToString();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                if (catid.Text == "")
                {
                    MessageBox.Show("select the category to delete");
                }
                else
                {
                    con.Open();
                    string query = "delete from categoryTb where catid=" + catid.Text + "";
                    SqlCommand cmd = new SqlCommand(query,con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("delete sucessfuly");
                    con.Close();
                    populate();
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                if (catid.Text == "" || catname.Text == "" || catdes.Text == "")
                {
                    MessageBox.Show("missing information");
                }
                else
                {
                    con.Open();
                    string query = "update categoryTb set catname='" + catname.Text + "',catdep='" + catdes.Text + "'where catid=" + catid.Text + ";";
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

        private void button1_Click(object sender, EventArgs e)
        {
            product prod = new product();
            prod.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            seller sel = new seller();
            sel.Show();
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
