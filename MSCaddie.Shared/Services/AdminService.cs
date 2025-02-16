using AutoMapper;
using Microsoft.Extensions.Logging;
using MSCaddie.Shared.Dtos;
using MSCaddie.Shared.Interfaces;
using MSCaddie.Shared.Models;

namespace MSCaddie.Shared.Services;
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
        //    .ForMember(dest => dest.ValueInt, opt => opt.MapFrom(src => ConvertToInt(src.DataValue, src.SystemType)))
        //    .ForMember(dest => dest.ValueDateTime, opt => opt.MapFrom(src => ConvertToDateTime(src.DataValue, src.SystemType)))
        //    .ForMember(dest => dest.ValueText, opt => opt.MapFrom(src => ConvertToString(src.DataValue, src.SystemType)))
        //    .ForMember(dest => dest.ValidFrom, opt => opt.MapFrom(src => src.ValidFrom))
        //    .ForMember(dest => dest.ValidTo, opt => opt.MapFrom(src => src.ValidTo))
        //    .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description));

            //    cfg.CreateMap<PropertyModel, PropertyDto>()
            //        .ForMember(dest => dest.DataValue, opt => opt.MapFrom(src => ConvertToDataValue(src)))
            //        .ForMember(dest => dest.SystemType, opt => opt.MapFrom(src => DetermineSystemType(src)))
            //        .ForMember(dest => dest.ValidFrom, opt => opt.MapFrom(src => src.ValidFrom))
            //        .ForMember(dest => dest.ValidTo, opt => opt.MapFrom(src => src.ValidTo))
            //        .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description)); 
            })
            .CreateMapper();
    }
    //#region Conversion

    //// Method to convert DataValue to int
    //private int ConvertToInt(string? dataValue, string? systemType)
    //{
    //    if (systemType == "System.Int32" && int.TryParse(dataValue, out var result))
    //    {
    //        return result;
    //    }
    //    return 0;  // Default value if conversion fails
    //}

    //// Method to convert DataValue to DateTime
    //private DateTime ConvertToDateTime(string? dataValue, string? systemType)
    //{
    //    if (systemType == "System.DateTime" && DateTime.TryParse(dataValue, out var dateResult))
    //    {
    //        return dateResult;
    //    }
    //    return DateTime.MinValue;  // Default value if conversion fails
    //}

    //// Method to convert DataValue to string
    //private string ConvertToString(string? dataValue, string? systemType)
    //{
    //    if (systemType == "System.String")
    //    {
    //        return dataValue ?? string.Empty;
    //    }
    //    return string.Empty;  // Default value if not a string
    //}

    //// Method to convert PropertyModel back to DataValue
    //private string ConvertToDataValue(PropertyModel src)
    //{
    //    if (src.ValueInt != 0) return src.ValueInt.ToString();
    //    if (src.ValueDateTime != DateTime.MinValue) return src.ValueDateTime.ToString("yyyy-MM-dd");
    //    return src.ValueText ?? string.Empty;
    //}

    //// Method to determine SystemType based on the PropertyModel
    //private string DetermineSystemType(PropertyModel src)
    //{
    //    if (src.ValueInt != 0) return "System.Int32";
    //    if (src.ValueDateTime != DateTime.MinValue) return "System.DateTime";
    //    return "System.String";
    //}
    //#endregion

    public async Task<SettingsModel> GetSettings()
    {
        _logger.LogInformation($"Called GetSettings()");

        _logger.LogInformation($"Called GetMatchplayTeams()");
        SettingsDto dto = await _repo.GetSettings();
        return mapper.Map<SettingsModel>(dto);

        //IEnumerable<PropertyDto> dtos = await _repo.GetSettings();
        //SettingsModel model = new();
        //model.Season = dtos.Select(x => x.Id == 0).FirstOrDefault() .<int>("Season");
        //model.SeasonStart = _repo.GetPropertyValue<DateTime>("DSeasonStart");
        //model.SeasonEnd = _repo.GetPropertyValue<DateTime>("DSeasonEnd");
        //model.AbbreviationMensSection = _repo.GetPropertyValue<string>("MensSection");
        //model.RyderCupSponsorName = _repo.GetPropertyValue<string>("MensSectionSponsor");
        //model.MaxHcpForARekken = _repo.GetPropertyValue<int>("GroupAUpperBound");
        //model.MaxHcpForBRekken = _repo.GetPropertyValue<int>("GroupBUpperBound");
        //model.NoOfRoundsToRank = _repo.GetPropertyValue<int>("MinRoundsPlayed");
        //model.GuidForGolfbox = _repo.GetPropertyValue<string>("WsGroupGuid");
        //model.UsernameForGolfbox = _repo.GetPropertyValue<string>("WsUsername");
        //model.AccountForGolfbox = _repo.GetPropertyValue<string>("WsAccount");
        //model.PasswordForGolfbox = _repo.GetPropertyValue<string>("WsPassword");
        //return model;
    }

    public async Task<int> SettingsUpsert(SettingsModel model)
    {
        _logger.LogInformation($"Called SettingsUpsert()");

        SettingsDto dto = mapper.Map<SettingsDto>(model);

        return await _repo.SettingsUpsert(dto);

        //_repo.PropertyValueUpsert<int>("Season", model.Season.Value);
        //_repo.PropertyValueUpsert<DateTime>("DSeasonStart", model.SeasonStart.Value);
        //_repo.PropertyValueUpsert<DateTime>("DSeasonEnd", model.SeasonEnd.Value);
        //_repo.PropertyValueUpsert<string>("MensSection", model.AbbreviationMensSection);
        //_repo.PropertyValueUpsert<string>("MensSectionSponsor", model.RyderCupSponsorName);
        //_repo.PropertyValueUpsert<int>("MinRoundsPlayed", model.NoOfRoundsToRank.Value);
        //_repo.PropertyValueUpsert<int>("GroupAUpperBound", model.MaxHcpForARekken.Value);
        //_repo.PropertyValueUpsert<int>("GroupBUpperBound", model.MaxHcpForBRekken.Value);
        //return 0;
    }

}

