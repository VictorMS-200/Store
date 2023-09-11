using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Store.Models;

public class ProductModel
{
    [Key]
    public int IdProduct { get; set; }

    [Required, MaxLength(128)]
    public string Name { get; set; }

    public int Stock { get; set; }

    public double Price { get; set; }

    public int IdCategory { get; set; }

    [ForeignKey("IdCategory")]
    public CategoryModel Category { get; set; }
}