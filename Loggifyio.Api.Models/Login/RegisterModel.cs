using System;
using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata;

namespace Loggifyio.Api.Models
{
    public class RegisterModel
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
    }

}
