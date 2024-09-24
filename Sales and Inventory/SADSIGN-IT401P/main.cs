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
using iTextSharp.text;
using System.Reflection;
using iTextSharp.text.pdf;
using Word = Microsoft.Office.Interop.Word;
using System.Text.RegularExpressions;

namespace SADSIGN_IT401P
{
    public delegate void asd();
    public partial class frmadd : Form
    {

        public static int totalcredit;
        public static int total1;

        //pang kuha ng total
        public string bgtotal;
        public int minus;
        public int finals = 0;

        public SqlCommand cmd;
        public SqlConnection con;
        public SqlDataAdapter da;
        public DataTable dt;
        public IDataReader rd;

        //Data
        public int sum;
        public string ProdID;
        public string ProdName;
        public string Stacks;
        public string Cate;
        public string SubCate;
        public string Price;
        public string Barc;
        public string Supplier;
        public string DateSupplied;
        public string Sex;

        //Editting
        public string EditProdID;
        public string EditProdName;
        public string EditStacks;
        public string EditCate;
        public string EditSubCate;
        public string EditPrice;
        public string EditBarc;
        public string EditSupplier;
        public string EditDateSupplied;

        //Purchase
        public string PurProductID;
        public string PurQuantity;
        public string tempQ;
        public string PurProductName;
        public string PurPrice;
        public string PurtotalPrice;
        public int sinPrice;
        public int totalPrice;
        public int tmpQuantity;
        public int purQuantity;
        public int newQuantity;

        public string sales;
        public string sales1;

        //Cart
        public string Name;
        public string Quantity;
        public string Price2;
        public string TotalPrice;
        public DateTime OrderDate;

        //History
        public static string showHistory;

        public event asd evtFrm1;
        public frmadd()
        {
            InitializeComponent();



        }

        private void Form1_Load(object sender, EventArgs e)
        {




            DeletePending1();
            ShowData();
            ShowSalesData();
            ShowDataEdit();
            ShowDataDel();
            txtSearch.Focus();
      


            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridView3.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridView4.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridView5.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridView6.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

          
            ShowCompute();

           
            if (frmAdminPass.username != "admin") //fake
            {

                btnAddpeke.Visible = true;


                btnEditPeke.Visible = true;


                btnDeletePeke.Visible = true;


                btnSalesPeke.Visible = true;


                btnAccountPeke.Visible = true;




            }
            else
            {
                btnAddProduct.Visible = true;


                btnEditRecord.Visible = true;

                btnDeleteRecord.Visible = true;


                btnSales.Visible = true;


                btnDeleteRecord.Visible = true;

            }



        }










        public void ShowCompute()
        {
            int A = 0;
            int B = 0;
            for (A = 0; A < dataGridView6.Rows.Count; A++)
            {
                B += Convert.ToInt32(dataGridView6.Rows[A].Cells[4].Value);
            }
            textBox1.Text = B.ToString();
            bgtotal = textBox1.Text;
        }

        //Validation for ProductID 
        public void Validates()
        {
            con = new SqlConnection(@"Data Source=LAPTOP-VDMJ8H9C\SQLEXPRESS;Initial Catalog=db_Inventory;Integrated Security=True");
            cmd = new SqlCommand("Select ProductID from tbl_Inventory where ProductID = '" + txtProdID.Text + "'", con);
            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                MessageBox.Show("ProductID Already Exist!");
                con.Close();
            }
            else
            {
                InsertData();
            }
        }
        //validation for Inserting new product
        public void InsertData()
        {
            if (txtProdID.Text == String.Empty)
            {
                MessageBox.Show("Product ID textfield is empty.");
                txtProdID.Focus();
            }
            else if (txtProdName.Text == String.Empty)
            {
                MessageBox.Show("Product Name Textfield is empty.");
                txtProdName.Focus();
            }
            else if (txtStacks.Text == String.Empty)
            {
                MessageBox.Show("Stack Textfield is empty.");
                txtStacks.Focus();
            }
            else if (txtCate.Text == String.Empty)
            {
                MessageBox.Show("Categorie Textfield is empty.");
                txtCate.Focus();
            }
            else if (txtSubCate.Text == String.Empty)
            {
                MessageBox.Show("Sub Categorie Textfield is empty.");
                txtSubCate.Focus();
            }
            else if (txtPrice.Text == String.Empty)
            {
                MessageBox.Show("Price Textfield is empty.");
                txtPrice.Focus();
            }
            else if (txtBarc.Text == String.Empty)
            {
                MessageBox.Show("Barcode Textfield is empty.");
                txtBarc.Focus();
            }
            else if (txtSupplier.Text == String.Empty)
            {
                MessageBox.Show("Supplier Textfield is empty.");
                txtSupplier.Focus();
            }
            else if (cmbMonth.Text == String.Empty)
            {
                MessageBox.Show("Select Month");
                cmbMonth.Focus();
            }
            else if (cmbDate.Text == String.Empty)
            {
                MessageBox.Show("Select Date");
                cmbDate.Focus();
            }
            else if (cmbYear.Text == String.Empty)
            {
                MessageBox.Show("Select Year");
                cmbYear.Focus();
            }
            else
            {
                con = new SqlConnection(@"Data Source=LAPTOP-VDMJ8H9C\SQLEXPRESS;Initial Catalog=db_Inventory;Integrated Security=True");
                try
                {
                    cmd = new SqlCommand("INSERT INTO tbl_Inventory (ProductID, ProductName, Stacks, Category, SubCategory, Price, Barcode, Supplier, DateSupplied)" +
                    "VALUES (@ProductID, @ProductName, @Stacks, @Category, @SubCategory, @Price, @Barcode, @Supplier, @DateSupplied)", con);
                    cmd.Parameters.AddWithValue("@ProductID", txtProdID.Text.ToUpper());
                    cmd.Parameters.AddWithValue("@ProductName", txtProdName.Text.ToUpper());
                    cmd.Parameters.AddWithValue("@Stacks", txtStacks.Text.ToUpper());
                    cmd.Parameters.AddWithValue("@Category", txtCate.Text.ToUpper());
                    cmd.Parameters.AddWithValue("@SubCategory", txtSubCate.Text.ToUpper());
                    cmd.Parameters.AddWithValue("@Price", txtPrice.Text.ToUpper());
                    cmd.Parameters.AddWithValue("@Barcode", txtBarc.Text.ToUpper());
                    cmd.Parameters.AddWithValue("@Supplier", txtSupplier.Text.ToUpper());
                    cmd.Parameters.AddWithValue("@DateSupplied", cmbMonth.Text + "/" + cmbDate.Text + "/" + cmbYear.Text);
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
                finally
                {
                    MessageBox.Show("Successfully Encoded!");

                    txtProdID.Text = string.Empty;
                    txtProdName.Text = string.Empty;
                    txtStacks.Text = string.Empty;
                    txtCate.Text = string.Empty;
                    txtSubCate.Text = string.Empty;
                    txtPrice.Text = string.Empty;
                    txtBarc.Text = string.Empty;
                    txtSupplier.Text = string.Empty;
                    cmbMonth.Text = string.Empty;
                    cmbDate.Text = string.Empty;
                    cmbYear.Text = string.Empty;
                    con.Close();
                }
            }
        }
        //Validation for creating account
        public void Validates1()
        {
            con = new SqlConnection(@"Data Source=LAPTOP-VDMJ8H9C\SQLEXPRESS;Initial Catalog=db_Inventory;Integrated Security=True");
            cmd = new SqlCommand("Select Username from tbl_AccountAd where Username = '" + txtUsername.Text + "'", con);
            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                MessageBox.Show("Username already Exist!");
                con.Close();
            }
            else
            {
                InsertData1();
            }
        }
        //Validation for creating account
        public void InsertData1()
        {
            if (txtLNameA.Text == String.Empty)
            {
                MessageBox.Show("Last Name textfield is empty.");
                txtLNameA.Focus();
            }
            else if (txtMNameA.Text == String.Empty)
            {
                MessageBox.Show("Middle Name is Empty");
                txtMNameA.Focus();
            }
            else if (txtFnameA.Text == String.Empty)
            {
                MessageBox.Show("First Name Textfield is empty.");
                txtFnameA.Focus();
            }
            else if (cboAge.Text == String.Empty)
            {
                MessageBox.Show("Age Textfield is empty.");
                cboAge.Focus();
            }
            else if (rbnFemale.Checked == false && rbnMale.Checked == false)
            {
                MessageBox.Show("Select Gender");
                return;
            }
            else if (txtEmailadd.Text == String.Empty)
            {
                MessageBox.Show("Email Address Textfield is empty.");
                txtEmailadd.Focus();
            }

            else if (txtContact.Text == String.Empty)
            {
                MessageBox.Show("Contact Textfield is empty.");
                txtContact.Focus();
            }
            else if (txtAddress.Text == String.Empty)
            {
                MessageBox.Show("Address Textfield is empty.");
                txtAddress.Focus();
            }
            else if (cboMonth.Text == "Month")
            {
                MessageBox.Show("Select Month.");
                cboMonth.Focus();
            }
            else if (cboDay.Text == "Day")
            {
                MessageBox.Show("Select Day.");
                txtSupplier.Focus();
            }
            else if (cboYear.Text == "Year")
            {
                MessageBox.Show("Select Year.");
                cboYear.Focus();
            }

            else if (txtUsername.Text == String.Empty)
            {
                MessageBox.Show("UserName is Empty");
                txtUsername.Focus();
            }
            else if (txtPassword.Text == String.Empty)
            {
                MessageBox.Show("Password is Empty");
                txtPassword.Focus();
            }
            else
            {
                string Gender = Sex;
                con = new SqlConnection(@"Data Source=LAPTOP-VDMJ8H9C\SQLEXPRESS;Initial Catalog=db_Inventory;Integrated Security=True");
                try
                {
                    cmd = new SqlCommand("INSERT INTO tbl_AccountAd (Username, Pass, Lname, FName, MName, Address  ,Gender, BDay, Contact, Email)" +
                    "VALUES (@Username, @Pass, @Lname, @FName, @MName, @Address,@Gender, @BDay, @Contact, @Email)", con);
                    cmd.Parameters.AddWithValue("@Username", txtUsername.Text);
                    cmd.Parameters.AddWithValue("@Pass", txtPassword.Text);
                    cmd.Parameters.AddWithValue("@LName", txtLNameA.Text);
                    cmd.Parameters.AddWithValue("@FName", txtFnameA.Text);
                    cmd.Parameters.AddWithValue("@MName", txtMNameA.Text);
                    cmd.Parameters.AddWithValue("@Address", txtAddress.Text);
                    cmd.Parameters.AddWithValue("@Gender", Gender);
                    cmd.Parameters.AddWithValue("@BDay", cboMonth.Text + "/" + cboDay.Text + "/" + cboYear.Text);
                    cmd.Parameters.AddWithValue("@Contact", txtContact.Text);
                    cmd.Parameters.AddWithValue("@Email", txtEmailadd.Text);
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
                finally
                {
                    MessageBox.Show("Successfully Encoded!");


                    txtFnameA.Clear();

                    txtMNameA.Clear();

                    txtLNameA.Clear();

                    txtEmailadd.Clear();

                    txtContact.Clear();

                    txtAddress.Clear();

                    txtUsername.Clear();

                    txtPassword.Clear();
                    con.Close();
                }
            }
        }
        private void logAsAdministratorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAdminPass fr = new frmAdminPass();
            fr.Show();
        }
        //This code will filter the search textfield
        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            DataView DV = new DataView(dt);
            DV.RowFilter = String.Format("ProductID LIKE '%{0}%'", txtSearch.Text);
            dataGridView1.DataSource = DV;
        }
        //Load all the Data of Datagridview
        public void ShowData()
        {
            con = new SqlConnection(@"Data Source=LAPTOP-VDMJ8H9C\SQLEXPRESS;Initial Catalog=db_Inventory;Integrated Security=True");
            con.Open();
            da = new SqlDataAdapter("SELECT * FROM tbl_Inventory", con);
            dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            dataGridView2.DataSource = dt;
            dataGridView3.DataSource = dt;
            dataGridView4.DataSource = dt;
            con.Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            Validates();
            RefreshDatabase();

        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ShowData();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtProdName.Text = String.Empty;
            txtProdID.Text = String.Empty;
            txtStacks.Text = String.Empty;
            txtCate.Text = String.Empty;
            txtSubCate.Text = String.Empty;
            txtPrice.Text = String.Empty;
            txtBarc.Text = String.Empty;
            txtSupplier.Text = String.Empty;
            cmbYear.Text = String.Empty;
            cmbDate.Text = String.Empty;
            cmbMonth.Text = String.Empty;
        }
        public void Refreshcart()
        {
            con = new SqlConnection(@"Data Source=LAPTOP-VDMJ8H9C\SQLEXPRESS;Initial Catalog=db_Inventory;Integrated Security=True");
            con.Open();
            da = new SqlDataAdapter("SELECT * FROM tbl_cart", con);
            dt = new DataTable();
            da.Fill(dt);
            dataGridView6.DataSource = dt;
            con.Close();
        }

        public void RefreshDatabase()
        {
            con = new SqlConnection(@"Data Source=LAPTOP-VDMJ8H9C\SQLEXPRESS;Initial Catalog=db_Inventory;Integrated Security=True");
            con.Open();
            da = new SqlDataAdapter("SELECT * FROM tbl_Inventory", con);
            dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            dataGridView2.DataSource = dt;
            dataGridView3.DataSource = dt;
            dataGridView4.DataSource = dt;
            con.Close();
        }

        public void ShowDataEdit()
        {
            con = new SqlConnection(@"Data Source=LAPTOP-VDMJ8H9C\SQLEXPRESS;Initial Catalog=db_Inventory;Integrated Security=True");
            con.Open();
            da = new SqlDataAdapter("SELECT * FROM tbl_Inventory", con);
            dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            dataGridView2.DataSource = dt;
            dataGridView3.DataSource = dt;
            dataGridView4.DataSource = dt;
            con.Close();
        }
        public void ShowDataDel()
        {
            con = new SqlConnection(@"Data Source=LAPTOP-VDMJ8H9C\SQLEXPRESS;Initial Catalog=db_Inventory;Integrated Security=True");
            con.Open();
            da = new SqlDataAdapter("SELECT * FROM tbl_Inventory", con);
            dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            dataGridView2.DataSource = dt;
            dataGridView3.DataSource = dt;
            dataGridView4.DataSource = dt;
            con.Close();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Are you sure you want to delete this product?", "Confirm Delete?", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if (dialogResult == DialogResult.OK)
            {
                con = new SqlConnection(@"Data Source=LAPTOP-VDMJ8H9C\SQLEXPRESS;Initial Catalog=db_Inventory;Integrated Security=True");
                con.Open();
                da = new SqlDataAdapter("Delete FROM tbl_Inventory where ProductID = '" + txtDelProdID.Text + "'", con);
                dt = new DataTable();
                da.Fill(dt);
                dataGridView1.DataSource = dt;
                dataGridView2.DataSource = dt;
                dataGridView3.DataSource = dt;
                dataGridView4.DataSource = dt;
                con.Close();
                RefreshDatabase();
                txtDelProdID.Text = string.Empty;
                return;
            }
            else if (dialogResult == DialogResult.Cancel)
            {
                return;
            }
            Close();
        }

        private void txtDelProdID_TextChanged(object sender, EventArgs e)
        {
            DataView DV = new DataView(dt);
            DV.RowFilter = String.Format("ProductID LIKE '%{0}%'", txtDelProdID.Text);
            dataGridView3.DataSource = DV;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            con = new SqlConnection(@"Data Source=LAPTOP-VDMJ8H9C\SQLEXPRESS;Initial Catalog=db_Inventory;Integrated Security=True");
            try
            {
                cmd = new SqlCommand("UPDATE tbl_Inventory SET ProductName = @ProductName, Stacks = @Stacks, Category = @Category, SubCategory = @SubCategory, Price = @Price, Barcode = @Barcode, Supplier = @Supplier WHERE ProductID=@ProductID", con);
                cmd.Parameters.AddWithValue("@ProductID", txtEditProdID.Text);
                cmd.Parameters.AddWithValue("@ProductName", txtEditProdName.Text);
                cmd.Parameters.AddWithValue("@Stacks", txtEditStocks.Text);
                cmd.Parameters.AddWithValue("@Category", txtEditCate.Text);
                cmd.Parameters.AddWithValue("@SubCategory", txtEditSubCate.Text);
                cmd.Parameters.AddWithValue("@Price", txtEditPrice.Text);
                cmd.Parameters.AddWithValue("@Barcode", txtEditBarc.Text);
                cmd.Parameters.AddWithValue("@Supplier", txtEditSupplier.Text);
                con.Open();
                cmd.ExecuteNonQuery();
            }
            finally
            {

                txtEditProdID.Text = string.Empty;

                txtEditProdName.Text = string.Empty;

                txtEditStocks.Text = string.Empty;

                txtEditCate.Text = string.Empty;

                txtEditSubCate.Text = string.Empty;

                txtEditPrice.Text = string.Empty;

                txtEditBarc.Text = string.Empty;

                txtEditSupplier.Text = string.Empty;

                MessageBox.Show("Successfully Updated Records!");
                RefreshDatabase();
                con.Close();
            }
        }


        private void tabControl1_tabPage3_Selected(object sender, TabControlEventArgs e)
        {
            MessageBox.Show("Note: In this section, you can only update records one at a time!");
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Close();
        }
        //Validation for Log out update of time out
        private void btnLogout_Click(object sender, EventArgs e)
        {
         /*   btnLogout.FlatAppearance.BorderColor = Color.FromArgb(0, 255, 255, 255);
            DialogResult dialogResult = MessageBox.Show("Are you sure you want to Logout?", "Confirm Logout", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (dialogResult == DialogResult.OK)
            {
                string log = frmAdminPass.login;
                string userN = frmAdminPass.user;
                string Ldate = DateTime.Now.ToString();
                //history for employee
                if (log == "EMPLOYEE")
                {
                    string sql = "UPDATE tbl_EmpHistory SET Logout_Time='" + Ldate + "' WHERE Login_Time='" + frmAdminPass.logRef + "'";
                    con.Open();
                    cmd = new SqlCommand(sql, con);
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
                //history for admin
                else if (log == "ADMIN")
                {
                    string sql = "UPDATE tbl_AdminHistory SET Logout_Time='" + Ldate + "' WHERE Login_Time='" + frmAdminPass.logRef + "'";
                    con.Open();
                    cmd = new SqlCommand(sql, con);
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
                frmAdminPass fp = new frmAdminPass();
                fp.Show();
                this.Close();
            }
            else if (dialogResult == DialogResult.Cancel)
            {
                return;
            }*/
        }
        //validation for gender 
        private void rbnFemale_CheckedChanged(object sender, EventArgs e)
        {
            if (rbnFemale.Checked == true)
            {
                Sex = "Female";
            }
        }
        //validation for gender
        private void rbnMale_CheckedChanged(object sender, EventArgs e)
        {
            if (rbnMale.Checked == true)
            {
                Sex = "Male";
            }
        }
        //validation for Contact Number
        private void txtContact_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            {
                if (!Char.IsDigit(ch) && ch != 8)
                {
                    e.Handled = true;
                    MessageBox.Show("Please Enter Valid Value");
                }
            }
        }

        //validation for ProductID & Quantity
        //computation for stocks
        private void btnPurchase_Click(object sender, EventArgs e)
        {


            if (txtPurProductID.Text == string.Empty)
            {
                MessageBox.Show("Product ID is Empty.");
                return;
            }
            else if (txtPurQuantity.Text == string.Empty)
            {
                MessageBox.Show("Quantity is Empty.");
                return;
            }
            else
            {
                PurProductID = txtPurProductID.Text;
                purQuantity = Convert.ToInt32(txtPurQuantity.Text);
                con = new SqlConnection(@"Data Source=LAPTOP-VDMJ8H9C\SQLEXPRESS;Initial Catalog=db_Inventory;Integrated Security=True");
                cmd = new SqlCommand("SELECT * FROM tbl_Inventory WHERE ProductID ='" + PurProductID + "'", con);
                con.Open();
                SqlDataReader RED = cmd.ExecuteReader();
                if (RED.Read())
                {
                    tempQ = RED["Stacks"].ToString();
                    tmpQuantity = int.Parse(tempQ);
                    PurPrice = RED["Price"].ToString();
                    sinPrice = int.Parse(PurPrice);
                    PurProductName = RED["ProductName"].ToString();
                    if (tmpQuantity >= purQuantity)
                    {
                        newQuantity = tmpQuantity - purQuantity;
                        sinPrice = Convert.ToInt32(PurPrice);
                        totalPrice = sinPrice * purQuantity;
                        txtPurProductID.Text = String.Empty;
                        txtPurQuantity.Text = String.Empty;
                        PurchasePending();
                        ShowPendingData();
                        ShowCompute();
                    }
                    else
                    {
                        MessageBox.Show("Invalid Purchase Quantity!");
                    }
                }
            }
        }
        //this code will insert the data/s of Cart to the Database
        public void PurchaseData()
        {
            for (int row = 0; row < dataGridView6.Rows.Count - 1; row++)
            {
                try
                {
                    Name = dataGridView6.Rows[row].Cells[1].Value.ToString();
                    Quantity = dataGridView6.Rows[row].Cells[2].Value.ToString();
                    Price2 = dataGridView6.Rows[row].Cells[3].Value.ToString();
                    TotalPrice = dataGridView6.Rows[row].Cells[4].Value.ToString();
                    OrderDate = DateTime.Now;
                    con = new SqlConnection(@"Data Source=LAPTOP-VDMJ8H9C\SQLEXPRESS;Initial Catalog=db_Inventory;Integrated Security=True");
                    con.Open();
                    string command = "INSERT INTO tbl_Purchases(ProductName, Quantity, Price, TotalPrice, DateOfPurchase) VALUES('" + Name + "', '" + Quantity + "', '" + Price2 + "', '" + TotalPrice + "', '" + OrderDate + "')";
                    SqlCommand cmd = new SqlCommand(command, con);
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
                catch (Exception ee) { MessageBox.Show(ee.Message); }
            }
        }
        //This code will delete the Data in Datagridview of Cart
        public void DeletePending()
        {
            con = new SqlConnection(@"Data Source=LAPTOP-VDMJ8H9C\SQLEXPRESS;Initial Catalog=db_Inventory;Integrated Security=True");
            try
            {
                cmd = new SqlCommand("DELETE FROM tbl_Cart", con);
                con.Open();
                cmd.ExecuteNonQuery();
            }
            finally
            {
                MessageBox.Show("Transaction Completed.");
                con.Close();
            }
        }
            public void DeletePending1()
        {
            con = new SqlConnection(@"Data Source=LAPTOP-VDMJ8H9C\SQLEXPRESS;Initial Catalog=db_Inventory;Integrated Security=True");
            try
            {
                cmd = new SqlCommand("DELETE FROM tbl_Cart", con);
                con.Open();
                cmd.ExecuteNonQuery();
            }
            finally
            {
                
                con.Close();
            }
        }
        //This code will copy the selected row to another Datagridview
        public void PurchasePending()
        {
            con = new SqlConnection(@"Data Source=LAPTOP-VDMJ8H9C\SQLEXPRESS;Initial Catalog=db_Inventory;Integrated Security=True");
            try
            {
                cmd = new SqlCommand("INSERT INTO tbl_Cart (ProductName, Quantity, Price, TotalPrice, OrderDate)" +
                "VALUES (@ProductName, @Quantity, @Price, @TotalPrice, @OrderDate)", con);
                cmd.Parameters.AddWithValue("@ProductName", PurProductName);
                cmd.Parameters.AddWithValue("@Quantity", purQuantity);
                cmd.Parameters.AddWithValue("@Price", sinPrice);
                cmd.Parameters.AddWithValue("@TotalPrice", totalPrice.ToString());
                cmd.Parameters.AddWithValue("@OrderDate", DateTime.Now);
                con.Open();
                cmd.ExecuteNonQuery();
            }
            finally
            {

                con.Close();
            }
        }
        //This code will refresh the Datagridview of Cart
        public void ShowPendingData()
        {
            con = new SqlConnection(@"Data Source=LAPTOP-VDMJ8H9C\SQLEXPRESS;Initial Catalog=db_Inventory;Integrated Security=True");
            con.Open();
            da = new SqlDataAdapter("SELECT * FROM tbl_Cart", con);
            dt = new DataTable();
            da.Fill(dt);
            dataGridView6.DataSource = dt;
            con.Close();
        }
        //This code will load the Datagridview of Sales 
        public void ShowSalesData()
        {
            con = new SqlConnection(@"Data Source=LAPTOP-VDMJ8H9C\SQLEXPRESS;Initial Catalog=db_Inventory;Integrated Security=True");
            con.Open();
            da = new SqlDataAdapter("SELECT * FROM tbl_Purchases", con);
            dt = new DataTable();
            da.Fill(dt);
            dataGridView5.DataSource = dt;
            con.Close();
        }
        //This code will load the Datagridview of Cart
        public void refreshCart()
        {
            con = new SqlConnection(@"Data Source=LAPTOP-VDMJ8H9C\SQLEXPRESS;Initial Catalog=db_Inventory;Integrated Security=True");
            con.Open();
            da = new SqlDataAdapter("SELECT * FROM tbl_Cart", con);
            dt = new DataTable();
            da.Fill(dt);
            dataGridView6.DataSource = dt;
        }
        //This code will load the Datagridview of Sales
        public void RefreshSales()
        {
            con = new SqlConnection(@"Data Source=LAPTOP-VDMJ8H9C\SQLEXPRESS;Initial Catalog=db_Inventory;Integrated Security=True");
            con.Open();
            da = new SqlDataAdapter("SELECT * FROM tbl_Purchases", con);
            dt = new DataTable();
            da.Fill(dt);
            dataGridView5.DataSource = dt;
        }
        //This will code will update the stock of a product.
        public void SalesData()
        {
            con = new SqlConnection(@"Data Source=LAPTOP-VDMJ8H9C\SQLEXPRESS;Initial Catalog=db_Inventory;Integrated Security=True");
            try
            {
                cmd = new SqlCommand("UPDATE tbl_Inventory SET Stacks = @Stacks WHERE ProductID=@ProductID", con);
                cmd.Parameters.AddWithValue("@ProductID", PurProductID);
                cmd.Parameters.AddWithValue("@Stacks", newQuantity);
                con.Open();
                cmd.ExecuteNonQuery();
            }
            finally
            {
                RefreshDatabase();
                con.Close();
            }
        }
        //This code will compute the Total Sales
        /* private void button4_Click(object sender, EventArgs e)
         {
             int sum = 0;

             for (int i = 0; i < dataGridView5.Rows.Count; ++i)
             {
                 sum += Convert.ToInt32(dataGridView5.Rows[i].Cells[4].Value);
             }
             resibo();
           
         }*/
        /*    public void resibo()
            {
                tsales.Text = Convert.ToString(sum);
                sales = sum;
            }*/
        //This code will delete the data of Sales all of it
        private void btnC_Click(object sender, EventArgs e)
        {


            DialogResult dialogResult = MessageBox.Show("Are you sure you want to delete the entier sales?", "Confirm Clear?", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if (dialogResult == DialogResult.OK)
            {
                con = new SqlConnection(@"Data Source=LAPTOP-VDMJ8H9C\SQLEXPRESS;Initial Catalog=db_Inventory;Integrated Security=True");
                con.Open();
                da = new SqlDataAdapter("Delete FROM tbl_Purchases", con);
                dt = new DataTable();
                da.Fill(dt);
                dataGridView5.DataSource = dt;
                con.Close();
                RefreshSales();
                return;
            }
            else if (dialogResult == DialogResult.Cancel)
            {
                return;
            }
            Close();
        }
        //This code will create a docx type that can be save and print for sales
        private void btnCreate_Click(object sender, EventArgs e)
        {
            int sum = 0;

            for (int i = 0; i < dataGridView5.Rows.Count; ++i)
            {
                sum += Convert.ToInt32(dataGridView5.Rows[i].Cells[4].Value);
            }
            sales = Convert.ToString(sum);



            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Word Documents (*.docx)|*.docx";
            sfd.FileName = DateTime.Now.ToString();
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                Export_Data_To_Word(dataGridView5, sfd.FileName);
            }
        }
        //this code will transfer the data to the docx for sales
        public void Export_Data_To_Word(DataGridView dataGridView5, string filename)
        {
            if (dataGridView5.Rows.Count != 0)
            {
                int RowCount = dataGridView5.Rows.Count;
                int ColumnCount = dataGridView5.Columns.Count;
                Object[,] DataArray = new object[RowCount + 1, ColumnCount + 1];
                //add rows
                int r = 0;
                for (int c = 0; c <= ColumnCount - 1; c++)
                {
                    for (r = 0; r <= RowCount - 1; r++)
                    {
                        DataArray[r, c] = dataGridView5.Rows[r].Cells[c].Value;
                    } //end row loop
                } //end column loop
                Word.Document oDoc = new Word.Document();
                oDoc.Application.Visible = true;
                //page orintation
                oDoc.PageSetup.Orientation = Word.WdOrientation.wdOrientLandscape;
                dynamic oRange = oDoc.Content.Application.Selection.Range;
                string oTemp = "";
                for (r = 0; r <= RowCount - 1; r++)
                {
                    for (int c = 0; c <= ColumnCount - 1; c++)
                    {
                        oTemp = oTemp + DataArray[r, c] + "\t";
                    }
                }
                //table format
                oRange.Text = oTemp;
                object Separator = Word.WdTableFieldSeparator.wdSeparateByTabs;
                object ApplyBorders = true;
                object AutoFit = true;
                object AutoFitBehavior = Word.WdAutoFitBehavior.wdAutoFitContent;
                oRange.ConvertToTable(ref Separator, ref RowCount, ref ColumnCount,
                                      Type.Missing, Type.Missing, ref ApplyBorders,
                                      Type.Missing, Type.Missing, Type.Missing,
                                      Type.Missing, Type.Missing, Type.Missing,
                                      Type.Missing, ref AutoFit, ref AutoFitBehavior, Type.Missing);
                oRange.Select();
                oDoc.Application.Selection.Tables[1].Select();
                oDoc.Application.Selection.Tables[1].Rows.AllowBreakAcrossPages = 0;
                oDoc.Application.Selection.Tables[1].Rows.Alignment = 0;
                oDoc.Application.Selection.Tables[1].Rows[1].Select();
                oDoc.Application.Selection.InsertRowsAbove(1);
                oDoc.Application.Selection.Tables[1].Rows[1].Select();
                //header row style
                oDoc.Application.Selection.Tables[1].Rows[1].Range.Bold = 1;
                oDoc.Application.Selection.Tables[1].Rows[1].Range.Font.Name = "Tahoma";
                oDoc.Application.Selection.Tables[1].Rows[1].Range.Font.Size = 14;
                //add header row manually
                for (int c = 0; c <= ColumnCount - 1; c++)
                {
                    oDoc.Application.Selection.Tables[1].Cell(1, c + 1).Range.Text = dataGridView5.Columns[c].HeaderText;
                }
                //table style 
                oDoc.Application.Selection.Tables[1].set_Style("Grid Table 4 - Accent 5");
                oDoc.Application.Selection.Tables[1].Rows[1].Select();
                oDoc.Application.Selection.Cells.VerticalAlignment = Word.WdCellVerticalAlignment.wdCellAlignVerticalCenter;
                //header text
                foreach (Word.Section section in oDoc.Application.ActiveDocument.Sections)
                {
                    Word.Range headerRange = section.Headers[Word.WdHeaderFooterIndex.wdHeaderFooterPrimary].Range;
                    headerRange.Fields.Add(headerRange, Word.WdFieldType.wdFieldPage);
                    headerRange.Text = "Company Sales Report \n ADONAI-JIREH TRADING CORPORATION \n 561 Col.Bonny Serrano, Brgy.Bayanihan, Quezon City\n" + DateTime.Now.ToString();

                    headerRange.Font.Size = 12;
                    headerRange.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                }

                foreach (Word.Section section in oDoc.Application.ActiveDocument.Sections)
                {
                    Word.Range footerRange = section.Footers[Word.WdHeaderFooterIndex.wdHeaderFooterPrimary].Range;
                    footerRange.Font.ColorIndex = Word.WdColorIndex.wdDarkRed;
                    footerRange.Font.Size = 20;
                    footerRange.Text = "Total Sales Records for this month : " + sales + "php";
                }


                //save the file

                oDoc.SaveAs2(filename);
            }
        }
        //Validate the new account 
        private void btnSave_Click(object sender, EventArgs e)
        {
            Validates1();
        }
        //validation for Purchase Quantity 
        private void txtPurQuantity_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            {
                if (!Char.IsDigit(ch) && ch != 8)
                {
                    e.Handled = true;
                    MessageBox.Show("Please Enter Valid Value");
                }
            }
        }
        //validation for Contact Number
        private void txtContact_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            {
                if (!Char.IsDigit(ch) && ch != 8)
                {
                    e.Handled = true;
                    MessageBox.Show("Please Enter Valid Value");
                }
            }
        }


        //validation for Price
        private void txtEditPrice_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            {
                if (!Char.IsDigit(ch) && ch != 8)
                {
                    e.Handled = true;
                    MessageBox.Show("Please Enter Valid Value");
                }
            }
        }
        //validation for barcode
        private void txtEditBarc_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            {
                if (!Char.IsDigit(ch) && ch != 8)
                {
                    e.Handled = true;
                    MessageBox.Show("Please Enter Valid Value");
                }
            }
        }
        //validation for Stocks
        private void txtEditStocks_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            {
                if (!Char.IsDigit(ch) && ch != 8)
                {
                    e.Handled = true;
                    MessageBox.Show("Please Enter Valid Value");
                }
            }
        }
        //validation for Price
        private void txtPrice_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            {
                if (!Char.IsDigit(ch) && ch != 8)
                {
                    e.Handled = true;
                    MessageBox.Show("Please Enter Valid Value");
                }
            }
        }
        //validation for Barcode
        private void txtBarc_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            {
                if (!Char.IsDigit(ch) && ch != 8)
                {
                    e.Handled = true;
                    MessageBox.Show("Please Enter Valid Value");
                }
            }
        }
        //validation for Stacks
        private void txtStacks_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            {
                if (!Char.IsDigit(ch) && ch != 8)
                {
                    e.Handled = true;
                    MessageBox.Show("Please Enter Valid Value");
                }
            }
        }



        //this code will create a file type docx for the reciept
        public void btnPrintR_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "0")
            {
                MessageBox.Show("Error");
            }

            else if (textBox2.Text == String.Empty)
            {
                MessageBox.Show("Enter Cash Amount");
            }
            else if (Convert.ToInt32(textBox2.Text) < Convert.ToInt32(textBox1.Text))
            {
                MessageBox.Show("Insufficient payment");
            }
            else
            {
                for (int i = 0; i < dataGridView6.Rows.Count; ++i)
                {
                    sum += Convert.ToInt32(dataGridView6.Rows[i].Cells[4].Value);
                }
                sales1 = Convert.ToString(sum);
                DateTime dt1 = DateTime.Now; // Or whatever
                string s = dt1.ToString("MMMM-dd-yyyy  hh mm ss");

                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "Word Documents (*.docx)|*.docx";

                sfd.FileName = s;
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    Export_Data_To_Word1(dataGridView6, sfd.FileName);
                }
            }
        }
        //the data in DatagridView6 (cart) will copy to the docx file that can be saved and print 
        public void Export_Data_To_Word1(DataGridView dataGridView6, string filename)
        {
            if (dataGridView6.Rows.Count != 0)
            {
                int RowCount = dataGridView6.Rows.Count;
                int ColumnCount = dataGridView6.Columns.Count;
                Object[,] DataArray = new object[RowCount + 1, ColumnCount + 1];
                //add rows
                int r = 0;
                for (int c = 0; c <= ColumnCount - 1; c++)
                {
                    for (r = 0; r <= RowCount - 1; r++)
                    {
                        DataArray[r, c] = dataGridView6.Rows[r].Cells[c].Value;
                    } //end row loop
                } //end column loop
                Word.Document oDoc = new Word.Document();
                oDoc.Application.Visible = true;
                //page orintation
                oDoc.PageSetup.Orientation = Word.WdOrientation.wdOrientLandscape;
                dynamic oRange = oDoc.Content.Application.Selection.Range;
                string oTemp = "";
                for (r = 0; r <= RowCount - 1; r++)
                {
                    for (int c = 0; c <= ColumnCount - 1; c++)
                    {
                        oTemp = oTemp + DataArray[r, c] + "\t";
                    }
                }
                //table format
                oRange.Text = oTemp;
                object Separator = Word.WdTableFieldSeparator.wdSeparateByTabs;
                object ApplyBorders = true;
                object AutoFit = true;
                object AutoFitBehavior = Word.WdAutoFitBehavior.wdAutoFitContent;

                oRange.ConvertToTable(ref Separator, ref RowCount, ref ColumnCount,
                                      Type.Missing, Type.Missing, ref ApplyBorders,
                                      Type.Missing, Type.Missing, Type.Missing,
                                      Type.Missing, Type.Missing, Type.Missing,
                                      Type.Missing, ref AutoFit, ref AutoFitBehavior, Type.Missing);
                oRange.Select();
                oDoc.Application.Selection.Tables[1].Select();
                oDoc.Application.Selection.Tables[1].Rows.AllowBreakAcrossPages = 0;
                oDoc.Application.Selection.Tables[1].Rows.Alignment = 0;
                oDoc.Application.Selection.Tables[1].Rows[1].Select();
                oDoc.Application.Selection.InsertRowsAbove(1);
                oDoc.Application.Selection.Tables[1].Rows[1].Select();
                //header row style
                oDoc.Application.Selection.Tables[1].Rows[1].Range.Bold = 1;
                oDoc.Application.Selection.Tables[1].Rows[1].Range.Font.Name = "Tahoma";
                oDoc.Application.Selection.Tables[1].Rows[1].Range.Font.Size = 14;
                //add header row manually
                for (int c = 0; c <= ColumnCount - 1; c++)
                {
                    oDoc.Application.Selection.Tables[1].Cell(1, c + 1).Range.Text = dataGridView6.Columns[c].HeaderText;
                }
                //table style 
                oDoc.Application.Selection.Tables[1].set_Style("Grid Table 4 - Accent 5");
                oDoc.Application.Selection.Tables[1].Rows[1].Select();
                oDoc.Application.Selection.Cells.VerticalAlignment = Word.WdCellVerticalAlignment.wdCellAlignVerticalCenter;
                //header text
                foreach (Word.Section section in oDoc.Application.ActiveDocument.Sections)
                {
                    Word.Range headerRange = section.Headers[Word.WdHeaderFooterIndex.wdHeaderFooterPrimary].Range;
                    headerRange.Fields.Add(headerRange, Word.WdFieldType.wdFieldPage);
                    headerRange.Text = "WONDER PETS CORPORATION \n Blk 46 Malunggay St. Tumana Marikina City\n" + DateTime.Now.ToString();

                    headerRange.Font.Size = 12;
                    headerRange.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                }

                foreach (Word.Section section in oDoc.Application.ActiveDocument.Sections)
                {
                    Word.Range footerRange = section.Footers[Word.WdHeaderFooterIndex.wdHeaderFooterPrimary].Range;
                    footerRange.Font.ColorIndex = Word.WdColorIndex.wdDarkRed;
                    footerRange.Font.Size = 17;
                    footerRange.Text = "Total Amount: " + sales1 + "php" + "\nCash: " + minus + "php" + "\nChange: " + finals + "php" + "\nPayment Method: Cash";
                }
                //save the file
                oDoc.SaveAs(filename);
            }
            PurchaseData();
            SalesData();
            DeletePending();
            refreshCart();
            RefreshSales();
            textBox1.Text = String.Empty;
            textBox2.Text = String.Empty;
            textBox3.Text = String.Empty;

        }
        //Delete the data of DatagridView6(cart)
        private void btnCartClear_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Are you sure you want to delete?", "Confirm Clear?", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if (dialogResult == DialogResult.OK)
            {
                con = new SqlConnection(@"Data Source=LAPTOP-VDMJ8H9C\SQLEXPRESS;Initial Catalog=db_Inventory;Integrated Security=True");
                con.Open();
                da = new SqlDataAdapter("Delete FROM tbl_Cart", con);
                dt = new DataTable();
                da.Fill(dt);
                dataGridView6.DataSource = dt;
                con.Close();
                refreshCart();
                return;
            }
            else if (dialogResult == DialogResult.Cancel)
            {
                return;
            }
            Close();
        }


        //transfer the data of datagridview in textbox once the productID is click
        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dataGridView2.Rows[e.RowIndex];
                txtEditProdID.Text = row.Cells["ProductID"].Value.ToString();
                txtEditProdName.Text = row.Cells["ProductName"].Value.ToString();
                txtEditStocks.Text = row.Cells["Stacks"].Value.ToString();
                txtEditCate.Text = row.Cells["Category"].Value.ToString();
                txtEditSubCate.Text = row.Cells["SubCategory"].Value.ToString();
                txtEditPrice.Text = row.Cells["Price"].Value.ToString();
                txtEditBarc.Text = row.Cells["Barcode"].Value.ToString();
                txtEditSupplier.Text = row.Cells["Supplier"].Value.ToString();
            }
        }
        //transfer the data of datagridview in textbox once the productID is click
        private void dataGridView4_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dataGridView4.Rows[e.RowIndex];
                txtPurProductID.Text = row.Cells["ProductID"].Value.ToString();

            }
        }
        //History Button
        private void btnHistory_Click(object sender, EventArgs e)
        {

        }
        //transfer the data of datagridview in textbox once the productID is click
        private void dataGridView3_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dataGridView3.Rows[e.RowIndex];
                txtDelProdID.Text = row.Cells["ProductID"].Value.ToString();
            }
        }

        private void btnShowSalesToday_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            label40.Visible = true;
            label34.Visible = false;
            label35.Visible = false;
            label36.Visible = false;
            label37.Visible = false;
            label38.Visible = false;
            label39.Visible = false;
            tabControl1.SelectedTab = tabPage1;
            
        }

        private void btnAddProduct_Click(object sender, EventArgs e)
        {
            label38.Visible = true;
            label34.Visible = false;
            label35.Visible = false;
            label36.Visible = false;
            label37.Visible = false;
            label39.Visible = false;
            label40.Visible = false;
            tabControl1.SelectedTab = tabPage2;
        }

        private void btnEditRecord_Click(object sender, EventArgs e)
        {
            label37.Visible = true;
            label34.Visible = false;
            label35.Visible = false;
            label36.Visible = false;
            label38.Visible = false;
            label39.Visible = false;
            label40.Visible = false;
            tabControl1.SelectedTab = tabPage3;
            
        }

        private void btnDeleteRecord_Click(object sender, EventArgs e)
        {
            label36.Visible = true;
            label34.Visible = false;
            label35.Visible = false;
            label37.Visible = false;
            label38.Visible = false;
            label39.Visible = false;
            label40.Visible = false;
            tabControl1.SelectedTab = tabPage4;
        }

        private void btnAddAccount_Click(object sender, EventArgs e)
        {
            label35.Visible = true;
            label34.Visible = false;
            label36.Visible = false;
            label37.Visible = false;
            label38.Visible = false;
            label39.Visible = false;
            label40.Visible = false;
            tabControl1.SelectedTab = tabPage5;
        }

        private void btnPurchas_Click(object sender, EventArgs e)
        {
            label39.Visible = true;
            label34.Visible = false;
            label35.Visible = false;
            label36.Visible = false;
            label37.Visible = false;
            label38.Visible = false;
            label40.Visible = false;
            tabControl1.SelectedTab = tabPage6;
            

        }

        private void btnSales_Click(object sender, EventArgs e)
        {
            label34.Visible = true;
            label35.Visible = false;
            label36.Visible = false;
            label37.Visible = false;
            label38.Visible = false;
            label39.Visible = false;
            label40.Visible = false;
            tabControl1.SelectedTab = tabPage7;
            


        }

        private void txtPurProductID_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void edet_Click(object sender, EventArgs e)
        {

        }

        private void txtPurQuantity_TextChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView6_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label26_Click(object sender, EventArgs e)
        {

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void tabPage6_Click(object sender, EventArgs e)
        {

        }

        private void txtEditCate_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtEditSubCate_TextChanged(object sender, EventArgs e)
        {

        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {

        }

        private void groupBox11_Enter(object sender, EventArgs e)
        {

        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }


        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void btnAddpeke_Click(object sender, EventArgs e)
        {
            label38.Visible = true;
            label34.Visible = false;
            label35.Visible = false;
            label36.Visible = false;
            label37.Visible = false;
            label39.Visible = false;
            label40.Visible = false;
            MessageBox.Show("Not Authorize.");
            return;

        }

        private void viewHelpToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void technicalSupportToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void loginHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            showHistory = "NoLogout";
            password pass = new password();
            pass.Show();
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void groupBox8_Enter(object sender, EventArgs e)
        {

        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBox1 about = new AboutBox1();
            about.Show();
        }

        private void label34_Click(object sender, EventArgs e)
        {

        }

        private void btnDeletePeke_Click(object sender, EventArgs e)
        {
            label36.Visible = true;
            label34.Visible = false;
            label35.Visible = false;
            label37.Visible = false;
            label38.Visible = false;
            label39.Visible = false;
            label40.Visible = false;
            MessageBox.Show("Not Authorize.");
            return;
        }

        private void btnAccountPeke_Click(object sender, EventArgs e)
        {
            label35.Visible = true;
            label34.Visible = false;
            label36.Visible = false;
            label37.Visible = false;
            label38.Visible = false;
            label39.Visible = false;
            label40.Visible = false;
            MessageBox.Show("Not Authorize.");
            return;
        }

        private void btnEditPeke_Click(object sender, EventArgs e)
        {
            label37.Visible = true;
            label34.Visible = false;
            label35.Visible = false;
            label36.Visible = false;
            label38.Visible = false;
            label39.Visible = false;
            label40.Visible = false;
            MessageBox.Show("Not Authorize.");
            return;
        }

        private void btnSalesPeke_Click(object sender, EventArgs e)
        {
            label34.Visible = true;
            label35.Visible = false;
            label36.Visible = false;
            label37.Visible = false;
            label38.Visible = false;
            label39.Visible = false;
            label40.Visible = false;
            MessageBox.Show("Not Authorize.");
            return;
        }

        private void textBox2_TextChanged_1(object sender, EventArgs e)
        {
            if (textBox2.Text == String.Empty)
            {
                textBox3.Text = String.Empty;
                btnPrintR.Visible = false;
                button5.Enabled = true;
                button1.Enabled = true;
                
            }
            else
            {
                button5.Enabled =false;
                button1.Enabled = false;
                btnPrintR.Visible = true;
                int newgrandtotal = Convert.ToInt32(bgtotal);
                minus = Convert.ToInt32(textBox2.Text);
                finals = minus - newgrandtotal;
                textBox3.Text = finals.ToString();
            }
        }

        private void textBox2_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            {
                if (!Char.IsDigit(ch) && ch != 8)
                {
                    e.Handled = true;
                    MessageBox.Show("Please Enter Valid Value");
                }
            }
        }

        private void txtEmailadd_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
         
            
            
            
            
            if (evtFrm1 != null)
            {
                evtFrm1();
            }

            textBox3.Text = card.one;
            textBox2.Text = card.two;
         //   button3.Visible = true;
            
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click_1(object sender, EventArgs e)
        {

        }

        private void label20_Click(object sender, EventArgs e)
        {

        }

        private void radioButton1_CheckedChanged_1(object sender, EventArgs e)
        {
           
           
           
            
            /*
            if (radioButton1.Checked == true)
            {
                textBox2.Text = card.cardbalance.ToString();
                textBox3.Text = card.currentbalance.ToString();
            }
            */

        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
         
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (textBox2.Text == String.Empty && textBox1.Text == String.Empty && textBox2.Text == String.Empty)
            {
                MessageBox.Show("Fill up first the credit card form");
                button3.Visible = false;
            }

            for (int i = 0; i < dataGridView6.Rows.Count; ++i)
            {
                sum += Convert.ToInt32(dataGridView6.Rows[i].Cells[4].Value);
            }
            sales1 = Convert.ToString(sum);
            DateTime dt1 = DateTime.Now; // Or whatever
            string s = dt1.ToString("MMMM-dd-yyyy  hh mm ss");

            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Word Documents (*.docx)|*.docx";

            sfd.FileName = s;
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                Export_Data_To_Word11(dataGridView6, sfd.FileName);
            }
        }

            public void Export_Data_To_Word11(DataGridView dataGridView6, string filename)
        {
            if (dataGridView6.Rows.Count != 0)
            {
                int RowCount = dataGridView6.Rows.Count;
                int ColumnCount = dataGridView6.Columns.Count;
                Object[,] DataArray = new object[RowCount + 1, ColumnCount + 1];
                //add rows
                int r = 0;
                for (int c = 0; c <= ColumnCount - 1; c++)
                {
                    for (r = 0; r <= RowCount - 1; r++)
                    {
                        DataArray[r, c] = dataGridView6.Rows[r].Cells[c].Value;
                    } //end row loop
                } //end column loop
                Word.Document oDoc = new Word.Document();
                oDoc.Application.Visible = true;
                //page orintation
                oDoc.PageSetup.Orientation = Word.WdOrientation.wdOrientLandscape;
                dynamic oRange = oDoc.Content.Application.Selection.Range;
                string oTemp = "";
                for (r = 0; r <= RowCount - 1; r++)
                {
                    for (int c = 0; c <= ColumnCount - 1; c++)
                    {
                        oTemp = oTemp + DataArray[r, c] + "\t";
                    }
                }
                //table format
                oRange.Text = oTemp;
                object Separator = Word.WdTableFieldSeparator.wdSeparateByTabs;
                object ApplyBorders = true;
                object AutoFit = true;
                object AutoFitBehavior = Word.WdAutoFitBehavior.wdAutoFitContent;

                oRange.ConvertToTable(ref Separator, ref RowCount, ref ColumnCount,
                                      Type.Missing, Type.Missing, ref ApplyBorders,
                                      Type.Missing, Type.Missing, Type.Missing,
                                      Type.Missing, Type.Missing, Type.Missing,
                                      Type.Missing, ref AutoFit, ref AutoFitBehavior, Type.Missing);
                oRange.Select();
                oDoc.Application.Selection.Tables[1].Select();
                oDoc.Application.Selection.Tables[1].Rows.AllowBreakAcrossPages = 0;
                oDoc.Application.Selection.Tables[1].Rows.Alignment = 0;
                oDoc.Application.Selection.Tables[1].Rows[1].Select();
                oDoc.Application.Selection.InsertRowsAbove(1);
                oDoc.Application.Selection.Tables[1].Rows[1].Select();
                //header row style
                oDoc.Application.Selection.Tables[1].Rows[1].Range.Bold = 1;
                oDoc.Application.Selection.Tables[1].Rows[1].Range.Font.Name = "Tahoma";
                oDoc.Application.Selection.Tables[1].Rows[1].Range.Font.Size = 14;
                //add header row manually
                for (int c = 0; c <= ColumnCount - 1; c++)
                {
                    oDoc.Application.Selection.Tables[1].Cell(1, c + 1).Range.Text = dataGridView6.Columns[c].HeaderText;
                }
                //table style 
                oDoc.Application.Selection.Tables[1].set_Style("Grid Table 4 - Accent 5");
                oDoc.Application.Selection.Tables[1].Rows[1].Select();
                oDoc.Application.Selection.Cells.VerticalAlignment = Word.WdCellVerticalAlignment.wdCellAlignVerticalCenter;
                //header text
                foreach (Word.Section section in oDoc.Application.ActiveDocument.Sections)
                {
                    Word.Range headerRange = section.Headers[Word.WdHeaderFooterIndex.wdHeaderFooterPrimary].Range;
                    headerRange.Fields.Add(headerRange, Word.WdFieldType.wdFieldPage);
                    headerRange.Text = "WONDER PETS CORPORATION \n Blk 46 Malunggay St. Tumana Marikina City\n" + DateTime.Now.ToString();

                    headerRange.Font.Size = 12;
                    headerRange.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                }

                foreach (Word.Section section in oDoc.Application.ActiveDocument.Sections)
                {
                    Word.Range footerRange = section.Footers[Word.WdHeaderFooterIndex.wdHeaderFooterPrimary].Range;
                    footerRange.Font.ColorIndex = Word.WdColorIndex.wdDarkRed;
                    footerRange.Font.Size = 17;
                    footerRange.Text = "Total Amount: " + sales1 + "php" + " \nCash: " + minus + "php" + " \nChange: " + finals + "php" + "\nPayment Method: Credit Card\nCard Holder: "+card.cardnamelipat+"\nCreditNo.: "+card.cardnumlipat;
                }
                //save the file
                oDoc.SaveAs(filename);
                }
            PurchaseData();
            SalesData();
            DeletePending();
            refreshCart();
            RefreshSales();
            textBox1.Text = String.Empty;
            textBox2.Text = String.Empty;
            textBox3.Text = String.Empty;
        

        }

        public void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
           
        
          

            }

        private void button5_Click(object sender, EventArgs e)
        {
            
            button1.Enabled = false;
                    
                textBox3.Text = card.cardbalance.ToString();
                textBox2.Text = card.currentbalance.ToString();
            
            
            
                textBox3.Text = String.Empty;
                textBox2.Text = String.Empty;
            



                totalcredit = Convert.ToInt32(textBox1.Text);
           
           
            button5.Enabled = false;
            card oFrm2 = new card();
            oFrm2.evtFrm += new  ShowFrm(oFrm2_evtFrm);
            oFrm2.Show();
        }

        void oFrm2_evtFrm()
        {
            button5.Enabled = true;
        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            if (textBox3.Text != null)
            {
                btnPrintR.Enabled = true;
            }
        }

        private void logoutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            DialogResult dialogResult = MessageBox.Show("Are you sure you want to Logout?", "Confirm Logout", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (dialogResult == DialogResult.OK)
            {
                string log = frmAdminPass.login;
                string userN = frmAdminPass.user;
                string Ldate = DateTime.Now.ToString();
                //history for employee
                if (log == "EMPLOYEE")
                {
                    string sql = "UPDATE tbl_EmpHistory SET Logout_Time='" + Ldate + "' WHERE Login_Time='" + frmAdminPass.logRef + "'";
                    con.Open();
                    cmd = new SqlCommand(sql, con);
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
                //history for admin
                else if (log == "ADMIN")
                {
                    string sql = "UPDATE tbl_AdminHistory SET Logout_Time='" + Ldate + "' WHERE Login_Time='" + frmAdminPass.logRef + "'";
                    con.Open();
                    cmd = new SqlCommand(sql, con);
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
                frmAdminPass fp = new frmAdminPass();
                fp.Show();
                this.Close();
            }
            else if (dialogResult == DialogResult.Cancel)
            {
                return;
            }
        }

        }
    }