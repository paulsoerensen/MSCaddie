using System.ComponentModel.DataAnnotations;

namespace  MSCaddie.Shared.Models;
public class RegisterRequest
{
    [Required, EmailAddress]
    public string Email { get; set; } = string.Empty;

    public string UserName { get; set; } = string.Empty;
    [Required]
    public int VgcNo { get; set; }

    [Required, MinLength(4)]
    public string Password { get; set; } = string.Empty;
    [Required, Compare("Password")]
    public string ConfirmPassword { get; set; } = string.Empty;
}
