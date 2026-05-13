using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sablou_Web.Services.Repositories;

namespace Sablou_Web.Pages.Chokolader
{
    public class VoresChokoladerModel : PageModel
    {
        private readonly ChokoladeRepository _repository;

        public VoresChokoladerModel(ChokoladeRepository repository)
        {
            _repository = repository;
        }

        public List<Sablou_Web.Models.Chokolade> Chokolader { get; set; } = new();
        [BindProperty(SupportsGet = true)]
        public string Search { get; set; } = string.Empty;
        [BindProperty(SupportsGet = true)]
        public string Sort { get; set; } = "name";
        public int Page { get; set; } = 1;
        public int TotalPages { get; set; } = 1;

        public void OnGet()
        {

            Chokolader = _repository.Data.Values.ToList();
        }
    }
}