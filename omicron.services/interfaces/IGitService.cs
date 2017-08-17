using System.Collections.Generic;
using System.Threading.Tasks;
using omicron.domain.entities;

namespace omicron.services.interfaces
{
    public interface IGitService
    {
         Task<List<GitRepo>> GetTrendingAsync(int quantity = 20);
    }
}