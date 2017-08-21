using System.Threading.Tasks;

namespace omicron.infra
{
    public interface IRestClient
    {
        TOutput Get<TOutput>(string url, object parameters = null) where TOutput : new();
    }
}