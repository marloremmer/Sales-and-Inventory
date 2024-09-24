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
    public partial class frmAdminPass : Form
    {
        public SqlCommand cmd;
        public SqlConnection con;
        public SqlDataAdapter da;
        public DataTable dt;

        //History
        public static string login;
        public static string user;
        public static string logRef;
        public static string username;

        public frmAdminPass()
        {
            InitializeComponent();
        }

        private void frmAdminPass_Load(object sender, EventArgs e)
        {
            this.AcceptButton = btnOkay;
        }
        //validation for exit
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Are you sure you want to exit?", "Confirm Exit?", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (dialogResult == DialogResult.OK)
            {
                this.Close();
            }
            else if (dialogResult == DialogResult.Cancel)
            {
                return;
            }
            this.Close();
        }
        //Show the log in page of Admin
        private void button2_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabAdmin;
        }
        //show the log in page of employee 
        private void btnEmp_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabEmployee;
        }
        //Read if the the account is valid for Administrator
        private void btnOkay_Click_1(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(@"Data Source=LAPTOP-VDMJ8H9C\SQLEXPRESS;Initial Catalog=db_Inventory;Integrated Security=True");
            con.Open();
            try{         
                string str = @"SELECT Username, Pass FROM tbl_Account WHERE Username = '"+txtAdmin.Text+"' AND Pass='"+txtPass.Text+"'";
                cmd = new SqlCommand(str,con);
                SqlDataReader reader = cmd.ExecuteReader();        
                if(reader.HasRows)
                {
                    con.Close();
                    try
                    {
                        con.Open();
                        string date = DateTime.Now.ToString();
                        user = txtAdmin.Text;
                        string command = @"INSERT INTO tbl_AdminHistory (Admin, Login_Time,Logout_Time) VALUES('" + user + "','" + date + "','still logged in')";
                        cmd = new SqlCommand(command, con);
                        cmd.ExecuteNonQuery();
                        logRef = date;
                        username = "admin";
                        frmadd main = new frmadd();
                        main.Show();
                        this.Hide();
                    }
                    catch (Exception ee)
                    {
                        MessageBox.Show(ee.Message);
                    }
                    con.Close();
                }
                else
                {
                    MessageBox.Show("Incorrect Username or Password");
                }
            }
            catch(Exception ee){MessageBox.Show(ee.Message);}
        }
        //show the password
        private void chkPass_CheckedChanged_1(object sender, EventArgs e)
        {   
            txtPass.PasswordChar = chkPass.Checked ? '\0' : '*';
        }
        //show the log in page of employee 
        private void btnEmp_Click_1(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabEmployee;
            login = "EMPLOYEE";
        }
        //Read if the the account is valid for Employee
        private void btnOK_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(@"Data Source=LAPTOP-VDMJ8H9C\SQLEXPRESS;Initial Catalog=db_Inventory;Integrated Security=True");
            con.Open();
            try{
                user = txtUsername.Text;
                string str = @"SELECT Username, Pass FROM tbl_Accountad WHERE Username = '"+user+"' AND Pass='"+txtUserPass.Text+"'";
                cmd = new SqlCommand(str,con);
                SqlDataReader reader = cmd.ExecuteReader();
                if(reader.HasRows)
                {
                    con.Close();
                    try
                    {
                        con.Open();
                        string date = DateTime.Now.ToString();
                        string command = @"INSERT INTO tbl_EmpHistory (Employee, Login_Time, Logout_Time) VALUES('" + user + "','" + date + "','still logged in')";
                        cmd = new SqlCommand(command, con);
                        cmd.ExecuteNonQuery();
                        logRef = date;
                        username = "employee";
                        frmadd main = new frmadd();
                        main.Show();
                        this.Hide();
                    }
                    catch (Exception ee) { MessageBox.Show(ee.Message); 
                    }
                    con.Close();
                }
                else
                {
                    MessageBox.Show("Incorrect Username or Password");
                }
            }
            catch(Exception ee){MessageBox.Show(ee.Message);
            }
        }
        //Show the password of employee 
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            txtUserPass.PasswordChar = checkBox1.Checked ? '\0' : '*';
        }
        //Show the log in page of Administrator
        private void btnAdmin_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabAdmin;
            login = "ADMIN";
        }

        private void tabAdmin_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {

        }
    }
}
