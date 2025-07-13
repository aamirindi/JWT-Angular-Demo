using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using JwtAuthDemo.DTOs;
using JwtAuthDemo.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JwtAuthDemo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ProductController : ControllerBase
    {
        private readonly IProduct _pro;

        public ProductController(IProduct productService)
        {
            _pro = productService;
        }

        [HttpPost("add")]
        public IActionResult AddProduct([FromBody] ProductDTO dto)
        {
            _pro.AddProduct(dto);
            return Ok(new { success = true, message = "Product added successfully." });
        }

        [HttpGet("all")]
        public IActionResult GetAllProducts()
        {
            var products = _pro.GetProducts();
            return Ok(new { success = true, data = products });
        }

        [HttpGet("{id}")]
        public IActionResult GetProductById(int id)
        {
            var product = _pro.GetProductById(id);
            if (product == null)
                return NotFound(new { success = false, message = "Product not found." });

            return Ok(new { success = true, data = product });
        }

        [HttpPut("update")]
        public IActionResult UpdateProduct([FromBody] ProductDTO dto)
        {
            _pro.UpdateProduct(dto);
            return Ok(new { success = true, message = "Product updated successfully." });
        }

        [HttpDelete("delete/{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult DeleteProduct(int id)
        {
            _pro.DeleteProduct(id);
            return Ok(new { success = true, message = "Product deleted successfully." });
        }
    }
}