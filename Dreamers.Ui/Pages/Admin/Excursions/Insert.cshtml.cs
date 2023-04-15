using Dreamers.Ui.Dtos;
using Dreamers.Ui.Infrastructure;
using Dreamers.Ui.Models;
using Dreamers.Ui.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Dreamers.Ui.Pages.Admin.Excursions
{
    public class InsertModel : PageModel
    {
        [BindProperty]
        public ExcursionAddDto ExcursionAddDto { get; set; } = new ExcursionAddDto();
        public ExcursionRepo ExcursionRepository { get; }
        public IFileService FileService { get; }

        public InsertModel(ExcursionRepo excursionRepository, IFileService fileService)
        {
            ExcursionRepository = excursionRepository;
            FileService = fileService;
        }

        public void OnGet()
        {

        }

        public void OnPost()
        {
            FileService.UploadFile(ExcursionAddDto.BannerPhoto, "excursions");
            FileService.UploadFile(ExcursionAddDto.MainPhoto, "excursions");
            foreach (var file in ExcursionAddDto.Photos)
                FileService.UploadFile(file, "excursions");

            var storedExcursion = ExcursionRepository.StoreExcursion(new Excursion
            {
                BannerPhoto = ExcursionAddDto.BannerPhoto.FileName,
                MainPhoto = ExcursionAddDto.MainPhoto.FileName,
                Name = ExcursionAddDto.Name,
                Price = ExcursionAddDto.Price,
                VideoLink = ExcursionAddDto.VideoLink,
                ExcursionLocalizeds = new List<ExcursionLocalized>() {
                    new ExcursionLocalized {
                        Title = ExcursionAddDto.Title,
                        Description = ExcursionAddDto.Description,
                        City = ExcursionAddDto.City,
                        Period = ExcursionAddDto.Period,
                        Introduction = ExcursionAddDto.Introduction,
                        BannerDescription = ExcursionAddDto.BannerDescription,
                    }
                },
            });

            var excursionPhotos = ExcursionAddDto.Photos.Select(x => new ExcursionPhoto {
                ExcursionId = storedExcursion.Id,
                Photo = x.FileName
            }).ToList();
            ExcursionRepository.StoreExcursionPhotos(storedExcursion.Id, excursionPhotos);
        }

    }
}
