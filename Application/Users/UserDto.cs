using Domain.Entities;

namespace Application.Users
{
    public class UserDto
    {
        public UserDto(User user, string role, string jwtToken)
        {
            Id = user.Id;
            DisplayName = user.DisplayName;
            Email = user.Email;
            Role = role;
            Token = jwtToken;
        }

        public string Id { get; set; }
        public string DisplayName { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
        public string Role { get; set; }
    }
}