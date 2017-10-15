using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MannikToolbox.Forms
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
    }
}
