namespace SADSIGN_IT401P
{
    partial class frmLoginHistory
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnAdminHistory = new System.Windows.Forms.Button();
            this.btnEmployeeHistory = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGridView1.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Top;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(382, 303);
            this.dataGridView1.TabIndex = 0;
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(247, 337);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 22);
            this.btnClose.TabIndex = 1;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnAdminHistory
            // 
            this.btnAdminHistory.Location = new System.Drawing.Point(29, 309);
            this.btnAdminHistory.Name = "btnAdminHistory";
            this.btnAdminHistory.Size = new System.Drawing.Size(148, 22);
            this.btnAdminHistory.TabIndex = 2;
            this.btnAdminHistory.Text = "Admin History";
            this.btnAdminHistory.UseVisualStyleBackColor = true;
            this.btnAdminHistory.Click += new System.EventHandler(this.btnAdminHistory_Click);
            // 
            // btnEmployeeHistory
            // 
            this.btnEmployeeHistory.Location = new System.Drawing.Point(29, 337);
            this.btnEmployeeHistory.Name = "btnEmployeeHistory";
            this.btnEmployeeHistory.Size = new System.Drawing.Size(148, 22);
            this.btnEmployeeHistory.TabIndex = 3;
            this.btnEmployeeHistory.Text = "Employees History";
            this.btnEmployeeHistory.UseVisualStyleBackColor = true;
            this.btnEmployeeHistory.Click += new System.EventHandler(this.btnEmployeeHistory_Click);
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(247, 309);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(75, 22);
            this.btnClear.TabIndex = 4;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // frmLoginHistory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(382, 372);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.btnEmployeeHistory);
            this.Controls.Add(this.btnAdminHistory);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.dataGridView1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmLoginHistory";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Log-In History";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnAdminHistory;
        private System.Windows.Forms.Button btnEmployeeHistory;
        private System.Windows.Forms.Button btnClear;
    }
}