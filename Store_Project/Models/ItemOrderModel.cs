using System.ComponentModel.DataAnnotations.Schema;

namespace Store.Models;


[Table("ItemOrder")]
public class ItemOrderModel
{
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public int IdOrder { get; set; }

    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public int IdProduct { get; set; }

    public int Quantity { get; set; }

    public double UnitPrice { get; set; }

    [ForeignKey("IdOrder")]
    public OrderModel Order { get; set; }

    [ForeignKey("IdProduct")]
    public ProductModel Product { get; set; }

    [NotMapped]
    public double TotalPrice { get => this.Quantity * this.UnitPrice; }
}