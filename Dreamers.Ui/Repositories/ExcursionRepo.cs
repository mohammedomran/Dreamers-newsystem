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
    }
}
