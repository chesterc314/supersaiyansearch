using System;
using System.Collections.Generic;
using System.Net;
using RestSharp;
using SuperSaiyanSearch.Integration.Interfaces;

namespace SuperSaiyanSearch.Integration
{
    public class HttpClient : IHttpClient
    {
        private readonly RestClient _restClient;
        private readonly CookieContainer _cc;
        public HttpClient()
        {
            _restClient = new RestClient();
            _cc = new CookieContainer();
            _restClient.CookieContainer = _cc;
        }
        public IRestResponse Get(string url, ICollection<KeyValuePair<string, string>> headers)
        {
            var response = this.GetRequest(url, headers);
            var code = (int)response.StatusCode;
            int codeResult = (code / 200);
            if(codeResult != 1)
            {
               response = this.GetRequest(url, headers);
            }
            return response;
        }

        private IRestResponse GetRequest(string url, ICollection<KeyValuePair<string, string>> headers)
        {
            var request = new RestRequest();
            request.AddHeaders(headers);
            _restClient.BaseUrl = new Uri(url);
            var response = _restClient.Get(request);
            return response;
        }
    }
}