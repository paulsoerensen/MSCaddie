using System.ComponentModel.DataAnnotations;

namespace  MSCaddie.Repository.Models;
public class ForgotPasswordRequest
{
    [Required, EmailAddress]
    public string Email { get; set; } = string.Empty;
}
