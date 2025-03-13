using MSCaddie.Shared.Dtos;
using  MSCaddie.Shared.Models;


namespace  MSCaddie.Shared.Interfaces;

public interface IAdminRepository
{
    Dictionary<string, string> Info();

    string Connectionstring { get; set; }
    string Database { get; }
    string DatabaseServer { get; }

    #region Settings
    List<string> GetPropertyList();
    TValue? GetPropertyValue<TValue>(string key);
    Task<int> PropertyValueUpsert<TValue>(string key, TValue value);

    Task<SettingsDto> GetSettings();
    Task<int> SettingsUpsert(SettingsDto model);


    string? WsAccount { get; }
    string? WsUsername { get; }
    string? WsPassword { get; }
    string? WsGroupGuid { get; }
    DateTime SeasonStart { get; }
    int Season { get; }
    DateTime SeasonEnd { get; }

    #endregion
    #region User
    Task<User?> UserUpsert(User model);
    Task<User?> GetUserByEmail(string email);
    Task<User?> GetUserByVgcNo(int vgcNo);
    Task<User?> GetUserByToken(string token);
    Task<User?> GetUserByResetToken(string token);
    #endregion
}
