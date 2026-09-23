using GiftOfTheGiversPrototype.Data;
using GiftOfTheGiversPrototype.Models;
using GiftOfTheGiversPrototype.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace GiftOfTheGiversPrototype.Controllers;

public class VolunteerController : Controller
{
    private readonly ApplicationDbContext _context;

    public VolunteerController(ApplicationDbContext context) => _context = context;

    [HttpGet]
    public IActionResult Register() => View(new VolunteerViewModel());

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Register(VolunteerViewModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        _context.Volunteers.Add(new Volunteer
        {
            FullName = model.FullName,
            Email = model.Email,
            Skills = model.Skills,
            Availability = model.Availability,
            SubmittedAt = DateTime.Now
        });

        await _context.SaveChangesAsync();

        TempData["Success"] = "Thank you! Your volunteer application has been received.";
        return RedirectToAction(nameof(Register));
    }
}
