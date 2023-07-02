using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Deepak_Project.Models
{
    public class CartModel
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("BookModel")]
        public virtual BookModel Book { get; set; }
        [ForeignKey("UserModel")]
        public virtual UserModel User { get; set; }

        [Required]
        public int Quantity { get; set; }

    }
}
