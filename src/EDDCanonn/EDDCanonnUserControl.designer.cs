using System.Windows.Forms;

namespace EDDCanonn
{
    partial class EDDCanonnUserControl
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.RichTextBox richTextBox1;

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

        }
    }
}
