using System.ComponentModel.DataAnnotations;

namespace CustomServiceRegistration.Models
{
    public class ApplicationModel
    {
        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }
    }
}
