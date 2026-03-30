using MSCaddie.Repository.Dtos;

namespace MSCaddie.Repository.Interfaces;

public interface ITourRepository
{
    Task<IEnumerable<TourDto?>> GetTours(int season);
    Task<TourDto> TourUpsert(TourDto dto);
    Task<int> Subscribe(int tourId, int vgcNo);
    Task<int> Unsubscribe(int tourId, int vgcNo);

    Task<IEnumerable<TourPlayerDto?>> GetTourPlayers(int tourId);
    Task<IEnumerable<TourPlayerDto?>> GetNonTourPlayers(int tourId);

    Task<TourPlayerDto> TourPlayerUpsert(TourPlayerDto dto);
}
