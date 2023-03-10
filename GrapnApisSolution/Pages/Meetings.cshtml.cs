using GrapnApisSolution.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Graph;

namespace GrapnApisSolution.Pages
{
    public class MeetingsModel : PageModel
    {
        public IEnumerable<Model.Event>? Events { get; set; }

        private readonly GraphServiceClient _graphServiceClient;

        public MeetingsModel(GraphServiceClient graphServiceClient)
        {
            _graphServiceClient = graphServiceClient;
        }
        public async Task OnGetAsync()
        {
            var result = await this._graphServiceClient.Me.Events.Request().GetAsync();

            List<Model.Event> listOfMeetings = new();

            if (result != null)
            {
                if (result != null)
                {
                    for (var i = 0; i < result.Count; i++)
                    {
                        var meeting = new Model.Event
                        {
                            EventName = result[i].Subject ?? "",
                            StartDateTime = $"{DateTime.Parse(result[i].Start.DateTime).ToString("MM/dd/yyyy hh:mm tt")} {result[i].Start.TimeZone}",
                            EndDateTime = $"{DateTime.Parse(result[i].End.DateTime).ToString("MM/dd/yyyy hh:mm tt")} {result[i].End.TimeZone}"
                        };
                        listOfMeetings.Add(meeting);
                    }
                }
            }

            Events = listOfMeetings;
        }
    }
}
