using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Sablou_Web.Models;
using Sablou_Web.Services;

namespace Sablou_Web.Pages.Chokolader;

public class OversigtModel : PageModel
{
   public IDataService Repo { get; }

    public OversigtModel(IDataService repo)
    {
        Repo = repo;
        
    }
    public async Task OnGet()
    {
       
    }
}
