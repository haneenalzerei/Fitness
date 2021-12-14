using System;
using System.Collections.Generic;

#nullable disable

namespace Fitness.Models
{
    public partial class FitnessUserInformation
    {
        public decimal Userinformationid { get; set; }
        public decimal? Height { get; set; }
        public decimal? Weight { get; set; }
        public decimal? Userid { get; set; }

        public virtual FitnessUser User { get; set; }
    }
}
