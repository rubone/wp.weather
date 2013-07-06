using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Clima.Model
{

    public class ServiceModel
    {

        public event EventHandler<WeatherEventArgs> GetWeatherCompleted;

        public void GetWeather(String Ciudad)
        {
            string uri = String.Format ("http://weather.service.msn.com/find.aspx?outputview=search&weadegreetype=C&culture=es-Es&weasearchstr={0}", Ciudad);


            

            ObservableCollection<weather> clima;
            WebClient client = new WebClient();
            client.DownloadStringCompleted += (s, a) =>
            {
                if (a.Error == null && !a.Cancelled)
                {

                    var result = a.Result;
                    string resultado = result;

                    var doc = XDocument.Parse(result);

                    var query = from c in doc.Descendants("weather")
                                select new weather()
                                {
                                    Ciudad = c.Attribute("weatherfullname").Value,
                                    latidud = c.Attribute("lat").Value,
                                    longitud = c.Attribute("lon").Value,
                                    TypoGrados = c.Attribute("degreetype").Value,
                                    Cielo = c.Element("current").Attribute("skytext").Value,
                                    Temperatura = c.Element("current").Attribute("temperature").Value + c.Attribute("degreetype").Value
                                };

                    var results = query.ToList();

                    if (GetWeatherCompleted != null)
                    {
                        GetWeatherCompleted(this, new WeatherEventArgs(results));
                    }

                }
            };
            client.DownloadStringAsync(new Uri(uri, UriKind.Absolute));
        }
    }


}
