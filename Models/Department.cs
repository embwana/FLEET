using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FLEET.Models
{
    public class Department
    {

        [Key]
        public int DepartmentId { get; set; }
        [Required]
        [Display(Name = "Department Name")]
        [StringLength(50)]
        public string DepartmentName { get; set; }
        
       
        [Required]
        [Display(Name = "Comment")]
        [StringLength(50)]
        public string Comment { get; set; }
        public ICollection<FleetCustodian> FleetCustodians { get; set; }
        public ICollection<FleetSize> FleetSizes { get; set; }
        public ICollection<Department> Departments { get; set; }
        public ICollection<Grounded> Grounded { get; set; }

    }
}
