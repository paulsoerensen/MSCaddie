using AutoMapper;
using Microsoft.Extensions.Logging;
using MSCaddie.Repository.Dtos;
using MSCaddie.Repository.Interfaces;
using MSCaddie.Repository.Interfaces;
using MSCaddie.Repository.Models;


namespace MSCaddie.Repository.Services;

public class AdminService : IAdminService
{
    private readonly IAdminRepository _repo;
    ILogger<AdminService> _logger;
    IMapper mapper;

    private List<string> _settings;
    public AdminService(IAdminRepository repo,
        ILogger<AdminService> logger)
    {
        _repo = repo;
        _settings = _repo.GetPropertyList();
        _logger = logger;
        mapper = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<SettingsDto, SettingsModel>().ReverseMap();
        }).CreateMapper();
    }
    public async Task<SettingsModel> GetSettings()
    {
        _logger.LogInformation($"Called GetSettings()");

        SettingsDto dto = _repo.GetSettings();
        dto.Database = _repo.Database;
        dto.DatabaseServer = _repo.DatabaseServer;
        return mapper.Map<SettingsModel>(dto);
    }

    public async Task<int> SettingsUpsert(SettingsModel model)
    {
        _logger.LogInformation($"Called SettingsUpsert()");

        SettingsDto dto = mapper.Map<SettingsDto>(model);
        return await _repo.SettingsUpsert(dto);
    }

}

