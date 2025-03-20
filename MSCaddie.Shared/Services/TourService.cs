using MSCaddie.Shared.Models;
using MSCaddie.Shared.Interfaces;
using MSCaddie.Shared.Dtos;
using AutoMapper;

namespace MSCaddie.Shared.Services;

public class TourService : ITourService
{
    private readonly ITourRepository _repo;
    private readonly int season;
    IMapper mapper;

    public TourService(ITourRepository repo, IAdminRepository _repoAdmin)
    {
        _repo = repo;
        season = _repoAdmin?.Season ?? DateTime.Now.Year;
        mapper = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<TourDto, TourModel>().ReverseMap();
            cfg.CreateMap<TourPlayerDto, TourPlayerModel>().ReverseMap();
            cfg.CreateMap<TourPlayerDto, PlayerModel>();
        })
        .CreateMapper();

    }
    public async Task<IEnumerable<TourModel>?> GetTours()
    {
        var dtos = await _repo.GetTours(season);
        //return await _client.GetFromJsonAsync<PlayerDto>($"BaseAddress/{vgcno}");
        return mapper.Map<IEnumerable<TourModel>>(dtos);
    }

    public async Task<IEnumerable<TourPlayerModel?>?> GetTourPlayers(int tourId)
    {
        var dtos =  await _repo.GetTourPlayers(tourId);
        return mapper.Map<IEnumerable<TourPlayerModel>>(dtos);
    }

    /// <summary>
    /// Only to select from players not already signed up for the tour
    /// </summary>
    /// <param name="tourId"></param>
    /// <returns></returns>
    public async Task<IEnumerable<PlayerModel?>?> GetNonTourPlayers(int tourId)
    {
        var dtos = await _repo.GetNonTourPlayers(tourId);
        return mapper.Map<IEnumerable<PlayerModel>>(dtos);
    }

    public async Task<int> Unsubscribe(int tourId, int vgcNo)
    {
        return await _repo.Unsubscribe(tourId, vgcNo);
    }

    public async Task<int> Subscribe(int tourId, int vgcNo)
    {
        TourPlayerDto dto = new TourPlayerDto()
        {
            TourId = tourId, VgcNo = vgcNo, SignedUp = true, LastUpdateBy = "xx"
        };
        dto = await _repo.TourPlayerUpsert(dto);
        return 1;
    }

    public async Task<TourModel> UpsertTour(TourModel model)
    {
        TourDto dto = mapper.Map<TourDto>(model);

        dto = await _repo.TourUpsert(dto);
        return mapper.Map<TourModel>(dto);
    }

    public async Task<TourPlayerModel> UpsertTourPlayer(TourPlayerModel model)
    {
        TourPlayerDto dto = mapper.Map<TourPlayerDto>(model);

        dto = await _repo.TourPlayerUpsert(dto);
        return mapper.Map<TourPlayerModel>(dto);
    }

}

