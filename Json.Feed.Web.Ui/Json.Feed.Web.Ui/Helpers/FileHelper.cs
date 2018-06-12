using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Json.Feed.Web.Ui.Helpers
{
    public static  class FileHelper
    {
        public static  string JsonData
        {
            get
            {
                return HttpContext.Current.Server.MapPath("~/App_Data/Country.json");
            }
        }
    }
}