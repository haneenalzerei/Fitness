using System;
using System.Collections.Generic;

#nullable disable

namespace Fitness.Models
{
    public partial class FitnessUserLogin
    {
        public decimal Userloginid { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public decimal? Userid { get; set; }

        public virtual FitnessUser User { get; set; }
    }
}
