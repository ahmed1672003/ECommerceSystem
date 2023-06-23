namespace ECommerce.Application.Features.Users.UserDTOs;
public class PostUserDTO
{
    public string? UserName { get; set; }
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }
    public string Password { get; set; }
    public string ConfirmedPassword { get; set; }
}
