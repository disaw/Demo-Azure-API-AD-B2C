using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models
{
    public class Product
    {
        [Key]
        [Required]
        public string Id { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(100)")]
        public string Description { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        public string Model { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        public string Brand { get; set; }
    }
}
