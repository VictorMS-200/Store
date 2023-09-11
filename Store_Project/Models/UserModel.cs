using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Store.Models;


[Table("User")]
public class UserModel
{
    [Key]
    public int IdUser { get; set; }

    [Required, MaxLength(128)]
    public string Name { get; set; }

    [Required, MaxLength(128)]
    public string Email { get; set; }

    [MaxLength(128)]
    public string Password { get; set; }

    [ReadOnly(true)]
    public DateTime? RegistrationDate { get; }
}