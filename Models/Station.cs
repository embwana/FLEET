using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FLEET.Models
{
    public class Station
    {

        [Key]
        public int StationId { get; set; }

        [Required]
        [Display(Name = "Station Name")]
        [StringLength(50)]
        public string StationName { get; set; }

        [Required]
        [Display(Name = "Location")]
        [StringLength(50)]
        public string Location { get; set; }

        public int? DepartmentId { get; set; }
       
        [Display(Name = "Department Name")]
        [StringLength(50)]
        public Department DepartmentName { get; set; }
        //public Department Department { get; set; }
        //public int FleetCategoryId { get; set; }
        [Required]
        [Display(Name ="Fleet Number")]
        public int FleetNumber { get; set; }
        public ICollection<FleetSize> FleetSizes { get; set; }
        public ICollection<FleetCategory> fleetCategory { get; set; }
        public ICollection<FleetCustodian> FleetCustodians { get; set; }
        public ICollection<Grounded> Grounded { get; set; }
    }
}
