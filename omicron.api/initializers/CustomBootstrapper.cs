using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Options;
using omicron.api.configuration;
using omicron.domain.repositories;
using omicron.infra;
using omicron.infra.repositories;
using omicron.services;
using omicron.services.interfaces;

namespace omicron.api.initializers
{
    public class CustomBoostrapper : Nancy.DefaultNancyBootstrapper
    {
        private readonly IApplicationBuilder appBuilder;

        public CustomBoostrapper(IApplicationBuilder appBuilder)
        {
            this.appBuilder = appBuilder;
        }

        protected override void ConfigureApplicationContainer(Nancy.TinyIoc.TinyIoCContainer container)
        {
            base.ConfigureApplicationContainer(container);
            container.Register<IOptions<Configuration>>((c, p) => (IOptions<Configuration>)appBuilder.ApplicationServices.GetService(typeof(IOptions<Configuration>)));
            container.Register<IRestClient>((c, p) => new RestClient(container.Resolve<IOptions<Configuration>>().Value.ApiBaseUrl));
            container.Register<IScraper>((c, p) => new HtmlScraper(container.Resolve<IOptions<Configuration>>().Value.GithubUrl));
            container.Register<IGitRepoRepository, GitRepoRepository>();
            container.Register<IGitService, GitService>();
        }
    }
}