using System.Security.Cryptography.X509Certificates;
using DataBase.Data;
using DataBase.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Diploma.Controllers;
[ApiExplorerSettings(IgnoreApi = true)]
[Route("[controller]")]
public class PartnersController : Controller
{
    private IPartnersDataManager PartnersDataManager { get; set; }

    public PartnersController(IPartnersDataManager partnersDataManager)
    {
        PartnersDataManager = partnersDataManager;
    }
    [HttpGet(Name = "GetPartners")]
    public async Task<IActionResult> Get()
    {
        return View(await PartnersDataManager.GetPartnersAsync());
    }

    [HttpPost(Name = "PostTest")]
    public async Task<IActionResult> Post(string? shortNamePart)
    {
        return View("Get",await PartnersDataManager.GetPartnerAsync(shortNamePart));
    }
    public IActionResult Create()
    {
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> Create(Partner partner)
    {
        await PartnersDataManager.AddPartnerAsync(partner);
        return RedirectToAction("Get");
    }
    
    
    
    [HttpPost]
    public async Task<IActionResult> Delete(int? id)
    {
        if (id != null)
        {
            await PartnersDataManager.DeletePartnerAsync(id.Value);
            return RedirectToAction("Get");
        }
        return NotFound();
    }
    public async Task<IActionResult> Edit(int? id)
    {
        if(id!=null)
        {
            Partner? partner = await PartnersDataManager.EditPartner(id.Value);
            if (partner != null) return View(partner);
        }
        return NotFound();
    }
    [HttpPost]
    public async Task<IActionResult> Edit(Partner partner)
    {
        await PartnersDataManager.EditPartner(partner);
        return RedirectToAction("Get");
    }
}