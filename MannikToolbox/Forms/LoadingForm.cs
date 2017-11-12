using System.Drawing;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace DOLToolbox.Forms
{
    public partial class LoadingForm : Form
    {
        private static string ImagePath => $@"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}\Assets\loading.gif";

        public ProgressBar ProgressBar;
        public Label ProgressText;

        public LoadingForm()
        {
            InitializeComponent();

            ProgressBar = progressBar1;
            ProgressText = lblText;

            pictureBox1.Image = Image.FromFile(ImagePath);
        }
    }
}
