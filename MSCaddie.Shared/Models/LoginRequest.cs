using System.ComponentModel.DataAnnotations;

namespace  MSCaddie.Shared.Models;
public class LoginRequest
{
    [Required, EmailAddress]
    public string Email { get; set; } = string.Empty;
    [Required, MinLength(4)]
    public string Password { get; set; } = string.Empty;
    [Required]
    public int VgcNo { get; set; }
}
