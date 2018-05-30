﻿using System;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Jayway.UWPJumpList.Common;

namespace Jayway.UWPJumpList
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    sealed partial class App : Application
    {
        /// <summary>
        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public App()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Invoked when the application is launched normally by the end user.  Other entry points
        /// will be used such as when the application is launched to open a specific file.
        /// </summary>
        /// <param name="e">Details about the launch request and process.</param>
        protected override async void OnLaunched(LaunchActivatedEventArgs e)
        {
            Frame rootFrame = Window.Current.Content as Frame;

            // Do not repeat app initialization when the Window already has content,
            // just ensure that the window is active
            if (rootFrame == null)
            {
                await CheckForUpdate();

                // Create a Frame to act as the navigation context and navigate to the first page
                rootFrame = new Frame();

                // Place the frame in the current Window
                Window.Current.Content = rootFrame;
            }

            if (e.PrelaunchActivated == false)
            {
                if (rootFrame.Content == null)
                {
                    // When the navigation stack isn't restored navigate to the first page,
                    // configuring the new page by passing required information as a navigation 
                    // parameter
                    rootFrame.Navigate(typeof(MainPage), e.Arguments);
                }
                else if (e.Kind == ActivationKind.Launch)
                {
                    ((MainPage)rootFrame.Content).NavigateToColor(e.Arguments);
                }

                // Ensure the current window is active
                Window.Current.Activate();
            }
        }

        private async Task CheckForUpdate()
        {
            const string VersionKey = "AppVer";
            var localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
            var packageVer = Package.Current.Id.Version;

            string oldVer = localSettings.Values[VersionKey] as string ?? "";
            string currentVer = $"{packageVer.Major}.{packageVer.Minor}.{packageVer.Build}.{packageVer.Revision}";

            if (currentVer != oldVer)
            {
                localSettings.Values[VersionKey] = currentVer;

                // If this is the first app run or if Update task for some reason didn't complete after version update, 
                // recreate the jump list.
                await InitJumpList();
            }
        }

        private Task InitJumpList() => new JumpListManager().RefreshJumpList();
    }
}
