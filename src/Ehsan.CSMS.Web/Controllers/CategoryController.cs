using Microsoft.AspNetCore.Mvc;
using Ehsan.CSMS.IService;
using System.Threading.Tasks;
using Ehsan.CSMS.Web.Models.CategoryViewModels;
using Ehsan.CSMS.Models;
using Ehsan.CSMS.Dtos.CategoryDto;
using System;
using Ehsan.CSMS.Web.Filters.ActionFilter;

namespace Ehsan.CSMS.Web.Controllers;

public class CategoryController : Controller
{
    private readonly ICategoryServices _categoryService;
    public CategoryController(ICategoryServices categoryServices)
    {
        _categoryService = categoryServices;
    }

    // GET: CategoryController
    public async Task<ActionResult> Index(CategoryViewModel categoryViewModel)
    {
        if (categoryViewModel.categorySearchCriteria == null)
            categoryViewModel.categorySearchCriteria = new CategorySearchCriteria();

        categoryViewModel.categories = await _categoryService.SearchAsync(categoryViewModel.categorySearchCriteria);
        return View(categoryViewModel);
    }

    //    // GET: CategoryController/Details/5
    public async Task<ActionResult> Details(Guid? id)
    {
        var category = await _categoryService.GetByIdAsync(id);
        if (category is null)
        {
            return RedirectToAction(nameof(Index));
        }
        return View(category);
    }

    //    // GET: CategoryController/Create
    public ActionResult Create()
    {
        return View();
    }

    // POST: CategoryController/Create
    [HttpPost]
    [TypeFilter(typeof(ModelValidatorActionFilter))]
    public async Task<ActionResult> Create(CategoryAddRequest? categoryRequest)
    {
        if (ModelState.IsValid)
        {
            await _categoryService.AddAsync(categoryRequest);
            return RedirectToAction(nameof(Index));
        }
        return View();
    }

    // GET: CategoryController/Edit/5
    public async Task<ActionResult> Edit(Guid? id)
    {
        var category = await _categoryService.GetByIdAsync(id);
        if(category is null)
        {
            return RedirectToAction(nameof(Index));
        }
        var categoryUpdateRequest = new CategoryUpdateRequest()
        {
            Id = category.Id,
            Name = category.Name
        };
        return View(categoryUpdateRequest);
    }

    // POST: CategoryController/Edit/5
    [HttpPost]
    [TypeFilter(typeof(ModelValidatorActionFilter))]
    public async Task<ActionResult> Edit(CategoryUpdateRequest? categoryRequest)
    {
        if (ModelState.IsValid)
        {
            await _categoryService.UpdateAsync(categoryRequest);
            return RedirectToAction(nameof(Index));
        }
        return View();
    }
    public async Task<ActionResult> Delete(Guid? id)
    {
        var isDeleted = await _categoryService.DeleteByIdAsync(id);
        if (isDeleted)
        {
            return RedirectToAction(nameof(Index));
        }
        return BadRequest("Failed to delete the Category");
    }

}