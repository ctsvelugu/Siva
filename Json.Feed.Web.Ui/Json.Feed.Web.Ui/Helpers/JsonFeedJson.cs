using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using FrozenWareHouse.FrameWork.Web;
namespace Json.Feed.Web.Ui
{
    public class JsonFeedJson<T> where T : class
    {
       
            public static string GetJsonfromlist(List<T> Obj)
            {
                return Obj.ToJSON();
            }
            public static string GetJsonFromSingleOrDefault(T Obj)
            {
                return Obj.ToJSON();
            }
            public static string GetJsonFromString(string Value)
            {
                return Value.ToJSON();
            }
            public static List<T> JsonToList(string JsonResponseOutput)
            {
                JavaScriptSerializer JavaScriptSearilizer = new JavaScriptSerializer();
                JavaScriptSearilizer.MaxJsonLength = 2147483647;
                List<T> ListSourceItems = JavaScriptSearilizer.Deserialize<List<T>>(JsonResponseOutput);
                return ListSourceItems;
            }
            public static T JsonToSingleOrDefault(string JsonResponseOutput)
            {
                JavaScriptSerializer JavaScriptSearilizer = new JavaScriptSerializer();
                JavaScriptSearilizer.MaxJsonLength = 2147483647;
                T person = (T)JavaScriptSearilizer.Deserialize<T>(JsonResponseOutput);
                return person;
            }
        
    }
}