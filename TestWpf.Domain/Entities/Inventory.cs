using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestWpf.Domain.Entities;

public class Inventory
{
    [Key]
    public Guid InventoryId { get; set; }

    [Required]
    public string Name { get; set; }

    [Required]
    public string Description { get; set; }

    [Required]
    public int Count { get; set; }

    [ForeignKey("AssetId")]
    public Guid AssetId { get; set; }

    public virtual Asset Asset { get; set; }
}
