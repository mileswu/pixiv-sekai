using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using System.Net;
using Newtonsoft.Json.Linq;
using System.Diagnostics;
using System.IO;

namespace pixiv_sekai
{
    public class PixivAPI
    {
        private string Token { get; set; }

        async public Task<bool> Login(string username, string password)
        {
            WebRequest webRequest = WebRequest.Create("https://oauth.secure.pixiv.net/auth/token");
            webRequest.Method = "POST";
            webRequest.ContentType = "application/x-www-form-urlencoded";

            string postDataString = "username=" + username + "&password=" + password + "&grant_type=password&client_id=bYGKuGVw91e0NMfPGp44euvGt59s&client_secret=HP3RmkgAmEGro0gn1x9ioawQE8WMfvLXDz3ZqxpK";
            byte[] postDataBytes = Encoding.UTF8.GetBytes(postDataString);
            using (Stream newStream = await webRequest.GetRequestStreamAsync())
            {
                await newStream.WriteAsync(postDataBytes, 0, postDataBytes.Length);
            }

            string responseBody;
            try
            {
                WebResponse webResponse = await webRequest.GetResponseAsync();
                responseBody = new StreamReader(webResponse.GetResponseStream()).ReadToEnd();
            }
            catch (WebException exception)
            {
                WebResponse webResponse = exception.Response;
                responseBody = new StreamReader(webResponse.GetResponseStream()).ReadToEnd();
                Debug.WriteLine("ERROR");
                Debug.WriteLine(responseBody);
                return false;
            }

            JObject responseJSON = JObject.Parse(responseBody);

            Token = (string)responseJSON["response"]["access_token"];
            int lifetime = (int)responseJSON["response"]["expires_in"];
            Debug.WriteLine("Obtained token (token = " + Token + ", lifetime = " + lifetime + ")");
            return true;
        }
    }
}
