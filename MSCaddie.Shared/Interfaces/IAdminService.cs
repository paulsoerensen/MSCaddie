using MSCaddie.Shared.Models;

namespace MSCaddie.Shared.Interfaces;

public interface IAdminService
{
    Task<SettingsModel> GetSettings();
    Task<int> SettingsUpsert(SettingsModel model);

}
