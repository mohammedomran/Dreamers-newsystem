using Dreamers.Ui.Dtos;
using Dreamers.Ui.Infrastructure;
using Dreamers.Ui.Models;
using Dreamers.Ui.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Dreamers.Ui.Pages.Admin.Excursions
{
    public class EditModel : PageModel
    {
        public ExcursionRepo ExcursionRepository { get; set; }
        public IFileService FileService { get; set; }
        public Excursion Excursion { get; set; }
        [BindProperty]
        public ExcursionAddDto ExcursionAddDto { get; set; } = new ExcursionAddDto();

        public EditModel(ExcursionRepo excursionRepository, IFileService fileService)
        {
            ExcursionRepository = excursionRepository;
            FileService = fileService;
        }

        public void OnGet(int excursionId)
        {
            Excursion = ExcursionRepository.GetExcursion(excursionId);
            ExcursionAddDto.Introduction = Excursion.Introduction;
            ExcursionAddDto.Description = Excursion.Description;
            ExcursionAddDto.BannerDescription = Excursion.BannerDescription;

        }

        public void OnPostMainPhoto(int excursionId)
        {
            Excursion = ExcursionRepository.GetExcursion(excursionId);

            ExcursionAddDto.MainPhoto = Request.Form.Files.GetFile("MainPhoto");
            FileService.UploadFile(ExcursionAddDto.MainPhoto, "excursions");
            ExcursionRepository.UpdateExcursionMainPhoto(excursionId, ExcursionAddDto.MainPhoto.FileName);
        }

        public void OnPostBannerPhoto(int excursionId)
        {
            Excursion = ExcursionRepository.GetExcursion(excursionId);

            ExcursionAddDto.MainPhoto = Request.Form.Files.GetFile("BannerPhoto");
            FileService.UploadFile(ExcursionAddDto.BannerPhoto, "excursions");
            ExcursionRepository.UpdateExcursionBannerPhoto(excursionId, ExcursionAddDto.BannerPhoto.FileName);
        }

        public void OnPostExcursionPhotos(int excursionId)
        {
            Excursion = ExcursionRepository.GetExcursion(excursionId);

            var submitButtonValue = Request.Form["submitButton"];
            if (submitButtonValue == "delete")
            {
                //ExcursionRepo.DeleteExcursionPhoto(Excursion.Id, );
            }

            if (submitButtonValue == "storephoto")
            {

                foreach (var file in ExcursionAddDto.Photos)
                    FileService.UploadFile(file, "excursions");

                var excursionPhotos = ExcursionAddDto.Photos.Select(x => new ExcursionPhoto
                {
                    ExcursionId = Excursion.Id,
                    Photo = x.FileName
                }).ToList();
                ExcursionRepository.StoreExcursionPhotos(Excursion.Id, excursionPhotos);

            }
        }

        public void OnPostTextData(int excursionId)
        {
            Excursion = ExcursionRepository.GetExcursion(excursionId);

            var storedExcursion = ExcursionRepository.UpdateExcursion(new Excursion
            {
                Id = Excursion.Id,
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

        }


    }
}
