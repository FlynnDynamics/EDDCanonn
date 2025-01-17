using System.Windows.Forms;

namespace EDDCanonn
{
    partial class EDDCanonnUserControl
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private TextBox eventOutput;
        private Button testButton;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            // TextBox for event output
            this.eventOutput = new TextBox
            {
                Multiline = true,
                ScrollBars = ScrollBars.Vertical,
                Dock = DockStyle.Fill,
                ReadOnly = true
            };

            // Button for test action
            this.testButton = new Button
            {
                Text = "Test",
                Dock = DockStyle.Top
            };
            this.testButton.Click += TestButton_Click;

            // Adding controls
            this.Controls.Add(this.eventOutput);
            this.Controls.Add(this.testButton);
        }


    }
}
