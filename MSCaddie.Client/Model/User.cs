using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MSCaddie.Client.Model
{
    public class User
    {
        public string? Name { get; set; }
        public string? EmailAddress { get; set; }
        public string? Password { get; set; }
        public string? Source { get; set; }

        public Role Role { get; set; }
        public string? ConfirmPassword { get; set; }
        public string? AccessToken { get; set; }
        public string? RefreshToken { get; set; }
    }
}
