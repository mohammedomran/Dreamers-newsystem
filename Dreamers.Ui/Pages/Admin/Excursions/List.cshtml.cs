using Dreamers.Ui.Dtos;
using Dreamers.Ui.Infrastructure;
using Dreamers.Ui.Models;
using Dreamers.Ui.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Dreamers.Ui.Pages.Admin.Excursions
{
    public class ListModel : PageModel
    {
        
        public IQueryable<Excursion> Excursions { get; private set; }

        public ListModel(ExcursionRepo excursionRepository)
        {
            Excursions = excursionRepository.GeteExcursions();
        }


        public void OnGet()
        {

        }

    }
}
