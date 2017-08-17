using System.Threading.Tasks;
using RestSharp;

namespace omicron.infra
{
    public class RestClient : IRestClient
    {
        private readonly RestSharp.RestClient client;

        public RestClient(string baseUrl)
        {
            this.client = new RestSharp.RestClient(baseUrl);
        }

        public async Task<TOutput> GetAsync<TOutput>(string url, object parameters) where TOutput : new()
        {
            var request = new RestRequest(url) { Method = Method.GET };
            request.AddHeader("User-Agent", "omicron");
            if(parameters != null) request.AddObject(parameters);

            var taskCompletion = new TaskCompletionSource<IRestResponse<TOutput>>();
            client.ExecuteAsync<TOutput>(request, r => taskCompletion.SetResult(r));
            
            var result = taskCompletion.Task.Result;
            return result.Data;
        }
    }
}