﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace Dreamers.Ui.Models
{
    public partial class Language
    {
        public Language()
        {
            SocialNetworkLocalizeds = new HashSet<SocialNetworkLocalized>();
        }

        public int Id { get; set; }
        public string Lcid { get; set; }
        public string Name { get; set; }
        public bool? IsActive { get; set; }
        public int? DisplayOrder { get; set; }

        public virtual ICollection<SocialNetworkLocalized> SocialNetworkLocalizeds { get; set; }
    }
}