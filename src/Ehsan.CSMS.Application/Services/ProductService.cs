using AutoMapper;
using Ehsan.CSMS.Dtos;
using Ehsan.CSMS.Entities;
using Ehsan.CSMS.IRepositories;
using Ehsan.CSMS.IService;
using Ehsan.CSMS.Models;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using Volo.Abp.ObjectMapping;

namespace Ehsan.CSMS.Services
{
    public class ProductService : ApplicationService, IProductService
    {
        private readonly IProductRepository _ProductRepository;
        public ProductService(IProductRepository IProductRepository)
        {
            _ProductRepository = IProductRepository;
        }

        public async Task AddAsync(ProductDto product)
        {
            try
            {
                var productEntity = ObjectMapper.Map<ProductDto, Product>(product);
                await _ProductRepository.AddAsync(productEntity);
            }
            catch(DbException exception)
            {
                new InvalidOperationException("Failed to add product", exception);
            }  
        }

        public async Task DeleteAsync(ProductDto product)
        {
            try 
            {
                var productEntity = ObjectMapper.Map<ProductDto, Product>(product);
                await _ProductRepository.DeleteAsync(productEntity);
            } catch (DbException exception) 
            {
                new InvalidOperationException("Failed to delete product", exception);
            }
        }

        public async Task DeletebyIdAsync(int id)
        {
            try 
            {
                await _ProductRepository.DeletebyIdAsync(id);
            }
            catch (DbException exception) 
            {
                new InvalidOperationException("Failed to delete product", exception);
            }
        }

        public async Task<List<ProductDto>> GetAllAsync()
        {
            var products = await _ProductRepository.GetAllAsync();
            var productDtos = ObjectMapper.Map<List<Product>, List<ProductDto>>(products);
            return productDtos;
        }

        public async Task<ProductDto> GetbyIdAsync(int id)
        {
            try 
            {
                var products = await _ProductRepository.GetbyIdAsync(id);
                var productDtos = ObjectMapper.Map<Product, ProductDto>(products);
                return productDtos;
            } catch (DbException exception) 
            {
                return null;
            } 
        }

        public async Task<double> GetPricePerUnintbyIdAsync(int id)
        {
            try
            {
                double ProductPrice  = await _ProductRepository.GetPricePerUnintbyIdAsync(id);
                return ProductPrice;
            }
            catch (DbException exception)
            {
                return 0;
            }
        }

        public async Task<List<ProductDto>> SearchAsync(ProductSearchCriteria productSearchCriteria)
        {
            try
            {
                var products = await _ProductRepository.SearchAsync(productSearchCriteria);
                var productDtos = ObjectMapper.Map<List<Product>, List<ProductDto>>(products);
                return productDtos;
            }
            catch(DbException exception)
            {
                return null;
            }

        }

        public async Task UpdateAsync(ProductDto product)
        {
            try 
            {
                var productEntity = ObjectMapper.Map<ProductDto, Product>(product);
                await _ProductRepository.UpdateAsync(productEntity);
            }
            catch (DbException exception)
            { 
                new InvalidOperationException("Failed to update product", exception);
            }
        }

        public async Task UpdateProductNameAsync(int id, string name)
        {
            try 
            {
                await _ProductRepository.UpdateProductNameAsync(id, name);
            }
            catch (DbException exception) 
            {
                new InvalidOperationException("Failed to update product", exception);
            }
        }

        public async Task UpdateProductPriceAsync(int id, double price)
        {
            try
            {
                await _ProductRepository.UpdateProductPriceAsync(id, price);
            }
            catch (DbException exception) 
            {
                new InvalidOperationException("Failed to update product", exception);
            }
        }
    }
}
