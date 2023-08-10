using System.ComponentModel.DataAnnotations;

namespace Store.Models;

public class CategoryModel
{
    [Key]
    public int IdCategory { get; set; }
    
    [Required, MaxLength(128)]
    public string? Name { get; set; }
}