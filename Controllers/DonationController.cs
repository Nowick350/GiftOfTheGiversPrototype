using GiftOfTheGiversPrototype.Data;
using GiftOfTheGiversPrototype.Models;
using GiftOfTheGiversPrototype.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GiftOfTheGiversPrototype.Controllers;

public class DonationController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;

    public DonationController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    [HttpGet]
    public IActionResult Donate()
    {
        var model = new DonateViewModel();

        if (User.Identity?.IsAuthenticated == true)
        {
            model.DonorName = User.Identity.Name?.Split('@')[0] ?? "";
        }

        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Donate(DonateViewModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        var user = await _userManager.GetUserAsync(User);

        var donation = new Donation
        {
            DonorName = model.DonorName,
            UserId = user?.Id,
            Amount = model.Amount,
            Currency = model.Currency,
            Frequency = model.Frequency,
            PaymentMethod = model.PaymentMethod,
            ReceiptNumber = $"GOTG-{DateTime.Now:yyyyMMddHHmmss}-{Random.Shared.Next(100,999)}",
            CreatedAt = DateTime.Now
        };

        _context.Donations.Add(donation);
        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Receipt), new { id = donation.Id });
    }

    public async Task<IActionResult> Receipt(int id)
    {
        var donation = await _context.Donations.FindAsync(id);

        if (donation == null)
            return NotFound();

        return View(donation);
    }
}
