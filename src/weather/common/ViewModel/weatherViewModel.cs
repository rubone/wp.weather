using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Microsoft.Phone.Shell;
using common.Model;

namespace common.ViewModel
{
    public class weatherViewModel : NotificationEnableObject
    {


        public static string Ciudad {get;set;}


      static   ObservableCollection<weather> weatherList;

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

      static  ServiceModel serviceModel = new ServiceModel();


        public weatherViewModel()
        {

            serviceModel.GetWeatherCompleted += (s, a) =>
                {
                   var  weatherlist = new ObservableCollection<weather>(a.Results);
                       WeatherList = weatherlist;
                       UpdateTile(WeatherList.FirstOrDefault().Ciudad +  ' ' + WeatherList.FirstOrDefault().Temperatura);  //+ "°" + WeatherList.FirstOrDefault().TypoGrados  );
                };
        }


        private void UpdateTile(string text)
        {
            var tile = Microsoft.Phone.Shell.ShellTile.ActiveTiles.First();


            FlipTileData TileData = new FlipTileData()
                    {
                       Title = "Weather",
                       BackTitle = "Weather",
                       BackContent = text,
                       WideBackContent = text,
                    };

            

            tile.Update(TileData);
        }


                   public static void Ejecutar(){

                     
                        serviceModel.GetWeather(Ciudad  );

                   }
    

       
    }
}
