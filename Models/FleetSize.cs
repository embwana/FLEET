using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FLEET.Models
{
    public class FleetSize
    {
        [Key]
        public int FleetSizeId { get; set; }

        public int? FleetCategoryId { get; set; }
        
        [Display(Name = "Fleet Category")]
        public FleetCategory FleetCategory { get; set; }

        public int? StationId { get; set; }

        
        [Display(Name = "Station Name")]
        public Station StationName { get; set; }

        public int? DepartmentId { get; set; }
     
        [Display(Name = "Department ")]
        public Department DepartmentName { get; set; }
    }
}
