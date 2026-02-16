using System.Collections.Concurrent;
using FrameworkTask1.Models;

namespace FrameworkTask1.Services;

public class PerfumeStore
{
    private readonly ConcurrentDictionary<Guid, Perfume> _perfumes = new();

    public IReadOnlyList<Perfume> GetAll(
        string? name = null,
        string? brand = null,
        decimal? minPrice = null,
        decimal? maxPrice = null,
        string? sortBy = null,
        string? sortOrder = null)
    {
        IEnumerable<Perfume> query = _perfumes.Values;

        if (!string.IsNullOrWhiteSpace(name))
            query = query.Where(p => p.Name.Contains(name, StringComparison.OrdinalIgnoreCase));

        if (!string.IsNullOrWhiteSpace(brand))
            query = query.Where(p => p.Brand.Contains(brand, StringComparison.OrdinalIgnoreCase));

        if (minPrice.HasValue)
            query = query.Where(p => p.Price >= minPrice.Value);

        if (maxPrice.HasValue)
            query = query.Where(p => p.Price <= maxPrice.Value);

        var descending = string.Equals(sortOrder, "desc", StringComparison.OrdinalIgnoreCase);

        query = sortBy?.ToLowerInvariant() switch
        {
            "name"     => descending ? query.OrderByDescending(p => p.Name)     : query.OrderBy(p => p.Name),
            "brand"    => descending ? query.OrderByDescending(p => p.Brand)    : query.OrderBy(p => p.Brand),
            "price"    => descending ? query.OrderByDescending(p => p.Price)    : query.OrderBy(p => p.Price),
            "volume"   => descending ? query.OrderByDescending(p => p.VolumeInMl) : query.OrderBy(p => p.VolumeInMl),
            "created"  => descending ? query.OrderByDescending(p => p.CreatedAt)  : query.OrderBy(p => p.CreatedAt),
            _          => query.OrderByDescending(p => p.CreatedAt)
        };

        return query.ToList().AsReadOnly();
    }

    public Perfume? GetById(Guid id)
    {
        return _perfumes.GetValueOrDefault(id);
    }

    public Perfume Add(CreatePerfumeRequest request)
    {
        var perfume = new Perfume
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            Brand = request.Brand,
            Price = request.Price,
            VolumeInMl = request.VolumeInMl,
            CreatedAt = DateTime.UtcNow
        };

        _perfumes[perfume.Id] = perfume;
        return perfume;
    }
}
