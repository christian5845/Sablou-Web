using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sablou_Web.Services.Repositories;

namespace Sablou_Web.Pages.Chokolader
{
    public class InformationModel : PageModel
    {
        private readonly ChokoladeRepository _repository;

        public InformationModel(ChokoladeRepository repository)
        {
            _repository = repository;
        }

        public Sablou_Web.Models.Chokolade? Chokolade { get; set; }

        public IActionResult OnGet(int id)
        {
            Chokolade = _repository.GetItem(id);

            if (Chokolade == null)
            {
                return NotFound();
            }

            return Page();
        }
    }
}