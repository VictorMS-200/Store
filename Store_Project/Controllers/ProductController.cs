using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Store.Models;

namespace Store.Controllers;

public class ProductController : Controller
{
    private readonly StoreWebContext _context;

    public ProductController(StoreWebContext context)
    {
        this._context = context;
    }

    public async Task<IActionResult> Index()
    {
        return View(await _context.Products.OrderBy(x => x.Name).Include(x => x.Category).AsNoTracking().ToListAsync());
    }

    [HttpGet]
    public async Task<IActionResult> Register(int? id)
    {
        var categories = _context.Categories.OrderBy(x => x.Name).AsNoTracking().ToList();
        var categoriesSelectList = new SelectList(
            categories, nameof(CategoryModel.IdCategory), nameof(CategoryModel.Name));

        ViewBag.Categories = categoriesSelectList;

        if(id.HasValue)
        {
            var Product = await _context.Products.FindAsync(id);

            if (Product == null)
            {
                return NotFound();
            }
            return View(Product);
        }
        return View(new ProductModel());
    }

    [HttpPost]
    public async Task<IActionResult> Register(int? id, [FromForm] ProductModel model)
    {
        if (ModelState.IsValid)
        {
            if (id.HasValue)
            {
                if (ProductsExist(id.Value))
                {
                    _context.Update(model);
                    if (await _context.SaveChangesAsync() > 0)
                    {
                        TempData["message"] = MessageModel.Serializer("Product successfully updated.");
                    }
                    else
                    {
                        TempData["message"] = MessageModel.Serializer("Error updating product.", TypeMessage.Error);
                    }
                }
                else
                {
                    TempData["message"] = MessageModel.Serializer("Product not found.", TypeMessage.Error);
                }
            }
            else
            {
                _context.Add(model);
                if (await _context.SaveChangesAsync() > 0)
                {
                    TempData["message"] = MessageModel.Serializer("Product successfully created.");
                }
                else
                {
                    TempData["message"] = MessageModel.Serializer("Error creating product.", TypeMessage.Error);
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
            TempData["message"] = MessageModel.Serializer("Product not informed.", TypeMessage.Error);
            return RedirectToAction("Index");
        }

        var Product = await _context.Products.FindAsync(id);

        if (Product == null)
        {
            TempData["message"] = MessageModel.Serializer("Product not found.", TypeMessage.Error);
            return RedirectToAction("Index");
        }

        return View(Product);
    }


    private bool ProductsExist(int id)
    {
        return _context.Products.Any(x => x.IdProduct == id);

    }

    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {
        var Product = await _context.Products.FindAsync(id);
        if (Product != null)
        {
            _context.Products.Remove(Product);
            if (await _context.SaveChangesAsync() > 0)
                TempData["message"] = MessageModel.Serializer("Product success removed.");
            else
                TempData["message"] = MessageModel.Serializer("Wasn't possible to delete the product.", TypeMessage.Error);
        }
        else
        {
            TempData["message"] = MessageModel.Serializer("Product not found.", TypeMessage.Error);
        }
        return RedirectToAction("Index");
    }

}