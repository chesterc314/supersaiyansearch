using System;
using System.Collections.Generic;
using RestSharp;
using SuperSaiyanSearch.Integration.Interfaces;

namespace SuperSaiyanSearch.Integration
{
    public class HttpClient : IHttpClient
    {
        private readonly RestClient _restClient;
        public HttpClient()
        {
            _restClient = new RestClient();
        }
        public IRestResponse Get(string url, ICollection<KeyValuePair<string, string>> headers)
        {
            var response = this.GetRequst(url, headers);
            var code = (int)response.StatusCode;
            int codeResult = (code / 200);
            if(codeResult != 1)
            {
               response = this.GetRequst(url, headers);
            }
            return response;
        }

        private IRestResponse GetRequst(string url, ICollection<KeyValuePair<string, string>> headers)
        {
            var request = new RestRequest();
            request.AddHeaders(headers);
            _restClient.BaseUrl = new Uri(url);
            var response = _restClient.Get(request);
            return response;
        }
    }
}