using Dreamers.Ui.Dtos;
using Dreamers.Ui.Infrastructure;
using Dreamers.Ui.Models;
using Dreamers.Ui.Repositories;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Dreamers.Ui.Pages
{
    public class IndexModel : PageModel
    {
        private ExcursionRepo excursionRepo;

        public IQueryable<Excursion> Excursions { get; private set; }

        public IndexModel(ExcursionRepo excursionRepo)
        {
            this.excursionRepo = excursionRepo;
            Excursions = excursionRepo.GeteFeaturedExcursions();
        }

        public void OnGet()
        {
            
        }

    }
}