using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Newtonsoft.Json.Linq;

namespace Test
{
    class Program
    {
        public enum ResponseFormat
        {
            xml = 1,
            json = 2
        }

        public static string Url = "http://localhost:50662/api";

        #region Decrypt
        public static string key = "br0Bgf/UTietg3b+Z4gaOzvInWnMIgvTAEJ/FkJG6Zs="; // рандомный ключ и вектор
        public static string vector = "Nsm9KqrHkhXVAkOvm9q1kA==";
        public static string Decrypt(string data, string key, string initializingVector)
        {
            using (RijndaelManaged provider = new RijndaelManaged())
            {
                byte[] keybytes = Convert.FromBase64String(key);
                byte[] initVector = Convert.FromBase64String(initializingVector);
                var foggyBytes = Convert.FromBase64String(data);

                string result = Encoding.UTF8.GetString(Transform(foggyBytes, provider.CreateDecryptor(keybytes, initVector)));

                return result;
            }
        }
        private static byte[] Transform(byte[] textBytes, ICryptoTransform transform)
        {
            using (var buf = new MemoryStream())
            {
                using (var stream = new CryptoStream(buf, transform, CryptoStreamMode.Write))
                {
                    stream.Write(textBytes, 0, textBytes.Length);
                    stream.FlushFinalBlock();
                    return buf.ToArray();
                }
            }
        }
        #endregion

        public static object GetJsonResponse(string url)
        {
            try
            {
                using (WebClient client = new System.Net.WebClient())
                {
                    Stream stream = client.OpenRead(url);
                    StreamReader reader = new StreamReader(stream);

                    string resultString = reader.ReadToEnd();
                    object result = JsonConvert.DeserializeObject(resultString);

                    return result;
                }
            }
            catch (Exception e)
            {
                return null;
    
            }
        }

        public static object Test(string url, ResponseFormat format)
        {
            using (WebClient client = new System.Net.WebClient())
            {
                Stream stream = client.OpenRead(string.Format("{0}&type={1}", url, format == ResponseFormat.json ? "json" : "xml"));
                StreamReader reader = new StreamReader(stream);
                string result = reader.ReadToEnd();

                switch (format)
                {
                    case ResponseFormat.xml:
                        XDocument xmlresult = XDocument.Parse(result);
                        return xmlresult;
                    case ResponseFormat.json:
                       
                        return JsonConvert.DeserializeObject(result);
                    default:
                        break;
                }
            }
            return null;
        }
        static object TestUser(int id, ResponseFormat format)
        {
            string action = "user";
            object result = Test(string.Format("{0}/{1}/?id={2}",Url,action,id),format);
            return result;
        }
        static object TestAlbum(int id, ResponseFormat format)
        {
            string action = "album";
            object result = Test(
                    string.Format("{0}/{1}/?id={2}", 
                                    Url, 
                                    action, 
                                    id
                                    ), format);
            return result;
        }
        static object TestUserAlbums(int userid, ResponseFormat format)
        {
            string action = "album";
            object result = Test(
                    string.Format("{0}/{1}/?userId={2}",
                                    Url,
                                    action,
                                    userid
                                    ), format);
            return result;
        }


        static string CheckEmail(JObject value)
        {
            JProperty email = null;
            string decryptedMail = string.Empty;
            email = value.Descendants()
                    .Where(x => x.Type == JTokenType.Property)
                        .Where(x => ((JProperty)x).Name == "Email")
                            .Cast<JProperty>().FirstOrDefault();

            if (email != null)
                decryptedMail = Decrypt(email.Value.ToString(), key, vector);
            return decryptedMail;
        }
        static string CheckEmail(XDocument value)
        {
            XElement email = null;
            string decryptedMail = string.Empty;
            email = value.Descendants().Where(x => x.Name.LocalName == "Email").FirstOrDefault();

            if (email != null)
                decryptedMail = Decrypt(email.Value.ToString(), key, vector);
            return decryptedMail;
        }


        static void Main(string[] args)
        {
            JObject jsonResult = null;
            XDocument xmlResult = null;
            jsonResult = (JObject)TestUser(-1, ResponseFormat.json) ?? null;
            Console.WriteLine(CheckEmail(jsonResult));
            jsonResult = (JObject)TestUser(6, ResponseFormat.json) ?? null;
            Console.WriteLine(CheckEmail(jsonResult));
            jsonResult = (JObject)TestUser(2, ResponseFormat.json) ?? null;
            Console.WriteLine(CheckEmail(jsonResult));

            jsonResult = (JObject)TestAlbum(2, ResponseFormat.json) ?? null;

            xmlResult = (XDocument)TestUser(3, ResponseFormat.xml) ?? null;
            Console.WriteLine(CheckEmail(xmlResult));
            xmlResult = (XDocument)TestUser(3, ResponseFormat.xml) ?? null;
            Console.WriteLine(CheckEmail(xmlResult));


            xmlResult = (XDocument)TestAlbum(2, ResponseFormat.xml) ?? null;
            jsonResult = (JObject)TestAlbum(1, ResponseFormat.json) ?? null;
            xmlResult = (XDocument)TestUserAlbums(1, ResponseFormat.xml) ?? null;

        }
    }
}
