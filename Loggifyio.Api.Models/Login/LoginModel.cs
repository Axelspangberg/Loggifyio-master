using System;
using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata;

namespace Loggifyio.Api.Models
{
    public class LoginModel
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
    }

}
