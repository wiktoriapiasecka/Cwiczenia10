namespace Cwiczenia10.Entities;

public class Component
{
    public string Code { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;

    public int ComponentManufacturersId { get; set; }
    public ComponentManufacturer Manufacturer { get; set; } = null!;

    public int ComponentTypesId { get; set; }
    public ComponentType Type { get; set; } = null!;

    public ICollection<PCComponent> PCComponents { get; set; } = new List<PCComponent>();
}