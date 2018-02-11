using System.IO;
using System.Net;
using Newtonsoft.Json;

namespace LoginetApi.Models.Common
{
    public static class JsonHelper<T>
    {
        public  static T GetJsonResponse(string url) 
        {
            using (WebClient client = new System.Net.WebClient())
            {
                Stream stream = client.OpenRead(url);
                StreamReader reader = new StreamReader(stream);

                string resultString = reader.ReadToEnd();

                T result = JsonConvert.DeserializeObject<T>(resultString);

                return result;
            }
        }
    }
}