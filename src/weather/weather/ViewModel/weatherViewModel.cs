using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Microsoft.Phone.Shell;
using Clima.Model;

namespace Clima.ViewModel
{
    public class weatherViewModel : NotificationEnableObject
    {

        bool isBusy;

        public bool IsBusy
        {
            get { return isBusy; }
            set
            {
                isBusy = value;
                OnPropertyChanged();
            }
        }



        String ciudad;
        public String Ciudad
        {
            get { return Ciudad; }
            set
            {
                ciudad = value;
            }
        }


        ObservableCollection<weather> weatherList;

        public ObservableCollection<weather> WeatherList
        {
            get
            {

                if (weatherList == null)
                {
                    weatherList = new ObservableCollection<weather>();
                }

                if (DesignerProperties.IsInDesignTool)
                {
                    for (int i = 0; i < 5; i++)
                    {
                        // ObservableCollection<weather> weatherList;
                        weatherList = new ObservableCollection<weather>();
                        //  imageList.Add(new image() { caption = "Foto", url = "http://photos.end.com.ni/2013/06/639x400_1371397184_Mario AmadorBP11.jpg" });
                        weatherList.Add(new weather() { Ciudad = "Managua", Temperatura = "32", Cielo = "Cielo", longitud = "0.00", latidud = "0.00", TypoGrados = "C" });

                    }
                }

                return weatherList;

            }
            set
            {
                weatherList = value;
                OnPropertyChanged();
            }
        }

        ServiceModel serviceModel = new ServiceModel();

        System.Windows.Threading.Dispatcher dispatcher;

        public weatherViewModel()
        {
            serviceModel.GetWeatherCompleted += (s, a) =>
                {
                   var  weatherlist = new ObservableCollection<weather>(a.Results);

                   dispatcher.BeginInvoke(() =>
                   {

                       WeatherList = weatherlist;

                       //UpdateTile();

                       UpdateTile(WeatherList.FirstOrDefault().Ciudad + ", Temperatura:" + WeatherList.FirstOrDefault().Temperatura + "°" + WeatherList.FirstOrDefault().TypoGrados  );

                       IsBusy = false;

                   });
                };
        }


        private void UpdateTile(string text)
        {
            var tile = Microsoft.Phone.Shell.ShellTile.ActiveTiles.First();

            //var iconicTile = new IconicTileData()
            //{
            //    Count = WeatherList.Count,
            // //   WideContent1 = WeatherList.Count.ToString() + " " + "Clima",
            // Title = WeatherList.FirstOrDefault().Temperatura ,
                 
            //  //  WideContent1 = "Última actualización: " + DateTime.Now.AddDays(-7).ToString()
              
               
            //};

            FlipTileData TileData = new FlipTileData()
                    {
                       Title = "[title]",
                       BackTitle = "[back of Tile title]",
                       BackContent = "[back of medium Tile size content]",
                       WideBackContent = text,
                       Count = 10,
                       //SmallBackgroundImage = [small Tile size URI],
                       //BackgroundImage = [front of medium Tile size URI],
                       //BackBackgroundImage = [back of medium Tile size URI],
                       //WideBackgroundImage = [front of wide Tile size URI],
                       //WideBackBackgroundImage = [back of wide Tile size URI],
                    };

            tile.Update(TileData);
        }

        //public void UpdateTileText(string text)
        //{

        //    //ShellTile tile = ShellTile.ActiveTiles.First();
        //    var tile = Microsoft.Phone.Shell.ShellTile.ActiveTiles.First();


        //    FlipTileData data = new FlipTileData();

        //    data.BackContent = text;


        //    tile.Update(data);

        //}


        ActionCommand getWeatherCommand;
        public ActionCommand GetWeatherCommand
        {
            get
            {
                if (getWeatherCommand == null)
                {
                    getWeatherCommand = new ActionCommand(() =>
                    {

                        IsBusy = true;

                        dispatcher = App.Current.RootVisual.Dispatcher;

                        serviceModel.GetWeather(this.ciudad );
                    });
                }
                return getWeatherCommand;
            }
        }

       
    }
}
