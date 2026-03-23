using Catalog.Domain.Entities;

namespace Catalog.Application.Services;

public interface IGameCatalogService
{
    Task<IReadOnlyList<Game>> GetGamesAsync(CancellationToken cancellationToken = default);
    Task<Game?> GetGameByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<Game> CreateGameAsync(Game game, CancellationToken cancellationToken = default);
    Task<Game?> UpdateGameAsync(Game game, CancellationToken cancellationToken = default);
    Task<bool> DeleteGameAsync(Guid id, CancellationToken cancellationToken = default);
    Task<Game?> CreatePromotionAsync(Guid id, decimal discountPercentage, CancellationToken cancellationToken = default);
}
