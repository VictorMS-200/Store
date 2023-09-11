using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Store.Models;

namespace Store.Controllers;

public class AddressController : Controller
{
    private readonly StoreWebContext _context;

    public AddressController(StoreWebContext context)
    {
        this._context = context;
    }

    public async Task<IActionResult> Index(int? cid)
    {
        if (cid.HasValue)
        {
            var customer = await _context.Customers.FindAsync(cid);
            if (customer != null)
            {
                _context.Entry(customer).Collection(x => x.Addresses).Load();
                ViewBag.Customer = customer;
                return View(customer.Addresses);
            }
            else
            {
                TempData["message"] = MessageModel.Serializer("Customer not found.", TypeMessage.Error);
                return RedirectToAction("Index", "Customer");
            }
        }
        else
        {
            TempData["message"] = MessageModel.Serializer("Only one customer address can be shown!", TypeMessage.Error);
            return RedirectToAction("Index", "Customer");
        }
    }

    [HttpGet]
    public async Task<IActionResult> Register(int? cid, int? aid)
    {
        if (cid.HasValue)
        {
            var customer = await _context.Customers.FindAsync(cid);
            if (customer != null)
            {
                ViewBag.Customer = customer;
                if (aid.HasValue)
                {
                    _context.Entry(customer).Collection(x => x.Addresses).Load();
                    var Address = customer.Addresses.FirstOrDefault(x => x.IdAddress == aid);
                    if (Address != null)
                        return View(Address);

                    else
                        TempData["message"] = MessageModel.Serializer("Address not found.", TypeMessage.Error);

                }
                else
                    return View(new AddressModel());
            }
            else
                TempData["message"] = MessageModel.Serializer("Customer not found.", TypeMessage.Error);

            return RedirectToAction("Index");
        }
        else
        {
            TempData["message"] = MessageModel.Serializer("No owner has been informed.", TypeMessage.Error);
            return RedirectToAction("Index", "Customer");
        }
    }

    [HttpPost]
    public async Task<IActionResult> Register([FromForm]int? IdUser, [FromForm] AddressModel model)
    {
        if (IdUser.HasValue)
        {
            var customer = await _context.Customers.FindAsync(IdUser);
            ViewBag.Customer = customer;

            if (ModelState.IsValid)
            {
                if (customer.Addresses.Count().Equals(0))
                    model.Selected = true;

                model.ZipCode = GetNormalizedZipCode(model.ZipCode);

                if (model.IdAddress > 0)
                {
                    if (model.Selected)
                        customer.Addresses.ToList().ForEach(x => x.Selected = false);
                    
                    if (AddressExist(IdUser.Value, model.IdAddress))
                    {
                        var defaultAddress = customer.Addresses.FirstOrDefault(x => x.IdAddress == model.IdAddress);
                        _context.Entry(defaultAddress).CurrentValues.SetValues(model);

                        if (_context.Entry(defaultAddress).State == EntityState.Unchanged)
                            TempData["message"] = MessageModel.Serializer("No changes were made.", TypeMessage.Error);
                        
                        else
                        {
                            if (await _context.SaveChangesAsync() > 0)
                                TempData["message"] = MessageModel.Serializer("Address success updated.");
                            
                            else
                                TempData["message"] = MessageModel.Serializer("Wasn't possible to update the Address.", TypeMessage.Error);
                        }
                    }
                    else
                        TempData["message"] = MessageModel.Serializer("Address not found.", TypeMessage.Error);
                }
                else
                {
                    model.IdAddress  = customer.Addresses.Count() > 0 ? customer.Addresses.Max(x => x.IdAddress) + 1 : 1;

                    _context.Customers.FirstOrDefault(x => x.IdUser == IdUser).Addresses.Add(model);

                    if (await _context.SaveChangesAsync() > 0)
                        TempData["message"] = MessageModel.Serializer("Address success registered.");
                    else
                        TempData["message"] = MessageModel.Serializer("Wasn't possible to register the Address.", TypeMessage.Error);
                    
                }
                return RedirectToAction("Index", new { cid = IdUser });
            }
            else
                return View(model);
        }
        else
        {
            TempData["message"] = MessageModel.Serializer("No owner has been informed.", TypeMessage.Error);
            return RedirectToAction("Index", "Customer");
        } 
    }

    private string GetNormalizedZipCode(string zipCode)
    {
        string normalizedZipCode = zipCode.Replace("-", "").Replace(".", "").Trim();
        return normalizedZipCode.Insert(normalizedZipCode.Length - 3, "-");
    }

    [HttpGet]
    public async Task<IActionResult> Delete(int? cid, int? aid)
    {
        if (!cid.HasValue)
        {
            TempData["message"] = MessageModel.Serializer("Customer not informed.", TypeMessage.Error);
            return RedirectToAction("Index", "Customer");
        }

        if (!aid.HasValue)
        {
            TempData["message"] = MessageModel.Serializer("Address not informed.", TypeMessage.Error);
            return RedirectToAction("Index", new { cid = cid });
        }

        var customer = await _context.Customers.FindAsync(cid);
        var Address = customer.Addresses.FirstOrDefault(e => e.IdAddress == aid);

        if (Address == null)
        {
            TempData["message"] = MessageModel.Serializer("Address not found.", TypeMessage.Error);
            return RedirectToAction("Index", new { cid = cid });
        }

        ViewBag.Customer = customer;
        return View(Address);
    }


    private bool AddressExist(int cid, int aid)
    {
        return _context.Customers.FirstOrDefault(c => c.IdUser == cid).Addresses.Any(x => x.IdAddress == aid);
    }

    [HttpPost]
    public async Task<IActionResult> Delete(int idUser, int idAddress)
    {
        var customer = await _context.Customers.FindAsync(idUser);
        var Address = customer.Addresses.FirstOrDefault(e => e.IdAddress == idAddress);

        if (Address != null)
        {
            customer.Addresses.Remove(Address);
            
            if (await _context.SaveChangesAsync() > 0)
            {
                TempData["message"] = MessageModel.Serializer("Address success deleted.");
            
                if (Address.Selected && customer.Addresses.Count() > 0)
                {
                    customer.Addresses.FirstOrDefault().Selected = true;

                    await _context.SaveChangesAsync();
                }
            }

            else
                TempData["message"] = MessageModel.Serializer("Wasn't possible to delete the Address.", TypeMessage.Error);
        }
        else
            TempData["message"] = MessageModel.Serializer("Address not found.", TypeMessage.Error);

        return RedirectToAction("Index", new { cid = idUser });
    }

}