using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JwtAuthDemo.DTOs;

namespace JwtAuthDemo.Repository
{
    public interface IProduct
    {
        void AddProduct(ProductDTO dto);
        List<ProductDTO> GetProducts();
        ProductDTO GetProductById(int id);
        void DeleteProduct(int id);
        void UpdateProduct(ProductDTO dto);
    }
}