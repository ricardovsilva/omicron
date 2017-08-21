using System.Collections.Generic;
using System.Threading.Tasks;
using omicron.domain.entities;

namespace omicron.services.interfaces
{
    public interface IGitService
    {
         List<GitRepo> GetTrending(int quantity = 20);
    }
}