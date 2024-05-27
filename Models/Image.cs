using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClothesBack.Models
{
    public class Image
    {
        [Key]
        public Guid ImageId {  get; set; }

        [Required]
        public Guid ProductId { get; set; }
        [ForeignKey("ProductId")]
        public Product Product { get; set; }

        public byte[] Data { get; set; }
    }
}
