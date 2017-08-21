using System.Collections.Generic;
using omicron.domain.entities;
using omicron.services.interfaces;
using omicron.infra;
using System;
using System.Linq;
using System.Threading.Tasks;
using omicron.services.model;
using omicron.domain.repositories;

namespace omicron.services
{
    public class GitService : IGitService
    {
        private readonly IRestClient gitClient;
        private readonly IScraper gitScraper;
        private readonly IGitRepoRepository gitRepoRepository;

        public GitService(IRestClient gitClient, IScraper gitScraper, IGitRepoRepository gitRepoRepository)
        {
            this.gitClient = gitClient;
            this.gitScraper = gitScraper;
            this.gitRepoRepository = gitRepoRepository;
        }
        
        public List<GitRepo> GetTrending(int quantity = 20)
        {
            var trendingRepositories = this.gitRepoRepository.GetTodayTrending().ToList();
            if(trendingRepositories.Any()) return trendingRepositories;

            trendingRepositories = ScrapTrending();
            foreach(var repository in trendingRepositories)
            {
                var repositoryDetails = gitClient.Get<GitRepoResponse>($"/repos/{repository.FullName}");
                repository.Id = repositoryDetails.id;
                repository.Name = repositoryDetails.name;
                repository.Stars = repositoryDetails.watchers_count;
                repository.Language = repositoryDetails.language;
            }

            return trendingRepositories;
        }

        private List<GitRepo> ScrapTrending()
        {
            var result = new List<GitRepo>();
            var trendingPage = gitScraper.Get("/trending");
            var repositoryNodes = trendingPage.DocumentNode.SelectNodes("//ol/li");

            foreach(var repositoryNode in repositoryNodes)
            {
                var gitRepo = new GitRepo();
                gitRepo.FullName = repositoryNode.SelectNodes("*[1]/h3/a").First().Attributes["href"].Value.Substring(1);
                var todayStarsText = repositoryNode.SelectNodes("div[last()]/span[last()]").First().InnerText;
                gitRepo.TodayStars = int.Parse(todayStarsText.Replace("\n", string.Empty).Replace(",", string.Empty).Trim().Split(' ').First());

                result.Add(gitRepo);
            }

            return result;
        }
    }
}