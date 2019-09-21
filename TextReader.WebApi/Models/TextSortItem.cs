using System.ComponentModel.DataAnnotations;

namespace TextReader.WebApi.Models
{
    public class TextSortItem
    {

        [Required]
        public SortOption SortOption { get; set; }

        [Required]
        public string Text { get; set; }
    }
}
