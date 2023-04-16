using Dreamers.Ui.Dtos;
using Dreamers.Ui.Enums;
using Dreamers.Ui.Interfaces;
using Dreamers.Ui.Models;
using Microsoft.EntityFrameworkCore;

namespace Dreamers.Ui.Repositories
{
    public class ExcursionRepo
    {
        private readonly AppDbContext _context;

        public ExcursionRepo(AppDbContext appDbContext)
        {
            this._context = appDbContext;
        }

        public IQueryable<Excursion> GeteExcursions()
        {
            return _context.Excursions.Include(x => x.ExcursionLocalizeds)
                .Include(x => x.ExcursionPhotos);
        }

        public Excursion GetExcursion(string urlName)
        {
            return _context.Excursions.Include(x => x.ExcursionLocalizeds)
                .Include(x => x.ExcursionPhotos)
                .FirstOrDefault(x => x.Name == urlName);
        }

        public Excursion GetExcursion(int id)
        {
            return _context.Excursions.Include(x => x.ExcursionLocalizeds)
                .Include(x => x.ExcursionPhotos)
                .FirstOrDefault(x => x.Id == id);
        }

        public Excursion StoreExcursion(Excursion excursion)
        {
            excursion.Name = ConvertToValidUrlPageName(excursion.Name);
            _context.Excursions.Add(excursion);
            _context.SaveChanges();
            return excursion;
        }

        public Booking StoreExcursionBooking(Booking excursionBooking)
        {
            excursionBooking.Status = (int)PaymenStatus.Pending;
            _context.Bookings.Add(excursionBooking);
            _context.SaveChanges();
            return excursionBooking;
        }

        public Booking UpdateExcursionBooking(string bookingKey, int status)
        {
            var excursionBooking = _context.Bookings.FirstOrDefault(x => x.Key == bookingKey);
            excursionBooking.Status = status;
            _context.SaveChanges();
            return excursionBooking;
        }

        public Booking GetExcursionBooking(string bookingKey)
        {
            return _context.Bookings.Include(x=>x.Excursion)
                .ThenInclude(x=>x.ExcursionLocalizeds)
                .FirstOrDefault(x => x.Key == bookingKey);
        }

        internal void StoreExcursionPhotos(int id, List<ExcursionPhoto> excursionPhotos)
        {
            foreach(var photo in excursionPhotos)
                _context.ExcursionPhotos.Add(photo);

            _context.SaveChanges();
        }

        private string ConvertToValidUrlPageName(string input)
        {
            // Remove invalid characters
            string invalidChars = new string(Path.GetInvalidFileNameChars()) + new string(Path.GetInvalidPathChars());
            string cleanedInput = string.Join("", input.Split(invalidChars.ToCharArray()));

            // Replace spaces with hyphens
            string urlPageName = cleanedInput.Replace(" ", "-");

            return urlPageName;
        }

        internal IQueryable<Excursion> GeteFeaturedExcursions()
        {
            return _context.Excursions
                .Include(x => x.ExcursionLocalizeds)
                .Include(x => x.ExcursionPhotos)
                .Take(5);
        }

        internal object UpdateExcursion(Excursion excursion)
        {
            var excursionToBeUpdated = _context.Excursions.Include(x => x.ExcursionLocalizeds)
                .Include(x => x.ExcursionPhotos)
                .FirstOrDefault(x => x.Id == excursion.Id);

            excursionToBeUpdated.Name = excursion.Name;
            excursionToBeUpdated.Price = excursion.Price;
            excursionToBeUpdated.VideoLink = excursion.VideoLink;

            var excursionLocalized = excursionToBeUpdated.ExcursionLocalizeds.FirstOrDefault();
            excursionLocalized.BannerDescription = excursion.ExcursionLocalizeds.FirstOrDefault().BannerDescription;
            excursionLocalized.Description = excursion.ExcursionLocalizeds.FirstOrDefault().Description;
            excursionLocalized.Introduction = excursion.ExcursionLocalizeds.FirstOrDefault().Introduction;
            excursionLocalized.Title = excursion.ExcursionLocalizeds.FirstOrDefault().Title;
            excursionLocalized.City = excursion.ExcursionLocalizeds.FirstOrDefault().City;
            excursionLocalized.Period = excursion.ExcursionLocalizeds.FirstOrDefault().Period;
            _context.SaveChanges();
            
            return excursionToBeUpdated;
        }

        internal Excursion UpdateExcursionMainPhoto(int excursionId, string fileName)
        {
            var excursionToBeUpdated = _context.Excursions.Include(x => x.ExcursionLocalizeds)
                            .Include(x => x.ExcursionPhotos)
                            .FirstOrDefault(x => x.Id == excursionId);

            excursionToBeUpdated.MainPhoto = fileName;
            _context.SaveChanges();

            return excursionToBeUpdated;
        }

        internal Excursion UpdateExcursionBannerPhoto(int excursionId, string fileName)
        {
            var excursionToBeUpdated = _context.Excursions.Include(x => x.ExcursionLocalizeds)
                            .Include(x => x.ExcursionPhotos)
                            .FirstOrDefault(x => x.Id == excursionId);

            excursionToBeUpdated.BannerPhoto = fileName;
            _context.SaveChanges();

            return excursionToBeUpdated;
        }
    }
}
