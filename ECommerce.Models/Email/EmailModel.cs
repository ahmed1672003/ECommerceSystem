namespace ECommerce.Models.Email;
public class EmailModel
{
    public string ServiceMessage { get; set; }
    public string Subject { get; set; }
    public string MailFrom { get; set; }
    public string MailTo { get; set; }
    public string Message { get; set; }
    public bool IsSendSuccess { get; set; }
    public string DisplayName { get; set; }
}