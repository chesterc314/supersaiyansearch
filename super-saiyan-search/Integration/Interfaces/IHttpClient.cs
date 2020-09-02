using System.Collections.Generic;
using RestSharp;

namespace SuperSaiyanSearch.Integration.Interfaces
{
    public interface IHttpClient
    {
        IRestResponse Get (string url, ICollection<KeyValuePair<string, string>> headers);
    }
}