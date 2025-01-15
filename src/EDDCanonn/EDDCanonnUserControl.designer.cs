using System.Windows.Forms;

namespace EDDCanonn
{
    partial class EDDCanonnUserControl
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private TextBox eventOutput;
        private void InitializeComponent()
        {
           
        
            this.eventOutput = new TextBox
            {
                Multiline = true,
                ScrollBars = ScrollBars.Vertical,
                Dock = DockStyle.Fill,
                ReadOnly = true
            };
            this.Controls.Add(this.eventOutput);
            // 
            // buttonCurrentSys
            // 
            this.buttonCurrentSys.Location = new System.Drawing.Point(20, 60);
            this.buttonCurrentSys.Name = "buttonCurrentSys";
            this.buttonCurrentSys.Size = new System.Drawing.Size(200, 30);
            this.buttonCurrentSys.TabIndex = 1;
            this.buttonCurrentSys.Text = "Get System Info";
            this.buttonCurrentSys.UseVisualStyleBackColor = true;
            this.buttonCurrentSys.Click += new System.EventHandler(this.buttonCurrentSys_Click);
            // 
            // DemonstrationUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.eventOutput);
            this.Controls.Add(this.buttonCurrentSys);
            this.Name = "DemonstrationUserControl";
            this.Size = new System.Drawing.Size(250, 120);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.Button buttonCurrentSys;
    }
}
