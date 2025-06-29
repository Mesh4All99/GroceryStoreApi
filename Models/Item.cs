using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace GroceryStore.Models
{
    public class Item
    {
        [Key]
        public int ItemId { get; set; }
        [Required]
        [MaxLength(100)]
        public required string Name { get; set; }
        [Precision(5,2)]
        public  double Price { get; set; }
        public string? ComanyName { get; set; }
        public string? SupplierName { get; set; }
        public DateTime DateOfPurchase { get; set; } = DateTime.Now;

    }
}
