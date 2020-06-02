using System.ComponentModel.DataAnnotations;

namespace Pilllar.Vocal.Models
{
    public class UserDtoForCreation
    {
        public string Name { get; set; }
        
        [EmailAddress]
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
    }
}
