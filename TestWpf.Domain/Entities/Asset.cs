using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestWpf.Domain.Entities;

public class Asset
{
    [Key]
    public Guid AssetId { get; set; }

    [Required]
    public string Name { get; set; }

    [Required]
    public string Description { get; set; }

    [ForeignKey("InventoryId")]
    public Guid InvetoryId { get; set; }

    public virtual Inventory Inventory { get; set; }
}
