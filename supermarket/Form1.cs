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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public static string sellname = "";
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\DELL\OneDrive\Documents\marketdb.mdf;Integrated Security=True;Connect Timeout=30");
        private void label7_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            forname.Text = "";
            forpass.Text = "";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (forname.Text == "" || forpass.Text == "")
            {
                MessageBox.Show("enter the username and password");
            }
            else
            {
                if (rolecb.SelectedIndex > -1)
                { 
                if (rolecb.SelectedItem.ToString() == "ADMIN")
                {
                    if (forname.Text == "KASUN" && forpass.Text == "2002")
                    {
                            category cat = new category();
                            cat.Show();
                            this.Hide();
                        }
                    else
                    {
                        MessageBox.Show("if you are the admin,enter the correct password and id");
                    }
                }
                    else
                    {
                       // MessageBox.Show("you in the seller section");
                       con.Open();
                        SqlDataAdapter sda = new SqlDataAdapter("select count(8) from sellerTb where sellname ='"+forname.Text+"'and sellpass='"+forpass.Text+"'",con);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        if (dt.Rows[0][0].ToString()=="1")
                        {
                            sellname=forname.Text;
                            selling sell = new selling();
                            sell.Show();
                            this.Hide();
                            con.Close();
                        }
                        else
                        {
                            MessageBox.Show("wrong user name or password");
                        }
                       con.Close();
                    }

                }
                else
                {
                    MessageBox.Show("select a role");
                }
            }
        }
    }
}
