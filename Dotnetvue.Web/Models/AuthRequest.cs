using System.ComponentModel.DataAnnotations;

namespace Dotnetvue.Web.Models
{
    public class AuthRequest
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}