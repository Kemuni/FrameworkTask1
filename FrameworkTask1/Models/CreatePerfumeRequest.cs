namespace FrameworkTask1.Models;

public class CreatePerfumeRequest
{
    public string Name { get; set; } = string.Empty;
    public string Brand { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int VolumeInMl { get; set; }
}
