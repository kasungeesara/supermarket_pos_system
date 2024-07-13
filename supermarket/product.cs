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
    public partial class product : Form
    {
        public product()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\DELL\OneDrive\Documents\marketdb.mdf;Integrated Security=True;Connect Timeout=30");
        private void label7_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                if (proid.Text == "")
                {
                    MessageBox.Show("select the product to delete");
                }
                else
                {
                    con.Open();
                    string query = "delete from productTb where proid="+proid.Text+"";
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
        private void fillcombo()
        {
            //this method is use to bind the combobox to database
            con.Open();
            SqlCommand cmd = new SqlCommand("SELECT catname FROM categoryTb", con);
            SqlDataReader rdr;
            rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("catname", typeof(string));
            dt.Load(rdr);
            selcat.ValueMember = "catname";
            selcat.DataSource= dt;
            comboBox2.ValueMember = "catname";
            comboBox2.DataSource= dt;
            con.Close();
        }
        private void product_Load(object sender, EventArgs e)
        {
            fillcombo();
            populate();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            category cat = new category();
            cat.Show();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                string query = "INSERT INTO productTb VALUES('" + proid.Text + "','" + proname.Text + "'," + proqty.Text + "," + proprice.Text + ",'" + selcat.SelectedValue.ToString() + "')";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("product added succesfully");
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
                if (proid.Text == "")
                {
                    MessageBox.Show("missing products");
                }
                else
                {
                    con.Open();
                    string query = "UPDATE productTb SET proname='" + proname.Text + "',proqty='" + proqty.Text + "',proprice='" + proprice.Text + "',procat='" + selcat.SelectedValue.ToString() + "'WHERE proid=" + proid.Text + ";";
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

        private void prodgv_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            proid.Text = prodgv.SelectedRows[0].Cells[0].Value.ToString();
            proname.Text = prodgv.SelectedRows[0].Cells[1].Value.ToString();
            proqty.Text = prodgv.SelectedRows[0].Cells[2].Value.ToString();
            proprice.Text = prodgv.SelectedRows[0].Cells[3].Value.ToString();
            selcat.SelectedValue = prodgv.SelectedRows[0].Cells[4].Value.ToString();
        }
        private void populate()
        {
            con.Open();
            string query = "SELECT *FROM productTb";
            SqlDataAdapter sda = new SqlDataAdapter(query, con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            prodgv.DataSource = ds.Tables[0];
            con.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            seller sel = new seller();
            sel.Show();
            this.Hide();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            //populate();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }

        private void comboBox2_SelectionChangeCommitted(object sender, EventArgs e)
        {
            con.Open();
            string query = "select * from productTb where procat='" + comboBox2.SelectedValue.ToString() + "'";
            SqlDataAdapter sda = new SqlDataAdapter(query,con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            prodgv.DataSource= ds.Tables[0];
            con.Close();
        }

        private void selcat_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {
            populate();
        }

        private void label9_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 log = new Form1();
            log.Show();
        }
    }
}
