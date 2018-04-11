namespace eddtk
{
    partial class demoForm
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
            this.rendererEDDTK3D = new EDDTK.Plot3D.Rendering.View.Renderer3D();
            this.SuspendLayout();
            // 
            // rendererEDDTK3D
            // 
            this.rendererEDDTK3D.BackColor = System.Drawing.Color.Black;
            this.rendererEDDTK3D.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rendererEDDTK3D.Location = new System.Drawing.Point(0, 0);
            this.rendererEDDTK3D.Name = "rendererEDDTK3D";
            this.rendererEDDTK3D.Size = new System.Drawing.Size(484, 461);
            this.rendererEDDTK3D.TabIndex = 0;
            this.rendererEDDTK3D.VSync = false;
            // 
            // demoForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(484, 461);
            this.Controls.Add(this.rendererEDDTK3D);
            this.Name = "demoForm";
            this.Text = "Demo EDDTK";
            this.Load += new System.EventHandler(this.demoForm_Load_1);
            this.ResumeLayout(false);

        }

        #endregion

        private EDDTK.Plot3D.Rendering.View.Renderer3D rendererEDDTK3D;
    }
}

