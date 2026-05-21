using Cwiczenia10.Data;
using Cwiczenia10.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Cwiczenia10.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PcsController : ControllerBase
{
    private readonly AppDbContext _context;

    public PcsController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetPcs()
    {
        var pcs = await _context.PCs
            .Select(pc => new PcDto
            {
                Id = pc.Id,
                Name = pc.Name,
                Weight = pc.Weight,
                Warranty = pc.Warranty,
                CreatedAt = pc.CreatedAt,
                Stock = pc.Stock
            })
            .ToListAsync();

        return Ok(pcs);
    }
    
    [HttpGet("{id:int}/components")]
    public async Task<IActionResult> GetPcWithComponents(int id)
    {
        var pc = await _context.PCs
            .Include(pc => pc.PCComponents)
            .ThenInclude(pc => pc.Component)
            .ThenInclude(c => c.Manufacturer)
            .Include(pc => pc.PCComponents)
            .ThenInclude(pc => pc.Component)
            .ThenInclude(c => c.Type)
            .FirstOrDefaultAsync(pc => pc.Id == id);

        if (pc == null)
            return NotFound();

        var result = new PcDetailsDto
        {
            Id = pc.Id,
            Name = pc.Name,
            Weight = pc.Weight,
            Warranty = pc.Warranty,
            CreatedAt = pc.CreatedAt,
            Stock = pc.Stock,

            Components = pc.PCComponents.Select(pcComponent => new PcComponentDto
            {
                Amount = pcComponent.Amount,

                Component = new ComponentDto
                {
                    Code = pcComponent.Component.Code,
                    Name = pcComponent.Component.Name,
                    Description = pcComponent.Component.Description,

                    Manufacturer = new ManufacturerDto
                    {
                        Id = pcComponent.Component.Manufacturer.Id,
                        Abbreviation = pcComponent.Component.Manufacturer.Abbreviation,
                        FullName = pcComponent.Component.Manufacturer.FullName,
                        FoundationDate = DateOnly.FromDateTime(
                            pcComponent.Component.Manufacturer.FoundationDate)
                    },

                    Type = new ComponentTypeDto
                    {
                        Id = pcComponent.Component.Type.Id,
                        Abbreviation = pcComponent.Component.Type.Abbreviation,
                        Name = pcComponent.Component.Type.Name
                    }
                }
            }).ToList()
        };

        return Ok(result);
    }
}