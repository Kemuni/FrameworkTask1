using System.Collections.Concurrent;
using FrameworkTask1.Models;

namespace FrameworkTask1.Services;

public class PerfumeStore
{
    private readonly ConcurrentDictionary<Guid, Perfume> _perfumes = new();

    public IReadOnlyList<Perfume> GetAll()
    {
        return _perfumes.Values.ToList().AsReadOnly();
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
