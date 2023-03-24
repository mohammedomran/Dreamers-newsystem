﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace Dreamers.Ui.Models
{
    public partial class Excursion
    {
        public Excursion()
        {
            Bookings = new HashSet<Booking>();
            ExcursionLocalizeds = new HashSet<ExcursionLocalized>();
            ExcursionPhotos = new HashSet<ExcursionPhoto>();
        }

        public int Id { get; set; }
        public string MainPhoto { get; set; }
        public string BannerPhoto { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string VideoLink { get; set; }
        public bool IsActive { get; set; }
        public int? DisplayOrder { get; set; }
        public DateTime? DateCreated { get; set; }

        public virtual ICollection<Booking> Bookings { get; set; }
        public virtual ICollection<ExcursionLocalized> ExcursionLocalizeds { get; set; }
        public virtual ICollection<ExcursionPhoto> ExcursionPhotos { get; set; }
    }
}