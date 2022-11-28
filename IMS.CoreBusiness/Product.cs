﻿using IMS.CoreBusiness.Validations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.CoreBusiness
{
    public class Product
    {
        public int ProductId { get; set; }

        [Required]
        public string ProductName { get; set; } = string.Empty;

        [Range(0, int.MaxValue, ErrorMessage = "Quantity must be greater or equal to {0}")]
        public int Quantity { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Price must be greater or equal to {0}")]
        [Product_EnsurePriceIsGreaterThanInventoriesPrice]
        public double Price { get; set; }

        public bool IsActive { get; set; } = true;

        public List<ProductInventory>? ProductInventories { get; set; }

        public double TotalInventoryCost()
        {
            return this.ProductInventories.Sum(x => x.Inventory?.Price * x.InventoryQuantity ?? 0);
        }

        public bool ValidatePricing()
        {
            // we are not validating the product object when the product inventories are not loaded
            if (ProductInventories == null || ProductInventories.Count <= 0) return true;
            
            if (this.TotalInventoryCost() > Price) return false;

            return true;
        }
    }
}
