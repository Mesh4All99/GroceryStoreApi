﻿using Microsoft.EntityFrameworkCore;

namespace GroceryStore.DTO
{
    public class ShowItemsDTO
    {
        public int ItemId { get; set; }
        public required string Name { get; set; }
        public double Price { get; set; }
        public string? ComanyName { get; set; }
    }
}
