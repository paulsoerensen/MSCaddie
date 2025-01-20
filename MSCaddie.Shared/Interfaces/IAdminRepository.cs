using  MSCaddie.Shared.Models;


namespace  MSCaddie.Shared.Interfaces;

public interface IAdminRepository
{
    Dictionary<string, string> Info();

    string Connectionstring { get; set; }

    #region Settings
    public List<string> GetPropertyList();
    public TValue? GetPropertyValue<TValue>(string key);

    public string? WsAccount { get; }
    public string? WsUsername { get; }
    public string? WsPassword { get; }
    public string? WsGroupGuid { get; }
    public DateTime SeasonStart { get; }
    public int Season { get; }
    public DateTime SeasonEnd { get; }

    #endregion
    #region User
    Task<User?> UserUpsert(User model);
    Task<User?> GetUserByEmail(string email);
    Task<User?> GetUserByVgcNo(int vgcNo);
    Task<User?> GetUserByToken(string token);
    Task<User?> GetUserByResetToken(string token);
    #endregion
}
