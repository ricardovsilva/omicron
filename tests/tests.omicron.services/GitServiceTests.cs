using FluentAssertions;
using Moq;
using AutoMoq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using omicron.services;
using omicron.infra;
using omicron.services.model;
using omicron.domain.repositories;
using System.Collections.Generic;
using omicron.domain.entities;
using AutoBogus;
using System.Linq;

namespace tests.omicron.services
{
    [TestClass]
    public class GitServiceTests
    {
        private AutoMoqer mocker;
        private GitService gitService;

        [TestInitialize]
        public void Setup()
        {
            this.mocker = new AutoMoqer();
            this.gitService = mocker.Create<GitService>();
        }

        [TestMethod]
        public void GetTodayTrendingRepositories_WithoutTrendingInDatabase_ShouldGetFromGitPagesAndGitApi()
        {
            this.gitService.GetTrending();
            this.mocker.Verify<IRestClient>(restClient => restClient.Get<object>("foo", null), Times.AtLeastOnce());
            this.mocker.Verify<IScraper>(scrapper => scrapper.Get("foo"), Times.AtLeastOnce());
        }

        [TestMethod]
        public void GetTodayTrendingRepositories_TrendingInDatabase_ShouldGetFromDatabaseOnly()
        {
            this.mocker.GetMock<IGitRepoRepository>().Setup(gitRepoRepository => gitRepoRepository.GetTodayTrending()).Returns(AutoFaker.Generate<GitRepo>(10).AsQueryable());
            this.gitService.GetTrending();

            this.mocker.Verify<IGitRepoRepository>(gitRepoRepository => gitRepoRepository.GetTodayTrending(), Times.AtLeastOnce());
            this.mocker.Verify<IRestClient>(restClient => restClient.Get<object>("foo", null), Times.Never());
            this.mocker.Verify<IScraper>(scrapper => scrapper.Get("foo"), Times.Never());
        }
    }
}