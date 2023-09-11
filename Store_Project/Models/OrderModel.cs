using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Store.Models;


[Table("Order")]
public class OrderModel
{
    [Key]
    public int IdOrder { get; set; }

    public DateTime? OrderData { get; set; }

    public DateTime? DeliveryData { get; set; }

    public double? TotalValue { get; set; }

    public int IdCustomer { get; set; }

    [ForeignKey("IdCustomer")]
    public CustomerModel Customer { get; set; }

    public AddressModel DeliveryAddress { get; set; }

    public ICollection<ItemOrderModel> OrderedItems { get; set; }

}