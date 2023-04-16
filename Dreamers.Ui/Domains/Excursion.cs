﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace Dreamers.Ui.Models
{
    public partial class Excursion
    {
        public string MainPhotoPath => $"/photos/excursions/{MainPhoto}";
        public string BannerPhotoPath => $"/photos/excursions/{BannerPhoto}";
        public string Title => ExcursionLocalizeds.FirstOrDefault().Title;
        public string Introduction => ExcursionLocalizeds.FirstOrDefault().Introduction;
        public string Description => ExcursionLocalizeds.FirstOrDefault().Description;
        public string BannerDescription => ExcursionLocalizeds.FirstOrDefault().BannerDescription;
        public string Period => ExcursionLocalizeds.FirstOrDefault().Period;
        public string City => ExcursionLocalizeds.FirstOrDefault().City;
    }

    public partial class ExcursionPhoto
    {
        public string PhotoPath => $"/photos/excursions/{Photo}";
    }
}