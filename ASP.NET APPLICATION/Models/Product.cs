using System.ComponentModel.DataAnnotations;

namespace ASP.NET_APPLICATION.Models
{
    public class Product
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Range(0, double.MaxValue, ErrorMessage = "Price must be a positive number")]
        public decimal Price { get; set; }
        [Range(0, double.MaxValue, ErrorMessage = "Calories must be a positive number")]
        public int Calories { get; set; }
        [Range(0, double.MaxValue, ErrorMessage = "Sugar must be a positive number")]
        public int Sugar { get; set; }
    }
}
