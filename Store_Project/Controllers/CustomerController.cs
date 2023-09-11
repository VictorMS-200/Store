using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Store.Models;

namespace Store.Controllers;

public class CustomerController : Controller
{
    private readonly StoreWebContext _context;

    public CustomerController(StoreWebContext context)
    {
        this._context = context;
    }

    public async Task<IActionResult> Index()
    {
        return View(await _context.Customers.OrderBy(x => x.Name).AsNoTracking().ToListAsync());
    }

    [HttpGet]
    public async Task<IActionResult> Register(int? id)
    {
        if(id.HasValue)
        {
            var Customer = await _context.Customers.FindAsync(id);

            if (Customer == null)
            {
                TempData["message"] = MessageModel.Serializer("Customer not found.", TypeMessage.Error);
                return RedirectToAction("Index");
            }
            return View(Customer);
        }
        return View(new CustomerModel());
    }

    [HttpPost]
    public async Task<IActionResult> Register(int? id, [FromForm] CustomerModel model)
    {
        if (ModelState.IsValid)
        {
            if (id.HasValue)
            {
                if (CustomersExist(id.Value))
                {
                    _context.Customers.Update(model);
                    _context.Entry(model).Property(x => x.Password).IsModified = false;

                    if (await _context.SaveChangesAsync() > 0)
                    {
                        TempData["message"] = MessageModel.Serializer("Customer successfully updated.");
                    }
                    else
                    {
                        TempData["message"] = MessageModel.Serializer("Error updating Customer.", TypeMessage.Error);
                    }
                }
                else
                {
                    TempData["message"] = MessageModel.Serializer("Customer not found.", TypeMessage.Error);
                }
            }
            else
            {
                _context.Customers.Add(model);
                
                if (await _context.SaveChangesAsync() > 0)
                {
                    TempData["message"] = MessageModel.Serializer("Customer successfully created.");
                }
                else
                {
                    TempData["message"] = MessageModel.Serializer("Error creating Customer.", TypeMessage.Error);
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
            TempData["message"] = MessageModel.Serializer("Customer not informed.", TypeMessage.Error);
            return RedirectToAction("Index");
        }

        var Customer = await _context.Customers.FindAsync(id);

        if (Customer == null)
        {
            TempData["message"] = MessageModel.Serializer("Customer not found.", TypeMessage.Error);
            return RedirectToAction("Index");
        }

        return View(Customer);
    }


    private bool CustomersExist(int id)
    {
        return _context.Customers.Any(x => x.IdUser == id);

    }

    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {
        var Customer = await _context.Customers.FindAsync(id);
        if (Customer != null)
        {
            _context.Customers.Remove(Customer);

            if (await _context.SaveChangesAsync() > 0)
                TempData["message"] = MessageModel.Serializer("Customer success removed.");
            else
                TempData["message"] = MessageModel.Serializer("Wasn't possible to delete the Customer.", TypeMessage.Error);
        }
        else
        {
            TempData["message"] = MessageModel.Serializer("Customer not found.", TypeMessage.Error);
        }
        return RedirectToAction("Index");
    }

}