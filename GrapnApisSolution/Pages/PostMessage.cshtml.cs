using GrapnApisSolution.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Graph;

namespace GrapnApisSolution.Pages
{
    public class PostMessageModel : PageModel
    {
        private readonly ILogger<PostMessageModel> _logger;

        [BindProperty]
        public TeamsMessage TeamsMessage { get; set; }

        [BindProperty]
        public List<SelectListItem> TeamsList { get; set; }

        [BindProperty]
        public List<SelectListItem> ChannelsList { get; set; }

        [BindProperty]
        public string TeamId { get; set; }

        [BindProperty]
        public string ChannelId { get; set; }

        private readonly GraphServiceClient _graphServiceClient;

        public PostMessageModel(GraphServiceClient graphServiceClient)
        {
            _graphServiceClient = graphServiceClient;
        }
        public async Task OnGetAsync()
        {
            var result = await this._graphServiceClient.Me.JoinedTeams.Request().GetAsync();
            TeamsList = result.Select(x => new SelectListItem { Text = x.DisplayName, Value = x.Id }).ToList();
        }

        public async Task<IActionResult> OnPost()
        { 
            if (ModelState.IsValid && !string.IsNullOrEmpty(TeamId) && !string.IsNullOrEmpty(ChannelId))
            {
                var requestBodyTeam = new ChatMessage
                {
                    Body = new ItemBody
                    {
                        Content = TeamsMessage.TeamsMsg
                    },
                };
                var result = this._graphServiceClient.Teams[TeamId].Channels[ChannelId].Messages.Request().AddAsync(requestBodyTeam);
                return RedirectToPage("Success");
            }
            else
            {
                return Page();
            }
        }

        public async Task<JsonResult> OnGetChannelsAsync(string TeamId)
        {
            var result = await this._graphServiceClient.Teams[TeamId].Channels.Request().GetAsync();
            ChannelsList = result.Select(x => new SelectListItem { Text = x.DisplayName, Value = x.Id }).ToList();
            return new JsonResult(ChannelsList);
        }
    }
}