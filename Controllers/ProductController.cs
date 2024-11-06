using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.WebSockets;
using VintageApp.Business.Operations.Product;
using VintageApp.Business.Operations.Product.Dtos;
using VintageApp.WebApi.Filters;
using VintageApp.WebApi.Models;


namespace VintageApp.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var Product = await _productService.GetProduct(id);

            if (Product is null)
                return NotFound();
            else
                return Ok(Product);

        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var products = await _productService.GetAllProducts();

            return Ok(products);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddProduct(AddProductRequest request)
        {
            var addProductDto = new AddProductDto
            {
                ProductName = request.ProductName,
                Price = request.Price,
                StockQuantity = request.StockQuantity,
                ProductType = request.ProductType,
            };

            var result = await _productService.AddProduct(addProductDto);

            if (result.IsSuccess)
                return Ok();
            else
                return BadRequest(result.Message);

        }

        [HttpPatch("{id}/price")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AdjustPrice(int id, int changeBy)
        {
            var result = await _productService.AdjustPrice(id, changeBy);

            if (!result.IsSuccess)
                return NotFound(result.Message);
            else
                return Ok();
        }

        [HttpDelete]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var result = await _productService.DeleteProduct(id);
            if (!result.IsSuccess)
                return NotFound(result.Message);
            else
                return Ok();
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        [TimeControlFilter]
        public async Task<IActionResult> UpdateProduct(int id, UpdateProductRequest request)
        {
            var updateProductDto = new UpdateProductDto
            {
                Id = id,
                ProductName = request.ProductName,
                Price = request.Price,
                StockQuantity = request.StockQuantity,
                ProductType = request.ProductType,

            };

            var result = await _productService.UpdateProduct(updateProductDto);

            if (!result.IsSuccess)
                return NotFound(result.Message);
            else 
                return await Get(id);


        }

    
    
    
    
    }
}
