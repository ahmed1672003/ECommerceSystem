namespace ECommerce.Models.Email;
public class ConfirmEmailRequestModel
{
    public string? Token { get; set; }
    public string? UserId { get; set; }
}
