using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Store.Models;

namespace Store.Controllers;

public class CategoryController : Controller
{
    private readonly StoreWebContext _context;

    public CategoryController(StoreWebContext context)
    {
        this._context = context;
    }

    public async Task<IActionResult> Index()
    {
        return View( await _context.Categories.OrderBy(x => x.Name).AsNoTracking().ToListAsync());
    }

    [HttpGet]
    public async Task<IActionResult> Register(int? id)
    {
        if(id.HasValue)
        {
            var category = await _context.Categories.FindAsync(id);

            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }
        return View(new CategoryModel());
    }

    [HttpPost]
    public async Task<IActionResult> Register(int? id, [FromForm] CategoryModel model)
    {
        if (ModelState.IsValid)
        {
            if (id.HasValue)
            {
                if (CategoriesExist(id.Value))
                {
                    _context.Update(model);
                    if (await _context.SaveChangesAsync() > 0)
                    {
                        TempData["message"] = MessageModel.Serializer("Category successfully updated.");
                    }
                    else
                    {
                        TempData["message"] = MessageModel.Serializer("Error updating category.", TypeMessage.Error);
                    }
                }
                else
                {
                    TempData["message"] = MessageModel.Serializer("Category not found.", TypeMessage.Error);
                }
            }
            else
            {
                _context.Add(model);
                if (await _context.SaveChangesAsync() > 0)
                {
                    TempData["message"] = MessageModel.Serializer("Category successfully created.");
                }
                else
                {
                    TempData["message"] = MessageModel.Serializer("Error creating category.", TypeMessage.Error);
                }
            }
            return RedirectToAction("Index");
        }
        else
        {
            return View(model);
        }
    }


    [HttpGet]
    public async Task<IActionResult> Delete(int? id)
    {
        if (!id.HasValue)
        {
            TempData["message"] = MessageModel.Serializer("Category not informed.", TypeMessage.Error);
            return RedirectToAction("Index");
        }

        var category = await _context.Categories.FindAsync(id);

        if (category == null)
        {
            TempData["message"] = MessageModel.Serializer("Category not found.", TypeMessage.Error);
            return RedirectToAction("Index");
        }

        return View(category);
    }


    private bool CategoriesExist(int id)
    {
        return _context.Categories.Any(x => x.IdCategory == id);

    }

    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {
        var category = await _context.Categories.FindAsync(id);
        if (category != null)
        {
            _context.Categories.Remove(category);
            if (await _context.SaveChangesAsync() > 0)
                TempData["message"] = MessageModel.Serializer("Category success removed.");
            else
                TempData["message"] = MessageModel.Serializer("Wasn't possible to delete the category.", TypeMessage.Error);
        }
        else
        {
            TempData["message"] = MessageModel.Serializer("Category not found.");
        }
        return RedirectToAction("Index");
    }

}