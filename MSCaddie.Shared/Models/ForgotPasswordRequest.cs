using System.ComponentModel.DataAnnotations;

namespace  MSCaddie.Shared.Models;
public class ForgotPasswordRequest
{
    [Required, EmailAddress]
    public string Email { get; set; } = string.Empty;
}
