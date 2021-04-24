using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FLEET.Models
{
    public class Insurance
    {

        [Key]
        public int InsuranceId { get; set; }
        [Required]
        [Display(Name = "Insurance Number")]
        public string InsuranceNumber { get; set; }
        [Required]
        [Display(Name = "Details")]
        [StringLength(100)]
        public string Details { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name = "Issued Date")]
        public DateTime Issued { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name = "Renewed Date")]
        public DateTime renewed { get; set; }
        public int InsuranceProviderId { get; set; }
        [Display(Name = "Insurance Provider")]
        public InsuranceProvider InsuranceProvider { get; set; }

        public ICollection<FleetCategory> FleetCategory { get; set; }

        internal static IQueryable<Insurance> AsNoTracking()
        {
            throw new NotImplementedException();
        }
    }
}
