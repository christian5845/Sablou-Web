using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Sablou_Web.Models;
using Sablou_Web.Pages.BrugerLogin;
using Sablou_Web.Services;

namespace Sablou_Web.Pages.Kurve
{
    public class KurvModel : PageModel
    {
        private IDataService _repo;

        public List<KurvLinje> KurvLinje { get; set; } = new List<KurvLinje>();

        public KurvModel(IDataService repo)
        {
            _repo = repo;
        }

        public void OnGet()
        {
        }
    }
}


