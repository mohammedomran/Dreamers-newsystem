namespace Dreamers.Ui.Dtos
{
    public class ExcursionAddDto
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string VideoLink { get; set; }
        public IFormFile MainPhoto { get; set; }
        public IFormFile BannerPhoto { get; set; }
        public List<IFormFile> Photos { get; set; }

        public string Title { get; set; }
        public string Introduction { get; set; }
        public string Description { get; set; }
        public string BannerDescription { get; set; }
        public string Period { get; set; }
        public string City { get; set; }
    }
}
