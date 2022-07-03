using Resources;
using System.ComponentModel.DataAnnotations;

namespace DigiStore.Models
{
    public class ProductModel
    {
        [Display(Name = nameof(Titles.ProductId), ResourceType = typeof(Titles))]
        [Required(ErrorMessageResourceName = nameof(Messages.Required), ErrorMessageResourceType = typeof(Messages))]
        [Range(1, int.MaxValue)]
        //[Range(1, int.MaxValue, ErrorMessage = "Please enter a value bigger than {1}")]
        public int ProductId { get; set; }

        [Display(Name = nameof(Titles.ProductName), ResourceType = typeof(Titles))]
        [Required(ErrorMessageResourceName = nameof(Messages.Required), ErrorMessageResourceType = typeof(Messages))]
        [StringLength(128, MinimumLength = 16)]
        public string ProductName { get; set; }

        [Display(Name = nameof(Titles.CategoryId), ResourceType = typeof(Titles))]
        [Required(ErrorMessageResourceName = nameof(Messages.Required), ErrorMessageResourceType = typeof(Messages))]
        public int CategoryId { get; set; }

        [Display(Name = nameof(Titles.Price), ResourceType = typeof(Titles))]
        [Required(ErrorMessageResourceName = nameof(Messages.Required), ErrorMessageResourceType = typeof(Messages))]
        [Range(1000, 10000)]
        public int Price { get; set; }

        public string? Email { get; set; }

    }
}
