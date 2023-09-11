using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Store.Models;


[Table("Customer")]
public class CustomerModel : UserModel
{
    [Required, Column(TypeName = "char(14)")]
    public string CPF { get; set; }

    public DateTime DateBirth { get; set; }

    [NotMapped]
    public int Age {
        get => (int) Math.Floor((DateTime.Now - DateBirth).TotalDays / 365.2425);
    }

    public ICollection<AddressModel> Addresses { get; set; }

    public ICollection<OrderModel> Orders { get; set; }
}