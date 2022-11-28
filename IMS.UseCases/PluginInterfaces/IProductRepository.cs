﻿using IMS.CoreBusiness;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.UseCases.PluginInterfaces
{
    public interface IProductRepository
    {
        Task AddProductAsync(Product product);
        Task<List<Product>> GetProductsByNameAsync(string name);
        Task<Product> GetProductByIdAsync(int productId);
        Task UpdateProductAsync(Product product);
        Task DeleteProductAsync(int productId);
    }
}
