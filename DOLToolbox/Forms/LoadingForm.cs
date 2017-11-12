using System.Windows.Forms;
using DOLToolbox.Services;

namespace DOLToolbox.Forms
{
    public partial class LoadingForm : Form
    {
        private readonly ImageService _imageService = new ImageService();

        public ProgressBar ProgressBar;
        public Label ProgressText;

        public LoadingForm()
        {
            InitializeComponent();

            ProgressBar = progressBar1;
            ProgressText = lblText;

            pictureBox1.Image = _imageService.Load("loading.gif", pictureBox1.Width, pictureBox1.Height);
        }
    }
}
