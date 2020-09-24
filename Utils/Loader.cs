using Osmrt.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Streams;

namespace Osmrt.app.Utils
{
    public class Loader
    {
        private const bool DEBUG = false;
        private static Loader loader;

        public static void CreateInstanse(string host, string login, string password)
        {
            loader = new Loader(host, login, password);
        }

        public static Loader GetInstanse()
        {
            return loader;
        }

        private string token;
        private string host;

        public string getToken()
        {
            return token;
        }

        private Loader(string host, string login, string password)
        {
            this.host = host;

            WebRequest request = WebRequest.Create(host + "/auth");
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            String authData = "UserName=" + login + "&Password=" + password;
            Stream dataStream = request.GetRequestStream();
            byte[] bytes = Encoding.UTF8.GetBytes(authData);
            dataStream.Write(bytes, 0, bytes.Length);
            dataStream.Close();
            WebResponse response = request.GetResponse();
            // Get the stream containing content returned by the server.  
            // The using block ensures the stream is automatically closed.
            using (dataStream = response.GetResponseStream())
            {
                // Open the stream using a StreamReader for easy access.  
                StreamReader reader = new StreamReader(dataStream);
                // Read the content.  
                string responseFromServer = reader.ReadToEnd();
                dynamic data = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(responseFromServer);
                token = data.token;
            }

            // Close the response.  
            response.Close();
        }

        public bool isAuthorized()
        {
            return token != null;
        }

        public dynamic RPC(string action, string method, string body)
        {
            WebRequest request = WebRequest.Create(host + "/rpc");
            request.Method = "POST";
            request.ContentType = "application/json";
            request.Headers.Add("rpc-authorization", "Token " + token);
            Stream dataStream = request.GetRequestStream();
            byte[] bytes = Encoding.UTF8.GetBytes("[{\"action\":\"" + action + "\", \"method\":\"" + method + "\", \"tid\":1, \"data\":" + body + ", \"type\":\"rpc\"}]");
            dataStream.Write(bytes, 0, bytes.Length);
            dataStream.Close();
            WebResponse response = request.GetResponse();
            // Get the stream containing content returned by the server.  
            // The using block ensures the stream is automatically closed.
            dynamic data;
            using (dataStream = response.GetResponseStream())
            {
                // Open the stream using a StreamReader for easy access.  
                StreamReader reader = new StreamReader(dataStream);
                // Read the content.  
                string responseFromServer = reader.ReadToEnd();
                data = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(responseFromServer);
            }

            // Close the response.  
            response.Close();
            return data;
        }
    }
}
