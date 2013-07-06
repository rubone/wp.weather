using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using weather.Resources;
using Microsoft.Phone.Scheduler;


using System.IO.IsolatedStorage;

namespace Clima
{
    public partial class MainPage : PhoneApplicationPage
    {

        public MainPage()
        {
            InitializeComponent();

            // Sample code to localize the ApplicationBar
            BuildLocalizedApplicationBar();
        }


        const string taskName = "TareaClima";

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            string Ciudad = "";
            if (IsolatedStorageSettings.ApplicationSettings.Contains("Ciudad"))
            {
                Ciudad = IsolatedStorageSettings.ApplicationSettings["Ciudad"].ToString();

            }
            common.ViewModel.weatherViewModel.Ciudad = Ciudad;
            common.ViewModel.weatherViewModel.Ejecutar();   
        }


        // Sample code for building a localized ApplicationBar
        private void BuildLocalizedApplicationBar()
        {
            // Set the page's ApplicationBar to a new instance of ApplicationBar.
            ApplicationBar = new ApplicationBar();

            // Create a new button and set the text value to the localized string from AppResources.
            ApplicationBarIconButton appBarButton = new ApplicationBarIconButton(new Uri("/Assets/feature.settings.png", UriKind.Relative));
            appBarButton.Text = AppResources.AppBarButtonText;

            appBarButton.Click += appBarButton_Click;

            ApplicationBar.Buttons.Add(appBarButton);


        }

        void appBarButton_Click(object sender, EventArgs e)
        {
            string uri = string.Format("/Configuracion.xaml");
            NavigationService.Navigate(new Uri(uri, UriKind.Relative));
        }
    }
}