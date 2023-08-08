namespace ECommerce.Models.Email;

public class ConfirmEmailResponseModel : ConfirmEmailRequestModel
{
    public bool IsEmailConfirmed { get; set; } = false;
}