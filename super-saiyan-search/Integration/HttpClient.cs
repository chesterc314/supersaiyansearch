using System;
using System.Collections.Generic;
using System.Net;
using RestSharp;
using SuperSaiyanSearch.Integration.Interfaces;

namespace SuperSaiyanSearch.Integration
{
    public class HttpClient : IHttpClient
    {
        private const int RETRY_COUNT = 3;
        private readonly RestClient _restClient;
        public HttpClient()
        {
            _restClient = new RestClient();
            _restClient.UserAgent= "Mozilla/5.0 (Windows NT x.y; Win64; x64; rv:10.0) Gecko/20100101 Firefox/10.0";
            _restClient.CookieContainer = new CookieContainer();
        }
        public IRestResponse Get(string url, ICollection<KeyValuePair<string, string>> headers)
        {
            var request = new RestRequest();
            var response = this.GetRequest(request, url, headers);
            var count = 0;
            while (!response.IsSuccessful && count < RETRY_COUNT)
            {
                response = this.GetRequest(request, url, headers);
                if(response.IsSuccessful)
                {
                    break;
                }
                ++count;
            }
            return response;
        }

        private IRestResponse GetRequest(RestRequest request, string url, ICollection<KeyValuePair<string, string>> headers)
        {
            request.AddHeaders(headers);
            _restClient.BaseUrl = new Uri(url);
            var response = _restClient.Get(request);
            return response;
        }
    }
}