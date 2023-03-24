using Dreamers.Ui.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Dreamers.Ui.Pages.Admin.Excursions
{
    public class ListModel : PageModel
    {
        [BindProperty]
        public ExcursionAddDto ExcursionAddDto { get; set; } = new ExcursionAddDto();

        public ListModel()
        {

        }

        public void OnGet()
        {

        }

        public void OnPost()
        {
            var x = ExcursionAddDto;
        }

    }
}
