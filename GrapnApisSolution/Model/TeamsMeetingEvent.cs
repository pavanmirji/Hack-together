using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace GrapnApisSolution.Model
{
    public class TeamsMeetingEvent
    {

        [Required]
        public string? Subject { get; set; }

        [Required]
        public string? Body { get; set; }

        [Required]
        [BindProperty, DisplayFormat(DataFormatString = "{0:yyyy-MM-ddTHH:mm}", ApplyFormatInEditMode = true)]
        public DateTime StartDate { get; set; }

        [Required]
        [BindProperty, DisplayFormat(DataFormatString = "{0:yyyy-MM-ddTHH:mm}", ApplyFormatInEditMode = true)]
        public DateTime EndDate { get; set; }

        [Required]
        public string? EmailId { get; set; }
    }
}
