using MSCaddie.Repository.Models;

namespace MSCaddie.Repository.Interfaces;

public interface IAdminService
{
    Task<SettingsModel> GetSettings();
    Task<int> SettingsUpsert(SettingsModel model);

}
