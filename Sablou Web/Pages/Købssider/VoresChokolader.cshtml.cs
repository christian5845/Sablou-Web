using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sablou_Web.Services;

namespace Sablou_Web.Pages.Købssider
{
    public class ButiksfrontModel : PageModel
    {
        public  IDataService Repositories { get; }

        public ButiksfrontModel()
        {
            Repositories = new Dataservice();
        }

        public void OnGet()
        {
        }
    }
}
