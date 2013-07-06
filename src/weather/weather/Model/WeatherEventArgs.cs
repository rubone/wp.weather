using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clima.Model
{
    public class WeatherEventArgs : EventArgs
    {
        public IList<weather> Results { get; private set; }

        public WeatherEventArgs(IList<weather> results)
        {

            List<weather> Clima = new List<weather>();
            Clima.Add(results.FirstOrDefault());

            this.Results = Clima;
            //this.Results = results ;
        }
    }
}
