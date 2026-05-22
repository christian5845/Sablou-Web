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

public class DetaljerModel : PageModel
{
    private IDataService _repositories;

    public DetaljerModel(IDataService repo)
    {
        _repositories = repo;
    }

    public Chokolade Chokolade { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var chokolade = _repositories.ChokoladeRepository.GetItem(id);

        if (chokolade is not null)
        {
            Chokolade = chokolade;

            return Page();
        }

        return NotFound();
    }
}
