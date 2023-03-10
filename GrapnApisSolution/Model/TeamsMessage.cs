using System.ComponentModel.DataAnnotations;

namespace GrapnApisSolution.Model
{
    public class TeamsMessage
    {
        [Required]
        public string? TeamsMsg { get; set; }
    }
}
