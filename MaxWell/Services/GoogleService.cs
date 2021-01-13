using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Acr.UserDialogs;
using MaxWell.Models;
using Google.Apis.Customsearch.v1;
using Google.Apis.Services;
using Newtonsoft.Json;

namespace MaxWell.Services
{
    class GoogleService
    {
        public GoogleService()
        {
            client = new HttpClient();
            //client.MaxResponseContentBufferSize = 256000;
            client.MaxResponseContentBufferSize = 10000000;
        }
        HttpClient client;
        private static  GoogleService Instance;

        public static GoogleService getInstance()
        {
            if (Instance == null) Instance = new GoogleService();
            return Instance;
        }

        /*
         * <script>
  (function() {
    var cx = '017563873115342459514:lbpkx4wsx4u';
    var gcse = document.createElement('script');
    gcse.type = 'text/javascript';
    gcse.async = true;
    gcse.src = 'https://cse.google.com/cse.js?cx=' + cx;
    var s = document.getElementsByTagName('script')[0];
    s.parentNode.insertBefore(gcse, s);
  })();
</script>
<gcse:search></gcse:search>
         */

        public string Search(string query)
        {
            string output = "";
            try
            {

                string apiKey = "AIzaSyD2woY0xhfYIVnEHW71mb4HnS79biwBPy4";
                string cx = "017563873115342459514:lbpkx4wsx4u";
                var svc = new Google.Apis.Customsearch.v1.CustomsearchService(new BaseClientService.Initializer { ApiKey = apiKey });
                var listRequest = svc.Cse.List(query);

                listRequest.Cx = cx;
                listRequest.Num = 1;
                listRequest.SearchType = CseResource.ListRequest.SearchTypeEnum.Image;
                listRequest.FileType = "png";
              //  listRequest.ImgType = CseResource.ListRequest.ImgTypeEnum.Photo;
               // listRequest.ImgColorType = CseResource.ListRequest.ImgColorTypeEnum.Color;
               // listRequest.Rights = "cc_noncommercial";
              //  listRequest.ImgSize= CseResource.ListRequest.ImgSizeEnum.Medium;
                //listRequest.Hq = "";
                //   listRequest.ImgDominantColor = CseResource.ListRequest.ImgDominantColorEnum.White;
                //   listRequest.FileType = "png";
                listRequest.Start = 1;

                var search = listRequest.Execute();

                if (search.Items.Count > 0)
                {
                    //      UserDialogs.Instance.AlertAsync(search.Items[0].Title + " " + search.Items[0].Link);
                    output = search.Items[0].Link;
                }
            }
            catch (Exception e)
            {
                UserDialogs.Instance.AlertAsync(e.Message);

            }
            return output;
        }
        public async Task<string> getImage(string name= "orange")
        {
            
            string output = "";
            string[] names = {"orange"};
            name = name.Replace(","," ").Replace("  "," ");
            var nameToSearch = name.Replace(",", " ").Replace("  ", " ");
            if (name.Contains(" "))
            {
                 names = name.Split(' ');
                nameToSearch = names[0] + " " + names[1];
              // if(names.Length>2) nameToSearch = names[0] + " " + names[1] + " " + names[2];
               
            }
            string query = nameToSearch + " transparent";

           
                output = Search(query);
            if (output.Equals(""))
            {
                nameToSearch = names[0] + " " + names[1];
 output = Search(nameToSearch);
            }
         
            if (output.Equals(""))
            {
                nameToSearch = names[0];
                output = Search(nameToSearch);
            }
            if (output.Equals(""))
            {
                nameToSearch = name;
                output = Search(nameToSearch);
            }
            return output;
            //  foreach (var result in search.Items)
            //  {
            //      UserDialogs.Instance.AlertAsync(result.Title + " " + result.Link);
            //     }
        }







        public async Task<ImageList> getImages(string name = "orange")
        {

            ImageList output = new ImageList();
            string[] names = { "orange" };
            name = name.Replace(",", " ").Replace("  ", " ");
            if (name.Contains(" "))
            {
                names = name.Split(' ');
                name = names[0] + " " + names[1];


            }
            string query = name + "";


            output = SearchImages(query);
            if (output.Equals(""))
                output = SearchImages(names[0]);
            return output;
            //  foreach (var result in search.Items)
            //  {
            //      UserDialogs.Instance.AlertAsync(result.Title + " " + result.Link);
            //     }
        }



        public ImageList SearchImages(string query)
        {
            ImageList output = new ImageList();
            try
            {

                string apiKey = "AIzaSyD2woY0xhfYIVnEHW71mb4HnS79biwBPy4";
                string cx = "017563873115342459514:lbpkx4wsx4u";
                var svc = new Google.Apis.Customsearch.v1.CustomsearchService(new BaseClientService.Initializer { ApiKey = apiKey });
                var listRequest = svc.Cse.List(query);

                listRequest.Cx = cx;
                listRequest.Num = 10;
                listRequest.SearchType = CseResource.ListRequest.SearchTypeEnum.Image;
                listRequest.FileType = "png";
                listRequest.ImgType = CseResource.ListRequest.ImgTypeEnum.Photo;
                listRequest.ImgColorType = CseResource.ListRequest.ImgColorTypeEnum.Color;
                listRequest.ImgSize = CseResource.ListRequest.ImgSizeEnum.Medium;

                //listRequest.Hq = "";
                //   listRequest.ImgDominantColor = CseResource.ListRequest.ImgDominantColorEnum.White;
                //   listRequest.FileType = "png";
                //listRequest.Start = 1;

                var search = listRequest.Execute();

                if (search.Items.Count > 0)
                {
                    output.Photos = new List<string>();
                    for (int i = 0; i < search.Items.Count; i++)
                    {
                        output.Photos.Add(search.Items[i].Link);
                    }
                    //      UserDialogs.Instance.AlertAsync(search.Items[0].Title + " " + search.Items[0].Link);
                    
                }
            }
            catch (Exception e)
            {
                UserDialogs.Instance.AlertAsync(e.Message);

            }
            return output;
        }




        /*
        public void getImage()
        {
            string apiKey = "AIzaSyD2woY0xhfYIVnEHW71mb4HnS79biwBPy4";
            string key = "";
            string qry = "nebulas";
            string cx = "";
            string fileType = "png,jpg";
            string searchType = "image";
            Uri url = new Uri("https://www.googleapis.com/customsearch/v1?key=" + key + "&amp;cx=" + cx + "&amp;q=" + qry + "&amp;fileType=" + fileType + "&amp;searchType=" + searchType + "&amp;alt=json");
            HttpURLConnection conn = (HttpURLConnection)url.openConnection();
            conn.setRequestMethod("GET");
            conn.setRequestProperty("Accept", "application/json");
            BufferedReader br = new BufferedReader(new InputStreamReader((conn.getInputStream())));
            GResults results = new Gson().fromJson(br, GResults.class);
            conn.disconnect();



            
            try
            {
                var response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    Foods = JsonConvert.DeserializeObject<List<Food>>(content);
                }
            }
            catch (Exception ex)
            {
             
                Debug.WriteLine(@"				ERROR {0}", ex.Message);
                throw ex;
            }

        }*/



    }
}
