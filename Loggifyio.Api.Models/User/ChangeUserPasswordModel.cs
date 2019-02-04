using System;
using System.ComponentModel.DataAnnotations;

namespace Loggifyio.Api.Models
{
    public class ChangeUserPasswordModel
    {
        [Required]
        public string Password { get; set; }
    }
}
