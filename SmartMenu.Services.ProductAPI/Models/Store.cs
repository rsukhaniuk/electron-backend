using System.ComponentModel.DataAnnotations;

namespace SmartMenu.Services.ProductAPI.Models
{
    public class Store
    {
        [Key]
        public int StoreId { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        public string Address { get; set; }

        public string PhoneNumber { get; set; }

        [Required]
        public string Hours { get; set; }
    }
}
