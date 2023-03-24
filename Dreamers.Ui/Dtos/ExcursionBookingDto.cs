namespace Dreamers.Ui.Dtos
{
    public class ExcursionBookingDto
    {
        public string FullName { get; set; }
        public string Excursion { get; set; }
        public string CheckIn { get; set; }
        public int Adults { get; set; }
        public int Children { get; set; }
        public decimal AdultPrice { get; set; }
        public decimal ChildrenPrice { get; set; }
        public decimal Total { get; set; }
        public string Email { get; internal set; }
    }
}