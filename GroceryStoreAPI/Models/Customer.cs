using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GroceryStoreAPI.Models
{
    public class Customer
    {
        [Key]
        public int CustomerId { get; set; }

        [Required]
        [Column(TypeName ="nvarchar(255)")]
        public string FirstName { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(255)")]
        public string LastName { get; set; }

        [Required]
        [Column(TypeName = "varchar(15)")]
        public string ContactNo { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(255)")]
        public string Email { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(500)")]
        public string Address { get; set; }

    }
}
