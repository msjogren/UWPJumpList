using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.UI.StartScreen;

namespace Jayway.UWPJumpList.Common
{
    public class JumpListManager
    {
        /// <summary>
        /// Clears and re-creates all custom items in the app's jump list.
        /// </summary>
        public async Task RefreshJumpList()
        {
            if (!JumpList.IsSupported()) return;

            try
            {
                JumpList jumpList = await JumpList.LoadCurrentAsync();
                jumpList.Items.Clear();
                jumpList.SystemGroupKind = JumpListSystemGroupKind.None;
                string packageId = Package.Current.Id.Name;

                foreach (string color in new[] { "Blue", "Green", "Red", "Yellow" })
                {
                    var item = JumpListItem.CreateWithArguments(color, $"ms-resource://{packageId}/Resources/{color}NavItem/Content");
                    item.GroupName = $"ms-resource://{packageId}/Resources/JumpListGroupColors";
                    item.Logo = new Uri($"ms-appx:///Assets/JumpList/{color}.png");
                    jumpList.Items.Add(item);
                }

                await jumpList.SaveAsync();
            }
            catch (Exception ex)
            {
                // TODO: Proper error handling. SaveAsync may fail, for example with exception
                //   "Unable to remove the file to be replaced." (Exception from HRESULT 0x80070497)
                Debug.WriteLine(ex);
            }
        }
    }
}
