﻿namespace ECommerce.Models.Email;
public class ConfirmEmailRequestModel
{
    public string? Code { get; set; }
    public string? UserId { get; set; }
}
