﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using uct.ItemName.Home;
using Microsoft.Practices.ServiceLocation;
using uct.ItemName.Services;


// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace uct.ItemName.Shell
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ShellPage : Page
    {
        private NavigationService navigationService => ServiceLocator.Current.GetInstance<NavigationService>();

        public ShellPage()
        {
            this.InitializeComponent();

            navigationService.SetNavigationFrame(frame);
        }

        private void OnItemClick(object sender, ItemClickEventArgs e)
        {
            var navigationItem = e.ClickedItem as ShellNavigationItem;
            if (navigationItem != null)
            {
                navigationService.Navigate(navigationItem.ViewModelName);
            }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter == null || String.IsNullOrEmpty(e.Parameter.ToString()))
            {
                navigationService.Navigate(typeof(HomeViewModel).FullName);
            }
            else
            {
                  navigationService.Navigate(e.Parameter.ToString());
            }
        }

        private void frame_Navigated(object sender, NavigationEventArgs e)
        {
            var viewModel = DataContext as ShellViewModel;

            if (viewModel != null)
            {
                var item = viewModel.NavigationItems.FirstOrDefault(i => i.ViewModelName == navigationService.GetViewModel(e.SourcePageType));
                if (item != null)
                {
                    viewModel.SelectedItem = item;
                    return;
                }
            }
        }
    }
}
