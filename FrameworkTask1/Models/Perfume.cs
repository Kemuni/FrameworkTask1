namespace FrameworkTask1.Models;

public class Perfume
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Brand { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int VolumeInMl { get; set; }
    public DateTime CreatedAt { get; set; }
}
