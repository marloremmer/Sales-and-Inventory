using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;

namespace SADSIGN_IT401P
{

    public delegate void ShowFrm();

    public partial class card : Form
    {
        public SqlCommand cmd;
        public SqlConnection con;
        public SqlDataAdapter da;
        public DataTable dt;
        public IDataReader rd;

        public int sum;
        public int sum1;

        public int cardnoa;
        public int cardnob; 
        public int cardnoc;
        public int cardnod;
        public int cardpass;
        public int cardmoney;
        public int cardpin;
        public string cardname;

        public string cardcompute;

        public static string cardnamelipat;
        public static string cardnumlipat;
        public static string one;
        public static string two;


        //lipat sa main form
        public static int cardbalance; //mapupunta sa cash
        public static int currentbalance; //mapupunta sa change

        public static bool ans;
        public event ShowFrm evtFrm;

        
        public card()
        {
            InitializeComponent();
        }

       

        private void txtCard1_TextChanged(object sender, EventArgs e)
        {
            if
              (txtCard1.Text.Length >= 4) txtCard2.Focus();

            if (txtCard1.Text == "4000" || txtCard1.Text == "4100" || txtCard1.Text == "4200" ||
                txtCard1.Text == "4300" || txtCard1.Text == "4400" || txtCard1.Text == "4500" ||
                txtCard1.Text == "4600" || txtCard1.Text == "4700" || txtCard1.Text == "4800" ||
                txtCard1.Text == "4900")
            {

                pictureBox1.BorderStyle = BorderStyle.Fixed3D;
            }
            else if (txtCard1.Text == "5000" || txtCard1.Text == "5100" || txtCard1.Text == "5200" ||
                txtCard1.Text == "5300" || txtCard1.Text == "5400" || txtCard1.Text == "5500")
            {
                pictureBox3.BorderStyle = BorderStyle.Fixed3D;
            }
            else
            {
                pictureBox1.BorderStyle = BorderStyle.None;
                pictureBox3.BorderStyle = BorderStyle.None;
            }
        }

        private void txtCard2_TextChanged(object sender, EventArgs e)
        {
            if
              (txtCard2.Text.Length >= 4) txtCard3.Focus();
        }

        private void txtCard3_TextChanged(object sender, EventArgs e)
        {
            if
             (txtCard3.Text.Length >= 4) txtCard4.Focus();
        }

        private void txtCard4_TextChanged(object sender, EventArgs e)
        {
            if (txtCard4.Text.Length >= 4) chkAgree.Focus();
        }

        private void txtCard1_KeyPress(object sender, KeyPressEventArgs e)
        {
            txtCard1.MaxLength = 4;

            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
        (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void txtCard2_KeyPress(object sender, KeyPressEventArgs e)
        {
            txtCard2.MaxLength = 4;
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
        (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void txtCard3_KeyPress(object sender, KeyPressEventArgs e)
        {
            txtCard3.MaxLength = 4;
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
        (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void txtCard4_KeyPress(object sender, KeyPressEventArgs e)
        {
            txtCard4.MaxLength = 4;
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
        (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void txtCardPin_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
           


        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

           

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
        

            
        }

        private void button2_Click(object sender, EventArgs e)
        {

            if (txtFullname.Text == String.Empty)
            {
                MessageBox.Show("Card Holder is Empty");
                txtFullname.Focus(); return;
            }
            else if (txtCard1.Text == String.Empty)
            {
                MessageBox.Show("Card Number is Empty");
                txtCard1.Focus();
                return;
            }
            else if (txtCard2.Text == String.Empty)
            {
                MessageBox.Show("Card Number is Empty");
                txtCard2.Focus();
                return;
            }
            else if (txtCard3.Text == String.Empty)
            {
                MessageBox.Show("Card Number is Empty");
                txtCard3.Focus();
                return;
            }
            else if (txtCard4.Text == String.Empty)
            {
                MessageBox.Show("Card Number is Empty");
                txtCard4.Focus();
                return;
            }
            else if (txtCardPin.Text == String.Empty)
            {
                MessageBox.Show("Card Number is Empty");
                txtCardPin.Focus();
                return;
            }

           
            cardnoa = int.Parse(txtCard1.Text);
            cardnob = int.Parse(txtCard2.Text);
            cardnoc = int.Parse(txtCard3.Text);
            cardnod = int.Parse(txtCard4.Text);
            cardpin = int.Parse(txtCardPin.Text);

            SqlConnection con = new SqlConnection(@"Data Source=LAPTOP-VDMJ8H9C\SQLEXPRESS;Initial Catalog=db_Inventory;Integrated Security=True");
            con.Open();
            try
            {
                string str = @"SELECT * FROM tbl_Credit WHERE cardno1 = '" + cardnoa + "' AND cardno2='" + cardnob + "' AND cardno3 ='" + cardnoc + "' AND cardno4='" + cardnod + "' AND cardpin='" + cardpin + "'";
                cmd = new SqlCommand(str, con);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    textBox1.Text = reader["cardmoney"].ToString();
                }
                else
                {
                    MessageBox.Show("Incorrect Username or Password");
                    return;
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
            }


            if (int.Parse(textBox1.Text) > int.Parse(cardcompute))
            {
                //dito mag compute
                sum = int.Parse(textBox1.Text) - int.Parse(cardcompute);
                textBox3.Text = sum.ToString();

            }
            else
            {
                MessageBox.Show("Balance error");
                return;
            }










            //dito mag update

            con = new SqlConnection(@"Data Source=LAPTOP-VDMJ8H9C\SQLEXPRESS;Initial Catalog=db_Inventory;Integrated Security=True");
            try
            {
                cmd = new SqlCommand("UPDATE tbl_Credit SET cardmoney =@cardmoney WHERE cardno4=@cardno4", con);
                cmd.Parameters.AddWithValue("@cardno4", txtCard4.Text);
                cmd.Parameters.AddWithValue("@cardmoney", sum);
                con.Open();
                cmd.ExecuteNonQuery();
                sum1 = sum;
                
            }
            finally
            {
                cardbalance = Convert.ToInt32(textBox3.Text); //current balance
                currentbalance = Convert.ToInt32(textBox1.Text); //remaining balance


                DialogResult result = MessageBox.Show("Succesfully Credited to your Account", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                if (result == DialogResult.OK)
                {
                    one = textBox1.Text;
                    two = textBox3.Text;

                    cardbalance = Convert.ToInt32(textBox3.Text); //current balance
                    currentbalance = Convert.ToInt32(textBox1.Text); //remaining balance

                    cardnamelipat = txtFullname.Text;
                    cardnumlipat = txtCard1.Text + txtCard2.Text + txtCard3.Text;
                }


                this.Close();

                con.Close();
                
            }

          



        }
 


        private void card_Load(object sender, EventArgs e)
        {
            cardcompute = frmadd.totalcredit.ToString();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (evtFrm != null)
            {
                evtFrm();
            }
            this.Close();
            
        }

        private void chkAgree_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAgree.Checked == true)
            {
                button2.Enabled = true;
            }
            else if (chkAgree.Checked == false)
            {
                button2.Enabled = false;
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            if (evtFrm != null)
            {
                evtFrm();
            }
            this.Close();
        }

       
    }
}