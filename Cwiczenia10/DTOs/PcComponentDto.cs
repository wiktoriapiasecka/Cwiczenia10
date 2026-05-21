namespace Cwiczenia10.DTOs;

public class PcComponentDto
{
    public int Amount { get; set; }
    public ComponentDto Component { get; set; } = null!;
}