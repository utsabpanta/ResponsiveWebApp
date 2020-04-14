using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using SortFeature.Data;
using SortFeature.Model;
using SortFeature.Interface;
using Microsoft.Extensions.Configuration;

namespace SortFeature.Tests
{
    public class JobListingsTests
    {
        private IConfiguration _config;
        public JobListingsTests()
        {
            var myConfiguration = new Dictionary<string, string>
            {
                {"GithubJobAPI:BaseUrl", "https://jobs.github.com/positions.json"}
            };
            _config = new ConfigurationBuilder().AddInMemoryCollection(myConfiguration).Build();
        }
        [Fact]
        public async Task GetJobsTest()
        {
            var controller = new ExternalApiClient(_config);
            var result = await controller.GetJobs();

            Assert.NotNull(result);
            Assert.IsType<List<JobDetails>>(result);
            Assert.True(result.Count > 0); // based on github jobs api, there is at least 1 job available 
        }

        [Theory]
        [InlineData("java")] // happy path
        [InlineData("node js")] // happy path
        [InlineData("(*&^%$$!")] // unhappy path
        [InlineData("123456")] // unhappy path
        public async Task SearchJobsTest(string searchTerm)
        {
            var controller = new ExternalApiClient(_config);
            var result = await controller.SearchJobs(searchTerm);

            Assert.NotNull(result);
            Assert.IsType<List<JobDetails>>(result);
        }
    }
}
