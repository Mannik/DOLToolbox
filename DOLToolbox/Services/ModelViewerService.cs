using DOL.Util.DOLModelViewer;

namespace DOLToolbox.Services
{
    public class ModelViewerService
    {
        public static DOLModelViewer Viewer { get; }

        static ModelViewerService()
        {
            Viewer = new DOLModelViewer();
        }
    }
}