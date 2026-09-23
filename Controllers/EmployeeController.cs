using GiftOfTheGiversPrototype.Data;
using GiftOfTheGiversPrototype.Models;
using GiftOfTheGiversPrototype.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GiftOfTheGiversPrototype.Controllers;

[Authorize(Roles = "Employee")]
public class EmployeeController : Controller
{
    private readonly ApplicationDbContext _context;

    public EmployeeController(ApplicationDbContext context) => _context = context;

    public async Task<IActionResult> Dashboard()
    {
        ViewBag.Volunteers = await _context.Volunteers
            .OrderByDescending(v => v.SubmittedAt)
            .ToListAsync();

        ViewBag.Donations = await _context.Donations
            .OrderByDescending(d => d.CreatedAt)
            .Take(10)
            .ToListAsync();

        return View(await _context.ReliefProjects.ToListAsync());
    }

    [HttpGet]
    public async Task<IActionResult> EditProject(int id)
    {
        var project = await _context.ReliefProjects.FindAsync(id);
        if (project == null) return NotFound();

        return View(new ProjectViewModel
        {
            Id = project.Id,
            Name = project.Name,
            Location = project.Location,
            Status = project.Status,
            Description = project.Description
        });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> EditProject(ProjectViewModel model)
    {
        if (!ModelState.IsValid) return View(model);

        var project = await _context.ReliefProjects.FindAsync(model.Id);
        if (project == null) return NotFound();

        project.Name = model.Name;
        project.Location = model.Location;
        project.Status = model.Status;
        project.Description = model.Description;

        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Dashboard));
    }
}
