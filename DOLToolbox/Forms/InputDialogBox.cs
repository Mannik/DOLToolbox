using System.Windows.Forms;

namespace DOLToolbox.Forms
{
    public partial class InputDialogBox : Form
    {
        public Label Caption;
        public TextBox Input;
        

        public InputDialogBox()
        {
            InitializeComponent();

            Caption = lblCaption;
            Input = txtValue;
        }

        private void txtValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                DialogResult = DialogResult.OK;
            }
        }
    }
}
