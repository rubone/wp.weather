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
using System.IO.IsolatedStorage;
using Microsoft.Phone.Scheduler;

namespace Clima
{
    public partial class Configuracion : PhoneApplicationPage
    {
        public Configuracion()
        {
            InitializeComponent();
            BuildLocalizedApplicationBar();

            this.Loaded +=Configuracion_Loaded;       
        }

        void Configuracion_Loaded(object sendet, RoutedEventArgs e)
        {
            if (IsolatedStorageSettings.ApplicationSettings.Contains("Ciudad"))
            {
                txtCiudad.Text = IsolatedStorageSettings.ApplicationSettings ["Ciudad"].ToString ();
            
            }

            if (IsolatedStorageSettings.ApplicationSettings.Contains("CheckActualizaciones"))
            {
                ckActualizaciones.IsChecked = Convert.ToBoolean(IsolatedStorageSettings.ApplicationSettings["CheckActualizaciones"].ToString());

            }
        
        }

        // Sample code for building a localized ApplicationBar
        private void BuildLocalizedApplicationBar()
        {
            // Set the page's ApplicationBar to a new instance of ApplicationBar.
            //ApplicationBar = new ApplicationBar();

            //// Create a new button and set the text value to the localized string from AppResources.
            //ApplicationBarIconButton appBarButton = new ApplicationBarIconButton(new Uri("/Assets/AppBar/appbar.add.rest.png", UriKind.Relative));
            //appBarButton.Text = AppResources.AppBarMenuBack;

            //appBarButton.Click += appBarButton_Click;

            //ApplicationBar.Buttons.Add(appBarButton);

           


        }

        void appBarButton_Click(object sender, EventArgs e)
        {
            string uri = string.Format("/MainPage.xaml");
            NavigationService.Navigate(new Uri(uri, UriKind.Relative));
        }

        const string taskName = "TareaClima";


        private void Button_Click(object sender, RoutedEventArgs e)
        {


            if(IsolatedStorageSettings.ApplicationSettings.Contains("Ciudad"))
            {
                IsolatedStorageSettings.ApplicationSettings .Remove ("Ciudad");
               
            }
            IsolatedStorageSettings.ApplicationSettings.Add("Ciudad", txtCiudad.Text);

            if (IsolatedStorageSettings.ApplicationSettings.Contains("CheckActualizaciones"))
            {
                IsolatedStorageSettings.ApplicationSettings.Remove("CheckActualizaciones");

            }
            IsolatedStorageSettings.ApplicationSettings.Add("CheckActualizaciones", ckActualizaciones.IsChecked.ToString ());

            common.ViewModel.weatherViewModel.Ciudad = IsolatedStorageSettings.ApplicationSettings["Ciudad"].ToString ();  

            if (  IsolatedStorageSettings.ApplicationSettings["CheckActualizaciones"].ToString()=="True")
            {
                var task = new PeriodicTask(taskName);
                task.Description = "Esta es mi tarea";


                var oldTask = ScheduledActionService.Find(taskName);
                if (oldTask != null)
                {
                    ScheduledActionService.Remove(taskName);
                }
                ScheduledActionService.Add(task);
                ScheduledActionService.LaunchForTest(taskName, TimeSpan.FromSeconds(10));

            }
            else
            {        
                var oldTask = ScheduledActionService.Find(taskName);
                if (oldTask != null)
                {
                    ScheduledActionService.Remove(taskName);
                }
            }
            
          
            NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));
            
        }
    }
}