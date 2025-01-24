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
    // get countAsync
    public class CategoryService : ApplicationService, ICategoryServices
    {
        private ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task AddAsync(CategoryDto category)
        {
            try 
            {
                var categoryEntity = ObjectMapper.Map<CategoryDto, Category>(category);
                await _categoryRepository.AddAsync(categoryEntity);
            }
            catch (DbException exception)
            { 
                new InvalidOperationException("Failed to add categpry", exception);
            }
        }

        public async Task DeleteAsync(CategoryDto category)
        {
            try
            {
                var categoryEntity = ObjectMapper.Map<CategoryDto, Category>(category);
                await _categoryRepository.DeleteAsync(categoryEntity);
            }
            catch (DbException exception)
            {
                new InvalidOperationException("Failed to delete category", exception);
            }
        }

        public async Task DeleteByIdAsync(int id)
        {
            try 
            {
                await _categoryRepository.DeleteByIdAsync(id);
            }
            catch (DbException exception)
            {
                new InvalidOperationException("Failed to delete category", exception);
            }
        }

        public async Task<List<CategoryDto>> GetAllAsync()
        {
            try 
            {
                var categories = await _categoryRepository.GetListAsync();
                var categoryDtos = ObjectMapper.Map<List<Category>, List<CategoryDto>>(categories);
                return categoryDtos;
            }
            catch (DbException exception) 
            {
                return null;
            }
        }
        public async Task<CategoryDto> GetbyIdAsync(int id)
        {
            try 
            {
                var categories = await _categoryRepository.GetbyIdAsync(id);
                var categoryDtos = ObjectMapper.Map<Category, CategoryDto>(categories);
                return categoryDtos;
            }
            catch(DbException exception)
            {
                return null;
            }
        }

        public async Task<CategoryDto> GetByNameAsync(string name)
        {
            try 
            {
                var categories = await _categoryRepository.GetByNameAsync(name);
                var categoryDtos = ObjectMapper.Map<Category, CategoryDto>(categories);
                return categoryDtos;
            }
            catch(DbException exception)
            {
                return null;
            }
        }
        public async Task UpdateAsync(CategoryDto category)
        {
            try
            {
                var categoryEntity = ObjectMapper.Map<CategoryDto, Category>(category);
                await _categoryRepository.UpdateAsync(categoryEntity);
            }
            catch (DbException exception)
            {
                new InvalidOperationException("Failed to update category", exception);
            }
        }
        public async Task UpdateCategoryNameAsync(int id, string name)
        {
            try 
            {
                await _categoryRepository.UpdateCategoryNameAsync(id, name);
            }
            catch (DbException exception)
            {
                new InvalidOperationException("Failed to update category", exception);
            }
        }
        public async Task<List<CategoryDto>> SearchAsync(CategorySearchCriteria categorySearchCriteria)
        {
            try
            {
                var categories = await _categoryRepository.SearchAsync(categorySearchCriteria);
                var categoryDtos = ObjectMapper.Map<List<Category>, List<CategoryDto>>(categories);
                return categoryDtos;
            }
            catch (DbException exception)
            {
                return null;
            }
        }

    }
}
