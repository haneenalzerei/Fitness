using System;
using System.Collections.Generic;

#nullable disable

namespace Fitness.Models
{
    public partial class FitnessUserDiet
    {
        public decimal Id { get; set; }
        public decimal? Userid { get; set; }
        public string Diettext { get; set; }

        public virtual FitnessUser User { get; set; }
    }
}
