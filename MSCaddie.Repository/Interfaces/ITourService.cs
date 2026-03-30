using MSCaddie.Repository.Models;

namespace MSCaddie.Repository.Interfaces;

public interface ITourService
{
    Task<IEnumerable<TourModel>?> GetTours();
    Task<IEnumerable<TourPlayerModel?>?> GetTourPlayers(int tourId);
    Task<IEnumerable<PlayerModel?>> GetNonTourPlayers(int tourId);

    Task<int> Subscribe(int tourId, int vgcNo);
    Task<int> Unsubscribe(int tourId, int vgcNo);
    Task<TourModel> UpsertTour(TourModel model);
    Task<TourPlayerModel> UpsertTourPlayer(TourPlayerModel model);
}
