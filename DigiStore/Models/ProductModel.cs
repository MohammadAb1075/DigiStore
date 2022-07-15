using Resources;
using System.ComponentModel.DataAnnotations;

namespace DigiStore.Models
{
    public class ProductModel
    {
        [Display(Name = nameof(DataDictionary.ProductId), ResourceType = typeof(Titles))]
        [Required(ErrorMessageResourceName = nameof(Messages.Required), ErrorMessageResourceType = typeof(Messages))]
        [Range(1, int.MaxValue)]
        //[Range(1, int.MaxValue, ErrorMessage = "Please enter a value bigger than {1}")]
        public int ProductId { get; set; }

        [Display(Name = nameof(DataDictionary.ProductName), ResourceType = typeof(Titles))]
        [Required(ErrorMessageResourceName = nameof(Messages.Required), ErrorMessageResourceType = typeof(Messages))]
        [StringLength(64, MinimumLength = 3, ErrorMessageResourceName = nameof(Messages.StringLength), ErrorMessageResourceType = typeof(Messages))]
        public string ProductName { get; set; }

        [Display(Name = nameof(DataDictionary.CategoryId), ResourceType = typeof(Titles))]
        [Required(ErrorMessageResourceName = nameof(Messages.Required), ErrorMessageResourceType = typeof(Messages))]
        public int CategoryId { get; set; }

        [Display(Name = nameof(DataDictionary.Price), ResourceType = typeof(Titles))]
        [Required(ErrorMessageResourceName = nameof(Messages.Required), ErrorMessageResourceType = typeof(Messages))]
        [Range(1000, 10000)]
        public int Price { get; set; }

        [Display(Name = nameof(DataDictionary.Email), ResourceType = typeof(Titles))]
        [EmailAddress]
        public string? Email { get; set; }
    }
}
