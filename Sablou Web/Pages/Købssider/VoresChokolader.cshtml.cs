using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sablou_Web.Services;

namespace Sablou_Web.Pages.Købssider
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
