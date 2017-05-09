using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace IPGMMS.Models
{
    public class MapLoc
    {
        public string location { get; set; }
    }

    public static async Task<List<MapLoc>> GetLocation(string address)
    {
        // examplehttps://maps.googleapis.com/maps/api/geocode/json?address=1600+Amphitheatre+Parkway,+Mountain+View,+CA&key=
        address = "1600+Amphitheatre+Parkway,+Mountain+View,+CA";

        string apiUrl = "https://maps.googleapis.com/maps/api/geocode/json?address=";
        string apiKey = "&key=AIzaSyCMzq0fLRdhVhgT42oiQrfu-gz9m0ftvhk";

        using (var client = new HttpClient())
        {
            string repUrl = apiUrl + address + apiKey;
            HttpResponseMessage response = await client.GetAsync(repUrl);
            if (response.IsSuccessStatusCode)
            {
                string result = await response.Content.ReadAsStringAsync();
                string test = "Debug line here";
            }
        }

    }
}