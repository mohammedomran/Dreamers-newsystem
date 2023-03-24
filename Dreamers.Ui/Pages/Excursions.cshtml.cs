using Dreamers.Ui.Models;
using Dreamers.Ui.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Dreamers.Ui.Pages
{
    public class ExcursionsModel : PageModel
    {
        private readonly ExcursionRepo excursionRepo;

        public IEnumerable<Excursion> Excursions { get; private set; }

        public ExcursionsModel(ExcursionRepo excursionRepo)
        {
            this.excursionRepo = excursionRepo;
            Excursions = excursionRepo.GeteExcursions();
        }

        public void OnGet()
        {
        }
    }
}
