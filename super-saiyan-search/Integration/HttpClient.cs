using System;
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
        public IRestResponse Get(string url)
        {
            var request = new RestRequest();
            _restClient.BaseUrl = new Uri(url);
            var response = _restClient.Get(request);
            return response;
        }
    }
}