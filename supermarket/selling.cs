using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
//using System.Data.SqlClient;

namespace supermarket
{
    public partial class selling : Form
    {
        public selling()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\DELL\OneDrive\Documents\marketdb.mdf;Integrated Security=True;Connect Timeout=30");
        private void populate()
        {
            con.Open();
            string query = "SELECT proname,proprice FROM productTb";
            SqlDataAdapter sda = new SqlDataAdapter(query, con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            prodgv1.DataSource = ds.Tables[0];
            con.Close();
        }
        private void populatebills()
        {
            con.Open();
            string query = "SELECT * FROM billTb";
            SqlDataAdapter sda = new SqlDataAdapter(query, con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            prodgv2.DataSource = ds.Tables[0];
            con.Close();
        }
        private void selling_Load(object sender, EventArgs e)
        {
            populate();
            populatebills();
            fillcombo();
            sellername.Text = Form1.sellname;
        }
        
        private void label7_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        
        private void prodgv1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            billproname.Text = prodgv1.SelectedRows[0].Cells[0].Value.ToString();
            billproprice.Text = prodgv1.SelectedRows[0].Cells[1].Value.ToString();
            
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            date1.Text=DateTime.Today.Day.ToString() + "/" + DateTime.Today.Month.ToString() + "/" + DateTime.Today.Year.ToString();
        }

        private void button6_Click(object sender, EventArgs e)
        {

        }
        int grdtotal = 0, n = 0;

        private void button4_Click(object sender, EventArgs e)
        {
            if (billid.Text == "")
            {
                MessageBox.Show("missing billid");
            }
            else
            {
                try
                {
                    con.Open();
                    string query = "INSERT INTO billTb VALUES('" + billid.Text + "','" + sellername.Text + "','" + date1.Text + "'," + Rs.Text + ")";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("order added succesfully");
                    con.Close();
                    populate();
                    populatebills();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (printPreviewDialog1.ShowDialog() == DialogResult.OK)
            {
                printDocument1.Print();
            }
        }

        private void prodgv2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawString("KASUN SUPERMARKET", new Font("century gothic", 25, FontStyle.Bold), Brushes.Red, new Point(230));
            e.Graphics.DrawString("Bill Id:  " + prodgv2.SelectedRows[0].Cells[0].Value.ToString(), new Font("century gothic", 20, FontStyle.Bold), Brushes.Blue, new Point(100,70));
            e.Graphics.DrawString("Seller Nmame:  " + prodgv2.SelectedRows[0].Cells[1].Value.ToString(), new Font("century gothic", 20, FontStyle.Bold), Brushes.Blue, new Point(100, 100));
            e.Graphics.DrawString("Bill Date:  " + prodgv2.SelectedRows[0].Cells[2].Value.ToString(), new Font("century gothic", 20, FontStyle.Bold), Brushes.Blue, new Point(100, 130));
            e.Graphics.DrawString("Total Amount:  " + prodgv2.SelectedRows[0].Cells[3].Value.ToString(), new Font("century gothic", 20, FontStyle.Bold), Brushes.Blue, new Point(100, 160));
            e.Graphics.DrawString("powered by kgk", new Font("century gothic", 15, FontStyle.Italic), Brushes.Purple, new Point(270,230));
        }

        private void button7_Click(object sender, EventArgs e)
        {
            populate();
        }

        private void selcat_SelectionChangeCommitted(object sender, EventArgs e)
        {
            con.Open();
            string query = "select proname,proqty from productTb where procat='" + selcat1.SelectedValue.ToString() + "'";
            SqlDataAdapter sda = new SqlDataAdapter(query, con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            prodgv1.DataSource = ds.Tables[0];
            con.Close();
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
            //selcat.ValueMember = "catname";
            //selcat.DataSource = dt;
            selcat1.ValueMember = "catname";
            selcat1.DataSource = dt;
            con.Close();
        }

        private void label9_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 log = new Form1();
            log.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            category cat = new category();
            cat.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (billproname.Text == "" || billproqty.Text == "")

            {
                MessageBox.Show("missing data");
            }
            else
            {


                int total = Convert.ToInt32(billproprice.Text) * Convert.ToInt32(billproqty.Text);
                DataGridViewRow newRow = new DataGridViewRow();
                newRow.CreateCells(prodgv3);
                newRow.Cells[0].Value = n + 1;
                newRow.Cells[1].Value = billproname.Text;
                newRow.Cells[2].Value = billproprice.Text;
                newRow.Cells[3].Value = billproqty.Text;
                newRow.Cells[4].Value = Convert.ToInt32(billproprice.Text) * Convert.ToInt32(billproqty.Text);
                prodgv3.Rows.Add(newRow);
                n++;
                grdtotal = grdtotal + total;
                Rs.Text = "" + grdtotal;
                populate();
            }
        }
    }
}
