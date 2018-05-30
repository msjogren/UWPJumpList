using Windows.ApplicationModel;
using Windows.ApplicationModel.Resources;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace Jayway.UWPJumpList
{
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();
            PackageVersion pv = Package.Current.Id.Version;
            AppVersionLabel.Text = $"v{pv.Major}.{pv.Minor}.{pv.Build}";
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            if (e.Parameter is string color && !string.IsNullOrEmpty(color))
            {
                NavigateToColor(color);
            }
        }

        internal void NavigateToColor(string color)
        {
            NavViewMenu.Header = ResourceLoader.GetForCurrentView().GetString(color + "NavItem/Content");
            ContentFrame.Navigate(typeof(ColorPage), color);
        }

        private void NavViewMenu_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {
            NavigateToColor((string)((NavigationViewItem)args.SelectedItem).Tag);
        }
    }
}
