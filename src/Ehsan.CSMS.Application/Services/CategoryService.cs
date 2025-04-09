using Ehsan.CSMS.Dtos.CategoryDto;
using Ehsan.CSMS.Entities;
using Ehsan.CSMS.Helper;
using Ehsan.CSMS.IRepositories;
using Ehsan.CSMS.IService;
using Ehsan.CSMS.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using Volo.Abp.ObjectMapping;

namespace Ehsan.CSMS.Services;

// get countAsync
public class CategoryService : ApplicationService, ICategoryServices
{
    private readonly ICategoryRepository _categoryRepository;

    public CategoryService(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<CategoryResponse> AddAsync(CategoryAddRequest? category)
    {
        if (category is null)
        { throw new ArgumentNullException(nameof(category)); }
        ValidationHelper.ValidateModel(category);
        if (await _categoryRepository.GetByNameAsync(category.Name) is not null)
        { throw new ArgumentException($"{category.Name} already exit"); }

        var categoryEntity = ObjectMapper.Map<CategoryAddRequest, Category>(category);
        var addedCategory = await _categoryRepository.AddAsync(categoryEntity);
        return ObjectMapper.Map<Category, CategoryResponse>(addedCategory);
    }

    public Task<bool> DeleteByIdAsync(Guid? id)
    {
        if (id is null)
        { throw new ArgumentNullException(nameof(id)); }
        return _categoryRepository.DeleteByIdAsync(id.Value);
    }

    public async Task<IEnumerable<CategoryResponse>> GetAllAsync()
    {
        var categories = await _categoryRepository.GetAllAsync();
        return ObjectMapper.Map<List<Category>, List<CategoryResponse>>(categories);
    }

    public async Task<CategoryResponse?> GetByIdAsync(Guid? id)
    {
        if (id is null)
        { throw new ArgumentNullException(nameof(id)); }
        var category = await _categoryRepository.GetByIdAsync(id.Value);
        return ObjectMapper.Map<Category?, CategoryResponse>(category);
    }


    public async Task<IEnumerable<CategoryResponse>> SearchAsync(CategorySearchCriteria searchCriteria)
    {
        var categories = await _categoryRepository.SearchAsync(searchCriteria);
        return ObjectMapper.Map<List<Category>, List<CategoryResponse>>(categories);
    }

    public async Task<CategoryResponse> UpdateAsync(CategoryUpdateRequest? category)
    {
        if (category is null)
        { throw new ArgumentNullException(nameof(category)); }
        ValidationHelper.ValidateModel(category);
        if(category.Id == Guid.Empty)
        { throw new ArgumentNullException(nameof(category)); }

        // unique check
        var categoryToCheck = await _categoryRepository.GetByNameAsync(category.Name!);
        if (categoryToCheck != null && categoryToCheck.Id != category.Id)
        { throw new ArgumentException($"Invalid Category {0}", nameof(category.Name)); }
        
        // valid id check 
        var categoryToUpdate = await _categoryRepository.GetByIdAsync(category.Id);
        if (categoryToUpdate is null)
        {
            { throw new ArgumentNullException(nameof(category)); }
        }

        categoryToUpdate.Name = category.Name;
        var updatedCategory = await _categoryRepository.UpdateAsync(categoryToUpdate);
        return ObjectMapper.Map<Category, CategoryResponse>(updatedCategory);
    }
}
