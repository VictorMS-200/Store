using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Store.Models;


[Owned, Table("Address")]
public class AddressModel
{
    public int IdAddress { get; set; }

    [Required]
    public string Street { get; set; }

    [Required]
    public string Number { get; set; }

    [Required]
    public string Neighborhood { get; set; }

    [Required]
    public string City { get; set; }

    [Required, Column(TypeName = "char(2)")]
    public string State { get; set; }

    [Required, Column(TypeName = "char(9)")]
    public string ZipCode { get; set; }

    public string Reference { get; set; }

    public string Complement { get; set; }

    public bool Selected { get; set; }
    
    [NotMapped]
    public string  FullAddress { 
        get {
            return $"{Street},{Number}, {Complement}, {Neighborhood}, {City}, {State}, ZipCode {ZipCode}";
        }
    }
}