using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FLEET.Models
{
    public class Grounded
    {
        [Key]
        public int GroundedId { get; set; }

        public int? FleetCategoryId { get; set; }

        [Display(Name = "Number Plate")]
        public FleetCategory NumberPlate { get; set; }

        public int? DepartmentId { get; set; }

        [Display(Name = "Department")]
        public Department Department { get; set; }

        
        public int? StationId { get; set; }

        [Display(Name = "Station")]
        public Station Station { get; set; }

        [Required]
        [Display(Name = "Remarks")]
        [StringLength(50)]
        public string Remarks { get; set; }
       


    }
}
