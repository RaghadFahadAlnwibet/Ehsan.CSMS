using Ehsan.CSMS.Dtos.ProductDto;
using Ehsan.CSMS.Entities;
using Ehsan.CSMS.Helper;
using Ehsan.CSMS.IRepositories;
using Ehsan.CSMS.IService;
using Ehsan.CSMS.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace Ehsan.CSMS.Services;

public class ProductService : ApplicationService, IProductService
{
    private readonly IProductRepository _productRepository;
    public ProductService(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<ProductResponse> AddAsync(ProductAddRequest? product)
    {
        if (product == null)
        { throw new ArgumentNullException(nameof(product)); }
        ValidationHelper.ValidateModel(product);
        if (await _productRepository.GetBynameAsync(product.Name!) is not null)
        { throw new ArgumentException($"{product.Name} already exists."); }

        var productEntity = ObjectMapper.Map<ProductAddRequest, Product>(product);
        var addedProduct = await _productRepository.AddAsync(productEntity);
        return ObjectMapper.Map<Product, ProductResponse>(addedProduct);
    }

    public Task<bool> DeleteByIdAsync(Guid? id)
    {
        if (id == null)
        { throw new ArgumentNullException(nameof(id)); }
        return _productRepository.DeleteByIdAsync(id.Value);
    }

    public async Task<IEnumerable<ProductResponse>> GetAllAsync()
    {
        var products = await _productRepository.GetAllAsync();
        return ObjectMapper.Map<List<Product>, List<ProductResponse>>(products);
    }

    public async Task<ProductResponse?> GetByIdAsync(Guid? id)
    {
        if (id == null)
        { throw new ArgumentNullException(nameof(id)); }
        var productEntity = await _productRepository.GetByIdAsync(id.Value);
        return ObjectMapper.Map<Product?, ProductResponse>(productEntity);
    }

    public async Task<double?> GetPriceById(Guid? Id)
    {
        if (Id == null)
        { throw new ArgumentNullException(nameof(Id)); }
        return await _productRepository.GetPriceById(Id.Value);
    }
    public async Task<IEnumerable<ProductResponse>> SearchAsync(ProductSearchCriteria searchCriteria)
    {
        var products = await _productRepository.SearchAsync(searchCriteria);
        return ObjectMapper.Map<List<Product>, List<ProductResponse>>(products);
    }

    public async Task<ProductResponse> UpdateAsync(ProductUpdateRequest? product)
    {
        if (product == null)
        { throw new ArgumentNullException(nameof(product)); }
        ValidationHelper.ValidateModel(product);
        if(product.Id == Guid.Empty)
        {
            throw new ArgumentNullException(nameof(product.Id));
        }

        // unique check
        var productToCheck = await _productRepository.GetBynameAsync(product.Name!);
        if (productToCheck is not null && productToCheck.Id != product.Id)
        { throw new ArgumentException($"{product.Name} already exists."); }

        // valid id check
        var productToUpdate = await _productRepository.GetByIdAsync(product.Id);
        if(productToUpdate == null)
        {
            { throw new ArgumentException($"Invalid Product {0}", nameof(Product.Id)); }
        }

        productToUpdate.Name = product.Name;
        productToUpdate.Price = product.Price;
        productToUpdate.CategoryId = product.CategoryId;
        var updatedProduct = await _productRepository.UpdateAsync(productToUpdate);
        return ObjectMapper.Map<Product, ProductResponse>(updatedProduct);
    }
}
