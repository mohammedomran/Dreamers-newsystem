﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace Dreamers.Ui.Models
{
    public partial class ExcursionLocalized
    {
        public int ExcursionId { get; set; }
        public int LanguageId { get; set; }
        public string Title { get; set; }
        public string Period { get; set; }
        public string BannerDescription { get; set; }
        public string Introduction { get; set; }
        public string Description { get; set; }
        public string City { get; set; }
        public string NeededDocuments { get; set; }

        public virtual Excursion Excursion { get; set; }
    }
}