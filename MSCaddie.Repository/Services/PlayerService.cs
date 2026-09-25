using MSCaddie.Repository.Interfaces;
using MSCaddie.Repository.Models;

namespace MSCaddie.Repository.Services;

public class PlayerService : IPlayerService
{
    private readonly IPlayerRepository _repo;
    private readonly IAdminRepository _repoAdmin;
    private readonly int season;

    public PlayerService(IPlayerRepository repo)
    {
        _repo = repo;
        season = _repoAdmin?.Season ?? DateTime.Now.Year;
    }
    public async Task<PlayerModel?> GetPlayer(int vgcno)
    {
        return await _repo.GetPlayer(vgcno);
        //return await _client.GetFromJsonAsync<PlayerDto>($"BaseAddress/{vgcno}");
    }
    public async Task<IEnumerable<PlayerModel?>?> GetPlayers()
    {
        var res =  await _repo.GetPlayers(season);
        return res?.Where(player => player?.Season == season);

        //return await _client.GetFromJsonAsync<IEnumerable<PlayerDto>>(BaseAddress);
    }
    public async Task<IEnumerable<PlayerModel?>?> GetNonMembers()
    {
        return await _repo.GetNonMembers(season);
        //return await _client.GetFromJsonAsync<IEnumerable<PlayerDto>>(BaseAddress);
    }
    public async Task<PlayerModel> UpsertPlayer(PlayerModel model)
    {
        await _repo.PlayerUpsert(model);
        model.Season = season;

        var dto = await _repo.MembershipUpsert(model);
        model.MemberShipId = dto.MembershipId;
        model.Season = dto.Season;

        return model;
    }

}

