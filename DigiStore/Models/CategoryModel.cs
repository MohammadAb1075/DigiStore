using System.ComponentModel.DataAnnotations;

namespace DigiStore.Models
{
    public class CategoryModel
    {
        [Required]
        public int CategoryId { get; set; }

        [Required]
        public string CategoryName { get; set; }

    }
}
