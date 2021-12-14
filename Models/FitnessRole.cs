using System;
using System.Collections.Generic;

#nullable disable

namespace Fitness.Models
{
    public partial class FitnessRole
    {
        public FitnessRole()
        {
            FitnessEmployees = new HashSet<FitnessEmployee>();
            FitnessUsers = new HashSet<FitnessUser>();
        }

        public decimal Roleid { get; set; }
        public string Rolename { get; set; }

        public virtual ICollection<FitnessEmployee> FitnessEmployees { get; set; }
        public virtual ICollection<FitnessUser> FitnessUsers { get; set; }
    }
}
