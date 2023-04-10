using DataBase.Data;
using DataBase.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Diploma.Controllers;

public class PartnersController : Controller
{
    ApplicationContext db;

    public PartnersController(ApplicationContext context)
    {
        db = context;
    }
    public async Task<IActionResult> Index()
    {
        return View(await db.Partners.ToListAsync());
    }
    public IActionResult Create()
    {
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> Create(Partner partner)
    {
        db.Partners.Add(partner);
        await db.SaveChangesAsync();
        return RedirectToAction("Index");
    }
    
    [HttpPost]
    public async Task<IActionResult> Delete(int? id)
    {
        if (id != null)
        {
            var partner = new Partner { Id = id.Value };
            db.Entry(partner).State = EntityState.Deleted;
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        return NotFound();
    }
    public async Task<IActionResult> Edit(int? id)
    {
        if(id!=null)
        {
            Partner? partner = await db.Partners.FirstOrDefaultAsync(p=>p.Id==id);
            if (partner != null) return View(partner);
        }
        return NotFound();
    }
    [HttpPost]
    public async Task<IActionResult> Edit(Partner user)
    {
        db.Partners.Update(user);
        await db.SaveChangesAsync();
        return RedirectToAction("Index");
    }
}