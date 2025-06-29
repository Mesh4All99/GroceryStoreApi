using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace GroceryStore.DTO
{
    public class PostItemDTO
    {
        public required string Name { get; set; }
        public double Price { get; set; }
        public string? ComanyName { get; set; }
        public string? SupplierName { get; set; }
        public DateTime DateOfPurchase { get; set; } = DateTime.Now;
    }
}
