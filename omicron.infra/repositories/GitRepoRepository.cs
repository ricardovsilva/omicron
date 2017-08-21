using System.Collections.Generic;
using System.Linq;
using omicron.domain.entities;
using omicron.domain.repositories;

namespace omicron.infra.repositories
{
    public class GitRepoRepository : IGitRepoRepository
    {
        public IQueryable<GitRepo> GetTrending()
        {
            throw new System.NotImplementedException();
        }

        public void Save(GitRepo gitRepo)
        {
            throw new System.NotImplementedException();
        }
    }
}