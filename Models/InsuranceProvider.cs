using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FLEET.Models
{
    public class InsuranceProvider
    {
        [Key]
        public int InsuranceProviderId { get; set; }
        [Required]
        [Display(Name = "Provider Name")]
        [StringLength(50)]
        public string ProviderName { get; set; }
        [Required]
        [Display(Name = " Service Offered")]
        [StringLength(50)]
        public string ServiceOffered { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name = "Year Established")]
        public DateTime EstablishedYear { get; set; }
    }
}
