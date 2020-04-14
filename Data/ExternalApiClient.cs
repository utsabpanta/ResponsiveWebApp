using SortFeature.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Flurl;
using Flurl.Http;
using Microsoft.Extensions.Configuration;
using SortFeature.Interface;

namespace SortFeature.Data
{
    public class ExternalApiClient :IExternalApiClient
    {        
        private readonly string _baseUrl;
        public ExternalApiClient(IConfiguration config)
        {
            _baseUrl = config.GetValue<string>("GithubJobAPI:BaseUrl");
        }

        public async Task<List<JobDetails>> GetJobs()
        {
            var response = _baseUrl.GetJsonAsync<List<JobDetails>>();
            return await response;
        }

        public async Task<List<JobDetails>> SearchJobs(string searchTerm)
        {
            var response = _baseUrl.SetQueryParam("search", searchTerm)
                .GetJsonAsync<List<JobDetails>>();
            return await response;
        }
    }
}
