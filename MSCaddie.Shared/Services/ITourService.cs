using MSCaddie.Shared.Dtos;
using MSCaddie.Shared.Models;

namespace MSCaddie.Shared.Services;

public interface ITourService
{
    Task<IEnumerable<TourModel>?> GetTours();
    Task<IEnumerable<TourPlayerModel?>?> GetTourPlayers(int tourId);
    Task<IEnumerable<TourPlayerModel?>> GetNonTourPlayers(int tourId);

    Task<int> Subscribe(int tourId, int vgcNo);
    Task<int> ToggleSubscribtion(int tourId, int vgcNo);
    Task<TourModel> UpsertTour(TourModel model);
    Task<TourPlayerModel> UpsertTourPlayer(TourPlayerModel model);
}
