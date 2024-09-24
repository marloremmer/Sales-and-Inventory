using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SADSIGN_IT401P
{
    public partial class password : Form
    {
        public SqlConnection con;
        
        public password()

        {
            InitializeComponent();
        }

        //Remove the text once the textbox is click
        private void txtPass_Click(object sender, EventArgs e)
        {
            txtPass.Text = string.Empty;
        }
        //validate is the password is correct
        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection con = new SqlConnection(@"Data Source=LAPTOP-VDMJ8H9C\SQLEXPRESS;Initial Catalog=db_Inventory;Integrated Security=True");
                con.Open();
                string str = @"SELECT Pass FROM tbl_Account WHERE Pass= '" + txtPass.Text + "'";
                SqlCommand cmd = new SqlCommand(str, con);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    //call the form of History
                    frmLoginHistory history = new frmLoginHistory();
                    history.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Incorrect Password");
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
            }
        }
    }
}