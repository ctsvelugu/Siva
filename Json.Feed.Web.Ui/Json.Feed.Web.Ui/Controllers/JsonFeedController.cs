
using Json.Feed.Web.Ui.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Json.Feed.Web.Ui.Controllers
{
    public class JsonFeedController : BaseApiController
    {

        protected virtual List<Entities.JsonFeedData> JsonFeedData
        {
            get
            {
                List<Entities.JsonFeedData> ResponseData = new List<Entities.JsonFeedData>();
                WebClient WClient = new WebClient();
                string SData = WClient.DownloadString("https://raw.githubusercontent.com/jbrooksuk/JSON-Airports/master/airports.json");
                if (!string.IsNullOrEmpty(SData))
                {
                    ResponseData = JsonFeedJson<Entities.JsonFeedData>.JsonToList(SData);
                    if (ResponseData != null && ResponseData.Count > 0)
                    {
                        ResponseData = ResponseData.Where(o => ( o.continent.Equals("eu", StringComparison.OrdinalIgnoreCase) && o.type.Equals("airport",StringComparison.OrdinalIgnoreCase))).ToList();
                        ResponseData = ResponseData.FindAll(o => o.name!=null && !o.name.Equals("", StringComparison.OrdinalIgnoreCase)).ToList();


                    }
                }
                return ResponseData;

            }
        }

        [HttpGet]
        public List<Entities.JsonFeedData> GetData(string Code,string CustomData=default(string))
        {
            List<Entities.JsonFeedData> ResponseData = this.JsonFeedData;

            if (!string.IsNullOrEmpty(Code))
            {
                ResponseData = ResponseData.Where(o => o.iso.Equals(Code, StringComparison.OrdinalIgnoreCase)).ToList();
            }
            return ResponseData;
        }

        [HttpGet]
        public List<Entities.CountryList> GetCountry(string Data)
        {
            List<Entities.CountryList> ResponseData = new List<Entities.CountryList>();
            List<Entities.CountryList> ReturedList = new List<Entities.CountryList>();
            WebClient WClient = new WebClient();
            //string CountryList = WClient.DownloadString("https://gist.githubusercontent.com/keeguon/2310008/raw/bdc2ce1c1e3f28f9cab5b4393c7549f38361be4e/countries.json");
            string CountryList = File.ReadAllText(FileHelper.JsonData);
            if (!string.IsNullOrEmpty(CountryList))
            {
                ResponseData = JsonFeedJson<Entities.CountryList>.JsonToList(CountryList);
                
                if(this.JsonFeedData!=null && this.JsonFeedData.Count > 0)
                {
                    this.JsonFeedData.ForEach(item =>
                    {
                        var ResultData = ResponseData.Where(o => o.code != null && o.code.Equals(item.iso, StringComparison.OrdinalIgnoreCase)).FirstOrDefault();
                        if(ResultData!=null)
                        {
                            var AliveCount = ReturedList.Where(o => o.code != null && o.code.Equals(item.iso, StringComparison.OrdinalIgnoreCase)).FirstOrDefault();
                            if(AliveCount==null)
                            {
                                ReturedList.Add(new Entities.CountryList()
                                {
                                    code = ResultData.code,
                                    name = ResultData.name
                                });
                            }
                        }
                    });
                }
               
            }
            return ReturedList;
        }
    }
}
