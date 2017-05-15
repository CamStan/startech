using IPGMMS.Abstract;
using IPGMMS.Models;
using IPGMMS.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace IPGMMS.Controllers
{
    public class MapController : MController
    {
        private IMemberRepository memberRepo;
        private IContactRepository contactRepo;
        private static HttpClient client = new HttpClient();
        private string apiUrl = "https://maps.googleapis.com/maps/api/geocode/json?address=";
        private string apiKey = "&key=AIzaSyCMzq0fLRdhVhgT42oiQrfu-gz9m0ftvhk";

        public MapController(IMemberRepository mRepo, IContactRepository cRepo)
        {
            memberRepo = mRepo;
            contactRepo = cRepo;
        }
        // GET: Map
        /// <summary>
        /// Shows map centered on the zip code given. If no zip code is given
        /// or the one entered is invalid, it will use 97304 as default. This
        /// is the zip code of the IPG HQ.
        /// </summary>
        /// <param name="zipCode">give as string to allow for chars</param>
        /// <returns>returns view and MapJson objects</returns>
        public async Task<ActionResult> Index(string zipCode)
        {
            // Tower of London zip code -> address = "EC3N 4AB";
            if (zipCode == null)
            {
                zipCode = "97304";
            }
            ViewBag.locations = await GetMapInfos();
            ViewBag.center = await GetCenter(zipCode);
            if (ViewBag.center == null)
            {
                ViewBag.center = await GetCenter("97304");
            }
            return View();
        }

        /// <summary>
        /// Retrieves all listing addresses from the database in order to find
        /// the longitude and latitude coordinates.
        /// </summary>
        /// <returns></returns>
        private async Task<List<MapInfo>> GetMapInfos()
        {
            List<MapInfo> mapinfos = new List<MapInfo>();

            var memberIDs = memberRepo.GetActiveMemberIDs();

            List<ContactInfo> listingInfo = new List<ContactInfo>();
            List<Tuple<int, ContactInfo>> tempList = new List<Tuple<int, ContactInfo>>();

            for (int i = 0; i < memberIDs.Length; i++)
            {
                ContactInfo listInfo = contactRepo.ListingInfoFromMID(memberIDs[i]);
                if (listInfo.StateName != null)
                {
                    tempList.Add(new Tuple<int, ContactInfo>(memberIDs[i], listInfo));
                }
            }

            foreach (var tup in tempList)
            {

                var address = tup.Item2.StreetAddress + ",+"
                            + tup.Item2.City + ",+"
                            + tup.Item2.StateName;
                address = address.Replace(" ", "+");
                Debug.WriteLine(address);
                MapJson locationCheck = await GetLocation(address);
                if (locationCheck != null)
                {
                    MapInfo mapinfo = new MapInfo();
                    Member member = memberRepo.Find(tup.Item1);

                    mapinfo.Position = locationCheck.results.FirstOrDefault().geometry.location;
                    mapinfo.FullName = member.FullName;
                    mapinfo.BusinessName = member.BusinessName;
                    mapinfo.MemberLevel = member.MemberLevel1.MLevel;
                    mapinfo.Website = member.Website;
                    mapinfo.Address1 = tup.Item2.StreetAddress;
                    mapinfo.Address2 = tup.Item2.City + ", "
                                     + tup.Item2.StateName + " "
                                     + tup.Item2.PostalCode;
                    mapinfo.PhoneNumber = tup.Item2.PhoneNumber;
                    mapinfo.Email = tup.Item2.Email;
                    
                    mapinfos.Add(mapinfo);
                }
            }
            return mapinfos;
        }


        /// <summary>
        /// Usings Google Geocoder API to retrieve the longitude and latitude
        /// coordinates (and other info) for a given address.
        /// </summary>
        /// <param name="address">streetnum+road+city+state</param>
        /// <returns>C# object representing the JSON from Google</returns>
        public async Task<MapJson> GetLocation(string address)
        {
            //Task<MapJson>
            // examplehttps://maps.googleapis.com/maps/api/geocode/json?address=1600+Amphitheatre+Parkway,+Mountain+View,+CA&key=

            //address = "1600+Amphitheatre+Parkway,+Mountain+View,+CA";
            string repUrl = apiUrl + address + apiKey;
            HttpResponseMessage response = await client.GetAsync(repUrl).ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                string apiResponse = await response.Content.ReadAsStringAsync();

                MapJson testObject = JsonConvert.DeserializeObject<MapJson>(apiResponse);

                // Will only add good addresses
                if (testObject.status == "OK")
                {
                    return testObject;
                }
            }

            return null;
        }

        private async Task<MapJson> GetCenter(string address)
        {

            string repUrl = apiUrl + ",&components=postal_code:" + address + apiKey;
            HttpResponseMessage response = await client.GetAsync(repUrl).ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                MapJson center = JsonConvert.DeserializeObject<MapJson>(apiResponse);

                // Returns only good addresses.
                if (center.status == "OK")
                {
                    return center;
                }
            }
            return null;
        }
    }
}