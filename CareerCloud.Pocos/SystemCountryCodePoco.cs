using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareerCloud.Pocos
{
    [Table("System_Country_Codes")]
    public class SystemCountryCodePoco : ICountry
    {
        [Key]
        public string? Code { get; set; }
        public string? Name { get; set; }
        public virtual ICollection<ApplicantProfilePoco> ApplicantProfile { get; set; }
        public virtual ICollection<ApplicantWorkHistoryPoco> ApplicantWorkHistory { get; set; }

    }
}
