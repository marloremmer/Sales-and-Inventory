using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace SADSIGN_IT401P
{
    public partial class frmLoginHistory : Form
    {
        //connection string
        public SqlConnection con = new SqlConnection(@"Data Source=LAPTOP-VDMJ8H9C\SQLEXPRESS;Initial Catalog=db_Inventory;Integrated Security=True");

        public string logout;

        public frmLoginHistory()
        {
            InitializeComponent();
        }

        //Update the Data of Datagridview
        public void updateTable(string sql)
        {
            try
            {
                SqlConnection conn = new SqlConnection(@"Data Source=LAPTOP-VDMJ8H9C\SQLEXPRESS;Initial Catalog=db_Inventory;Integrated Security=True");
                conn.Open();   
                DataTable dt = new DataTable();
                SqlDataAdapter adap = new SqlDataAdapter(sql, conn);
                adap.Fill(dt);
                dataGridView1.DataSource = dt;
                conn.Close();
            }
            catch (Exception ee) { MessageBox.Show(ee.Message); }
        }

       
  
        
        //hides the form of LoginHistory
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Hide(); 
        }
        //Load the Databese of Employee Time in & Time out
        private void btnEmployeeHistory_Click(object sender, EventArgs e)
        {
            string sql = "SELECT * FROM tbl_EmpHistory ";
            updateTable(sql);
            logout = "emp";    
        }
        //Clear the record of the selected User
        private void btnClear_Click(object sender, EventArgs e)
        {
            //employee
            if (logout == "emp")
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("DELETE FROM tbl_EmpHistory", con);
                    cmd.ExecuteNonQuery();
                    string sql = "SELECT * FROM tbl_EmpHistory ";
                    updateTable(sql);
                    con.Close();
                }
                catch (Exception ee) { MessageBox.Show(ee.Message); }
            }
            //administrator
            if (logout == "admin")
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("DELETE FROM tbl_AdminHistory", con);
                    cmd.ExecuteNonQuery();
                    string sql = "SELECT * FROM tbl_AdminHistory ";
                    updateTable(sql);
                    con.Close();
                }
                catch (Exception ee)
                {
                    MessageBox.Show(ee.Message);
                }
            }
            else if (logout == "")
            {
            }
        }
        //Load the Database of Admin Time in & Time Out
        private void btnAdminHistory_Click(object sender, EventArgs e)
        {
            string sql = "SELECT * FROM tbl_AdminHistory ";
            updateTable(sql);
            logout = "admin";
        }
    }
}