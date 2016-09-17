using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CustomServiceRegistration.Models
{
    public class UserModel
    {
        [Required]
        [Display(Name = "User Name")]
        public string UserName { get; set; }
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Required]
        [Display(Name = "SecondName")]
        public string SecondName { get; set; }
        [Required]
        [Display(Name = "Country Name")]
        public string CountryName { get; set; }
        [Required]
        [Display(Name = "Age")]
        [Range(1, 150)]
        public int Age { get; set; }
    }
}
