using System.Reflection;
using Windows.UI;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace Jayway.UWPJumpList
{
    public sealed partial class ColorPage : Page
    {
        public ColorPage()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            RootGrid.Background = new SolidColorBrush(GetColorFromString((string)e.Parameter));
        }

        private static Color GetColorFromString(string colorName) => (Color)typeof(Colors).GetTypeInfo().GetRuntimeProperty(colorName).GetValue(null);
    }
}
