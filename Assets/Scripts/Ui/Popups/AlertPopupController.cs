using DefaultNamespace.Ui;
using Utils;

namespace Ui.Popups
{
    public class AlertPopupController : PresenterController<AlertPopupController>
    {
        public AlertPopupData AlertPopupData { get; private set; }

        public override void Setup(IPresenterData data = null)
        {
            base.Setup(data);
            AlertPopupData = data.CastTo<AlertPopupData>();
        }
    }
}