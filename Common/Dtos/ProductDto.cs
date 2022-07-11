using Common.Enums;

namespace Common.Dtos;

public class ProductDto
{
    public string Code { get; set; } = null!;
    public string Brand { get; set; } = null!;
    public string Model { get; set; } = null!;
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public decimal Tax { get; set; }
    public int InStock { get; set; }
    public BasicUnitProduct BasicUnit { get; set; }
    public DateTime CreatedDateTime { get; set; }
}