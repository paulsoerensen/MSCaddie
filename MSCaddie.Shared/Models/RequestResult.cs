namespace MSCaddie.Shared.Models;
public class RequestResult
{
    public string Message { get; set; } = string.Empty;
    public bool Success { get; set; }
    public string UserName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
}
