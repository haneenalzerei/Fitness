using System;
using System.Collections.Generic;

#nullable disable

namespace Fitness.Models
{
    public partial class FitnessEmployee
    {
        public decimal Empid { get; set; }
        public string Fullname { get; set; }
        public string Phonenumber { get; set; }
        public string Email { get; set; }
        public decimal? Roleid { get; set; }

        public virtual FitnessRole Role { get; set; }
    }
}
