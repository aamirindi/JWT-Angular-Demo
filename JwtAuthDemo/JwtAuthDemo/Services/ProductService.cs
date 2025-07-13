using System;
using System.Collections.Generic;
using System.Linq;
using JwtAuthDemo.DTOs;
using JwtAuthDemo.Models;
using JwtAuthDemo.Repository;
using JwtAuthDemo.Data;

namespace JwtAuthDemo.Services
{
    public class ProductService : IProduct
    {
        private readonly ApplicationDbContext _db;

        public ProductService(ApplicationDbContext db)
        {
            _db = db;
        }

        public void AddProduct(ProductDTO dto)
        {
            var product = new Product
            {
                ProductName = dto.ProductName,
                Price = dto.Price,
                Description = dto.Description,
                CreatedAt = DateTime.UtcNow
            };

            _db.Products.Add(product);
            _db.SaveChanges();
        }

        public List<ProductDTO> GetProducts()
        {
            return _db.Products
                .Select(p => new ProductDTO
                {
                    ProductId = p.ProductId,
                    ProductName = p.ProductName,
                    Price = p.Price,
                    Description = p.Description,
                    CreatedAt = p.CreatedAt
                })
                .ToList();
        }

        public ProductDTO GetProductById(int id)
        {
            var product = _db.Products.FirstOrDefault(p => p.ProductId == id);
            if (product == null) return null;

            return new ProductDTO
            {
                ProductId = product.ProductId,
                ProductName = product.ProductName,
                Price = product.Price,
                Description = product.Description,
                CreatedAt = product.CreatedAt
            };
        }

        public void UpdateProduct(ProductDTO dto)
        {
            var product = _db.Products.FirstOrDefault(p => p.ProductId == dto.ProductId);
            if (product == null) return;

            product.ProductName = dto.ProductName;
            product.Price = dto.Price;
            product.Description = dto.Description;
            _db.SaveChanges();
        }

        public void DeleteProduct(int id)
        {
            var product = _db.Products.FirstOrDefault(p => p.ProductId == id);
            if (product == null) return;

            _db.Products.Remove(product);
            _db.SaveChanges();
        }
    }
}