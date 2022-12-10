
namespace DemoUserControl
{ 
    partial class DemonstrationUserControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.buttonAspShip = new System.Windows.Forms.Button();
            this.buttonAllShips = new System.Windows.Forms.Button();
            this.buttonShipyards = new System.Windows.Forms.Button();
            this.buttonOutfitting = new System.Windows.Forms.Button();
            this.buttonVisited = new System.Windows.Forms.Button();
            this.buttonCarrier = new System.Windows.Forms.Button();
            this.buttonSuits = new System.Windows.Forms.Button();
            this.buttonSys1 = new System.Windows.Forms.Button();
            this.buttonCurrentSys = new System.Windows.Forms.Button();
            this.buttonShipLoadout = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // richTextBox1
            // 
            this.richTextBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBox1.Location = new System.Drawing.Point(0, 377);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(803, 266);
            this.richTextBox1.TabIndex = 0;
            this.richTextBox1.Text = "";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.textBox1);
            this.panel1.Controls.Add(this.buttonAspShip);
            this.panel1.Controls.Add(this.buttonAllShips);
            this.panel1.Controls.Add(this.buttonShipyards);
            this.panel1.Controls.Add(this.buttonOutfitting);
            this.panel1.Controls.Add(this.buttonVisited);
            this.panel1.Controls.Add(this.buttonCarrier);
            this.panel1.Controls.Add(this.buttonSuits);
            this.panel1.Controls.Add(this.buttonSys1);
            this.panel1.Controls.Add(this.buttonCurrentSys);
            this.panel1.Controls.Add(this.buttonShipLoadout);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(803, 72);
            this.panel1.TabIndex = 1;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(684, 33);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 20);
            this.textBox1.TabIndex = 1;
            // 
            // buttonAspShip
            // 
            this.buttonAspShip.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.buttonAspShip.FlatAppearance.BorderColor = System.Drawing.Color.Red;
            this.buttonAspShip.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonAspShip.ForeColor = System.Drawing.SystemColors.Control;
            this.buttonAspShip.Location = new System.Drawing.Point(219, 3);
            this.buttonAspShip.Name = "buttonAspShip";
            this.buttonAspShip.Size = new System.Drawing.Size(102, 22);
            this.buttonAspShip.TabIndex = 0;
            this.buttonAspShip.Text = "Asp Ship";
            this.buttonAspShip.UseVisualStyleBackColor = false;
            this.buttonAspShip.Click += new System.EventHandler(this.buttonAspShip_Click);
            // 
            // buttonAllShips
            // 
            this.buttonAllShips.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.buttonAllShips.FlatAppearance.BorderColor = System.Drawing.Color.Red;
            this.buttonAllShips.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonAllShips.ForeColor = System.Drawing.SystemColors.Control;
            this.buttonAllShips.Location = new System.Drawing.Point(111, 3);
            this.buttonAllShips.Name = "buttonAllShips";
            this.buttonAllShips.Size = new System.Drawing.Size(102, 22);
            this.buttonAllShips.TabIndex = 0;
            this.buttonAllShips.Text = "All Ships";
            this.buttonAllShips.UseVisualStyleBackColor = false;
            this.buttonAllShips.Click += new System.EventHandler(this.buttonAllShips_Click);
            // 
            // buttonShipyards
            // 
            this.buttonShipyards.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.buttonShipyards.FlatAppearance.BorderColor = System.Drawing.Color.Red;
            this.buttonShipyards.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonShipyards.ForeColor = System.Drawing.SystemColors.Control;
            this.buttonShipyards.Location = new System.Drawing.Point(454, 31);
            this.buttonShipyards.Name = "buttonShipyards";
            this.buttonShipyards.Size = new System.Drawing.Size(102, 22);
            this.buttonShipyards.TabIndex = 0;
            this.buttonShipyards.Text = "Shipyards";
            this.buttonShipyards.UseVisualStyleBackColor = false;
            this.buttonShipyards.Click += new System.EventHandler(this.buttonShipyards_Click);
            // 
            // buttonOutfitting
            // 
            this.buttonOutfitting.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.buttonOutfitting.FlatAppearance.BorderColor = System.Drawing.Color.Red;
            this.buttonOutfitting.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonOutfitting.ForeColor = System.Drawing.SystemColors.Control;
            this.buttonOutfitting.Location = new System.Drawing.Point(562, 31);
            this.buttonOutfitting.Name = "buttonOutfitting";
            this.buttonOutfitting.Size = new System.Drawing.Size(102, 22);
            this.buttonOutfitting.TabIndex = 0;
            this.buttonOutfitting.Text = "Outfitting";
            this.buttonOutfitting.UseVisualStyleBackColor = false;
            this.buttonOutfitting.Click += new System.EventHandler(this.buttonOutfitting_Click);
            // 
            // buttonVisited
            // 
            this.buttonVisited.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.buttonVisited.FlatAppearance.BorderColor = System.Drawing.Color.Red;
            this.buttonVisited.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonVisited.ForeColor = System.Drawing.SystemColors.Control;
            this.buttonVisited.Location = new System.Drawing.Point(562, 3);
            this.buttonVisited.Name = "buttonVisited";
            this.buttonVisited.Size = new System.Drawing.Size(102, 22);
            this.buttonVisited.TabIndex = 0;
            this.buttonVisited.Text = "Visited";
            this.buttonVisited.UseVisualStyleBackColor = false;
            this.buttonVisited.Click += new System.EventHandler(this.buttonVisited_Click);
            // 
            // buttonCarrier
            // 
            this.buttonCarrier.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.buttonCarrier.FlatAppearance.BorderColor = System.Drawing.Color.Red;
            this.buttonCarrier.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonCarrier.ForeColor = System.Drawing.SystemColors.Control;
            this.buttonCarrier.Location = new System.Drawing.Point(454, 3);
            this.buttonCarrier.Name = "buttonCarrier";
            this.buttonCarrier.Size = new System.Drawing.Size(102, 22);
            this.buttonCarrier.TabIndex = 0;
            this.buttonCarrier.Text = "Carrier";
            this.buttonCarrier.UseVisualStyleBackColor = false;
            this.buttonCarrier.Click += new System.EventHandler(this.buttonCarrier_Click);
            // 
            // buttonSuits
            // 
            this.buttonSuits.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.buttonSuits.FlatAppearance.BorderColor = System.Drawing.Color.Red;
            this.buttonSuits.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonSuits.ForeColor = System.Drawing.SystemColors.Control;
            this.buttonSuits.Location = new System.Drawing.Point(346, 3);
            this.buttonSuits.Name = "buttonSuits";
            this.buttonSuits.Size = new System.Drawing.Size(102, 22);
            this.buttonSuits.TabIndex = 0;
            this.buttonSuits.Text = "Suits";
            this.buttonSuits.UseVisualStyleBackColor = false;
            this.buttonSuits.Click += new System.EventHandler(this.buttonSuits_Click);
            // 
            // buttonSys1
            // 
            this.buttonSys1.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.buttonSys1.FlatAppearance.BorderColor = System.Drawing.Color.Red;
            this.buttonSys1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonSys1.ForeColor = System.Drawing.SystemColors.Control;
            this.buttonSys1.Location = new System.Drawing.Point(111, 31);
            this.buttonSys1.Name = "buttonSys1";
            this.buttonSys1.Size = new System.Drawing.Size(102, 22);
            this.buttonSys1.TabIndex = 0;
            this.buttonSys1.Text = "Sys 1";
            this.buttonSys1.UseVisualStyleBackColor = false;
            this.buttonSys1.Click += new System.EventHandler(this.buttonSys1_Click);
            // 
            // buttonCurrentSys
            // 
            this.buttonCurrentSys.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.buttonCurrentSys.FlatAppearance.BorderColor = System.Drawing.Color.Red;
            this.buttonCurrentSys.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonCurrentSys.ForeColor = System.Drawing.SystemColors.Control;
            this.buttonCurrentSys.Location = new System.Drawing.Point(3, 31);
            this.buttonCurrentSys.Name = "buttonCurrentSys";
            this.buttonCurrentSys.Size = new System.Drawing.Size(102, 22);
            this.buttonCurrentSys.TabIndex = 0;
            this.buttonCurrentSys.Text = "Cur Sys";
            this.buttonCurrentSys.UseVisualStyleBackColor = false;
            this.buttonCurrentSys.Click += new System.EventHandler(this.buttonCurrentSys_Click);
            // 
            // buttonShipLoadout
            // 
            this.buttonShipLoadout.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.buttonShipLoadout.FlatAppearance.BorderColor = System.Drawing.Color.Red;
            this.buttonShipLoadout.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonShipLoadout.ForeColor = System.Drawing.SystemColors.Control;
            this.buttonShipLoadout.Location = new System.Drawing.Point(3, 3);
            this.buttonShipLoadout.Name = "buttonShipLoadout";
            this.buttonShipLoadout.Size = new System.Drawing.Size(102, 22);
            this.buttonShipLoadout.TabIndex = 0;
            this.buttonShipLoadout.Text = "Cur Ship";
            this.buttonShipLoadout.UseVisualStyleBackColor = false;
            this.buttonShipLoadout.Click += new System.EventHandler(this.buttonShipLoadout_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column4});
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Top;
            this.dataGridView1.Location = new System.Drawing.Point(0, 72);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(803, 305);
            this.dataGridView1.TabIndex = 2;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "Column1";
            this.Column1.Name = "Column1";
            // 
            // Column2
            // 
            this.Column2.FillWeight = 50F;
            this.Column2.HeaderText = "Column2";
            this.Column2.Name = "Column2";
            // 
            // Column3
            // 
            this.Column3.HeaderText = "Column3";
            this.Column3.Name = "Column3";
            // 
            // Column4
            // 
            this.Column4.HeaderText = "Column4";
            this.Column4.Name = "Column4";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(693, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(19, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "v1";
            // 
            // DemonstrationUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.panel1);
            this.Name = "DemonstrationUserControl";
            this.Size = new System.Drawing.Size(803, 643);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button buttonShipLoadout;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.Button buttonAspShip;
        private System.Windows.Forms.Button buttonAllShips;
        private System.Windows.Forms.Button buttonCurrentSys;
        private System.Windows.Forms.Button buttonSys1;
        private System.Windows.Forms.Button buttonSuits;
        private System.Windows.Forms.Button buttonShipyards;
        private System.Windows.Forms.Button buttonOutfitting;
        private System.Windows.Forms.Button buttonVisited;
        private System.Windows.Forms.Button buttonCarrier;
        private System.Windows.Forms.Label label1;
    }
}
