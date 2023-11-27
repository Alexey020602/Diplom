using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Mvc;
using DataBase.Data;
using DataBase.Models;
using Microsoft.EntityFrameworkCore;

namespace Diploma.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PartnersController : ControllerBase
{
    private ApplicationContext _context { get; set; }

    public PartnersController(ApplicationContext context)
    {
        this._context = context;
    }

    [HttpGet]
    public IActionResult ShowPartners()
    {
        var list = 
            from p in _context.Partners.Include(p=>p.PartnerType) 
            //where p.
            select p;

        return new JsonResult(list.ToList());
    }

    [HttpGet("{id}")]
    public IActionResult ShowPartner(int id)
    {
        var partner =
            (from p in _context.Partners
                where p.Id == id
                select p).FirstOrDefault();
        if (partner == null)
        {
            return NotFound("Нет соответсвующего партнера");
        }
        else
        {
            return new JsonResult(partner);
        }
    }

    [HttpPut("partners/{id:int}")]
    public IActionResult EditPartner(
        int id,
        [FromForm] string? fullName,
        [FromForm] string? shortName,
        [FromForm] string? address,
        [FromForm] string? site,
        [FromForm] string? contactData,
        [FromForm] string? city
        )
    {
        var partner = (
            from p in _context.Partners.Include(p => p.PartnerType)
            where p.Id == id
            select p
        ).ToList().FirstOrDefault();
        if (partner == null)
        {
            return NotFound("Нет соответсвующего партнера");
        }

        if (fullName != null)
        {
            partner.FullName = fullName;
        }
        
        if (shortName != null)
        {
            partner.ShortName = shortName;
        }

        if (address != null)
        {
            partner.Address = address;
        }

        if (site != null)
        {
            partner.Site = site;
        }

        if (contactData != null)
        {
            partner.ContactData = contactData;
        }

        if (city != null)
        {
            partner.City = city;
        }
        _context.Partners.Update(partner);
        _context.SaveChanges();
        return Ok();
    }
    
    [HttpDelete("partners/{id:int}")]
    public IActionResult EditPartner(
        int id
    )
    {
        var partner = (
            from p in _context.Partners.Include(p => p.PartnerType)
            where p.Id == id
            select p
        ).ToList().FirstOrDefault();
        if (partner == null)
        {
            return NotFound("Нет соответсвующего партнера");
        }
        _context.Partners.Remove(partner);
        _context.SaveChanges();
        return Ok();
    }

    [HttpPost("partners")]
    public IActionResult CreatePartner(
        [FromForm] [Required] string fullName,
        [FromForm] [Required] string shortName,
        [FromForm] string? address,
        [FromForm] string? site,
        [FromForm] string? contactData,
        [FromForm] string? city
        )
    {
        var type =
            (from t in _context.PartnerTypes
                select t).FirstOrDefault();
        var partner = new Partner()
        {
            FullName = fullName,
            ShortName = shortName,
            Address = address,
            Site = site,
            ContactData = contactData,
            City = city,
            PartnerType = type
        };

        _context.Partners.Add(partner);
        _context.SaveChanges();
        return Ok();
    }

    [HttpGet("partnertypes")]
    public IActionResult ShowPartnerTypes()
    {
        var list =
            from type in _context.PartnerTypes
            select type;
        return new JsonResult(list);
    }

    [HttpGet("partnertypes/{id}")]
    public IActionResult ShowPartnerType(int id)
    {
        var type = (from t in _context.PartnerTypes where t.Id == id select t).FirstOrDefault();
        if (type == null) return NotFound("Нет соответсвующего типа партнера");
        return new JsonResult(type);
    }
    
    [HttpDelete("partnertypes/{id}")]
    public IActionResult DeletePartnerType(int id)
    {
        var type = (from t in _context.PartnerTypes where t.Id == id select t).FirstOrDefault();
        if (type == null) return NotFound("Нет соответсвующего типа партнера");
        _context.PartnerTypes.Remove(type);
        _context.SaveChanges();
        return Ok();
    }
    
    [HttpPost("partnertypes")]
    public IActionResult CreatePartnerType(
        [FromForm] [Required] string name
    )
    {
        var type = new PartnerType()
        {
            Name = name
        };
        _context.PartnerTypes.Add(type);
        _context.SaveChanges();
        //if (type == null) return NotFound("Нет соответсвующего типа партнера");
        return new JsonResult(type);
    }
}