using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DOLToolbox.Controls
{
    public partial class DOLQuestControl : UserControl
    {
        public DOLQuestControl()
        {
            InitializeComponent();
        }

        private void DOLQuest_Load(object sender, EventArgs e)
        {
            LoadTabForms();
        }

        private void LoadTabForms()
        {


            UserControl control = new DataQuestControl { Dock = DockStyle.Fill };
            tabData.Controls.Add(control);

            control = new RewardQuestControl { Dock = DockStyle.Fill };
            tabLos.Controls.Add(control);
        }
    }
}
