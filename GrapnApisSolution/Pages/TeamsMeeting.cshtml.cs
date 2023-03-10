using GrapnApisSolution.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Graph;

namespace GrapnApisSolution.Pages
{
    public class TeamsMeetingModel : PageModel
    {
        [BindProperty]
        public TeamsMeetingEvent? TeamsMeetingEvent { get; set; }

        private readonly GraphServiceClient _graphServiceClient;
        public TeamsMeetingModel(GraphServiceClient graphServiceClient)
        {
            _graphServiceClient = graphServiceClient;
        }
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid && TeamsMeetingEvent != null)
            {
                var startDate = TeamsMeetingEvent.StartDate.ToString("yyyy-MM-ddTHH:mm:ssZ");
                var endDate = TeamsMeetingEvent.EndDate.ToString("yyyy-MM-ddTHH:mm:ssZ");


                var requestBody = new Microsoft.Graph.Event
                {
                    Subject = TeamsMeetingEvent.Subject,
                    Body = new ItemBody
                    {
                        ContentType = BodyType.Html,
                        Content = TeamsMeetingEvent.Body,
                    },
                    Start = new DateTimeTimeZone
                    {
                        DateTime = startDate,
                        TimeZone = "UTC",
                    },
                    End = new DateTimeTimeZone
                    {
                        DateTime = endDate,
                        TimeZone = "UTC",
                    },
                    Attendees = new List<Attendee>
                    {
                        new Attendee
                        {
                            EmailAddress = new EmailAddress
                            {
                                Address = TeamsMeetingEvent.EmailId
                            },
                            Type = AttendeeType.Required,
                        }
                    },
                    AllowNewTimeProposals = true
                };

                var result = await this._graphServiceClient.Me.Events.Request().AddAsync(requestBody);

                var meetingUrl = result.WebLink;
                ViewData["OutlookUrl"] = meetingUrl;
                return Page();
            }
            else
            {
                return Page();
            }
        }
    }
}
