using Codebase.Infrastructure.GameFlow;
using UniRx;

namespace Codebase.Core.UI.Popups
{
    public class OverlayPopup : Popup
    {
        protected override void OnInitialization()
        {
            base.OnInitialization();

            MessageBroker.Default
                .Receive<GameStatusMessage>()
                .Where(msg => msg.Message == LevelStatusMessage.Finished)
                .Subscribe(_ => LevelFinished())
                .AddTo(this);
        }

        private void LevelFinished()
        {
            ClosePopup();
        }
    }
}