using FastFoodRestaurant.Models;
using FastFoodRestaurant.Repository.IGenericRepository;
using Microsoft.AspNetCore.Mvc;

namespace FastFoodRestaurant.Controllers
{
    [Area("Admin")]
    public class PartyController : Controller
    {
        private readonly IPartyRepository _partyRepo;
        private const int pageSize = 4;

        public PartyController(IPartyRepository partyRepo)
        {
            _partyRepo = partyRepo;
        }

        public IActionResult Index(string? keyword, string? name,int pageNumber = 1)
        {
            List<Party> partiesList;

            if (!string.IsNullOrEmpty(keyword) && !string.IsNullOrEmpty(name))
            {
                partiesList = _partyRepo.GetAllExpression(p => p.PartyId == keyword && p.PhoneNumber == name).ToList();
            }
            else if (!string.IsNullOrEmpty(keyword))
            {
                partiesList = _partyRepo.GetAllExpression(p => p.PartyId == keyword).ToList();
            }
            else if (!string.IsNullOrEmpty(name))
            {
                partiesList = _partyRepo.GetAllExpression(p => p.PhoneNumber == name).ToList();
            }
            else
            {
                partiesList = _partyRepo.GetAll().ToList();
            }

            var pagedPartiesList = partiesList.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

            ViewData["CurrentPage"] = pageNumber;
            ViewData["TotalPages"] = (int)Math.Ceiling(partiesList.Count() / (double)pageSize);

            return View(pagedPartiesList);
        }
    }
}
