using System;
using System.Collections.Generic;

#nullable disable

namespace Fitness.Models
{
    public partial class FitnessUser
    {
        public FitnessUser()
        {
            FitnessUserDiets = new HashSet<FitnessUserDiet>();
            FitnessUserInformations = new HashSet<FitnessUserInformation>();
            FitnessUserLogins = new HashSet<FitnessUserLogin>();
        }

        public decimal Userid { get; set; }
        public string Fullname { get; set; }
        public string Phonenumber { get; set; }
        public string Email { get; set; }
        public decimal? Roleid { get; set; }

        public virtual FitnessRole Role { get; set; }
        public virtual ICollection<FitnessUserDiet> FitnessUserDiets { get; set; }
        public virtual ICollection<FitnessUserInformation> FitnessUserInformations { get; set; }
        public virtual ICollection<FitnessUserLogin> FitnessUserLogins { get; set; }
    }
}
