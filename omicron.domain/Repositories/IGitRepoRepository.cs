using System.Collections.Generic;
using System.Linq;
using omicron.domain.entities;

namespace omicron.domain.repositories
{
    public interface IGitRepoRepository
    {
        void Save(GitRepo gitRepo);
        IQueryable<GitRepo> GetTrending();
        IQueryable<GitRepo> GetTodayTrending();
    }
}