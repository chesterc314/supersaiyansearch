using RestSharp;

namespace SuperSaiyanSearch.Integration.Interfaces
{
    public interface IHttpClient
    {
        IRestResponse Get (string url);
    }
}