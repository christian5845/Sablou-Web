using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sablou_Web.Services;
using Sablou_Web.Models;

namespace Sablou_Web.Pages
{
    public class VoresChokoladerModel : PageModel
    {
        public  IDataService Repositories { get; }

        public VoresChokoladerModel()
        {
            Repositories = new Dataservice();
        }

        public void OnGet()
        {
        }
    }
}
