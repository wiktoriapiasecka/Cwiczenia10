namespace Cwiczenia10.DTOs;

public class PcDetailsDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public double Weight { get; set; }
    public int Warranty { get; set; }
    public DateTime CreatedAt { get; set; }
    public int Stock { get; set; }

    public List<PcComponentDto> Components { get; set; } = [];
}