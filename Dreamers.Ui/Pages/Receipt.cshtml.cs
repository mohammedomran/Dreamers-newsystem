using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Dreamers.Ui.Pages
{
    public class ReceiptModel : PageModel
    {
        public string BookingKey { get; private set; }

        public void OnGet(string bookingKey)
        {
            BookingKey = bookingKey;
        }
    }
}
