namespace WindowsFormsApplication1
{
    partial class frm_Import
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.textBox_MGCBContent = new System.Windows.Forms.TextBox();
            this.button_Import = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.textBox_MGCBContent);
            this.groupBox1.Location = new System.Drawing.Point(12, 36);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(784, 343);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "MGCB Content Preview";
            // 
            // textBox_MGCBContent
            // 
            this.textBox_MGCBContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox_MGCBContent.Location = new System.Drawing.Point(3, 16);
            this.textBox_MGCBContent.Multiline = true;
            this.textBox_MGCBContent.Name = "textBox_MGCBContent";
            this.textBox_MGCBContent.Size = new System.Drawing.Size(778, 324);
            this.textBox_MGCBContent.TabIndex = 0;
            // 
            // button_Import
            // 
            this.button_Import.Location = new System.Drawing.Point(15, 385);
            this.button_Import.Name = "button_Import";
            this.button_Import.Size = new System.Drawing.Size(782, 56);
            this.button_Import.TabIndex = 7;
            this.button_Import.Text = "Import && Build";
            this.button_Import.UseVisualStyleBackColor = true;
            this.button_Import.Click += new System.EventHandler(this.button_Import_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.Filter = "FBX and PNG files|*.png;*.fbx";
            this.openFileDialog1.Multiselect = true;
            this.openFileDialog1.Title = "Import Files...";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(271, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "Please select both an FBX file and its associated texture";
            // 
            // frm_Import
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(808, 453);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.button_Import);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "frm_Import";
            this.Text = "Asset Importer";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox textBox_MGCBContent;
        private System.Windows.Forms.Button button_Import;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Label label1;
    }
}