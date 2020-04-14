using SortFeature.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SortFeature.Interface
{
    public interface IExternalApiClient
    {
        public Task<List<JobDetails>> GetJobs();
        public Task<List<JobDetails>> SearchJobs(string searchTerm);
    }
}
