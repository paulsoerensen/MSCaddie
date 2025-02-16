

using MSCaddie.Shared.Models;

namespace MSCaddie.Shared.Services;

public interface IAdminService
{
    Task<SettingsModel> GetSettings();
    Task<int> SettingsUpsert(SettingsModel model);

}
