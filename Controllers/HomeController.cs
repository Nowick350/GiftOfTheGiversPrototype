using GiftOfTheGiversPrototype.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GiftOfTheGiversPrototype.Controllers;

public class HomeController : Controller
{
    private readonly ApplicationDbContext _context;

    public HomeController(ApplicationDbContext context) => _context = context;

    public async Task<IActionResult> Index()
    {
        var projects = await _context.ReliefProjects
            .OrderByDescending(p => p.Status == "Ongoing")
            .ThenBy(p => p.Name)
            .ToListAsync();

        return View(projects);
    }

    public IActionResult About() => View();
}
