using System.ComponentModel.DataAnnotations;

namespace Blog.MVC.DTOs
{
    public class RegisterDto
    {
        [EmailAddress]
        public string Email { get; set; } = default!;

        public string DisplayName { get; set; } = default!;

        public string Password { get; set; } = default!;

        [Phone]
        public string PhoneNumber { get; set; } = default!;
    }
}
