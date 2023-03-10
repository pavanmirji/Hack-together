using Azure.Identity;
using GrapnApisSolution.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Graph;
using Microsoft.Identity.Client;
using Microsoft.Identity.Web;

namespace GrapnApisSolution.Pages
{
    [AuthorizeForScopes(Scopes = new string[] { "ChannelMessage.Send", "Directory.Read.All", "Group.ReadWrite.All", "Calendars.ReadWrite" })]
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public string OutlookUrl { get; set; }

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGetAsync()
        {
            
        }
    }
}