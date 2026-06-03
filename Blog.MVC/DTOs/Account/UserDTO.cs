namespace Blog.MVC.DTOs.Account
{
    public record UserDto(
    string Id,
    string Email,
    string Name,
    string Token);
}
