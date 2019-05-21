using System;
using System.Windows.Forms;

namespace ClassDesign
{
    public partial class GetID : Form
    {
        public string text = null;
        public GetID()
        {
            InitializeComponent();
        }

        private void GetID_Load(object sender, EventArgs e)
        {

        }

        private void Button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            text = textBox1.Text;
            this.DialogResult = DialogResult.OK;
        }
    }
}
