using Nancy;
using omicron.domain.repositories;
using omicron.services.interfaces;

namespace omicron.api.modules
{
    public class GitRepositoryModule : NancyModule
    {
        private readonly IGitService gitService;

        public GitRepositoryModule(IGitService gitService)
        {
            this.gitService = gitService;

            Get("/trending", _ => { return Response.AsJson(gitService.GetTrendingAsync().Result); });
        }
    }
}