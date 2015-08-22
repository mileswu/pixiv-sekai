﻿using System;
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

        private T RetrieveLocalSetting<T>(string key)
        {
            if (ApplicationData.Current.LocalSettings.Values.ContainsKey(key))
            {
                return (T)ApplicationData.Current.LocalSettings.Values[key];
            }
            else
            {
                return default(T);
            }
        }
        private void StoreLocalSetting(string key, object value)
        {
            ApplicationData.Current.LocalSettings.Values[key] = value;
        }

        private string _Username;
        public string Username
        {
            get
            {
                if (_Username == null) _Username = RetrieveLocalSetting<string>("PixivAPI_Username");
                return _Username;
            }
            private set
            {
                _Username = value;
                StoreLocalSetting("PixivAPI_Username", value);
            }
        }

        private string _Password;
        public string Password
        {
            get
            {
                if (_Password == null) _Password = RetrieveLocalSetting<string>("PixivAPI_Password");
                return _Password;
            }
            private set
            {
                _Password = value;
                StoreLocalSetting("PixivAPI_Password", value);
            }
        }
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

            // Success so store username, password and token
            Token = (string)responseJSON["response"]["access_token"];
            Username = username;
            Password = password;
            int lifetime = (int)responseJSON["response"]["expires_in"];
            Debug.WriteLine("Obtained token (token = " + Token + ", lifetime = " + lifetime + ")");
            return true;
        }

        async public Task<List<string> > Rankings()
        {
            List<string> results = new List<string>();
            WebRequest webRequest = WebRequest.Create("https://public-api.secure.pixiv.net/v1/ranking/all?mode=daily&image_sizes=px_480mw&page=1&per_page=50");
            webRequest.Headers["Authorization"] = "Bearer " + Token;

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
                return results;
            }

            Debug.WriteLine(responseBody);

            JObject responseJSON = JObject.Parse(responseBody);
            Debug.WriteLine("Number of images returned = " + responseJSON["response"][0]["works"].Count());
            foreach (JToken i in responseJSON["response"][0]["works"])
            {
                JToken work = i["work"];
                results.Add((string)work["image_urls"]["px_480mw"]);
            }
            return results;
        }
    }
}