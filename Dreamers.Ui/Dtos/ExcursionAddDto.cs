namespace Dreamers.Ui.Dtos
{
    public class ExcursionAddDto
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public FileInfo MainPhoto { get; set; }
        public IFormFile BannerPhoto { get; set; }
    }
}
