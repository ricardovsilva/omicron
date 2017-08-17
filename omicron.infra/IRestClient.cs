using System.Threading.Tasks;

namespace omicron.infra
{
    public interface IRestClient
    {
        Task<TOutput> GetAsync<TOutput>(string url, object parameters = null) where TOutput : new();
    }
}