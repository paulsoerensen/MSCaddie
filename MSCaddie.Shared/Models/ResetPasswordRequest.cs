using System.ComponentModel.DataAnnotations;

namespace MSCaddie.Shared.Models;

public class ResetPasswordRequest
{
    [Required]
    public string Token { get; set; } = string.Empty;
    [Required, MinLength(4)]
    public string Password { get; set; } = string.Empty;
    [Required, Compare("Password")]
    public string ConfirmPassword { get; set; } = string.Empty;
    [Required]
    public string Email { get; set; } = string.Empty;
}
