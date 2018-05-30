using Windows.ApplicationModel.Background;
using Jayway.UWPJumpList.Common;

namespace Jayway.UWPJumpList.BackgroundTasks
{
    public sealed class UpdateTask : IBackgroundTask
    {
        public async void Run(IBackgroundTaskInstance taskInstance)
        {
            var deferral = taskInstance.GetDeferral();

            try
            {
                await new JumpListManager().RefreshJumpList();
            }
            finally
            {
                deferral.Complete();
            }
        }
    }
}
