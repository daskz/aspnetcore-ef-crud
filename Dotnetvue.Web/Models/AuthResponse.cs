using System;
using System.Text.Json.Serialization;
using Dotnetvue.Data.Models;

namespace Dotnetvue.Web.Models
{
    public class AuthResponse
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string JwtToken { get; set; }

        public AuthResponse(User user, string jwtToken)
        {
            Id = user.Id;
            Username = user.Username;
            JwtToken = jwtToken;
        }
    }
}