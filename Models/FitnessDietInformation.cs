using System;
using System.Collections.Generic;

#nullable disable

namespace Fitness.Models
{
    public partial class FitnessDietInformation
    {
        public decimal Dietid { get; set; }
        public string Diettext { get; set; }
        public decimal? Maxheight { get; set; }
        public decimal? Maxweight { get; set; }
    }
}
