using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SortFeature.Data;
using SortFeature.Model;
using System.Net.Http;
using System.Text.Json;
using Newtonsoft.Json;
using SortFeature.Interface;

namespace SortFeature.Pages.Jobs
{
    public class IndexModel : PageModel
    {
        private readonly IExternalApiClient _client;
        public IndexModel(IExternalApiClient client)
        {
            _client = client;
        }

        [BindProperty(SupportsGet = true)]
        public string Search { get; set; }

        public IList<JobDetails> Jobs { get;set; }

        public async Task OnGetAsync()
        {
            if (!string.IsNullOrEmpty(Search))
            {
                var response = await _client.SearchJobs(Search);
                Jobs = response.OrderBy(s => s.Company).ToList(); // sort by company name in alphabetical order
            }
            else
            {
                
                Jobs = await _client.GetJobs();
            }
        }
    }
}
