namespace Cwiczenia10.DTOs;

public class ComponentDto
{
    public string Code { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;

    public ManufacturerDto Manufacturer { get; set; } = null!;
    public ComponentTypeDto Type { get; set; } = null!;
}