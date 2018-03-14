using System.Net;
using System.Xml.Linq;
using System;

namespace MundoCompilado.RF.LatLang
{
    public static class Map
    {
        private static readonly string URL = "http://maps.googleapis.com/maps/api/geocode/xml?address={0}&sensor=false";

        //PESQUISA ENDERECO E RETORNA LOCATION QUE TEM AS PROPRIEDADES LAT E LNG
        public static Location Get(string address)
        {
            string xml = "";
            try
            {
                var request = WebRequest.Create(string.Format(URL, Uri.EscapeDataString(address)));
                var response = request.GetResponse();
                var xdoc = XDocument.Load(response.GetResponseStream());
                xml = xdoc.ToString();
                var result = xdoc.Element("GeocodeResponse").Element("result");
                var locationElement = result.Element("geometry").Element("location");
                var lat = locationElement.Element("lat");
                var lng = locationElement.Element("lng");

                return new Location(lat.Value, lng.Value);
            }
            catch (Exception)
            {
                Console.WriteLine("Não foi possível obter o endereço = {0}", xml);
                return null;
            }
        }
    }

    //CLASSE PARA ARMAZENAR LATITUDE E LONGITUDE DO ENDERECO PESQUISADO
    public class Location
    {
        public Location(string lat, string lng)
        {
            this.lat = Convert.ToDouble(lat, System.Globalization.CultureInfo.InvariantCulture);
            this.lng = Convert.ToDouble(lng, System.Globalization.CultureInfo.InvariantCulture);
        }

        public double lat { get; set; }
        public double lng { get; set; }
    }
}
