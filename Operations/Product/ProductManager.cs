using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VintageApp.Business.Operations.Product.Dtos;
using VintageApp.Business.Types;
using VintageApp.Data.Entities;
using VintageApp.Data.Repositories;
using VintageApp.Data.UnitOfWork;

namespace VintageApp.Business.Operations.Product
{
    public class ProductManager : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<ProductEntity> _productRepository;
        private readonly IRepository<OrderProductEntity> _orderProductRepository;

        public ProductManager(IUnitOfWork unitOfWork, IRepository<ProductEntity> productRepository, IRepository<OrderProductEntity> orderProductRepository )
        {
            _unitOfWork = unitOfWork;
            _productRepository = productRepository;
            _orderProductRepository = orderProductRepository;
        }


        public async Task<ServiceMessage> AddProduct(AddProductDto product)
        {
            var hasProduct = _productRepository.GetAll(x => x.ProductName.ToLower() == product.ProductName.ToLower()).Any();

            if (hasProduct)
            {
                return new ServiceMessage
                {
                    IsSuccess = false,
                    Message = "Ürün zaten mevcut"
                };
            }

            var ProductEntity = new ProductEntity
            {
                ProductName = product.ProductName,
                
            };

            _productRepository.Add(ProductEntity);

            try
            {
                await _unitOfWork.SaveChangesAsync();
               
            }
            catch (Exception)
            {
               
                throw new Exception("Ürün eklerken bir hata oluştu");
            }

            return new ServiceMessage
            {
                IsSuccess = true,
            };
        }

        public async Task<ServiceMessage> AdjustPrice(int id, int changeBy)
        {
            var product = _productRepository.GetById(id);

            product.Price = changeBy;

            _productRepository.Update(product);

            try
            {
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw new Exception("Fiyat değiştirilirken bir hata oluştu");
            }


            return new ServiceMessage
            { 
                IsSuccess = true, 
            };
           
        }

        public async Task<ServiceMessage> DeleteProduct(int id)
        {
            var product = _productRepository.GetById(id);

            if (product is null)
            {
                return new ServiceMessage
                {
                    IsSuccess = false,
                    Message = "Ürün bulunamadı"
                };
            }
             
            _productRepository.Delete(id);

            try
            {
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw new Exception("Silme işlemi sırasında bir hata meydana geldi");
            }
            
            return new ServiceMessage
            {
                IsSuccess = true,
            };
        }
        public async Task<List<ProductDto>> GetAllProducts()
        {
            var products = await _productRepository.GetAll()
               .Select(x => new ProductDto
               {
                   ProductName = x.ProductName,
                   Price = x.Price,
                   StockQuantity = x.StockQuantity,
                   ProductType = x.ProductType,
               }).ToListAsync();

            return products;
        }

        public async Task<ProductDto> GetProduct(int id)
        {
            var product = await _productRepository.GetAll(x => x.Id == id)
                .Select(x => new ProductDto
                {
                    ProductName = x.ProductName,
                    Price = x.Price,
                    StockQuantity = x.StockQuantity,
                    ProductType = x.ProductType,
                }).FirstOrDefaultAsync();
                
            return product;
        }

        public async Task<ServiceMessage> UpdateProduct(UpdateProductDto product)
        {
            var productEntity = _productRepository.GetById(product.Id);

            if (productEntity is null)
            {
                return new ServiceMessage
                {
                    IsSuccess = false,
                    Message = "Ürün bulunamadı"
                };
            }

            productEntity.ProductName = product.ProductName;
            productEntity.Price = product.Price;
            productEntity.StockQuantity = product.StockQuantity;
            productEntity.ProductType = product.ProductType;

            // Mark entity as modified
            _productRepository.Update(productEntity);

            try
            {
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw new Exception("Ürün güncellenirken bir hata ile karşılaşıldı.");
            }

            return new ServiceMessage
            {
                IsSuccess = true,
               
            };

        }
    }
}
