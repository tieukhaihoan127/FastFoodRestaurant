using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FastFoodRestaurant.Models
{
    public class Combo
    {
        [Key]
        public string ComboId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [MaxLength(100)]
        public string Description { get; set; }
        [Range(0, 1000)]
        public double Price { get; set; }
        public string PictureUrl { get; set; }
        [Required]
        public Boolean IsActive { get; set; }
        public Boolean IsForParty { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? DeleteDate { get; set; }
        public virtual ICollection<ComboDetail> ComboDetails { get; set; }
        public virtual ICollection<ComboInformation> ComboInformations { get; set; }
    }
}
